﻿module Paket.RuntimeGraphTests

open Paket
open NUnit.Framework
open FsUnit
open Paket.Domain
open Paket.TestHelpers

let supportAndDeps = """
{
  "runtimes": {
    "win": {
      "Microsoft.Win32.Primitives": {
        "runtime.win.Microsoft.Win32.Primitives": "4.3.0"
      },
      "System.Runtime.Extensions": {
        "runtime.win.System.Runtime.Extensions": "4.3.0"
      }
    }
  },
  "supports": {
    "uwp.10.0.app": {
      "uap10.0": [
        "win10-x86",
        "win10-x86-aot",
        "win10-arm",
        "win10-arm-aot"
      ]
    },
    "net45.app": {
      "net45": [
        "",
        "win-x86",
        "win-x64"
      ]
    }
  }
}"""


let rids = """
{
    "runtimes": {
        "base": {
        },
        "any": {
            "#import": [ "base" ]
        },
        "win": {
            "#import": [ "any" ]
        },
        "win-x86": {
            "#import": [ "win" ]
        }
   }
}
"""

let runtimeJsonMsNetCorePlatforms2_2_1 = """
{
  "runtimes": {
    "alpine": {
      "#import": [
        "linux-musl"
      ]
    },
    "alpine-corert": {
      "#import": [
        "alpine",
        "linux-musl-corert"
      ]
    },
    "alpine-x64": {
      "#import": [
        "alpine",
        "linux-musl-x64"
      ]
    },
    "alpine-x64-corert": {
      "#import": [
        "alpine-corert",
        "alpine-x64",
        "linux-musl-x64-corert"
      ]
    },
    "alpine.3.6": {
      "#import": [
        "alpine"
      ]
    },
    "alpine.3.6-corert": {
      "#import": [
        "alpine.3.6",
        "alpine-corert"
      ]
    },
    "alpine.3.6-x64": {
      "#import": [
        "alpine.3.6",
        "alpine-x64"
      ]
    },
    "alpine.3.6-x64-corert": {
      "#import": [
        "alpine.3.6-corert",
        "alpine.3.6-x64",
        "alpine.3.6",
        "alpine-x64-corert"
      ]
    },
    "alpine.3.7": {
      "#import": [
        "alpine.3.6"
      ]
    },
    "alpine.3.7-corert": {
      "#import": [
        "alpine.3.7",
        "alpine.3.6-corert"
      ]
    },
    "alpine.3.7-x64": {
      "#import": [
        "alpine.3.7",
        "alpine.3.6-x64"
      ]
    },
    "alpine.3.7-x64-corert": {
      "#import": [
        "alpine.3.7-corert",
        "alpine.3.7-x64",
        "alpine.3.7",
        "alpine.3.6-x64-corert"
      ]
    },
    "alpine.3.8": {
      "#import": [
        "alpine.3.7"
      ]
    },
    "alpine.3.8-corert": {
      "#import": [
        "alpine.3.8",
        "alpine.3.7-corert"
      ]
    },
    "alpine.3.8-x64": {
      "#import": [
        "alpine.3.8",
        "alpine.3.7-x64"
      ]
    },
    "alpine.3.8-x64-corert": {
      "#import": [
        "alpine.3.8-corert",
        "alpine.3.8-x64",
        "alpine.3.8",
        "alpine.3.7-x64-corert"
      ]
    },
    "alpine.3.9": {
      "#import": [
        "alpine.3.8"
      ]
    },
    "alpine.3.9-corert": {
      "#import": [
        "alpine.3.9",
        "alpine.3.8-corert"
      ]
    },
    "alpine.3.9-x64": {
      "#import": [
        "alpine.3.9",
        "alpine.3.8-x64"
      ]
    },
    "alpine.3.9-x64-corert": {
      "#import": [
        "alpine.3.9-corert",
        "alpine.3.9-x64",
        "alpine.3.9",
        "alpine.3.8-x64-corert"
      ]
    },
    "android": {
      "#import": [
        "linux"
      ]
    },
    "android-arm": {
      "#import": [
        "android",
        "linux-arm"
      ]
    },
    "android-arm-corert": {
      "#import": [
        "android-corert",
        "android-arm",
        "linux-arm-corert"
      ]
    },
    "android-arm64": {
      "#import": [
        "android",
        "linux-arm64"
      ]
    },
    "android-arm64-corert": {
      "#import": [
        "android-corert",
        "android-arm64",
        "linux-arm64-corert"
      ]
    },
    "android-corert": {
      "#import": [
        "android",
        "linux-corert"
      ]
    },
    "android.21": {
      "#import": [
        "android"
      ]
    },
    "android.21-arm": {
      "#import": [
        "android.21",
        "android-arm"
      ]
    },
    "android.21-arm-corert": {
      "#import": [
        "android.21-corert",
        "android.21-arm",
        "android.21",
        "android-arm-corert"
      ]
    },
    "android.21-arm64": {
      "#import": [
        "android.21",
        "android-arm64"
      ]
    },
    "android.21-arm64-corert": {
      "#import": [
        "android.21-corert",
        "android.21-arm64",
        "android.21",
        "android-arm64-corert"
      ]
    },
    "android.21-corert": {
      "#import": [
        "android.21",
        "android-corert"
      ]
    },
    "any": {
      "#import": [
        "base"
      ]
    },
    "aot": {
      "#import": [
        "any"
      ]
    },
    "base": {
      "#import": []
    },
    "centos": {
      "#import": [
        "rhel"
      ]
    },
    "centos-corert": {
      "#import": [
        "centos",
        "rhel-corert"
      ]
    },
    "centos-x64": {
      "#import": [
        "centos",
        "rhel-x64"
      ]
    },
    "centos-x64-corert": {
      "#import": [
        "centos-corert",
        "centos-x64",
        "rhel-x64-corert"
      ]
    },
    "centos.7": {
      "#import": [
        "centos",
        "rhel.7"
      ]
    },
    "centos.7-corert": {
      "#import": [
        "centos.7",
        "centos-corert",
        "rhel.7-corert"
      ]
    },
    "centos.7-x64": {
      "#import": [
        "centos.7",
        "centos-x64",
        "rhel.7-x64"
      ]
    },
    "centos.7-x64-corert": {
      "#import": [
        "centos.7-corert",
        "centos.7-x64",
        "centos.7",
        "centos-x64-corert"
      ]
    },
    "corert": {
      "#import": [
        "any"
      ]
    },
    "debian": {
      "#import": [
        "linux"
      ]
    },
    "debian-arm": {
      "#import": [
        "debian",
        "linux-arm"
      ]
    },
    "debian-arm-corert": {
      "#import": [
        "debian-corert",
        "debian-arm",
        "linux-arm-corert"
      ]
    },
    "debian-arm64": {
      "#import": [
        "debian",
        "linux-arm64"
      ]
    },
    "debian-arm64-corert": {
      "#import": [
        "debian-corert",
        "debian-arm64",
        "linux-arm64-corert"
      ]
    },
    "debian-armel": {
      "#import": [
        "debian",
        "linux-armel"
      ]
    },
    "debian-armel-corert": {
      "#import": [
        "debian-corert",
        "debian-armel",
        "linux-armel-corert"
      ]
    },
    "debian-corert": {
      "#import": [
        "debian",
        "linux-corert"
      ]
    },
    "debian-x64": {
      "#import": [
        "debian",
        "linux-x64"
      ]
    },
    "debian-x64-corert": {
      "#import": [
        "debian-corert",
        "debian-x64",
        "linux-x64-corert"
      ]
    },
    "debian-x86": {
      "#import": [
        "debian",
        "linux-x86"
      ]
    },
    "debian-x86-corert": {
      "#import": [
        "debian-corert",
        "debian-x86",
        "linux-x86-corert"
      ]
    },
    "debian.8": {
      "#import": [
        "debian"
      ]
    },
    "debian.8-arm": {
      "#import": [
        "debian.8",
        "debian-arm"
      ]
    },
    "debian.8-arm-corert": {
      "#import": [
        "debian.8-corert",
        "debian.8-arm",
        "debian.8",
        "debian-arm-corert"
      ]
    },
    "debian.8-arm64": {
      "#import": [
        "debian.8",
        "debian-arm64"
      ]
    },
    "debian.8-arm64-corert": {
      "#import": [
        "debian.8-corert",
        "debian.8-arm64",
        "debian.8",
        "debian-arm64-corert"
      ]
    },
    "debian.8-armel": {
      "#import": [
        "debian.8",
        "debian-armel"
      ]
    },
    "debian.8-armel-corert": {
      "#import": [
        "debian.8-corert",
        "debian.8-armel",
        "debian.8",
        "debian-armel-corert"
      ]
    },
    "debian.8-corert": {
      "#import": [
        "debian.8",
        "debian-corert"
      ]
    },
    "debian.8-x64": {
      "#import": [
        "debian.8",
        "debian-x64"
      ]
    },
    "debian.8-x64-corert": {
      "#import": [
        "debian.8-corert",
        "debian.8-x64",
        "debian.8",
        "debian-x64-corert"
      ]
    },
    "debian.8-x86": {
      "#import": [
        "debian.8",
        "debian-x86"
      ]
    },
    "debian.8-x86-corert": {
      "#import": [
        "debian.8-corert",
        "debian.8-x86",
        "debian.8",
        "debian-x86-corert"
      ]
    },
    "debian.9": {
      "#import": [
        "debian"
      ]
    },
    "debian.9-arm": {
      "#import": [
        "debian.9",
        "debian-arm"
      ]
    },
    "debian.9-arm-corert": {
      "#import": [
        "debian.9-corert",
        "debian.9-arm",
        "debian.9",
        "debian-arm-corert"
      ]
    },
    "debian.9-arm64": {
      "#import": [
        "debian.9",
        "debian-arm64"
      ]
    },
    "debian.9-arm64-corert": {
      "#import": [
        "debian.9-corert",
        "debian.9-arm64",
        "debian.9",
        "debian-arm64-corert"
      ]
    },
    "debian.9-armel": {
      "#import": [
        "debian.9",
        "debian-armel"
      ]
    },
    "debian.9-armel-corert": {
      "#import": [
        "debian.9-corert",
        "debian.9-armel",
        "debian.9",
        "debian-armel-corert"
      ]
    },
    "debian.9-corert": {
      "#import": [
        "debian.9",
        "debian-corert"
      ]
    },
    "debian.9-x64": {
      "#import": [
        "debian.9",
        "debian-x64"
      ]
    },
    "debian.9-x64-corert": {
      "#import": [
        "debian.9-corert",
        "debian.9-x64",
        "debian.9",
        "debian-x64-corert"
      ]
    },
    "debian.9-x86": {
      "#import": [
        "debian.9",
        "debian-x86"
      ]
    },
    "debian.9-x86-corert": {
      "#import": [
        "debian.9-corert",
        "debian.9-x86",
        "debian.9",
        "debian-x86-corert"
      ]
    },
    "fedora": {
      "#import": [
        "linux"
      ]
    },
    "fedora-corert": {
      "#import": [
        "fedora",
        "linux-corert"
      ]
    },
    "fedora-x64": {
      "#import": [
        "fedora",
        "linux-x64"
      ]
    },
    "fedora-x64-corert": {
      "#import": [
        "fedora-corert",
        "fedora-x64",
        "linux-x64-corert"
      ]
    },
    "fedora.23": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.23-corert": {
      "#import": [
        "fedora.23",
        "fedora-corert"
      ]
    },
    "fedora.23-x64": {
      "#import": [
        "fedora.23",
        "fedora-x64"
      ]
    },
    "fedora.23-x64-corert": {
      "#import": [
        "fedora.23-corert",
        "fedora.23-x64",
        "fedora.23",
        "fedora-x64-corert"
      ]
    },
    "fedora.24": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.24-corert": {
      "#import": [
        "fedora.24",
        "fedora-corert"
      ]
    },
    "fedora.24-x64": {
      "#import": [
        "fedora.24",
        "fedora-x64"
      ]
    },
    "fedora.24-x64-corert": {
      "#import": [
        "fedora.24-corert",
        "fedora.24-x64",
        "fedora.24",
        "fedora-x64-corert"
      ]
    },
    "fedora.25": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.25-corert": {
      "#import": [
        "fedora.25",
        "fedora-corert"
      ]
    },
    "fedora.25-x64": {
      "#import": [
        "fedora.25",
        "fedora-x64"
      ]
    },
    "fedora.25-x64-corert": {
      "#import": [
        "fedora.25-corert",
        "fedora.25-x64",
        "fedora.25",
        "fedora-x64-corert"
      ]
    },
    "fedora.26": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.26-corert": {
      "#import": [
        "fedora.26",
        "fedora-corert"
      ]
    },
    "fedora.26-x64": {
      "#import": [
        "fedora.26",
        "fedora-x64"
      ]
    },
    "fedora.26-x64-corert": {
      "#import": [
        "fedora.26-corert",
        "fedora.26-x64",
        "fedora.26",
        "fedora-x64-corert"
      ]
    },
    "fedora.27": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.27-corert": {
      "#import": [
        "fedora.27",
        "fedora-corert"
      ]
    },
    "fedora.27-x64": {
      "#import": [
        "fedora.27",
        "fedora-x64"
      ]
    },
    "fedora.27-x64-corert": {
      "#import": [
        "fedora.27-corert",
        "fedora.27-x64",
        "fedora.27",
        "fedora-x64-corert"
      ]
    },
    "fedora.28": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.28-corert": {
      "#import": [
        "fedora.28",
        "fedora-corert"
      ]
    },
    "fedora.28-x64": {
      "#import": [
        "fedora.28",
        "fedora-x64"
      ]
    },
    "fedora.28-x64-corert": {
      "#import": [
        "fedora.28-corert",
        "fedora.28-x64",
        "fedora.28",
        "fedora-x64-corert"
      ]
    },
    "fedora.29": {
      "#import": [
        "fedora"
      ]
    },
    "fedora.29-corert": {
      "#import": [
        "fedora.29",
        "fedora-corert"
      ]
    },
    "fedora.29-x64": {
      "#import": [
        "fedora.29",
        "fedora-x64"
      ]
    },
    "fedora.29-x64-corert": {
      "#import": [
        "fedora.29-corert",
        "fedora.29-x64",
        "fedora.29",
        "fedora-x64-corert"
      ]
    },
    "gentoo": {
      "#import": [
        "linux"
      ]
    },
    "gentoo-corert": {
      "#import": [
        "gentoo",
        "linux-corert"
      ]
    },
    "gentoo-x64": {
      "#import": [
        "gentoo",
        "linux-x64"
      ]
    },
    "gentoo-x64-corert": {
      "#import": [
        "gentoo-corert",
        "gentoo-x64",
        "linux-x64-corert"
      ]
    },
    "linux": {
      "#import": [
        "unix"
      ]
    },
    "linux-arm": {
      "#import": [
        "linux",
        "unix-arm"
      ]
    },
    "linux-arm-corert": {
      "#import": [
        "linux-corert",
        "linux-arm",
        "unix-arm-corert"
      ]
    },
    "linux-arm64": {
      "#import": [
        "linux",
        "unix-arm64"
      ]
    },
    "linux-arm64-corert": {
      "#import": [
        "linux-corert",
        "linux-arm64",
        "unix-arm64-corert"
      ]
    },
    "linux-armel": {
      "#import": [
        "linux",
        "unix-armel"
      ]
    },
    "linux-armel-corert": {
      "#import": [
        "linux-corert",
        "linux-armel",
        "unix-armel-corert"
      ]
    },
    "linux-corert": {
      "#import": [
        "linux",
        "unix-corert"
      ]
    },
    "linux-musl": {
      "#import": [
        "linux"
      ]
    },
    "linux-musl-arm": {
      "#import": [
        "linux-musl",
        "linux-arm"
      ]
    },
    "linux-musl-arm-corert": {
      "#import": [
        "linux-musl-corert",
        "linux-musl-arm",
        "linux-arm-corert"
      ]
    },
    "linux-musl-arm64": {
      "#import": [
        "linux-musl",
        "linux-arm64"
      ]
    },
    "linux-musl-arm64-corert": {
      "#import": [
        "linux-musl-corert",
        "linux-musl-arm64",
        "linux-arm64-corert"
      ]
    },
    "linux-musl-armel": {
      "#import": [
        "linux-musl",
        "linux-armel"
      ]
    },
    "linux-musl-armel-corert": {
      "#import": [
        "linux-musl-corert",
        "linux-musl-armel",
        "linux-armel-corert"
      ]
    },
    "linux-musl-corert": {
      "#import": [
        "linux-musl",
        "linux-corert"
      ]
    },
    "linux-musl-x64": {
      "#import": [
        "linux-musl",
        "linux-x64"
      ]
    },
    "linux-musl-x64-corert": {
      "#import": [
        "linux-musl-corert",
        "linux-musl-x64",
        "linux-x64-corert"
      ]
    },
    "linux-musl-x86": {
      "#import": [
        "linux-musl",
        "linux-x86"
      ]
    },
    "linux-musl-x86-corert": {
      "#import": [
        "linux-musl-corert",
        "linux-musl-x86",
        "linux-x86-corert"
      ]
    },
    "linux-x64": {
      "#import": [
        "linux",
        "unix-x64"
      ]
    },
    "linux-x64-corert": {
      "#import": [
        "linux-corert",
        "linux-x64",
        "unix-x64-corert"
      ]
    },
    "linux-x86": {
      "#import": [
        "linux",
        "unix-x86"
      ]
    },
    "linux-x86-corert": {
      "#import": [
        "linux-corert",
        "linux-x86",
        "unix-x86-corert"
      ]
    },
    "linuxmint.17": {
      "#import": [
        "ubuntu.14.04"
      ]
    },
    "linuxmint.17-corert": {
      "#import": [
        "linuxmint.17",
        "ubuntu.14.04-corert"
      ]
    },
    "linuxmint.17-x64": {
      "#import": [
        "linuxmint.17",
        "ubuntu.14.04-x64"
      ]
    },
    "linuxmint.17-x64-corert": {
      "#import": [
        "linuxmint.17-corert",
        "linuxmint.17-x64",
        "ubuntu.14.04-x64-corert"
      ]
    },
    "linuxmint.17.1": {
      "#import": [
        "linuxmint.17"
      ]
    },
    "linuxmint.17.1-corert": {
      "#import": [
        "linuxmint.17.1",
        "linuxmint.17-corert"
      ]
    },
    "linuxmint.17.1-x64": {
      "#import": [
        "linuxmint.17.1",
        "linuxmint.17-x64"
      ]
    },
    "linuxmint.17.1-x64-corert": {
      "#import": [
        "linuxmint.17.1-corert",
        "linuxmint.17.1-x64",
        "linuxmint.17.1",
        "linuxmint.17-x64-corert"
      ]
    },
    "linuxmint.17.2": {
      "#import": [
        "linuxmint.17.1"
      ]
    },
    "linuxmint.17.2-corert": {
      "#import": [
        "linuxmint.17.2",
        "linuxmint.17.1-corert"
      ]
    },
    "linuxmint.17.2-x64": {
      "#import": [
        "linuxmint.17.2",
        "linuxmint.17.1-x64"
      ]
    },
    "linuxmint.17.2-x64-corert": {
      "#import": [
        "linuxmint.17.2-corert",
        "linuxmint.17.2-x64",
        "linuxmint.17.2",
        "linuxmint.17.1-x64-corert"
      ]
    },
    "linuxmint.17.3": {
      "#import": [
        "linuxmint.17.2"
      ]
    },
    "linuxmint.17.3-corert": {
      "#import": [
        "linuxmint.17.3",
        "linuxmint.17.2-corert"
      ]
    },
    "linuxmint.17.3-x64": {
      "#import": [
        "linuxmint.17.3",
        "linuxmint.17.2-x64"
      ]
    },
    "linuxmint.17.3-x64-corert": {
      "#import": [
        "linuxmint.17.3-corert",
        "linuxmint.17.3-x64",
        "linuxmint.17.3",
        "linuxmint.17.2-x64-corert"
      ]
    },
    "linuxmint.18": {
      "#import": [
        "ubuntu.16.04"
      ]
    },
    "linuxmint.18-corert": {
      "#import": [
        "linuxmint.18",
        "ubuntu.16.04-corert"
      ]
    },
    "linuxmint.18-x64": {
      "#import": [
        "linuxmint.18",
        "ubuntu.16.04-x64"
      ]
    },
    "linuxmint.18-x64-corert": {
      "#import": [
        "linuxmint.18-corert",
        "linuxmint.18-x64",
        "ubuntu.16.04-x64-corert"
      ]
    },
    "linuxmint.18.1": {
      "#import": [
        "linuxmint.18"
      ]
    },
    "linuxmint.18.1-corert": {
      "#import": [
        "linuxmint.18.1",
        "linuxmint.18-corert"
      ]
    },
    "linuxmint.18.1-x64": {
      "#import": [
        "linuxmint.18.1",
        "linuxmint.18-x64"
      ]
    },
    "linuxmint.18.1-x64-corert": {
      "#import": [
        "linuxmint.18.1-corert",
        "linuxmint.18.1-x64",
        "linuxmint.18.1",
        "linuxmint.18-x64-corert"
      ]
    },
    "linuxmint.18.2": {
      "#import": [
        "linuxmint.18.1"
      ]
    },
    "linuxmint.18.2-corert": {
      "#import": [
        "linuxmint.18.2",
        "linuxmint.18.1-corert"
      ]
    },
    "linuxmint.18.2-x64": {
      "#import": [
        "linuxmint.18.2",
        "linuxmint.18.1-x64"
      ]
    },
    "linuxmint.18.2-x64-corert": {
      "#import": [
        "linuxmint.18.2-corert",
        "linuxmint.18.2-x64",
        "linuxmint.18.2",
        "linuxmint.18.1-x64-corert"
      ]
    },
    "linuxmint.18.3": {
      "#import": [
        "linuxmint.18.2"
      ]
    },
    "linuxmint.18.3-corert": {
      "#import": [
        "linuxmint.18.3",
        "linuxmint.18.2-corert"
      ]
    },
    "linuxmint.18.3-x64": {
      "#import": [
        "linuxmint.18.3",
        "linuxmint.18.2-x64"
      ]
    },
    "linuxmint.18.3-x64-corert": {
      "#import": [
        "linuxmint.18.3-corert",
        "linuxmint.18.3-x64",
        "linuxmint.18.3",
        "linuxmint.18.2-x64-corert"
      ]
    },
    "linuxmint.19": {
      "#import": [
        "ubuntu.18.04"
      ]
    },
    "linuxmint.19-corert": {
      "#import": [
        "linuxmint.19",
        "ubuntu.18.04-corert"
      ]
    },
    "linuxmint.19-x64": {
      "#import": [
        "linuxmint.19",
        "ubuntu.18.04-x64"
      ]
    },
    "linuxmint.19-x64-corert": {
      "#import": [
        "linuxmint.19-corert",
        "linuxmint.19-x64",
        "ubuntu.18.04-x64-corert"
      ]
    },
    "ol": {
      "#import": [
        "rhel"
      ]
    },
    "ol-corert": {
      "#import": [
        "ol",
        "rhel-corert"
      ]
    },
    "ol-x64": {
      "#import": [
        "ol",
        "rhel-x64"
      ]
    },
    "ol-x64-corert": {
      "#import": [
        "ol-corert",
        "ol-x64",
        "rhel-x64-corert"
      ]
    },
    "ol.7": {
      "#import": [
        "ol",
        "rhel.7"
      ]
    },
    "ol.7-corert": {
      "#import": [
        "ol.7",
        "ol-corert",
        "rhel.7-corert"
      ]
    },
    "ol.7-x64": {
      "#import": [
        "ol.7",
        "ol-x64",
        "rhel.7-x64"
      ]
    },
    "ol.7-x64-corert": {
      "#import": [
        "ol.7-corert",
        "ol.7-x64",
        "ol.7",
        "ol-x64-corert"
      ]
    },
    "ol.7.0": {
      "#import": [
        "ol.7",
        "rhel.7.0"
      ]
    },
    "ol.7.0-corert": {
      "#import": [
        "ol.7.0",
        "ol.7-corert",
        "rhel.7.0-corert"
      ]
    },
    "ol.7.0-x64": {
      "#import": [
        "ol.7.0",
        "ol.7-x64",
        "rhel.7.0-x64"
      ]
    },
    "ol.7.0-x64-corert": {
      "#import": [
        "ol.7.0-corert",
        "ol.7.0-x64",
        "ol.7.0",
        "ol.7-x64-corert"
      ]
    },
    "ol.7.1": {
      "#import": [
        "ol.7.0",
        "rhel.7.1"
      ]
    },
    "ol.7.1-corert": {
      "#import": [
        "ol.7.1",
        "ol.7.0-corert",
        "rhel.7.1-corert"
      ]
    },
    "ol.7.1-x64": {
      "#import": [
        "ol.7.1",
        "ol.7.0-x64",
        "rhel.7.1-x64"
      ]
    },
    "ol.7.1-x64-corert": {
      "#import": [
        "ol.7.1-corert",
        "ol.7.1-x64",
        "ol.7.1",
        "ol.7.0-x64-corert"
      ]
    },
    "ol.7.2": {
      "#import": [
        "ol.7.1",
        "rhel.7.2"
      ]
    },
    "ol.7.2-corert": {
      "#import": [
        "ol.7.2",
        "ol.7.1-corert",
        "rhel.7.2-corert"
      ]
    },
    "ol.7.2-x64": {
      "#import": [
        "ol.7.2",
        "ol.7.1-x64",
        "rhel.7.2-x64"
      ]
    },
    "ol.7.2-x64-corert": {
      "#import": [
        "ol.7.2-corert",
        "ol.7.2-x64",
        "ol.7.2",
        "ol.7.1-x64-corert"
      ]
    },
    "ol.7.3": {
      "#import": [
        "ol.7.2",
        "rhel.7.3"
      ]
    },
    "ol.7.3-corert": {
      "#import": [
        "ol.7.3",
        "ol.7.2-corert",
        "rhel.7.3-corert"
      ]
    },
    "ol.7.3-x64": {
      "#import": [
        "ol.7.3",
        "ol.7.2-x64",
        "rhel.7.3-x64"
      ]
    },
    "ol.7.3-x64-corert": {
      "#import": [
        "ol.7.3-corert",
        "ol.7.3-x64",
        "ol.7.3",
        "ol.7.2-x64-corert"
      ]
    },
    "ol.7.4": {
      "#import": [
        "ol.7.3",
        "rhel.7.4"
      ]
    },
    "ol.7.4-corert": {
      "#import": [
        "ol.7.4",
        "ol.7.3-corert",
        "rhel.7.4-corert"
      ]
    },
    "ol.7.4-x64": {
      "#import": [
        "ol.7.4",
        "ol.7.3-x64",
        "rhel.7.4-x64"
      ]
    },
    "ol.7.4-x64-corert": {
      "#import": [
        "ol.7.4-corert",
        "ol.7.4-x64",
        "ol.7.4",
        "ol.7.3-x64-corert"
      ]
    },
    "ol.7.5": {
      "#import": [
        "ol.7.4",
        "rhel.7.5"
      ]
    },
    "ol.7.5-corert": {
      "#import": [
        "ol.7.5",
        "ol.7.4-corert",
        "rhel.7.5-corert"
      ]
    },
    "ol.7.5-x64": {
      "#import": [
        "ol.7.5",
        "ol.7.4-x64",
        "rhel.7.5-x64"
      ]
    },
    "ol.7.5-x64-corert": {
      "#import": [
        "ol.7.5-corert",
        "ol.7.5-x64",
        "ol.7.5",
        "ol.7.4-x64-corert"
      ]
    },
    "ol.7.6": {
      "#import": [
        "ol.7.5",
        "rhel.7.6"
      ]
    },
    "ol.7.6-corert": {
      "#import": [
        "ol.7.6",
        "ol.7.5-corert",
        "rhel.7.6-corert"
      ]
    },
    "ol.7.6-x64": {
      "#import": [
        "ol.7.6",
        "ol.7.5-x64",
        "rhel.7.6-x64"
      ]
    },
    "ol.7.6-x64-corert": {
      "#import": [
        "ol.7.6-corert",
        "ol.7.6-x64",
        "ol.7.6",
        "ol.7.5-x64-corert"
      ]
    },
    "opensuse": {
      "#import": [
        "linux"
      ]
    },
    "opensuse-corert": {
      "#import": [
        "opensuse",
        "linux-corert"
      ]
    },
    "opensuse-x64": {
      "#import": [
        "opensuse",
        "linux-x64"
      ]
    },
    "opensuse-x64-corert": {
      "#import": [
        "opensuse-corert",
        "opensuse-x64",
        "linux-x64-corert"
      ]
    },
    "opensuse.13.2": {
      "#import": [
        "opensuse"
      ]
    },
    "opensuse.13.2-corert": {
      "#import": [
        "opensuse.13.2",
        "opensuse-corert"
      ]
    },
    "opensuse.13.2-x64": {
      "#import": [
        "opensuse.13.2",
        "opensuse-x64"
      ]
    },
    "opensuse.13.2-x64-corert": {
      "#import": [
        "opensuse.13.2-corert",
        "opensuse.13.2-x64",
        "opensuse.13.2",
        "opensuse-x64-corert"
      ]
    },
    "opensuse.42.1": {
      "#import": [
        "opensuse"
      ]
    },
    "opensuse.42.1-corert": {
      "#import": [
        "opensuse.42.1",
        "opensuse-corert"
      ]
    },
    "opensuse.42.1-x64": {
      "#import": [
        "opensuse.42.1",
        "opensuse-x64"
      ]
    },
    "opensuse.42.1-x64-corert": {
      "#import": [
        "opensuse.42.1-corert",
        "opensuse.42.1-x64",
        "opensuse.42.1",
        "opensuse-x64-corert"
      ]
    },
    "opensuse.42.2": {
      "#import": [
        "opensuse"
      ]
    },
    "opensuse.42.2-corert": {
      "#import": [
        "opensuse.42.2",
        "opensuse-corert"
      ]
    },
    "opensuse.42.2-x64": {
      "#import": [
        "opensuse.42.2",
        "opensuse-x64"
      ]
    },
    "opensuse.42.2-x64-corert": {
      "#import": [
        "opensuse.42.2-corert",
        "opensuse.42.2-x64",
        "opensuse.42.2",
        "opensuse-x64-corert"
      ]
    },
    "opensuse.42.3": {
      "#import": [
        "opensuse"
      ]
    },
    "opensuse.42.3-corert": {
      "#import": [
        "opensuse.42.3",
        "opensuse-corert"
      ]
    },
    "opensuse.42.3-x64": {
      "#import": [
        "opensuse.42.3",
        "opensuse-x64"
      ]
    },
    "opensuse.42.3-x64-corert": {
      "#import": [
        "opensuse.42.3-corert",
        "opensuse.42.3-x64",
        "opensuse.42.3",
        "opensuse-x64-corert"
      ]
    },
    "osx": {
      "#import": [
        "unix"
      ]
    },
    "osx-corert": {
      "#import": [
        "osx",
        "unix-corert"
      ]
    },
    "osx-x64": {
      "#import": [
        "osx",
        "unix-x64"
      ]
    },
    "osx-x64-corert": {
      "#import": [
        "osx-corert",
        "osx-x64",
        "unix-x64-corert"
      ]
    },
    "osx.10.10": {
      "#import": [
        "osx"
      ]
    },
    "osx.10.10-corert": {
      "#import": [
        "osx.10.10",
        "osx-corert"
      ]
    },
    "osx.10.10-x64": {
      "#import": [
        "osx.10.10",
        "osx-x64"
      ]
    },
    "osx.10.10-x64-corert": {
      "#import": [
        "osx.10.10-corert",
        "osx.10.10-x64",
        "osx.10.10",
        "osx-x64-corert"
      ]
    },
    "osx.10.11": {
      "#import": [
        "osx.10.10"
      ]
    },
    "osx.10.11-corert": {
      "#import": [
        "osx.10.11",
        "osx.10.10-corert"
      ]
    },
    "osx.10.11-x64": {
      "#import": [
        "osx.10.11",
        "osx.10.10-x64"
      ]
    },
    "osx.10.11-x64-corert": {
      "#import": [
        "osx.10.11-corert",
        "osx.10.11-x64",
        "osx.10.11",
        "osx.10.10-x64-corert"
      ]
    },
    "osx.10.12": {
      "#import": [
        "osx.10.11"
      ]
    },
    "osx.10.12-corert": {
      "#import": [
        "osx.10.12",
        "osx.10.11-corert"
      ]
    },
    "osx.10.12-x64": {
      "#import": [
        "osx.10.12",
        "osx.10.11-x64"
      ]
    },
    "osx.10.12-x64-corert": {
      "#import": [
        "osx.10.12-corert",
        "osx.10.12-x64",
        "osx.10.12",
        "osx.10.11-x64-corert"
      ]
    },
    "osx.10.13": {
      "#import": [
        "osx.10.12"
      ]
    },
    "osx.10.13-corert": {
      "#import": [
        "osx.10.13",
        "osx.10.12-corert"
      ]
    },
    "osx.10.13-x64": {
      "#import": [
        "osx.10.13",
        "osx.10.12-x64"
      ]
    },
    "osx.10.13-x64-corert": {
      "#import": [
        "osx.10.13-corert",
        "osx.10.13-x64",
        "osx.10.13",
        "osx.10.12-x64-corert"
      ]
    },
    "osx.10.14": {
      "#import": [
        "osx.10.13"
      ]
    },
    "osx.10.14-corert": {
      "#import": [
        "osx.10.14",
        "osx.10.13-corert"
      ]
    },
    "osx.10.14-x64": {
      "#import": [
        "osx.10.14",
        "osx.10.13-x64"
      ]
    },
    "osx.10.14-x64-corert": {
      "#import": [
        "osx.10.14-corert",
        "osx.10.14-x64",
        "osx.10.14",
        "osx.10.13-x64-corert"
      ]
    },
    "rhel": {
      "#import": [
        "linux"
      ]
    },
    "rhel-corert": {
      "#import": [
        "rhel",
        "linux-corert"
      ]
    },
    "rhel-x64": {
      "#import": [
        "rhel",
        "linux-x64"
      ]
    },
    "rhel-x64-corert": {
      "#import": [
        "rhel-corert",
        "rhel-x64",
        "linux-x64-corert"
      ]
    },
    "rhel.6": {
      "#import": [
        "rhel"
      ]
    },
    "rhel.6-corert": {
      "#import": [
        "rhel.6",
        "rhel-corert"
      ]
    },
    "rhel.6-x64": {
      "#import": [
        "rhel.6",
        "rhel-x64"
      ]
    },
    "rhel.6-x64-corert": {
      "#import": [
        "rhel.6-corert",
        "rhel.6-x64",
        "rhel.6",
        "rhel-x64-corert"
      ]
    },
    "rhel.7": {
      "#import": [
        "rhel"
      ]
    },
    "rhel.7-corert": {
      "#import": [
        "rhel.7",
        "rhel-corert"
      ]
    },
    "rhel.7-x64": {
      "#import": [
        "rhel.7",
        "rhel-x64"
      ]
    },
    "rhel.7-x64-corert": {
      "#import": [
        "rhel.7-corert",
        "rhel.7-x64",
        "rhel.7",
        "rhel-x64-corert"
      ]
    },
    "rhel.7.0": {
      "#import": [
        "rhel.7"
      ]
    },
    "rhel.7.0-corert": {
      "#import": [
        "rhel.7.0",
        "rhel.7-corert"
      ]
    },
    "rhel.7.0-x64": {
      "#import": [
        "rhel.7.0",
        "rhel.7-x64"
      ]
    },
    "rhel.7.0-x64-corert": {
      "#import": [
        "rhel.7.0-corert",
        "rhel.7.0-x64",
        "rhel.7.0",
        "rhel.7-x64-corert"
      ]
    },
    "rhel.7.1": {
      "#import": [
        "rhel.7.0"
      ]
    },
    "rhel.7.1-corert": {
      "#import": [
        "rhel.7.1",
        "rhel.7.0-corert"
      ]
    },
    "rhel.7.1-x64": {
      "#import": [
        "rhel.7.1",
        "rhel.7.0-x64"
      ]
    },
    "rhel.7.1-x64-corert": {
      "#import": [
        "rhel.7.1-corert",
        "rhel.7.1-x64",
        "rhel.7.1",
        "rhel.7.0-x64-corert"
      ]
    },
    "rhel.7.2": {
      "#import": [
        "rhel.7.1"
      ]
    },
    "rhel.7.2-corert": {
      "#import": [
        "rhel.7.2",
        "rhel.7.1-corert"
      ]
    },
    "rhel.7.2-x64": {
      "#import": [
        "rhel.7.2",
        "rhel.7.1-x64"
      ]
    },
    "rhel.7.2-x64-corert": {
      "#import": [
        "rhel.7.2-corert",
        "rhel.7.2-x64",
        "rhel.7.2",
        "rhel.7.1-x64-corert"
      ]
    },
    "rhel.7.3": {
      "#import": [
        "rhel.7.2"
      ]
    },
    "rhel.7.3-corert": {
      "#import": [
        "rhel.7.3",
        "rhel.7.2-corert"
      ]
    },
    "rhel.7.3-x64": {
      "#import": [
        "rhel.7.3",
        "rhel.7.2-x64"
      ]
    },
    "rhel.7.3-x64-corert": {
      "#import": [
        "rhel.7.3-corert",
        "rhel.7.3-x64",
        "rhel.7.3",
        "rhel.7.2-x64-corert"
      ]
    },
    "rhel.7.4": {
      "#import": [
        "rhel.7.3"
      ]
    },
    "rhel.7.4-corert": {
      "#import": [
        "rhel.7.4",
        "rhel.7.3-corert"
      ]
    },
    "rhel.7.4-x64": {
      "#import": [
        "rhel.7.4",
        "rhel.7.3-x64"
      ]
    },
    "rhel.7.4-x64-corert": {
      "#import": [
        "rhel.7.4-corert",
        "rhel.7.4-x64",
        "rhel.7.4",
        "rhel.7.3-x64-corert"
      ]
    },
    "rhel.7.5": {
      "#import": [
        "rhel.7.4"
      ]
    },
    "rhel.7.5-corert": {
      "#import": [
        "rhel.7.5",
        "rhel.7.4-corert"
      ]
    },
    "rhel.7.5-x64": {
      "#import": [
        "rhel.7.5",
        "rhel.7.4-x64"
      ]
    },
    "rhel.7.5-x64-corert": {
      "#import": [
        "rhel.7.5-corert",
        "rhel.7.5-x64",
        "rhel.7.5",
        "rhel.7.4-x64-corert"
      ]
    },
    "rhel.7.6": {
      "#import": [
        "rhel.7.5"
      ]
    },
    "rhel.7.6-corert": {
      "#import": [
        "rhel.7.6",
        "rhel.7.5-corert"
      ]
    },
    "rhel.7.6-x64": {
      "#import": [
        "rhel.7.6",
        "rhel.7.5-x64"
      ]
    },
    "rhel.7.6-x64-corert": {
      "#import": [
        "rhel.7.6-corert",
        "rhel.7.6-x64",
        "rhel.7.6",
        "rhel.7.5-x64-corert"
      ]
    },
    "rhel.8": {
      "#import": [
        "rhel"
      ]
    },
    "rhel.8-corert": {
      "#import": [
        "rhel.8",
        "rhel-corert"
      ]
    },
    "rhel.8-x64": {
      "#import": [
        "rhel.8",
        "rhel-x64"
      ]
    },
    "rhel.8-x64-corert": {
      "#import": [
        "rhel.8-corert",
        "rhel.8-x64",
        "rhel.8",
        "rhel-x64-corert"
      ]
    },
    "rhel.8.0": {
      "#import": [
        "rhel.8"
      ]
    },
    "rhel.8.0-corert": {
      "#import": [
        "rhel.8.0",
        "rhel.8-corert"
      ]
    },
    "rhel.8.0-x64": {
      "#import": [
        "rhel.8.0",
        "rhel.8-x64"
      ]
    },
    "rhel.8.0-x64-corert": {
      "#import": [
        "rhel.8.0-corert",
        "rhel.8.0-x64",
        "rhel.8.0",
        "rhel.8-x64-corert"
      ]
    },
    "sles": {
      "#import": [
        "linux"
      ]
    },
    "sles-corert": {
      "#import": [
        "sles",
        "linux-corert"
      ]
    },
    "sles-x64": {
      "#import": [
        "sles",
        "linux-x64"
      ]
    },
    "sles-x64-corert": {
      "#import": [
        "sles-corert",
        "sles-x64",
        "linux-x64-corert"
      ]
    },
    "sles.12": {
      "#import": [
        "sles"
      ]
    },
    "sles.12-corert": {
      "#import": [
        "sles.12",
        "sles-corert"
      ]
    },
    "sles.12-x64": {
      "#import": [
        "sles.12",
        "sles-x64"
      ]
    },
    "sles.12-x64-corert": {
      "#import": [
        "sles.12-corert",
        "sles.12-x64",
        "sles.12",
        "sles-x64-corert"
      ]
    },
    "sles.12.1": {
      "#import": [
        "sles.12"
      ]
    },
    "sles.12.1-corert": {
      "#import": [
        "sles.12.1",
        "sles.12-corert"
      ]
    },
    "sles.12.1-x64": {
      "#import": [
        "sles.12.1",
        "sles.12-x64"
      ]
    },
    "sles.12.1-x64-corert": {
      "#import": [
        "sles.12.1-corert",
        "sles.12.1-x64",
        "sles.12.1",
        "sles.12-x64-corert"
      ]
    },
    "sles.12.2": {
      "#import": [
        "sles.12.1"
      ]
    },
    "sles.12.2-corert": {
      "#import": [
        "sles.12.2",
        "sles.12.1-corert"
      ]
    },
    "sles.12.2-x64": {
      "#import": [
        "sles.12.2",
        "sles.12.1-x64"
      ]
    },
    "sles.12.2-x64-corert": {
      "#import": [
        "sles.12.2-corert",
        "sles.12.2-x64",
        "sles.12.2",
        "sles.12.1-x64-corert"
      ]
    },
    "sles.12.3": {
      "#import": [
        "sles.12.2"
      ]
    },
    "sles.12.3-corert": {
      "#import": [
        "sles.12.3",
        "sles.12.2-corert"
      ]
    },
    "sles.12.3-x64": {
      "#import": [
        "sles.12.3",
        "sles.12.2-x64"
      ]
    },
    "sles.12.3-x64-corert": {
      "#import": [
        "sles.12.3-corert",
        "sles.12.3-x64",
        "sles.12.3",
        "sles.12.2-x64-corert"
      ]
    },
    "tizen": {
      "#import": [
        "linux"
      ]
    },
    "tizen-armel": {
      "#import": [
        "tizen",
        "linux-armel"
      ]
    },
    "tizen-armel-corert": {
      "#import": [
        "tizen-corert",
        "tizen-armel",
        "linux-armel-corert"
      ]
    },
    "tizen-corert": {
      "#import": [
        "tizen",
        "linux-corert"
      ]
    },
    "tizen-x86": {
      "#import": [
        "tizen",
        "linux-x86"
      ]
    },
    "tizen-x86-corert": {
      "#import": [
        "tizen-corert",
        "tizen-x86",
        "linux-x86-corert"
      ]
    },
    "tizen.4.0.0": {
      "#import": [
        "tizen"
      ]
    },
    "tizen.4.0.0-armel": {
      "#import": [
        "tizen.4.0.0",
        "tizen-armel"
      ]
    },
    "tizen.4.0.0-armel-corert": {
      "#import": [
        "tizen.4.0.0-corert",
        "tizen.4.0.0-armel",
        "tizen.4.0.0",
        "tizen-armel-corert"
      ]
    },
    "tizen.4.0.0-corert": {
      "#import": [
        "tizen.4.0.0",
        "tizen-corert"
      ]
    },
    "tizen.4.0.0-x86": {
      "#import": [
        "tizen.4.0.0",
        "tizen-x86"
      ]
    },
    "tizen.4.0.0-x86-corert": {
      "#import": [
        "tizen.4.0.0-corert",
        "tizen.4.0.0-x86",
        "tizen.4.0.0",
        "tizen-x86-corert"
      ]
    },
    "tizen.5.0.0": {
      "#import": [
        "tizen.4.0.0"
      ]
    },
    "tizen.5.0.0-armel": {
      "#import": [
        "tizen.5.0.0",
        "tizen.4.0.0-armel"
      ]
    },
    "tizen.5.0.0-armel-corert": {
      "#import": [
        "tizen.5.0.0-corert",
        "tizen.5.0.0-armel",
        "tizen.5.0.0",
        "tizen.4.0.0-armel-corert"
      ]
    },
    "tizen.5.0.0-corert": {
      "#import": [
        "tizen.5.0.0",
        "tizen.4.0.0-corert"
      ]
    },
    "tizen.5.0.0-x86": {
      "#import": [
        "tizen.5.0.0",
        "tizen.4.0.0-x86"
      ]
    },
    "tizen.5.0.0-x86-corert": {
      "#import": [
        "tizen.5.0.0-corert",
        "tizen.5.0.0-x86",
        "tizen.5.0.0",
        "tizen.4.0.0-x86-corert"
      ]
    },
    "ubuntu": {
      "#import": [
        "debian"
      ]
    },
    "ubuntu-arm": {
      "#import": [
        "ubuntu",
        "debian-arm"
      ]
    },
    "ubuntu-arm-corert": {
      "#import": [
        "ubuntu-corert",
        "ubuntu-arm",
        "debian-arm-corert"
      ]
    },
    "ubuntu-arm64": {
      "#import": [
        "ubuntu",
        "debian-arm64"
      ]
    },
    "ubuntu-arm64-corert": {
      "#import": [
        "ubuntu-corert",
        "ubuntu-arm64",
        "debian-arm64-corert"
      ]
    },
    "ubuntu-corert": {
      "#import": [
        "ubuntu",
        "debian-corert"
      ]
    },
    "ubuntu-x64": {
      "#import": [
        "ubuntu",
        "debian-x64"
      ]
    },
    "ubuntu-x64-corert": {
      "#import": [
        "ubuntu-corert",
        "ubuntu-x64",
        "debian-x64-corert"
      ]
    },
    "ubuntu-x86": {
      "#import": [
        "ubuntu",
        "debian-x86"
      ]
    },
    "ubuntu-x86-corert": {
      "#import": [
        "ubuntu-corert",
        "ubuntu-x86",
        "debian-x86-corert"
      ]
    },
    "ubuntu.14.04": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.14.04-arm": {
      "#import": [
        "ubuntu.14.04",
        "ubuntu-arm"
      ]
    },
    "ubuntu.14.04-arm-corert": {
      "#import": [
        "ubuntu.14.04-corert",
        "ubuntu.14.04-arm",
        "ubuntu.14.04",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.14.04-corert": {
      "#import": [
        "ubuntu.14.04",
        "ubuntu-corert"
      ]
    },
    "ubuntu.14.04-x64": {
      "#import": [
        "ubuntu.14.04",
        "ubuntu-x64"
      ]
    },
    "ubuntu.14.04-x64-corert": {
      "#import": [
        "ubuntu.14.04-corert",
        "ubuntu.14.04-x64",
        "ubuntu.14.04",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.14.04-x86": {
      "#import": [
        "ubuntu.14.04",
        "ubuntu-x86"
      ]
    },
    "ubuntu.14.04-x86-corert": {
      "#import": [
        "ubuntu.14.04-corert",
        "ubuntu.14.04-x86",
        "ubuntu.14.04",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.14.10": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.14.10-arm": {
      "#import": [
        "ubuntu.14.10",
        "ubuntu-arm"
      ]
    },
    "ubuntu.14.10-arm-corert": {
      "#import": [
        "ubuntu.14.10-corert",
        "ubuntu.14.10-arm",
        "ubuntu.14.10",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.14.10-corert": {
      "#import": [
        "ubuntu.14.10",
        "ubuntu-corert"
      ]
    },
    "ubuntu.14.10-x64": {
      "#import": [
        "ubuntu.14.10",
        "ubuntu-x64"
      ]
    },
    "ubuntu.14.10-x64-corert": {
      "#import": [
        "ubuntu.14.10-corert",
        "ubuntu.14.10-x64",
        "ubuntu.14.10",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.14.10-x86": {
      "#import": [
        "ubuntu.14.10",
        "ubuntu-x86"
      ]
    },
    "ubuntu.14.10-x86-corert": {
      "#import": [
        "ubuntu.14.10-corert",
        "ubuntu.14.10-x86",
        "ubuntu.14.10",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.15.04": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.15.04-arm": {
      "#import": [
        "ubuntu.15.04",
        "ubuntu-arm"
      ]
    },
    "ubuntu.15.04-arm-corert": {
      "#import": [
        "ubuntu.15.04-corert",
        "ubuntu.15.04-arm",
        "ubuntu.15.04",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.15.04-corert": {
      "#import": [
        "ubuntu.15.04",
        "ubuntu-corert"
      ]
    },
    "ubuntu.15.04-x64": {
      "#import": [
        "ubuntu.15.04",
        "ubuntu-x64"
      ]
    },
    "ubuntu.15.04-x64-corert": {
      "#import": [
        "ubuntu.15.04-corert",
        "ubuntu.15.04-x64",
        "ubuntu.15.04",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.15.04-x86": {
      "#import": [
        "ubuntu.15.04",
        "ubuntu-x86"
      ]
    },
    "ubuntu.15.04-x86-corert": {
      "#import": [
        "ubuntu.15.04-corert",
        "ubuntu.15.04-x86",
        "ubuntu.15.04",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.15.10": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.15.10-arm": {
      "#import": [
        "ubuntu.15.10",
        "ubuntu-arm"
      ]
    },
    "ubuntu.15.10-arm-corert": {
      "#import": [
        "ubuntu.15.10-corert",
        "ubuntu.15.10-arm",
        "ubuntu.15.10",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.15.10-corert": {
      "#import": [
        "ubuntu.15.10",
        "ubuntu-corert"
      ]
    },
    "ubuntu.15.10-x64": {
      "#import": [
        "ubuntu.15.10",
        "ubuntu-x64"
      ]
    },
    "ubuntu.15.10-x64-corert": {
      "#import": [
        "ubuntu.15.10-corert",
        "ubuntu.15.10-x64",
        "ubuntu.15.10",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.15.10-x86": {
      "#import": [
        "ubuntu.15.10",
        "ubuntu-x86"
      ]
    },
    "ubuntu.15.10-x86-corert": {
      "#import": [
        "ubuntu.15.10-corert",
        "ubuntu.15.10-x86",
        "ubuntu.15.10",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.16.04": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.16.04-arm": {
      "#import": [
        "ubuntu.16.04",
        "ubuntu-arm"
      ]
    },
    "ubuntu.16.04-arm-corert": {
      "#import": [
        "ubuntu.16.04-corert",
        "ubuntu.16.04-arm",
        "ubuntu.16.04",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.16.04-arm64": {
      "#import": [
        "ubuntu.16.04",
        "ubuntu-arm64"
      ]
    },
    "ubuntu.16.04-arm64-corert": {
      "#import": [
        "ubuntu.16.04-corert",
        "ubuntu.16.04-arm64",
        "ubuntu.16.04",
        "ubuntu-arm64-corert"
      ]
    },
    "ubuntu.16.04-corert": {
      "#import": [
        "ubuntu.16.04",
        "ubuntu-corert"
      ]
    },
    "ubuntu.16.04-x64": {
      "#import": [
        "ubuntu.16.04",
        "ubuntu-x64"
      ]
    },
    "ubuntu.16.04-x64-corert": {
      "#import": [
        "ubuntu.16.04-corert",
        "ubuntu.16.04-x64",
        "ubuntu.16.04",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.16.04-x86": {
      "#import": [
        "ubuntu.16.04",
        "ubuntu-x86"
      ]
    },
    "ubuntu.16.04-x86-corert": {
      "#import": [
        "ubuntu.16.04-corert",
        "ubuntu.16.04-x86",
        "ubuntu.16.04",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.16.10": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.16.10-arm": {
      "#import": [
        "ubuntu.16.10",
        "ubuntu-arm"
      ]
    },
    "ubuntu.16.10-arm-corert": {
      "#import": [
        "ubuntu.16.10-corert",
        "ubuntu.16.10-arm",
        "ubuntu.16.10",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.16.10-arm64": {
      "#import": [
        "ubuntu.16.10",
        "ubuntu-arm64"
      ]
    },
    "ubuntu.16.10-arm64-corert": {
      "#import": [
        "ubuntu.16.10-corert",
        "ubuntu.16.10-arm64",
        "ubuntu.16.10",
        "ubuntu-arm64-corert"
      ]
    },
    "ubuntu.16.10-corert": {
      "#import": [
        "ubuntu.16.10",
        "ubuntu-corert"
      ]
    },
    "ubuntu.16.10-x64": {
      "#import": [
        "ubuntu.16.10",
        "ubuntu-x64"
      ]
    },
    "ubuntu.16.10-x64-corert": {
      "#import": [
        "ubuntu.16.10-corert",
        "ubuntu.16.10-x64",
        "ubuntu.16.10",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.16.10-x86": {
      "#import": [
        "ubuntu.16.10",
        "ubuntu-x86"
      ]
    },
    "ubuntu.16.10-x86-corert": {
      "#import": [
        "ubuntu.16.10-corert",
        "ubuntu.16.10-x86",
        "ubuntu.16.10",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.17.04": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.17.04-arm": {
      "#import": [
        "ubuntu.17.04",
        "ubuntu-arm"
      ]
    },
    "ubuntu.17.04-arm-corert": {
      "#import": [
        "ubuntu.17.04-corert",
        "ubuntu.17.04-arm",
        "ubuntu.17.04",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.17.04-arm64": {
      "#import": [
        "ubuntu.17.04",
        "ubuntu-arm64"
      ]
    },
    "ubuntu.17.04-arm64-corert": {
      "#import": [
        "ubuntu.17.04-corert",
        "ubuntu.17.04-arm64",
        "ubuntu.17.04",
        "ubuntu-arm64-corert"
      ]
    },
    "ubuntu.17.04-corert": {
      "#import": [
        "ubuntu.17.04",
        "ubuntu-corert"
      ]
    },
    "ubuntu.17.04-x64": {
      "#import": [
        "ubuntu.17.04",
        "ubuntu-x64"
      ]
    },
    "ubuntu.17.04-x64-corert": {
      "#import": [
        "ubuntu.17.04-corert",
        "ubuntu.17.04-x64",
        "ubuntu.17.04",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.17.04-x86": {
      "#import": [
        "ubuntu.17.04",
        "ubuntu-x86"
      ]
    },
    "ubuntu.17.04-x86-corert": {
      "#import": [
        "ubuntu.17.04-corert",
        "ubuntu.17.04-x86",
        "ubuntu.17.04",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.17.10": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.17.10-arm": {
      "#import": [
        "ubuntu.17.10",
        "ubuntu-arm"
      ]
    },
    "ubuntu.17.10-arm-corert": {
      "#import": [
        "ubuntu.17.10-corert",
        "ubuntu.17.10-arm",
        "ubuntu.17.10",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.17.10-arm64": {
      "#import": [
        "ubuntu.17.10",
        "ubuntu-arm64"
      ]
    },
    "ubuntu.17.10-arm64-corert": {
      "#import": [
        "ubuntu.17.10-corert",
        "ubuntu.17.10-arm64",
        "ubuntu.17.10",
        "ubuntu-arm64-corert"
      ]
    },
    "ubuntu.17.10-corert": {
      "#import": [
        "ubuntu.17.10",
        "ubuntu-corert"
      ]
    },
    "ubuntu.17.10-x64": {
      "#import": [
        "ubuntu.17.10",
        "ubuntu-x64"
      ]
    },
    "ubuntu.17.10-x64-corert": {
      "#import": [
        "ubuntu.17.10-corert",
        "ubuntu.17.10-x64",
        "ubuntu.17.10",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.17.10-x86": {
      "#import": [
        "ubuntu.17.10",
        "ubuntu-x86"
      ]
    },
    "ubuntu.17.10-x86-corert": {
      "#import": [
        "ubuntu.17.10-corert",
        "ubuntu.17.10-x86",
        "ubuntu.17.10",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.18.04": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.18.04-arm": {
      "#import": [
        "ubuntu.18.04",
        "ubuntu-arm"
      ]
    },
    "ubuntu.18.04-arm-corert": {
      "#import": [
        "ubuntu.18.04-corert",
        "ubuntu.18.04-arm",
        "ubuntu.18.04",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.18.04-arm64": {
      "#import": [
        "ubuntu.18.04",
        "ubuntu-arm64"
      ]
    },
    "ubuntu.18.04-arm64-corert": {
      "#import": [
        "ubuntu.18.04-corert",
        "ubuntu.18.04-arm64",
        "ubuntu.18.04",
        "ubuntu-arm64-corert"
      ]
    },
    "ubuntu.18.04-corert": {
      "#import": [
        "ubuntu.18.04",
        "ubuntu-corert"
      ]
    },
    "ubuntu.18.04-x64": {
      "#import": [
        "ubuntu.18.04",
        "ubuntu-x64"
      ]
    },
    "ubuntu.18.04-x64-corert": {
      "#import": [
        "ubuntu.18.04-corert",
        "ubuntu.18.04-x64",
        "ubuntu.18.04",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.18.04-x86": {
      "#import": [
        "ubuntu.18.04",
        "ubuntu-x86"
      ]
    },
    "ubuntu.18.04-x86-corert": {
      "#import": [
        "ubuntu.18.04-corert",
        "ubuntu.18.04-x86",
        "ubuntu.18.04",
        "ubuntu-x86-corert"
      ]
    },
    "ubuntu.18.10": {
      "#import": [
        "ubuntu"
      ]
    },
    "ubuntu.18.10-arm": {
      "#import": [
        "ubuntu.18.10",
        "ubuntu-arm"
      ]
    },
    "ubuntu.18.10-arm-corert": {
      "#import": [
        "ubuntu.18.10-corert",
        "ubuntu.18.10-arm",
        "ubuntu.18.10",
        "ubuntu-arm-corert"
      ]
    },
    "ubuntu.18.10-arm64": {
      "#import": [
        "ubuntu.18.10",
        "ubuntu-arm64"
      ]
    },
    "ubuntu.18.10-arm64-corert": {
      "#import": [
        "ubuntu.18.10-corert",
        "ubuntu.18.10-arm64",
        "ubuntu.18.10",
        "ubuntu-arm64-corert"
      ]
    },
    "ubuntu.18.10-corert": {
      "#import": [
        "ubuntu.18.10",
        "ubuntu-corert"
      ]
    },
    "ubuntu.18.10-x64": {
      "#import": [
        "ubuntu.18.10",
        "ubuntu-x64"
      ]
    },
    "ubuntu.18.10-x64-corert": {
      "#import": [
        "ubuntu.18.10-corert",
        "ubuntu.18.10-x64",
        "ubuntu.18.10",
        "ubuntu-x64-corert"
      ]
    },
    "ubuntu.18.10-x86": {
      "#import": [
        "ubuntu.18.10",
        "ubuntu-x86"
      ]
    },
    "ubuntu.18.10-x86-corert": {
      "#import": [
        "ubuntu.18.10-corert",
        "ubuntu.18.10-x86",
        "ubuntu.18.10",
        "ubuntu-x86-corert"
      ]
    },
    "unix": {
      "#import": [
        "any"
      ]
    },
    "unix-arm": {
      "#import": [
        "unix"
      ]
    },
    "unix-arm-corert": {
      "#import": [
        "unix-corert",
        "unix-arm"
      ]
    },
    "unix-arm64": {
      "#import": [
        "unix"
      ]
    },
    "unix-arm64-corert": {
      "#import": [
        "unix-corert",
        "unix-arm64"
      ]
    },
    "unix-armel": {
      "#import": [
        "unix"
      ]
    },
    "unix-armel-corert": {
      "#import": [
        "unix-corert",
        "unix-armel"
      ]
    },
    "unix-corert": {
      "#import": [
        "unix",
        "corert"
      ]
    },
    "unix-x64": {
      "#import": [
        "unix"
      ]
    },
    "unix-x64-corert": {
      "#import": [
        "unix-corert",
        "unix-x64"
      ]
    },
    "unix-x86": {
      "#import": [
        "unix"
      ]
    },
    "unix-x86-corert": {
      "#import": [
        "unix-corert",
        "unix-x86"
      ]
    },
    "win": {
      "#import": [
        "any"
      ]
    },
    "win-aot": {
      "#import": [
        "win",
        "aot"
      ]
    },
    "win-arm": {
      "#import": [
        "win"
      ]
    },
    "win-arm-aot": {
      "#import": [
        "win-aot",
        "win-arm"
      ]
    },
    "win-arm-corert": {
      "#import": [
        "win-corert",
        "win-arm"
      ]
    },
    "win-arm64": {
      "#import": [
        "win"
      ]
    },
    "win-arm64-aot": {
      "#import": [
        "win-aot",
        "win-arm64"
      ]
    },
    "win-arm64-corert": {
      "#import": [
        "win-corert",
        "win-arm64"
      ]
    },
    "win-corert": {
      "#import": [
        "win",
        "corert"
      ]
    },
    "win-x64": {
      "#import": [
        "win"
      ]
    },
    "win-x64-aot": {
      "#import": [
        "win-aot",
        "win-x64"
      ]
    },
    "win-x64-corert": {
      "#import": [
        "win-corert",
        "win-x64"
      ]
    },
    "win-x86": {
      "#import": [
        "win"
      ]
    },
    "win-x86-aot": {
      "#import": [
        "win-aot",
        "win-x86"
      ]
    },
    "win-x86-corert": {
      "#import": [
        "win-corert",
        "win-x86"
      ]
    },
    "win10": {
      "#import": [
        "win81"
      ]
    },
    "win10-aot": {
      "#import": [
        "win10",
        "win81-aot"
      ]
    },
    "win10-arm": {
      "#import": [
        "win10",
        "win81-arm"
      ]
    },
    "win10-arm-aot": {
      "#import": [
        "win10-aot",
        "win10-arm",
        "win10",
        "win81-arm-aot"
      ]
    },
    "win10-arm-corert": {
      "#import": [
        "win10-corert",
        "win10-arm",
        "win10",
        "win81-arm-corert"
      ]
    },
    "win10-arm64": {
      "#import": [
        "win10",
        "win81-arm64"
      ]
    },
    "win10-arm64-aot": {
      "#import": [
        "win10-aot",
        "win10-arm64",
        "win10",
        "win81-arm64-aot"
      ]
    },
    "win10-arm64-corert": {
      "#import": [
        "win10-corert",
        "win10-arm64",
        "win10",
        "win81-arm64-corert"
      ]
    },
    "win10-corert": {
      "#import": [
        "win10",
        "win81-corert"
      ]
    },
    "win10-x64": {
      "#import": [
        "win10",
        "win81-x64"
      ]
    },
    "win10-x64-aot": {
      "#import": [
        "win10-aot",
        "win10-x64",
        "win10",
        "win81-x64-aot"
      ]
    },
    "win10-x64-corert": {
      "#import": [
        "win10-corert",
        "win10-x64",
        "win10",
        "win81-x64-corert"
      ]
    },
    "win10-x86": {
      "#import": [
        "win10",
        "win81-x86"
      ]
    },
    "win10-x86-aot": {
      "#import": [
        "win10-aot",
        "win10-x86",
        "win10",
        "win81-x86-aot"
      ]
    },
    "win10-x86-corert": {
      "#import": [
        "win10-corert",
        "win10-x86",
        "win10",
        "win81-x86-corert"
      ]
    },
    "win7": {
      "#import": [
        "win"
      ]
    },
    "win7-aot": {
      "#import": [
        "win7",
        "win-aot"
      ]
    },
    "win7-arm": {
      "#import": [
        "win7",
        "win-arm"
      ]
    },
    "win7-arm-aot": {
      "#import": [
        "win7-aot",
        "win7-arm",
        "win7",
        "win-arm-aot"
      ]
    },
    "win7-arm-corert": {
      "#import": [
        "win7-corert",
        "win7-arm",
        "win7",
        "win-arm-corert"
      ]
    },
    "win7-arm64": {
      "#import": [
        "win7",
        "win-arm64"
      ]
    },
    "win7-arm64-aot": {
      "#import": [
        "win7-aot",
        "win7-arm64",
        "win7",
        "win-arm64-aot"
      ]
    },
    "win7-arm64-corert": {
      "#import": [
        "win7-corert",
        "win7-arm64",
        "win7",
        "win-arm64-corert"
      ]
    },
    "win7-corert": {
      "#import": [
        "win7",
        "win-corert"
      ]
    },
    "win7-x64": {
      "#import": [
        "win7",
        "win-x64"
      ]
    },
    "win7-x64-aot": {
      "#import": [
        "win7-aot",
        "win7-x64",
        "win7",
        "win-x64-aot"
      ]
    },
    "win7-x64-corert": {
      "#import": [
        "win7-corert",
        "win7-x64",
        "win7",
        "win-x64-corert"
      ]
    },
    "win7-x86": {
      "#import": [
        "win7",
        "win-x86"
      ]
    },
    "win7-x86-aot": {
      "#import": [
        "win7-aot",
        "win7-x86",
        "win7",
        "win-x86-aot"
      ]
    },
    "win7-x86-corert": {
      "#import": [
        "win7-corert",
        "win7-x86",
        "win7",
        "win-x86-corert"
      ]
    },
    "win8": {
      "#import": [
        "win7"
      ]
    },
    "win8-aot": {
      "#import": [
        "win8",
        "win7-aot"
      ]
    },
    "win8-arm": {
      "#import": [
        "win8",
        "win7-arm"
      ]
    },
    "win8-arm-aot": {
      "#import": [
        "win8-aot",
        "win8-arm",
        "win8",
        "win7-arm-aot"
      ]
    },
    "win8-arm-corert": {
      "#import": [
        "win8-corert",
        "win8-arm",
        "win8",
        "win7-arm-corert"
      ]
    },
    "win8-arm64": {
      "#import": [
        "win8",
        "win7-arm64"
      ]
    },
    "win8-arm64-aot": {
      "#import": [
        "win8-aot",
        "win8-arm64",
        "win8",
        "win7-arm64-aot"
      ]
    },
    "win8-arm64-corert": {
      "#import": [
        "win8-corert",
        "win8-arm64",
        "win8",
        "win7-arm64-corert"
      ]
    },
    "win8-corert": {
      "#import": [
        "win8",
        "win7-corert"
      ]
    },
    "win8-x64": {
      "#import": [
        "win8",
        "win7-x64"
      ]
    },
    "win8-x64-aot": {
      "#import": [
        "win8-aot",
        "win8-x64",
        "win8",
        "win7-x64-aot"
      ]
    },
    "win8-x64-corert": {
      "#import": [
        "win8-corert",
        "win8-x64",
        "win8",
        "win7-x64-corert"
      ]
    },
    "win8-x86": {
      "#import": [
        "win8",
        "win7-x86"
      ]
    },
    "win8-x86-aot": {
      "#import": [
        "win8-aot",
        "win8-x86",
        "win8",
        "win7-x86-aot"
      ]
    },
    "win8-x86-corert": {
      "#import": [
        "win8-corert",
        "win8-x86",
        "win8",
        "win7-x86-corert"
      ]
    },
    "win81": {
      "#import": [
        "win8"
      ]
    },
    "win81-aot": {
      "#import": [
        "win81",
        "win8-aot"
      ]
    },
    "win81-arm": {
      "#import": [
        "win81",
        "win8-arm"
      ]
    },
    "win81-arm-aot": {
      "#import": [
        "win81-aot",
        "win81-arm",
        "win81",
        "win8-arm-aot"
      ]
    },
    "win81-arm-corert": {
      "#import": [
        "win81-corert",
        "win81-arm",
        "win81",
        "win8-arm-corert"
      ]
    },
    "win81-arm64": {
      "#import": [
        "win81",
        "win8-arm64"
      ]
    },
    "win81-arm64-aot": {
      "#import": [
        "win81-aot",
        "win81-arm64",
        "win81",
        "win8-arm64-aot"
      ]
    },
    "win81-arm64-corert": {
      "#import": [
        "win81-corert",
        "win81-arm64",
        "win81",
        "win8-arm64-corert"
      ]
    },
    "win81-corert": {
      "#import": [
        "win81",
        "win8-corert"
      ]
    },
    "win81-x64": {
      "#import": [
        "win81",
        "win8-x64"
      ]
    },
    "win81-x64-aot": {
      "#import": [
        "win81-aot",
        "win81-x64",
        "win81",
        "win8-x64-aot"
      ]
    },
    "win81-x64-corert": {
      "#import": [
        "win81-corert",
        "win81-x64",
        "win81",
        "win8-x64-corert"
      ]
    },
    "win81-x86": {
      "#import": [
        "win81",
        "win8-x86"
      ]
    },
    "win81-x86-aot": {
      "#import": [
        "win81-aot",
        "win81-x86",
        "win81",
        "win8-x86-aot"
      ]
    },
    "win81-x86-corert": {
      "#import": [
        "win81-corert",
        "win81-x86",
        "win81",
        "win8-x86-corert"
      ]
    }
  }
}
"""

