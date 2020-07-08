
System.IO.Directory.SetCurrentDirectory __SOURCE_DIRECTORY__

#r @"packages/build/FAKE/tools/FakeLib.dll"
#r "System.IO.Compression.FileSystem"
#r "System.Xml.Linq"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Tools.Git
open System
open System.IO
open System.Security.Cryptography
open System.Xml.Linq
// The name of the project
// (used by attributes in AssemblyInfo, name of a NuGet package and directory in 'src')
let project = "Paket"

// Short summary of the project
// (used as description in AssemblyInfo and as a short summary for NuGet package)
let summary = "A dependency manager for .NET with support for NuGet packages and git repositories."

// Longer description of the project
// (used as a description for NuGet package; line breaks are automatically cleaned up)
let description = "A dependency manager for .NET with support for NuGet packages and git repositories."

// List of author names (for NuGet package)
let authors = [ "Paket team" ]

// Tags for your project (for NuGet package)
let tags = "nuget, bundler, F#"

// File system information
let solutionFile  = "Paket.sln"

// Pattern specifying assemblies to be tested using NUnit
let testAssemblies = "tests/**/bin/Release/net461/*Tests*.dll"
let integrationTestAssemblies = "integrationtests/Paket.IntegrationTests/bin/Release/net461/*Tests*.dll"

// Git configuration (used for publishing documentation in gh-pages branch)
// The profile where the project is posted
let gitOwner = "fsprojects"
let gitHome = "https://github.com/" + gitOwner

// The name of the project on GitHub
let gitName = "Paket"

// The url for the raw files hosted
let gitRaw = Environment.environVarOrDefault "gitRaw" "https://raw.github.com/fsprojects"

let dotnetcliVersion = DotNet.getSDKVersionFromGlobalJson()

let dotnetExePath = "dotnet"

let buildDir = "bin"
let buildDirNet461 = buildDir @@ "net461"
let buildDirNetCore = buildDir @@ "netcoreapp2.1"
let buildDirBootstrapper = "bin_bootstrapper"
let buildDirBootstrapperNet461 = buildDirBootstrapper @@ "net461"
let buildDirBootstrapperNetCore = buildDirBootstrapper @@ "netcoreapp2.1"
let tempDir = "temp"
let buildMergedDir = buildDir @@ "merged"
let paketFile = buildMergedDir @@ "paket.exe"

// Read additional information from the release notes document
let releaseNotesData =
    File.ReadAllLines "RELEASE_NOTES.md"
    |> ReleaseNotes.parseAll

let release = List.head releaseNotesData

let stable =
    match releaseNotesData |> List.tryFind (fun r -> r.NugetVersion.Contains("-") |> not) with
    | Some stable -> stable
    | _ -> release


let runDotnet workingDir args =
    let result =
        CreateProcess.fromRawCommandLine dotnetExePath args
        |> CreateProcess.withWorkingDirectory workingDir
        |> Proc.run
    if result.ExitCode <> 0 then
        failwithf "dotnet %s failed" args

let testSuiteFilterFlakyTests = Environment.environVarAsBoolOrDefault "PAKET_TESTSUITE_FLAKYTESTS" false

let genFSAssemblyInfo (projectPath: string) =
    let projectName = System.IO.Path.GetFileNameWithoutExtension(projectPath)
    let folderName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(projectPath))
    let basePath = "src" @@ folderName
    let fileName = basePath @@ "AssemblyInfo.fs"
    AssemblyInfoFile.createFSharp fileName
      [ AssemblyInfo.Title (projectName)
        AssemblyInfo.Product project
        AssemblyInfo.Company (authors |> String.concat ", ")
        AssemblyInfo.Description summary
        AssemblyInfo.Version release.AssemblyVersion
        AssemblyInfo.FileVersion release.AssemblyVersion
        AssemblyInfo.InformationalVersion release.NugetVersion ]

