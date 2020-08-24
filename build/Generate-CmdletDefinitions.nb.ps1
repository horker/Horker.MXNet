#!Notebook v1

#!CommandLine
Set-StrictMode -Version Latest
cd $HOME\work\mxnet
#!CommandLine
$SrcPath = "$HOME\work\mxnet.sharp\MxNet.Sharp\src\MxNet"
$OutPath = "$HOME\work\mxnet\source\Horker.MxNet.PowerShell\generated"
#!CommandLine
function Get-MethodSignature([string[]]$DefinitionLines) {
    $sigs = foreach ($def in $DefinitionLines) {
        # スペースと改行をスペース1つに変換
        $d = $def -replace "\s+|\r|\n", " "
#        write-host $d

        # genericsやタプルの中のカンマを一時的に置換する
        # (NDArray, (int, int, int, int))
        $d = $d -replace "([<(][\w\[\]?]+),\s*([<(][\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+[>)]{2})", "`$1**`$2**`$3**`$4**`$5"
        # (NDArray, (int, int, int))
        $d = $d -replace "([<(][\w\[\]?]+),\s*([<(][\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+[>)]{2})", "`$1**`$2**`$3**`$4"
        # (int, int, int, int)
        $d = $d -replace "([<(][\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+[>)])", "`$1**`$2**`$3**`$4"
        # (int, int, int)
        $d = $d -replace "([<(][\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+[>)])", "`$1**`$2**`$3"
        # (int, int)
        $d = $d -replace "([<(][\w\[\]?]+),\s*([\w\[\]?]+[>)])", "`$1**`$2"
#        write-host $d

        $m = ([regex]"(?:([\w<>()\[\]?*]+)\s+)?(\w+)\((.*)\)").Match($d)
        $returnType = $m.Groups[1].Value -replace "\*\*", ", "
        $methodName = $m.Groups[2].Value

        # genericsやタプルの中のカンマを一時的に置換する
        $p = $m.Groups[3].Value #-replace "([<(][\w\[\]?]+),\s*([\w\[\]?]+[>)])", "`$1**`$2"
        #$p = $p -replace "([<(][\w\[\]?]+),\s*([\w\[\]?]+),\s*([\w\[\]?]+[>)])", "`$1**`$2**`$3"
#        write-host $p

        $params = @()
        if (-not [string]::IsNullOrEmpty($p)) {
            $params = $p -split "\s*,\s*" | foreach {
                $m = ([regex]"(?:params\s+)?([\w<>()\[\]?*]+)\s+@?(\w+)(?:\s*=\s*(.+))?").Match($_)
                $paramType = $m.Groups[1].Value -replace "\*\*", ", "
                $paramName = $m.Groups[2].Value
                $defaultValue = $m.Groups[3].Value
            
                [PSCustomObject]@{
                    ParamType = ("Array", "IntPtr") -contains $paramType ? "System." + $paramType : $paramType
                    ParamName = $paramName
                    DefaultValue = $defaultValue
                }
            }
        }

        [PSCustomObject]@{
            ReturnType = $returnType
            MethodName = $methodName
            Params = $params
            SourceLine = $def
            AltMethodName = $methodName
        }
    }

    # 重複メソッド名は末尾に数値を付ける
    $nameCache = @{}
    foreach ($sig in $sigs) {
        $m = $sig.AltMethodName
        $suffix = 1
        while ($nameCache.Contains($m)) {
            ++$suffix
            if ($m -match "\d+$") {
                $m = $m -replace "\d+$", $suffix
            }
            else {
                $m = $m + $suffix
            }
            $sig.AltMethodName = $m
        }

        $nameCache[$m] = $true
    }

    $sigs
}
#!CommandLine
$s = Get-MethodSignature "        public static (NDArray, (int, int, int, int)) CenterCrop(NDArray src, (int, int) size,
            InterpolationFlags interp = InterpolationFlags.Area)"