let runtimeJsonMsNetCoreTargets2_1_0 = """
{
    "supports": {
        "uwp.10.0.app": {
            "uap10.0": [
                "win10-x86",
                "win10-x86-aot",
                "win10-x64",
                "win10-x64-aot",
                "win10-arm",
                "win10-arm-aot"
            ]
        },
        "net45.app": {
            "net45": [
                "",
                "win-x86",
                "win-x64"
            ]
        },
        "net451.app": {
            "net451": [
                "",
                "win-x86",
                "win-x64"
            ]
        },
        "net452.app": {
            "net452": [
                "",
                "win-x86",
                "win-x64"
            ]
        },
        "net46.app": {
            "net46": [
                "",
                "win-x86",
                "win-x64"
            ]
        },
        "net461.app": {
            "net461": [
                "",
                "win-x86",
                "win-x64"
            ]
        },
        "net462.app": {
            "net462": [
                "",
                "win-x86",
                "win-x64"
            ]
        },
        "netcoreapp1.0.app": {
            "netcoreapp1.0": [
                "win7-x86",
                "win7-x64",
                "osx.10.11-x64",
                "centos.7-x64",
                "debian.8-x64",
                "linuxmint.17-x64",
                "opensuse.13.2-x64",
                "rhel.7.2-x64",
                "ubuntu.14.04-x64",
                "ubuntu.16.04-x64"
            ]
        },
        "win8.app": {
          "win8": ""
        },
        "win81.app": {
          "win81": ""
        },
        "wp8.app": {
          "wp8": ""
        },
        "wp81.app": {
          "wp81": ""
        },
        "wpa81.app": {
          "wpa81": ""
        },
        "dnxcore50.app": {
            "dnxcore50": [
                "win7-x86",
                "win7-x64"
            ]
        }
    }
}
"""