let genCSAssemblyInfo (projectPath: string) =
    let projectName = System.IO.Path.GetFileNameWithoutExtension(projectPath)
    let folderName = System.IO.Path.GetDirectoryName(projectPath)
    let basePath = folderName @@ "Properties"
    let fileName = basePath @@ "AssemblyInfo.cs"
    AssemblyInfoFile.createCSharp fileName
      [ AssemblyInfo.Title (projectName)
        AssemblyInfo.Product project
        AssemblyInfo.Description summary
        AssemblyInfo.Version release.AssemblyVersion
        AssemblyInfo.FileVersion release.AssemblyVersion
        AssemblyInfo.InformationalVersion release.NugetVersion ]

// Generate assembly info files with the right version & up-to-date information
Target.create "AssemblyInfo" (fun _ ->
    let fsProjs =  !! "src/**/*.fsproj"
    let csProjs = !! "src/**/*.csproj"
    fsProjs |> Seq.iter genFSAssemblyInfo
    csProjs |> Seq.iter genCSAssemblyInfo
)

// --------------------------------------------------------------------------------------
// Clean build results

Target.create "Clean" (fun _ ->
    !! "src/**/bin"
    ++ "tests/**/bin"
    ++ buildDir
    ++ buildDirNet461
    ++ buildDirNetCore
    ++ buildDirBootstrapper
    ++ buildDirBootstrapperNet461
    ++ buildDirBootstrapperNetCore
    ++ tempDir
    |> Shell.cleanDirs

    !! "**/obj/**/*.nuspec"
    |> File.deleteAll
)

Target.create "CleanDocs" (fun _ ->
    Shell.cleanDirs ["docs/output"]
)

// --------------------------------------------------------------------------------------
// Build library & test project

Target.create "Build" (fun _ ->
    if Environment.isMono then
        DotNet.build id solutionFile
    else
        DotNet.build (DotNet.Options.withAdditionalArgs ["/p:SourceLinkCreate=true"]) solutionFile
)

Target.create "Restore" (fun _ ->
    DotNet.exec id "tool" "restore" |> ignore

    DotNet.restore id solutionFile
)

Target.create "Publish" (fun _ ->
    DotNet.publish (fun c ->
        { c with
            Framework = Some "net461"
            OutputPath = Some <| buildDirNet461
        }) "src/Paket"

    DotNet.publish (fun c ->
        { c with
            Framework = Some "netcoreapp2.1"
            OutputPath = Some<| buildDirNetCore
        }) "src/Paket"

    DotNet.publish (fun c ->
        { c with
            Framework = Some "net461"
            OutputPath = Some <| buildDirBootstrapperNet461
        }) "src/Paket.Bootstrapper"

    DotNet.publish (fun c ->
        { c with
            Framework = Some "netcoreapp2.1"
            OutputPath = Some <| buildDirBootstrapperNetCore
        }) "src/Paket.Bootstrapper"
)
"Clean" ==> "Build" ?=> "Publish"

// --------------------------------------------------------------------------------------
// Run the unit tests

Target.create "RunTests" (fun _ ->

    let runTest fw proj tfm =
        Directory.create (sprintf "tests_result/%s/%s" fw proj)

        let logFilePath = (sprintf "tests_result/%s/%s/TestResult.trx" fw proj) |> Path.GetFullPath
        let additionalArgs = [
            "--filter"
            (if testSuiteFilterFlakyTests then "TestCategory=Flaky" else "TestCategory!=Flaky")
            sprintf "--logger:trx;LogFileName=%s" logFilePath
            "--no-build"
            "-v"; "n"
        ]

        DotNet.test (DotNet.Options.withAdditionalArgs additionalArgs >> fun c ->
            { c with Framework = Some tfm}) "tests/Paket.Tests/Paket.Tests.fsproj"

    runTest "net" "Paket.Tests" "net461"
    runTest "netcore" "Paket.Tests" "netcoreapp3.0"

    runTest "net" "Paket.Bootstrapper.Tests" "net461"
    runTest "netcore" "Paket.Bootstrapper.Tests" "netcoreapp3.0"
)

