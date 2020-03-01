Set-StrictMode -Version Latest

task . Build, ImportDebug, Test

. "$PSScriptRoot\BuildTools.ps1"

############################################################
# Settings
############################################################

$ModuleName = "Horker.Numerics.LightGBM"

$ModulePath = "$PSScriptRoot\..\module\{0}\$ModuleName"

$ScriptFiles = "$PSScriptRoot\..\scripts\$ModuleName\*"

$ObjectPath = "$PSScriptRoot\..\source\Horker.Numerics.LightGBM.Tests\bin\Release\netcoreapp3.1"

$ObjectFiles = @(
    "$ObjectPath\Horker.Numerics.LightGBM.dll"
    "$ObjectPath\Horker.Numerics.LightGBM.pdb"
)

$LibPath = "$PSScriptRoot\..\lib\lightgbm"

$LibFiles = @(
    "$LibPath\LightGBMNet.Train.dll"
    "$LibPath\LightGBMNet.Train.pdb"
    "$LibPath\LightGBMNet.Tree.dll"
    "$LibPath\LightGBMNet.Tree.pdb"
)

$LibX64Path = "$PSScriptRoot\..\lib\lightgbm\x64"

$LibX64Files = @(
    "$LibX64Path\lib_lightgbm.dll"
    "$LibX64Path\lightgbm.exe"
)

$TestPath = "$PSScriptRoot\..\tests\$ModuleName"

############################################################
# Tasks
############################################################

# LightGBM specific task to copy native libraries
task Build {
    "Debug", "Release" | foreach {
        $build = $_
        Copy-ObjectFiles $ScriptFiles ($ModulePath -f $build)
        Copy-ObjectFiles ($ObjectFiles | foreach { $_-f $build }) ($ModulePath -f $build)
        Copy-ObjectFiles $LibFiles ($ModulePath -f $build)
        Copy-ObjectFiles $LibX64Files (Join-Path ($ModulePath -f $build) "x64")
    }
}
