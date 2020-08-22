Set-StrictMode -Version Latest

task . Build, ImportDebug, Test

. "$PSScriptRoot\BuildTools.ps1"

############################################################
# Settings
############################################################

$ModuleName = "Horker.MxNet"

$ModulePath = "$PSScriptRoot\..\module\{0}\$ModuleName"

$ScriptFiles = "$PSScriptRoot\..\scripts\$ModuleName\*"

$ObjectPath = "$PSScriptRoot\..\source\Horker.MxNet.PowerShell.Tests\bin\x64\{0}\netcoreapp3.1"

$ObjectFiles = @(
    "$ObjectPath\CsvHelper.dll"
    "$ObjectPath\MxNet.dll"
    "$ObjectPath\NumpyDotNet.dll"
    "$ObjectPath\NumpyLib.dll"
    "$ObjectPath\OpenCvSharp.Blob.dll"
    "$ObjectPath\OpenCvSharp.dll"
    "$ObjectPath\OpenCvSharp.Extensions.dll"
    "$ObjectPath\Horker.MxNet.PowerShell.dll"
    "$ObjectPath\Horker.MxNet.PowerShell.pdb"
)

$RuntimePath = "$PSScriptRoot\..\source\Horker.MxNet.PowerShell.Tests\bin\x64\Debug\netcoreapp3.1\runtimes\win-x64\native"

$RuntimeFiles = "$RuntimePath\*.dll"

$TestPath = "$PSScriptRoot\..\tests\$ModuleName"