let runtimeGraphMsNetCorePlatforms2_2_1 = RuntimeGraphParser.readRuntimeGraph runtimeJsonMsNetCorePlatforms2_2_1
let runtimeGraphMsNetCoreTargets2_1_0 = RuntimeGraphParser.readRuntimeGraph runtimeJsonMsNetCoreTargets2_1_0



[<Test>]
let ``Check if we can parse runtime support and runtime dependencies``() =
    let runtimeGraph = RuntimeGraphParser.readRuntimeGraph supportAndDeps

    runtimeGraph
    |> shouldEqual
        { Runtimes =
            [ { Rid = Rid.Of "win"; InheritedRids = [ ]
                RuntimeDependencies =
                  [ PackageName "Microsoft.Win32.Primitives", [ PackageName "runtime.win.Microsoft.Win32.Primitives", VersionRequirement.VersionRequirement (VersionRange.Minimum (SemVer.Parse "4.3.0"), PreReleaseStatus.No) ]
                    PackageName "System.Runtime.Extensions", [ PackageName "runtime.win.System.Runtime.Extensions", VersionRequirement.VersionRequirement (VersionRange.Minimum (SemVer.Parse "4.3.0"), PreReleaseStatus.No) ]
                  ]
                  |> Map.ofSeq } ]
            |> Seq.map (fun r -> r.Rid, r)
            |> Map.ofSeq
          Supports =
            [ { Name = "net45.app"
                Supported =
                  [ FrameworkIdentifier.DotNetFramework FrameworkVersion.V4_5, [ Rid.Of ""; Rid.Of "win-x86"; Rid.Of "win-x64" ]]
                  |> Map.ofSeq }
              { Name = "uwp.10.0.app"
                Supported =
                  [ FrameworkIdentifier.UAP UAPVersion.V10, [ Rid.Of "win10-x86"; Rid.Of "win10-x86-aot"; Rid.Of "win10-arm"; Rid.Of "win10-arm-aot" ]]
                  |> Map.ofSeq } ]
            |> Seq.map (fun c -> c.Name, c)
            |> Map.ofSeq
        }