Target.create "QuickTest" (fun _ ->
    DotNet.test (DotNet.Options.withAdditionalArgs[
        "--filter"; (if testSuiteFilterFlakyTests then "TestCategory=Flaky" else "TestCategory!=Flaky")
    ]) "tests/Paket.Tests/Paket.Tests.fsproj"
)
"Clean" ==> "QuickTest"

Target.create "QuickIntegrationTests" (fun _ ->
    DotNet.test (DotNet.Options.withAdditionalArgs [ "--filter"; "TestCategory=scriptgen" ])
        "integrationtests/Paket.IntegrationTests/Paket.IntegrationTests.fsproj"
)
"Clean" ==> "Publish" ==> "QuickIntegrationTests"


// --------------------------------------------------------------------------------------
// Build a NuGet package

let mergeLibs = [
    "paket.exe"
    "Paket.Core.dll"
    "FSharp.Core.dll"
    "Newtonsoft.Json.dll"
    "Argu.dll"
    "Chessie.dll"
    "Mono.Cecil.dll"
    "System.Net.Http.WinHttpHandler.dll"
    "System.Buffers.dll"
]

Target.create "MergePaketTool" (fun _ ->
    Directory.create buildMergedDir

    let toPack =
        mergeLibs
        |> List.map (fun l -> buildDirNet461 @@ l)
        |> String.separated " "

    let ilMergePath = Environment.CurrentDirectory </> "packages" </> "build" </> "ILRepack" </> "tools" </> "ILRepack.exe"
    let args = sprintf "/lib:%s /ver:%s /out:%s %s" buildDirNet461 release.AssemblyVersion paketFile toPack

    let result =
        CreateProcess.fromRawCommandLine ilMergePath args
        |> CreateProcess.withTimeout (TimeSpan.FromMinutes 5.)
        |> Proc.run

    if result.ExitCode <> 0 then failwithf "Error during ILRepack execution."
)
"Publish" ==> "MergePaketTool"

let runIntegrationTests fx fxDir =
    Directory.create <| sprintf "tests_result/%s/Paket.IntegrationTests" fxDir

    // improves the speed of the test-suite by disabling the runtime resolution.
    System.Environment.SetEnvironmentVariable("PAKET_DISABLE_RUNTIME_RESOLUTION", "true")

    let additionalArgs = [
        "--filter"; (if testSuiteFilterFlakyTests then "TestCategory=Flaky" else "TestCategory!=Flaky")
        sprintf "--logger:trx;LogFileName=%s" ("tests_result/net/Paket.IntegrationTests/TestResult.trx" |> Path.GetFullPath)
    ]

    DotNet.test (DotNet.Options.withAdditionalArgs additionalArgs >> fun c ->
        { c with
            Framework = Some fx
        }) "integrationtests/Paket.IntegrationTests/Paket.IntegrationTests.fsproj"

Target.create "RunIntegrationTestsNet" (fun _ -> runIntegrationTests "net" "net461")
"Clean" ==> "Publish" ==> "RunIntegrationTestsNet"

Target.create "RunIntegrationTestsNetCore" (fun _ -> runIntegrationTests "netcore" "netcoreapp3.0")
"Clean" ==> "Publish" ==> "RunIntegrationTestsNetCore"

let pfx = "code-sign.pfx"
let mutable isUnsignedAllowed = true
Target.create "EnsurePackageSigned" (fun _ -> isUnsignedAllowed <- false)

Target.create "SignAssemblies" (fun _ ->
    if not <| File.exists pfx then
        if isUnsignedAllowed then ()
        else failwithf "%s not found, can't sign assemblies" pfx
    else

    let filesToSign =
        !! "bin/**/*.exe"
        ++ "bin/**/Paket.Core.dll"
        ++ "bin_bootstrapper/**/*.exe"
        |> Seq.cache

    if Seq.length filesToSign < 3 then failwith "Didn't find enough files to sign"

    filesToSign
    |> Seq.iter (fun executable ->
        let signtool = Environment.CurrentDirectory @@ "tools" @@ "SignTool" @@ "signtool.exe"
        let args = ["sign"; "/f"; pfx; "/t:"; "http://timestamp.comodoca.com/authenticode"; executable]
        let result =
            CreateProcess.fromRawCommand signtool args
            |> Proc.run
        if result.ExitCode <> 0 then
            failwithf "Error during signing %s with %s" executable pfx)
)

