Set-StrictMode -Version Latest

task . Build, ImportDebug, Test

. "$PSScriptRoot\BuildTools.ps1"

############################################################
# Settings
############################################################

$ModuleName = "Horker.Numerics"

$ModulePath = "$PSScriptRoot\..\module\{0}\$ModuleName"

$ScriptFiles = "$PSScriptRoot\..\scripts\$ModuleName\*"

$ObjectPath = "$PSScriptRoot\..\source\Horker.Numerics.LightGBM.Tests\bin\Release\netcoreapp3.1"

$ObjectFiles = @(
    "$ObjectPath\Accord.dll"
    "$ObjectPath\Accord.Math.dll"
    "$ObjectPath\Accord.Math.Core.dll"
    "$ObjectPath\Accord.Statistics.dll"
    "$ObjectPath\Accord.MachineLearning.dll"
    "$ObjectPath\Horker.Numerics.dll"
    "$ObjectPath\Horker.Numerics.pdb"
    "$ObjectPath\Horker.Numerics.PowerShell.dll"
    "$ObjectPath\Horker.Numerics.PowerShell.pdb"
)
$TestPath = "$PSScriptRoot\..\tests\$ModuleName"