[<Test>]
let ``Check if we can parse runtime rids``() =
    let runtimeGraph = RuntimeGraphParser.readRuntimeGraph rids

    runtimeGraph
    |> shouldEqual
        { Runtimes =
            [ { Rid = Rid.Of "base"; InheritedRids = [ ]; RuntimeDependencies = Map.empty }
              { Rid = Rid.Of "any"; InheritedRids = [ Rid.Of "base" ]; RuntimeDependencies = Map.empty }
              { Rid = Rid.Of "win"; InheritedRids = [ Rid.Of "any" ]; RuntimeDependencies = Map.empty }
              { Rid = Rid.Of "win-x86"; InheritedRids = [ Rid.Of "win" ]; RuntimeDependencies = Map.empty } ]
            |> Seq.map (fun r -> r.Rid, r)
            |> Map.ofSeq
          Supports =
            []
            |> Map.ofSeq
        }

[<Test>]
let ``Check if we can merge two graphs``() =
    let r1 = RuntimeGraphParser.readRuntimeGraph rids
    let r2 = RuntimeGraphParser.readRuntimeGraph supportAndDeps
    let merged = RuntimeGraph.merge r1 r2
    let win = merged.Runtimes.[Rid.Of "win"]
    win.InheritedRids
        |> shouldEqual [ Rid.Of "any" ]
    win.RuntimeDependencies
        |> shouldEqual
             ([ PackageName "Microsoft.Win32.Primitives", [ PackageName "runtime.win.Microsoft.Win32.Primitives", VersionRequirement.VersionRequirement (VersionRange.Minimum (SemVer.Parse "4.3.0"), PreReleaseStatus.No) ]
                PackageName "System.Runtime.Extensions", [ PackageName "runtime.win.System.Runtime.Extensions", VersionRequirement.VersionRequirement (VersionRange.Minimum (SemVer.Parse "4.3.0"), PreReleaseStatus.No) ]
              ] |> Map.ofSeq)