Target.create "CalculateDownloadHash" (fun _ ->
    use stream = File.OpenRead(paketFile)
    use sha = SHA256.Create()
    let checksum = sha.ComputeHash(stream)
    let hash = BitConverter.ToString(checksum).Replace("-", String.Empty)
    File.WriteAllText(buildMergedDir @@ "paket-sha256.txt", sprintf "%s paket.exe" hash)
)

Target.create "AddIconToExe" (fun _ ->
    // add icon to paket.exe
    // workaround https://github.com/dotnet/fsharp/issues/1172
    let paketExeIcon = "src" @@ "Paket" @@ "paket.ico"

    // use resourcehacker to add the icon
    let rhPath = "paket-files" @@ "build" @@ "enricosada" @@ "add_icon_to_exe" @@ "rh" @@ "ResourceHacker.exe"
    let args = ["-open"; paketFile; "-save"; paketFile; "-action addskip -res"; paketExeIcon; "-mask ICONGROUP,MAINICON"]

    let result =
        CreateProcess.fromRawCommand rhPath args
        |> CreateProcess.withTimeout (TimeSpan.FromMinutes 1.)
        |> Proc.run
    if result.ExitCode <> 0 then
        failwithf "Error during adding icon %s to %s with %s %s" paketExeIcon paketFile rhPath (String.concat " " args)
)

let releaseNotesProp releaseNotesLines =
    let xn name = XName.Get(name)
    let text = releaseNotesLines |> String.concat Environment.NewLine
    let doc =
        XDocument(
            XComment("This document was automatically generated."),
            XElement(xn "Project",
                XElement(xn "PropertyGroup",
                    XElement(xn "PackageReleaseNotes", text)
                )
            )
        )

    let path = Path.GetTempFileName()
    doc.Save(path)
    path

Target.create "NuGet" (fun _ ->
    Paket.pack (fun p ->
        { p with
            ToolPath = "bin/merged/paket.exe"
            Version = release.NugetVersion
            TemplateFile = "src/Paket.Core/paket.template"
            ReleaseNotes = String.toLines release.Notes })
    // pack as .NET tools
    let releaseNotesPath = releaseNotesProp release.Notes
    let additionalArgs = [
        sprintf "/p:Version=%s" release.NugetVersion
        sprintf "/p:PackageReleaseNotesFile=%s" releaseNotesPath
        "/p:PackAsTool=true"
    ]

    DotNet.pack (DotNet.Options.withAdditionalArgs additionalArgs >> fun c ->
        { c with
            OutputPath = Some tempDir
        }) "src/Paket/Paket.fsproj"
    DotNet.pack (DotNet.Options.withAdditionalArgs additionalArgs >> fun c ->
        { c with
            OutputPath = Some tempDir
        }) "src/Paket.Bootstrapper/Paket.Bootstrapper.csproj"
)

Target.create "PublishNuGet" (fun _ ->
    if Environment.hasEnvironVar "PublishBootstrapper" |> not then
        !! (tempDir </> "*bootstrapper*")
        |> Seq.iter File.Delete

    Paket.push (fun p ->
        { p with
            ToolPath = "bin/merged/paket.exe"
            ApiKey = Environment.environVarOrDefault "NugetKey" ""
            WorkingDir = tempDir })
)

// --------------------------------------------------------------------------------------
// Generate the documentation

let disableDocs = false // https://github.com/fsprojects/FSharp.Formatting/issues/461

let fakePath = __SOURCE_DIRECTORY__ @@ "packages" @@ "build" @@ "FAKE" @@ "tools" @@ "FAKE.exe"
let fakeStartInfo fsiargs script workingDirectory args environmentVars =
    let args = seq {
        yield! args
        yield "--fsiargs"
        yield! fsiargs
        yield "-d:FAKE"
        yield script
    }
    CreateProcess.fromRawCommand fakePath args
    |> CreateProcess.withWorkingDirectory workingDirectory
    |> CreateProcess.withEnvironment environmentVars

