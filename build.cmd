@echo off

dotnet tool restore
dotnet tool run fake run ./build.fsx %*