[<Test>]
let ``Check that runtime dependencies are saved as such in the lockfile`` () =
    let lockFileData = """ """
    let getLockFile lockFileData = LockFile.Parse("",toLines lockFileData)
    let lockFile = lockFileData |> getLockFile

    let graph =
        [ "MyDependency", "3.2.0", [], RuntimeGraph.Empty
          "MyDependency", "3.3.3", [], RuntimeGraph.Empty
          "MyDependency", "4.0.0", [], RuntimeGraphParser.readRuntimeGraph """{
  "runtimes": {
    "win": {
      "MyDependency": {
        "MyRuntimeDependency": "4.0.0"
      }
    }
  }
}"""
          "MyRuntimeDependency", "4.0.0", [], RuntimeGraph.Empty
          "MyRuntimeDependency", "4.0.1", [], RuntimeGraph.Empty ]
        |> OfGraphWithRuntimeDeps

    let expectedLockFile = """NUGET
  remote: http://www.nuget.org/api/v2
    MyDependency (4.0)
    MyRuntimeDependency (4.0.1) - isRuntimeDependency: true"""

    let depsFile = DependenciesFile.FromSource("""source http://www.nuget.org/api/v2
nuget MyDependency""")
    let lockFile, resolution =
        UpdateProcess.selectiveUpdate true noSha1 (VersionsFromGraph graph) (PackageDetailsFromGraph graph) (GetRuntimeGraphFromGraph graph) lockFile depsFile PackageResolver.UpdateMode.Install SemVerUpdateMode.NoRestriction

    let result =
        lockFile.GetGroupedResolution()
        |> Seq.map (fun (KeyValue (_,resolved)) -> (string resolved.Name, string resolved.Version, resolved.IsRuntimeDependency))

    let expected =
        [("MyDependency","4.0.0", false);
        ("MyRuntimeDependency","4.0.1", true)]
        |> Seq.sortBy (fun (t,_,_) ->t)

    result
    |> Seq.sortBy (fun (t,_,_) ->t)
    |> shouldEqual expected

    lockFile.GetGroup(Constants.MainDependencyGroup).Resolution
    |> LockFileSerializer.serializePackages depsFile.Groups.[Constants.MainDependencyGroup].Options
    |> shouldEqual (normalizeLineEndings expectedLockFile)

