Set-StrictMode -Version Latest

task . Build, ImportDebug, Test

. "$PSScriptRoot\BuildTools.ps1"

############################################################
# Settings
############################################################

$SolutionFile = "$PSScriptRoot\..\source\Horker.MXNet.sln"

$ScriptFiles = "$PSScriptRoot\..\scripts\*"

$ModulePath = "$PSScriptRoot\..\module\{0}\Horker.MXNet"
$ObjectPath = "$PSScriptRoot\..\source\bin\x64\{0}"
$ObjectFiles = @(
    "$ObjectPath\Accord.dll"
    "$ObjectPath\Accord.Math.dll"
    "$ObjectPath\Accord.Math.Core.dll"
    "$ObjectPath\Accord.Statistics.dll"
    "$ObjectPath\Horker.MXNet.dll"
    "$ObjectPath\Horker.MXNet.pdb"
    "$ObjectPath\Horker.MXNet.PowerShell.dll"
    "$ObjectPath\Horker.MXNet.PowerShell.pdb"
    "$ObjectPath\Horker.Numerics.dll"
    "$ObjectPath\Horker.Numerics.pdb"
    "$ObjectPath\Horker.Numerics.PowerShell.dll"
    "$ObjectPath\Horker.Numerics.PowerShell.pdb"
)

############################################################
# Tasks
############################################################

task Build {
    "Debug", "Release" | foreach {
        $build = $_
        Copy-ObjectFiles $ScriptFiles ($ModulePath -f $build)
        Copy-ObjectFiles ($ObjectFiles | foreach { $_-f $build }) ($ModulePath -f $build)
    }
}

task Test {
    Invoke-Pester "$PSScriptRoot\..\tests"
}

task ImportDebug {
    Import-Module ($ModulePath -f "Debug") -Force
}

task Clean {
    "Debug", "Release" | foreach {
        Remove-Item2 ("$ModulePath\*" -f $_)
    }
}