$s
#!CommandLine
function Get-MethodDefinitionLine([string]$InFile) {
    $doc = Get-Content $InFile -Raw
    $matchCollection = ([regex]"(?m)^\s+public (?:static )?(?!(?:(?:partial )?class|enum))([^{]+)\r\n\s+{").Matches($doc)
    foreach ($m in $matchCollection) {
        $m.Groups[1].Value -replace "\s+|\r|\n", " "
    }
}
#!CommandLine
Get-MethodDefinitionLine $SrcPath\Image\Img.cs
#!CommandLine
function Get-ConstructorDefinitionLine([string]$InFile, [string]$ClassName) {
    $doc = Get-Content $InFile -Raw
    $matchCollection = ([regex]"(?m)^\s+public ($ClassName\([^:{]+\))").Matches($doc)
    foreach ($m in $matchCollection) {
        $m.Groups[1].Value -replace "`r`n", " "
    }
}
#!CommandLine
Get-ConstructorDefinitionLine C:\Users\msumi\work\mxnet.sharp\MxNet.Sharp\src\MxNet\Gluon\NN\BaseLayers\BatchNorm.cs BatchNorm
#!CommandLine
$TextInfo = [System.Globalization.CultureInfo]::CurrentCulture.TextInfo
function ConvertTo-PascalCase([string]$snakeCase) {
    ($snakeCase -split "_" | foreach { $TextInfo.ToTitleCase($_) }) -join ""
}
#!CommandLine
$ParamTemplate = @"
        [Parameter(Position = {0}, Mandatory = {1})]
        public {2} {3} {{ get; set; }}{4}
"@
#!CommandLine
function Get-ParamList($Params) {
    for ($i = 0; $i -lt $Params.Length; ++$i) {
        $p = $Params[$i]
        $ParamTemplate -f
            $i,
            ([string]::IsNullOrEmpty($p.DefaultValue) ? "true" : "false"),
            $p.ParamType,
            (ConvertTo-PascalCase $p.ParamName),
            ([string]::IsNullOrEmpty($p.DefaultValue) ? "" : " = $($p.DefaultValue);")
    }
}
#!CommandLine
Get-ParamList @()
#!CommandLine
$CmdletTemplate = @"

    [Cmdlet("{0}", "Mx{1}")]
    [Alias("mx.{2}.{3}")]
    [OutputType(typeof({4}))]
    public class {5}Mx{6} : PSCmdlet
    {{
{7}

        protected override void BeginProcessing()
        {{
            {8};
        }}
    }}
"@
#!CommandLine
function Get-CmdletDefinition($Signatures, [string]$Verb, [string]$Subcategory, [string]$Prefix, [string]$Postfix, [string]$Namespace) {
    foreach ($sig in $Signatures) {
        $invocation = "$Namespace" + "." + $sig.MethodName + "(" +
            (($sig.Params | foreach { ConvertTo-PascalCase $_.ParamName }) -join ", ") +
            ")"
        if ($sig.ReturnType -ne "void") {
            $invocation = "WriteObject($invocation)"
        }
        else {
            if ($Verb -eq "Get") {
                $Verb = "Set"
            }
        }

        $CmdletTemplate -f
            $Verb,
            ($Prefix + $sig.AltMethodName + $Postfix),
            $Subcategory,
            $sig.AltMethodName,
            ([string]::IsNullOrEmpty($sig.ReturnType) ? "void" : $sig.ReturnType) ,
            $Verb,
            ($Prefix + $sig.AltMethodName + $Postfix),
            ((Get-ParamList $sig.Params) -join "`r`n`r`n"),
            $invocation
    }
}            
#!CommandLine
$FileTemplate = @"
using System.Collections.Generic;
using System.Management.Automation;
using MxNet;
using MxNet.Initializers;
using MxNet.Optimizers;
using MxNet.Image;
using MxNet.Gluon;
using MxNet.Gluon.RNN;
using OpenCvSharp;
using NumpyDotNet;