[<Test>]
let ``Check that runtime dependencies we don't use are ignored`` () =
    let lockFileData = """ """
    let getLockFile lockFileData = LockFile.Parse("",toLines lockFileData)
    let lockFile = lockFileData |> getLockFile

    let graph =
        [ "MyDependency", "3.2.0", [], RuntimeGraph.Empty
          "MyDependency", "3.3.3", [], RuntimeGraph.Empty
          "MyDependency", "4.0.0", [], RuntimeGraphParser.readRuntimeGraph """{
  "runtimes": {
    "win": {
      "SomePackage": {
        "MyRuntimeDependency": "4.0.0"
      }
    }
  }
}"""
          "MyRuntimeDependency", "4.0.0", [], RuntimeGraph.Empty
          "MyRuntimeDependency", "4.0.1", [], RuntimeGraph.Empty ]
        |> OfGraphWithRuntimeDeps

    let depsFile = DependenciesFile.FromSource("""source http://www.nuget.org/api/v2
nuget MyDependency""")
    let lockFile, resolution =
        UpdateProcess.selectiveUpdate true noSha1 (VersionsFromGraph graph) (PackageDetailsFromGraph graph) (GetRuntimeGraphFromGraph graph) lockFile depsFile PackageResolver.UpdateMode.Install SemVerUpdateMode.NoRestriction

    let result =
        lockFile.GetGroupedResolution()
        |> Seq.map (fun (KeyValue (_,resolved)) -> (string resolved.Name, string resolved.Version, resolved.IsRuntimeDependency))

    let expected =
        [("MyDependency","4.0.0", false)]
        |> Seq.sortBy (fun (t,_,_) ->t)

    result
    |> Seq.sortBy (fun (t,_,_) ->t)
    |> shouldEqual expected