/// Run the given startinfo by printing the output (live)
let executeWithOutput cp =
    let result = Proc.run cp
    result.ExitCode

/// Helper to fail when the exitcode is <> 0
let executeHelper executer fail traceMsg failMessage configStartInfo =
    Trace.trace traceMsg
    let exit = executer configStartInfo
    if exit <> 0 then
        if fail then
            failwith failMessage
        else
            Trace.traceImportant failMessage
    else
        Trace.traceImportant "Succeeded"

let execute fail traceMsg failMessage configStartInfo =
    executeHelper executeWithOutput fail traceMsg failMessage configStartInfo

Target.create "GenerateReferenceDocs" (fun _ ->
    if disableDocs then () else
    let args = ["--define:RELEASE"; "--define:REFERENCE"]
    execute
      true
      (sprintf "Building reference documentation, this could take some time, please wait...")
      "generating reference documentation failed"
      (fakeStartInfo args "generate.fsx" "docs/tools" [] [])
)

let generateHelp' includeCommands fail debug =
    let args =
        [ if not debug then yield "--define:RELEASE"
          if includeCommands then yield "--define:COMMANDS"
          yield "--define:HELP"]
    execute
      fail
      (sprintf "Building documentation (%A), this could take some time, please wait..." includeCommands)
      "generating documentation failed"
      (fakeStartInfo args "generate.fsx" "docs/tools" [] [])

    Shell.cleanDir "docs/output/commands"

let generateHelp debug =
    if disableDocs then () else
    File.delete "docs/content/release-notes.md"
    Shell.copyFile "docs/content/" "RELEASE_NOTES.md"
    Shell.rename "docs/content/release-notes.md" "docs/content/RELEASE_NOTES.md"

    File.delete "docs/content/license.md"
    Shell.copyFile "docs/content/" "LICENSE.txt"
    Shell.rename "docs/content/license.md" "docs/content/LICENSE.txt"

    generateHelp' true true debug

Target.create "GenerateHelp" (fun _ -> generateHelp false)

Target.create "GenerateHelpDebug" (fun _ -> generateHelp true)

Target.create "KeepRunning" (fun _ ->
    use watcher = !! "docs/content/**/*.*" |> ChangeWatcher.run (fun changes ->
         generateHelp' false false false
    )
    Trace.traceImportant "Waiting for help edits. Press any key to stop."
    System.Console.ReadKey() |> ignore
)

Target.create "GenerateDocs" ignore

// --------------------------------------------------------------------------------------
// Release Scripts

Target.create "ReleaseDocs" (fun _ ->
    if disableDocs then () else
    let tempDocsDir = "temp/gh-pages"
    Shell.cleanDir tempDocsDir
    Repository.cloneSingleBranch "" (gitHome + "/" + gitName + ".git") "gh-pages" tempDocsDir

    CommandHelper.runSimpleGitCommand tempDocsDir "rm . -f -r" |> ignore
    Shell.copyRecursive "docs/output" tempDocsDir true |> Trace.tracefn "%A"

    File.WriteAllText("temp/gh-pages/latest",sprintf "https://github.com/fsprojects/Paket/releases/download/%s/paket.exe" release.NugetVersion)
    File.WriteAllText("temp/gh-pages/stable",sprintf "https://github.com/fsprojects/Paket/releases/download/%s/paket.exe" stable.NugetVersion)

    Staging.stageAll tempDocsDir
    Commit.exec tempDocsDir (sprintf "Update generated documentation for version %s" release.NugetVersion)
    Branches.push tempDocsDir
)

#load "paket-files/build/fsharp/FAKE/modules/Octokit/Octokit.fsx"
open Octokit

