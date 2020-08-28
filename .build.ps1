task . Compile, Build, ImportDebug, Test

Set-StrictMode -Version Latest

############################################################
# Settings
############################################################

$LocalRepoPath = "$HOME\localpsrepo"
$LocalRepoName = "LocalPSRepo"

$SolutionFile = "$PSScriptRoot\source\Horker.MXNet.sln"

$BuildFiles = @(
    "$PSScriptRoot\build\Horker.MXNet.build.ps1"
    "$PSScriptRoot\build\Horker.Numerics.build.ps1"
    "$PSScriptRoot\build\Horker.Numerics.LightGBM.build.ps1"
)

############################################################
# Tasks
############################################################

task Compile {
    dotnet build $SolutionFile -c Debug -r x64 -v minimal -nologo
    dotnet build $SolutionFile -c Release -r x64 -v minimal -nologo
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
    $BuildFiles | foreach {
        Invoke-Build -File $_ -Task PublishLocal
    }
}