[<Test>]
let ``Check that runtime dependencies are loaded from the lockfile`` () =
    let lockFile = """NUGET
  remote: http://www.nuget.org/api/v2
    MyDependency (4.0)
    MyRuntimeDependency (4.0.1) - isRuntimeDependency: true"""

    let lockFile = LockFileParser.Parse(toLines lockFile) |> List.head
    let packages = List.rev lockFile.Packages

    let expected =
        [("MyDependency","4.0", false);
        ("MyRuntimeDependency","4.0.1", true)]
        |> List.sortBy (fun (t,_,_) ->t)

    packages
    |> List.map (fun r -> string r.Name, string r.Version, r.IsRuntimeDependency)
    |> List.sortBy (fun (t,_,_) ->t)
    |> shouldEqual expected

    
[<Test>]
let ``Check that runtime inheritance works`` () =
    let runtimeGraph = RuntimeGraphParser.readRuntimeGraph rids
    let content =
        { NuGet.NuGetPackageContent.Path = "/c/test/blub";
          NuGet.NuGetPackageContent.Spec = Nuspec.All
          NuGet.NuGetPackageContent.Content =
            NuGet.ofFiles [
              "lib/netstandard1.1/testpackage.xml"
              "lib/netstandard1.1/testpackage.dll"
              "runtimes/win/lib/netstandard1.1/testpackage.xml"
              "runtimes/win/lib/netstandard1.1/testpackage.dll"
            ]}
    let model =
        InstallModel.EmptyModel (PackageName "testpackage", SemVer.Parse "1.0.0")
        |> InstallModel.addNuGetFiles content
        
    let targetProfile = Paket.TargetProfile.SinglePlatform(Paket.FrameworkIdentifier.DotNetStandard (Paket.DotNetStandardVersion.V1_6))
    model.GetRuntimeAssemblies runtimeGraph (Rid.Of "win-x86") (targetProfile)
    |> Seq.map (fun fi -> fi.Library.PathWithinPackage)
    |> Seq.toList
    |> shouldEqual [ "runtimes/win/lib/netstandard1.1/testpackage.dll" ]
    