Target.create "ReleaseGitHub" (fun _ ->
    let user =
        match Environment.environVarOrNone "github_user" with
        | Some s -> s
        | None ->
            eprintfn "Please update your release script to set 'github_user'!"
            UserInput.getUserInput "Username: "
    let pw =
        match Environment.environVarOrNone "github_password" with
        | Some s -> s
        | None ->
            eprintfn "Please update your release script to set 'github_password'!"
            UserInput.getUserPassword "Password: "
    let remote =
        CommandHelper.getGitResult "" "remote -v"
        |> Seq.filter (fun (s: string) -> s.EndsWith("(push)"))
        |> Seq.tryFind (fun (s: string) -> s.Contains(gitOwner + "/" + gitName))
        |> function None -> gitHome + "/" + gitName | Some (s: string) -> s.Split().[0]

    Staging.stageAll ""
    Commit.exec "" (sprintf "Bump version to %s" release.NugetVersion)
    Branches.pushBranch "" remote (Information.getBranchName "")

    Branches.tag "" release.NugetVersion
    Branches.pushTag "" remote release.NugetVersion

    Trace.tracefn "Creating gihub release"

    // release on github
    createClient user pw
    |> createDraft gitOwner gitName release.NugetVersion (release.SemVer.PreRelease <> None) release.Notes
    |> uploadFile "./bin/merged/paket.exe"
    |> uploadFile "./bin/merged/paket-sha256.txt"
    |> uploadFile "./bin_bootstrapper/net461/paket.bootstrapper.exe"
    |> uploadFile ".paket/paket.targets"
    |> uploadFile ".paket/Paket.Restore.targets"
    |> uploadFile (tempDir </> sprintf "Paket.%s.nupkg" (release.NugetVersion))
    |> releaseDraft
    |> Async.RunSynchronously
)


Target.create "Release" ignore
Target.create "BuildPackage" ignore
// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

let hasBuildParams buildParams =
    buildParams
    |> List.map Environment.hasEnvironVar
    |> List.exists id
let unlessBuildParams buildParams = not (hasBuildParams buildParams)
Target.create "All" ignore

let isMono = Environment.isMono
let isLocalBuild = BuildServer.isLocalBuild
let skipDocs = Environment.hasEnvironVar "SkipDocs"

"Clean"
  ==> "Restore"
  ==> "AssemblyInfo"
  ==> "Build"
  =?> ("RunTests", unlessBuildParams [ "SkipTests"; "SkipUnitTests" ])
  =?> ("GenerateReferenceDocs", isLocalBuild && not isMono && not skipDocs)
  =?> ("GenerateDocs", isLocalBuild && not isMono && not skipDocs)
  ==> "All"
  =?> ("ReleaseDocs", isLocalBuild && not isMono && not skipDocs)

"All"
  ==> "MergePaketTool"
  =?> ("AddIconToExe", not isMono)
  =?> ("RunIntegrationTestsNet", unlessBuildParams [ "SkipTests"; "SkipIntegrationTests"; "SkipIntegrationTestsNet" ] )
  =?> ("RunIntegrationTestsNetCore", unlessBuildParams [ "SkipTests"; "SkipIntegrationTests"; "SkipIntegrationTestsNetCore" ] )
  ==> "SignAssemblies"
  ==> "CalculateDownloadHash"
  =?> ("NuGet", unlessBuildParams [ "SkipNuGet" ])
  ==> "BuildPackage"

"EnsurePackageSigned"
  ?=> "SignAssemblies"


"CleanDocs"
  ==> "GenerateHelp"
  ==> "GenerateReferenceDocs"
  ==> "GenerateDocs"

"CleanDocs"
  ==> "GenerateHelpDebug"

"GenerateHelp"
  ==> "KeepRunning"

"BuildPackage"
  ==> "PublishNuGet"

"PublishNuGet"
  ==> "ReleaseGitHub"
  ==> "Release"

"ReleaseGitHub"
  ?=> "ReleaseDocs"

"ReleaseDocs"
  ==> "Release"

"EnsurePackageSigned"
  ==> "Release"

Target.runOrDefault "All"
