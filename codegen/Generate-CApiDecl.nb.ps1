#!Notebook v1

#!CommandLine
Set-StrictMode -Version Latest
#!Output
# 
#!CommandLine
cd $HOME\work\mxnet\codegen
#!Output
# 
#!CommandLine
$typeMap = Import-Csv -enc utf8 api_type_map.csv
#!Output
# 
#!CommandLine
$map = $typeMap | group Name
#!Output
# 
#!CommandLine
#!Output
# 
#!CommandLine
###################################################################
# Templates for declaration file
###################################################################

$declHeader = @"
using System;
using System.Runtime.InteropServices;

using AtomicSymbolCreator = System.IntPtr;
using DataIterCreator = System.IntPtr;
using DataIterHandle = System.IntPtr;
using ExecutorHandle = System.IntPtr;
using FunctionHandle = System.IntPtr;
using NDArrayHandle = System.IntPtr;
using ProfileHandle = System.IntPtr;
using SymbolHandle = System.IntPtr;
using size_t = System.Int64;
using int64_t = System.Int64;
using uint64_t = System.Int64;
using mx_int = System.Int32;
using mx_uint = System.Int32;
using mx_int64 = System.Int64;
using mx_uint64 = System.Int64;
using mx_float = System.Single;
using ExecutorMonitorCallback = System.IntPtr;
using KVStoreHandle = System.IntPtr;
using MXKVStoreServerController = System.IntPtr;
using RecordIOHandle = System.IntPtr;
using RtcHandle = System.IntPtr;
using CustomOpPropCreator = System.IntPtr;
using CudaModuleHandle = System.IntPtr;
using CudaKernelHandle = System.IntPtr;
using EngineAsyncFunc = System.IntPtr;
using EngineFuncParamDeleter = System.IntPtr;
using ContexHandle = System.IntPtr;
using EngineVarHandle = System.IntPtr;
using EngineFnPropertyHandle = System.IntPtr;
using EngineSyncFunc = System.IntPtr;
using ContextHandle = System.IntPtr;
using DLManagedTensorHandle = System.IntPtr;
using CachedOpHandle = System.IntPtr;
using MXKVStoreUpdater = System.IntPtr;
using MXKVStoreStrUpdater = System.IntPtr;

namespace Horker.MXNet.Core
{
    public static class CApiDeclaration
    {
"@

$declFooter = @"
    }
}
"@

$declTemplate = @"
        {0}
        [DllImport("libmxnet.dll")]
        public static extern {1} {2}( // {3}
        {4}
        );

"@
#!Output
# 
#!CommandLine
###################################################################
# Templates for wrapper file
###################################################################

$wrapperHeader = @"
using System;
using System.Runtime.InteropServices;

using AtomicSymbolCreator = System.IntPtr;
using DataIterCreator = System.IntPtr;
using DataIterHandle = System.IntPtr;
using ExecutorHandle = System.IntPtr;
using FunctionHandle = System.IntPtr;
using NDArrayHandle = System.IntPtr;
using ProfileHandle = System.IntPtr;
using SymbolHandle = System.IntPtr;
using size_t = System.Int64;
using int64_t = System.Int64;
using uint64_t = System.Int64;
using mx_int = System.Int32;
using mx_uint = System.Int32;
using mx_int64 = System.Int64;
using mx_uint64 = System.Int64;
using mx_float = System.Single;
using ExecutorMonitorCallback = System.IntPtr;
using KVStoreHandle = System.IntPtr;
using MXKVStoreServerController = System.IntPtr;
using RecordIOHandle = System.IntPtr;
using RtcHandle = System.IntPtr;
using CustomOpPropCreator = System.IntPtr;
using CudaModuleHandle = System.IntPtr;
using CudaKernelHandle = System.IntPtr;
using EngineAsyncFunc = System.IntPtr;
using EngineFuncParamDeleter = System.IntPtr;
using ContexHandle = System.IntPtr;
using EngineVarHandle = System.IntPtr;
using EngineFnPropertyHandle = System.IntPtr;
using EngineSyncFunc = System.IntPtr;
using ContextHandle = System.IntPtr;
using DLManagedTensorHandle = System.IntPtr;
using CachedOpHandle = System.IntPtr;
using MXKVStoreUpdater = System.IntPtr;
using MXKVStoreStrUpdater = System.IntPtr;

namespace Horker.MXNet.Core
{
    public static class CApi
    {
"@

$wrapperFooter = @"
    }
}
"@

$wrapperTemplate = @"
        {0}
        public static void {1}(
        {2}
        )
        {{
            var resultCode = CApiDeclaration.{3}({4});
            if (resultCode != 0)
            {{
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }}
        }}

"@
#!Output
# 
#!CommandLine
###################################################################
# Get signature
###################################################################

function Get-Signature {
    param(
        $func
    )

    if ([string]::IsNullOrEmpty($func[0].ParamName)) {
        return ""
    }

    $signature = ($func | foreach {
        # Type name
        if ([string]::IsNullOrEmpty($_.infer_prefix)) {
            $paramType = $_.infer_ClrType
        }
        else {
            $paramType = $_.infer_prefix + " " + $_.infer_ClrType
        }

        # Parameter name
        if ($_.ParamName -eq "out") {
            $paramName = "@out"
        }
        else {
            $paramName = $_.ParamName
        }

        "    {0,-20} {1,-20} // {2}" -f
            $paramType, ($paramName + ","), $_.ParamType
    }) -join "`r`n        "
    $signature = $signature -Replace ",([^,]+$)", " `$1"

    $signature
}
#!Output
# 
#!CommandLine
###################################################################
# Generate function declaration file
###################################################################

function Get-FunctionDeclaration {
    param(
        $func
    )
    $document = "/// func"
    $returnType = $func[0].ReturnType
    $name = $func[0].Name
    $signature = Get-Signature $func

    $declTemplate -f $document, $returnType, $name, $returnType, $signature
}
#!Output
# 
#!CommandLine
function Get-DeclarationFile {
    $declHeader
    $map | foreach {
        Get-FunctionDeclaration $_.Group
    }
    $declFooter
}
#!Output
# 
#!CommandLine
###################################################################
# Generate wrapper function file
###################################################################

function Get-WrapperFunction {
    param(
        $func
    )

    $document = "/// func"
    $name = $func[0].Name
    $signature = Get-Signature $func

    $arguments = ($func | foreach {
        # Parameter name
        if ($_.ParamName -eq "out") {
            $paramName = "@out"
        }
        else {
            $paramName = $_.ParamName
        }

        if ([string]::IsNullOrEmpty($_.infer_prefix)) {
            $arg = $paramName
        }
        else {
            $arg = $_.infer_prefix + " " + $paramName
        }
        $arg
    }) -join ", "

    $wrapperTemplate -f $document, $name, $signature, $name, $arguments
}

#!Output
# 
#!CommandLine
function Get-WrapperFile {
    $wrapperHeader
    $map | foreach {
        if ($_.Name -ne "MXGetLastError") {
            Get-WrapperFunction $_.Group
        }
    }
    $wrapperFooter
}
#!Output
# 
#!CommandLine
###################################################################
# Run
###################################################################

Get-DeclarationFile | Set-Content -enc utf8 ..\source\Horker.MXNet\Generated\CApiDeclaration.cs
#!Output
# 
#!CommandLine
###################################################################
# Run
###################################################################

Get-WrapperFile | Set-Content -enc utf8 ..\source\Horker.MXNet\Generated\CApi.cs
#!Output
# 
#!CommandLine
#!Output
# 
