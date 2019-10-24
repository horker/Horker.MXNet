Set-StrictMode -Version Latest

task . Build, ImportDebug, Test

. "$PSScriptRoot\BuildTools.ps1"

############################################################
# Settings
############################################################

$ModulePath = "$PSScriptRoot\..\module\{0}\Horker.MXNet"

$ScriptFiles = "$PSScriptRoot\..\scripts\Horker.MXNet\*"

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

$TestPath = "$PSScriptRoot\..\tests\Horker.MXNet"