namespace Horker.MxNet.PowerShell
{{{0}
}}
"@
#!CommandLine
function Get-ClassFile([string[]]$CmdletDefinitions) {
    $FileTemplate -f ($CmdletDefinitions -join "`r`n")
}
#!CommandLine
function Get-MethodWrapper([string]$InFile, [string]$Subcategory, [string]$Prefix, [string]$Postfix, [string]$Namespace, [string]$OutFile) {
    $def = Get-MethodDefinitionLine $InFile
    $sig = Get-MethodSignature $def
    $cd = Get-CmdletDefinition $sig "Get" $Subcategory $Prefix $Postfix $Namespace
    $doc = Get-ClassFile $cd
    $doc | Set-Content $OutFile
}
#!CommandLine
function Get-ConstructorWrapper($SpecList, [string]$OutFile) {
    $cd = foreach ($c in $SpecList) {
        $InFile = "$SrcPath\$($c[0])"
        $ClassName = $c[1]
        $Subcategory = $c[2]
        $Prefix = $c[3]
        $Postfix = $c[4]
        $Namespace = $c[5]
        Write-Host $InFile " " $ClassName
        $def = Get-ConstructorDefinitionLine $InFile $ClassName
        $sigs = Get-MethodSignature $def
        foreach ($sig in $sigs) {
            $sig.ReturnType = ($Namespace -replace "new ", "") + "." + $ClassName
            Get-CmdletDefinition $sig "New" $Subcategory $Prefix $Postfix $Namespace
        }
    }

    $doc = Get-ClassFile $cd
    $doc | Set-Content $OutFile
}
#!CommandLine
# Symbol operators
Get-MethodWrapper $SrcPath\Sym\Ops.cs "sym" "" "Symbol" "global::MxNet.sym" $OutPath\SymbolOpsCmdlets.cs
Get-MethodWrapper $SrcPath\Sym\ContribApi.cs "sym.contrib" "" "Symbol" "new global::MxNet.SymContribApi()" $OutPath\SymbolContribApiCmdlets.cs
Get-MethodWrapper $SrcPath\Sym\ImgApi.cs "sym.img" "Img" "Symbol" "new global::MxNet.SymImgApi()" $OutPath\SymbolImgApiCmdlets.cs
#Get-MethodWrapper $SrcPath\Sym\Random.cs "nd.random" "Random" "Symbol" "global::MxNet.sym.Random" $OutPath\SymbolRandomCmdlets.cs

