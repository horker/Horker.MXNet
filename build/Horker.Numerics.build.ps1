Set-StrictMode -Version Latest

task . Build, ImportDebug, Test

. "$PSScriptRoot\BuildTools.ps1"

############################################################
# Settings
############################################################

$ModuleName = "Horker.Numerics"

$ModulePath = "$PSScriptRoot\..\module\{0}\$ModuleName"

$ScriptFiles = "$PSScriptRoot\..\scripts\$ModuleName\*"

$ObjectPath = "$PSScriptRoot\..\source\Horker.Numerics.LightGBM.Tests\bin\{0}\netcoreapp3.1"
$ObjectPath2 = "$PSScriptRoot\..\source\Horker.Numerics.PowerShell\bin\{0}\netcoreapp3.1"

$ObjectFiles = @(
    "$ObjectPath\Accord.dll"
    "$ObjectPath\Accord.Math.dll"
    "$ObjectPath\Accord.Math.Core.dll"
    "$ObjectPath\Accord.Statistics.dll"
    "$ObjectPath\Accord.MachineLearning.dll"
    "$ObjectPath\XoshiroPRNG.Net.dll"
    "$ObjectPath\Horker.Numerics.dll"
    "$ObjectPath\Horker.Numerics.pdb"
    "$ObjectPath2\Horker.Numerics.PowerShell.dll"
    "$ObjectPath2\Horker.Numerics.PowerShell.pdb"
)
$TestPath = "$PSScriptRoot\..\tests\$ModuleName"
