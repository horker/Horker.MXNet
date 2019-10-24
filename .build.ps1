task . Compile, Build, ImportDebug, Test

Set-StrictMode -Version Latest

############################################################
# Settings
############################################################

$SolutionFile = "$PSScriptRoot\source\Horker.MXNet.sln"

$BuildFiles = @(
    "$PSScriptRoot\build\Horker.MXNet.build.ps1"
    "$PSScriptRoot\build\Horker.Numerics.build.ps1"
)

############################################################
# Tasks
############################################################

task Compile {
    msbuild $SolutionFile /p:Configuration=Debug /p:Platform=x64 /nologo /v:minimal
    msbuild $SolutionFile /p:Configuration=Release /p:Platform=x64 /nologo /v:minimal
}

task Build {
    $BuildFiles | foreach {
        Invoke-Build -File $_ -Task Build
    }
}

task Test {
    $BuildFiles | foreach {
        Invoke-Build -File $_ -Task Test
    }
}

task ImportDebug {
    $BuildFiles | foreach {
        Invoke-Build -File $_ -Task ImportDebug
    }
}

task Clean {
    $BuildFiles | foreach {
        Invoke-Build -File $_ -Task Clean
    }
}

task PublishLocal {
    rm ~\localpsrepo\Horker.MXNet.*.nupkg
    Publish-Module -path .\module\Release\Horker.MXNet\ -Repository LocalPSrepo -NuGetApiKey any
    Install-Module Horker.MXNet -Force
    Update-Module Horker.MXNet -Force
}