# NDArray operators
Get-MethodWrapper $SrcPath\NDArray\Ops.cs "nd" "" "NDArray" "global::MxNet.nd" $OutPath\NDArrayOpsCmdlets.cs
Get-MethodWrapper $SrcPath\NDArray\NdContribApi.cs "nd.contrib" "" "NDArray" "new global::MxNet.NDContribApi()" $OutPath\NDArrayContribApiCmdlets.cs
Get-MethodWrapper $SrcPath\NDArray\NdImgApi.cs "nd.img" "Img" "NDArray" "new global::MxNet.NDImgApi()" $OutPath\NDArrayImgApiCmdlets.cs
Get-MethodWrapper $SrcPath\NDArray\Random.cs "nd.random" "Random" "NDArray" "global::MxNet.nd.Random" $OutPath\NDArrayRandomCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Optimizers\AdaDelta.cs", "AdaDelta", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\Adagrad.cs", "AdaGrad", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\Adam.cs", "Adam", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\Adamax.cs", "Adamax", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\DCASGD.cs", "DCASGD", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\Ftlr.cs", "Ftlr", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\FTML.cs", "FTML", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\LBSGD.cs", "LBSGD", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\Nadam.cs", "Nadam", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\NAG.cs", "NAG", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\RMSProp.cs", "RMSProp", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\SGD.cs", "SGD", "optimizer", "", "", "new global::MxNet.Optimizers"),
    @("Optimizers\Signum.cs", "Signum", "optimizer", "", "", "new global::MxNet.Optimizers")
)
Get-ConstructorWrapper $Constructors $OutPath\OptimizerCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Metrics\Accuracy.cs", "Accuracy", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\BinaryAccuracy.cs", "BinaryAccuracy", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\CrossEntropy.cs", "CrossEntropy", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\F1.cs", "F1", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\MAE.cs", "MAE", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\MCC.cs", "MCC", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\MSE.cs", "MSE", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\NegativeLogLikelihood.cs", "NegativeLogLikelihood", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\PCC.cs", "PCC", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\PearsonCorrelation.cs", "PearsonCorrelation", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\Perplexity.cs", "Perplexity", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\RMSE.cs", "RMSE", "metric", "Metric", "", "new global::MxNet.Metrics"),
    @("Metrics\TopKAccuracy.cs", "TopKAccuracy", "metric", "Metric", "", "new global::MxNet.Metrics")
)
Get-ConstructorWrapper $Constructors $OutPath\MetricCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Schedulers\CosineScheduler.cs", "CosineScheduler", "scheduler", "", "", "new global::MxNet.Schedulers"),
    @("Schedulers\FactorScheduler.cs", "FactorScheduler", "scheduler", "", "", "new global::MxNet"),
    @("Schedulers\MultiFactorScheduler.cs", "MultiFactorScheduler", "scheduler", "", "", "new global::MxNet.Schedulers"),
    @("Schedulers\PolyScheduler.cs", "PolyScheduler", "scheduler", "", "", "new global::MxNet.Schedulers")
)
Get-ConstructorWrapper $Constructors $OutPath\SchedulerCmdlets.cs
#!CommandLine
# TODO: Bilinear, One, Zero
$Constructors = @(
    @("Initializers\Constant.cs", "Constant", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\LSTMBias.cs", "LSTMBias", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\Mixed.cs", "Mixed", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\MSRAPrelu.cs", "MSRAPrelu", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\Normal.cs", "Normal", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\Orthogonal.cs", "Orthogonal", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\Uniform.cs", "Uniform", "initializer", "", "Initializer", "new global::MxNet.Initializers"),
    @("Initializers\Xavier.cs", "Xavier", "initializer", "", "Initializer", "new global::MxNet.Initializers")
)
Get-ConstructorWrapper $Constructors $OutPath\InitializerCmdlets.cs
#!CommandLine
$Constructors = @(
    ,@("IO\NDArrayIter.cs", "NDArrayIter", "io", "", "", "new global::MxNet.IO")
)
Get-ConstructorWrapper $Constructors $OutPath\IOCmdlets.cs
#!CommandLine
Get-MethodWrapper $SrcPath\Image\Img.cs "image" "Image" "" "global::MxNet.Image.Img" $OutPath\ImageImgCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Image\BrightnessJitterAug.cs", "BrightnessJitterAug", "image", "Image", "", "new global::MxNet.Image"),
    @("Image\CastAug.cs", "CastAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\CenterCropAug.cs", "CenterCropJitterAug", "image", "Image", "", "new global::MxNet.Image"),
    @("Image\ColorJitterAug.cs", "ColorJitterAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\ColorNormalizeAug.cs", "ColorNormalizeAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\ContrastJitterAug.cs", "ContrastJitterAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\ForceResizeAug.cs", "ForceResizeAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\HorizontalFlipAug.cs", "HorizontalFlipAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\HueJitterAug.cs", "HueJitterAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\ImageIter.cs", "ImageIter", "image", "", "", "new global::MxNet.Image"),
    @("Image\LightingAug.cs", "LightingAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\RandomCropAug.cs", "RandomCropAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\RandomGrayAug.cs", "RandomGrayAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\RandomOrderAug.cs", "RandomOrderAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\RandomSizedCropAug.cs", "RandomSizedCropAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\ResizeAug.cs", "ResizeAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\SaturationJitterAug.cs", "SaturationJitterAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\SequentialAug.cs", "SequentialAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\Detection\DetBorrowAug.cs", "DetBorrowAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\Detection\DetHorizontalFlipAug.cs", "DetHorizontalFlipAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\Detection\DetRandomCropAug.cs", "DetRandomCropAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\Detection\DetRandomPadAug.cs", "DetRandomPadAug", "image", "", "", "new global::MxNet.Image"),
    @("Image\Detection\DetRandomSelectAug.cs", "DetRandomSelectAug", "image", "", "", "new global::MxNet.Image")
)
Get-ConstructorWrapper $Constructors $OutPath\ImageCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Gluon\Losses\CosineEmbeddingLoss.cs", "CosineEmbeddingLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\CTCLoss.cs", "CTCLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\HingeLoss.cs", "HingeLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\HuberLoss.cs", "HuberLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\KLDivLoss.cs", "KLDivLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\L1Loss.cs", "L1Loss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\L2Loss.cs", "L2Loss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\LogisticLoss.cs", "LogisticLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\PoissonNLLLoss.cs", "PoissonNLLLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\SigmoidBinaryCrossEntropyLoss.cs", "SigmoidBinaryCrossEntropyLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\SoftmaxCrossEntropyLoss.cs", "SoftmaxCrossEntropyLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\SquaredHingeLoss.cs", "SquaredHingeLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses"),
    @("Gluon\Losses\TripletLoss.cs", "TripletLoss", "gluon.loss", "Gluon", "", "new global::MxNet.Gluon.Losses")
)
Get-ConstructorWrapper $Constructors $OutPath\GluonLossCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Gluon\NN\Activations\ELU.cs", "ELU", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\Activations\GELU.cs", "GELU", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\Activations\LeakyReLU.cs", "LeakyReLU", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\Activations\PReLU.cs", "PReLU", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\Activations\SELU.cs", "SELU", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\Activations\Swish.cs", "Swish", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN")
)
Get-ConstructorWrapper $Constructors $OutPath\GluonNNActivationCmdlets.cs
#!CommandLine
# TODO: Lambda, HybridLambda
$Constructors = @(
    @("Gluon\NN\BaseLayers\BatchNorm.cs", "BatchNorm", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\Dense.cs", "Dense", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\Dropout.cs", "Dropout", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\Embedding.cs", "Embedding", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\Flatten.cs", "Flatten", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
#    @("Gluon\NN\BaseLayers\HybridLambda.cs", "HybridLambda", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\HybridSequential.cs", "HybridSequential", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN")
    @("Gluon\NN\BaseLayers\InstanceNorm.cs", "InstanceNorm", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
#    @("Gluon\NN\BaseLayers\Lambda.cs", "Lambda", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\LayerNorm.cs", "LayerNorm", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\BaseLayers\Sequential.cs", "Sequential", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN")
)
Get-ConstructorWrapper $Constructors $OutPath\GluonNNBaseLayerCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Gluon\NN\ConvLayers\AvgPool1D.cs", "AvgPool1D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\AvgPool2D.cs", "AvgPool2D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\AvgPool3D.cs", "AvgPool3D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\Conv1D.cs", "Conv1D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\Conv1DTranspose.cs", "Conv1DTranspose", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\Conv2D.cs", "Conv2D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\Conv2DTranspose.cs", "Conv2DTranspose", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\Conv3D.cs", "Conv3D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\Conv3DTranspose.cs", "Conv3DTranspose", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\GlobalAvgPool1D.cs", "GlobalAvgPool1D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\GlobalAvgPool2D.cs", "GlobalAvgPool2D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\GlobalAvgPool3D.cs", "GlobalAvgPool3D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\GlobalMaxPool1D.cs", "GlobalMaxPool1D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\GlobalMaxPool2D.cs", "GlobalMaxPool2D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\GlobalMaxPool3D.cs", "GlobalMaxPool3D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\MaxPool1D.cs", "MaxPool1D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\MaxPool2D.cs", "MaxPool2D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\MaxPool3D.cs", "MaxPool3D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN"),
    @("Gluon\NN\ConvLayers\ReflectionPad2D.cs", "ReflectionPad2D", "gluon.nn", "Gluon", "", "new global::MxNet.Gluon.NN")
)
Get-ConstructorWrapper $Constructors $OutPath\GluonNNConvLayerCmdlets.cs
#!CommandLine
$Constructors = @(
    @("Gluon\RNN\Cell\BidirectionalCell.cs", "BidirectionalCell", "gluon.rnn", "GluonRNN", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\DropoutCell.cs", "DropoutCell", "gluon.rnn", "GluonRNN", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\GRUCell.cs", "GRUCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\HybridSequentialRNNCell.cs", "HybridSequentialRNNCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\LSTMCell.cs", "LSTMCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\ResidualCell.cs", "ResidualCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\RnnCell.cs", "RnnCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\SequentialRNNCell.cs", "SequentialRNNCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN"),
    @("Gluon\RNN\Cell\ZoneoutCell.cs", "ZoneoutCell", "gluon.rnn", "Gluon", "", "new global::MxNet.Gluon.RNN")
)
Get-ConstructorWrapper $Constructors $OutPath\GluonRNNCellCmdlets.cs
#!CommandLine
$Constructors = @(
    ,@("Gluon\Trainer.cs", "Trainer", "gluon", "", "", "new global::MxNet.Gluon")
)
Get-ConstructorWrapper $Constructors $OutPath\GluonTrainerCmdlets.cs
#!CommandLine
$sig
#!CommandLine