[<Test>]
let ``Check that runtime inheritance works (2)`` () =
    let runtimeGraph = runtimeGraphMsNetCorePlatforms2_2_1
    let content =
        { NuGet.NuGetPackageContent.Path = "~/.nuget/packages/System.Runtime.InteropServices.RuntimeInformation";
          NuGet.NuGetPackageContent.Spec = Nuspec.All
          NuGet.NuGetPackageContent.Content =
            NuGet.ofFiles [
              "lib/MonoTouch10/_._"
              "lib/net45/System.Runtime.InteropServices.RuntimeInformation.dll"
              "lib/netstandard1.1/System.Runtime.InteropServices.RuntimeInformation.dll"
              "ref/MonoTouch10/_._"
              "ref/netstandard1.1/System.Runtime.InteropServices.RuntimeInformation.dll"
              "runtimes/aot/lib/netcore50/System.Runtime.InteropServices.RuntimeInformation.dll"
              "runtimes/unix/lib/netstandard1.1/System.Runtime.InteropServices.RuntimeInformation.dll"
              "runtimes/win/lib/net45/System.Runtime.InteropServices.RuntimeInformation.dll"
              "runtimes/win/lib/netcore50/System.Runtime.InteropServices.RuntimeInformation.dll"
              "runtimes/win/lib/netstandard1.1/System.Runtime.InteropServices.RuntimeInformation.dll"
            ]}
    let model =
        InstallModel.EmptyModel (PackageName "System.Runtime.InteropServices.RuntimeInformation", SemVer.Parse "4.3.0")
        |> InstallModel.addNuGetFiles content
        
    let targetProfile = Paket.TargetProfile.SinglePlatform(Paket.FrameworkIdentifier.DotNetStandard (Paket.DotNetStandardVersion.V1_6))
    model.GetRuntimeAssemblies runtimeGraph (Rid.Of "win10-x86") (targetProfile)
    |> Seq.map (fun fi -> fi.Library.PathWithinPackage)
    |> Seq.toList
    |> shouldEqual [ "runtimes/win/lib/netstandard1.1/System.Runtime.InteropServices.RuntimeInformation.dll" ]

[<Test>]
let ``Check correct inheritance list`` () =
    let runtimeGraph = RuntimeGraphParser.readRuntimeGraph rids
    RuntimeGraph.getInheritanceList (Rid.Of "win-x86") runtimeGraph
        |> shouldEqual [ Rid.Of "win-x86"; Rid.Of "win"; Rid.Of "any"; Rid.Of "base"]
    