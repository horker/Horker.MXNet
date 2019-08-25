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
        /// func
        [DllImport("libmxnet.dll")]
        public static extern string MXGetLastError( // string
        
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXLoadLib( // int
            string               path                 // const char*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXLibInfoFeatures( // int
            IntPtr               libFeature,          // const struct LibFeature**
            IntPtr               size                 // size_t*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRandomSeed( // int
            int                  seed                 // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRandomSeedContext( // int
            int                  seed,                // int
            int                  dev_type,            // int
            int                  dev_id               // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNotifyShutdown( // int
        
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetProcessProfilerConfig( // int
            int                  num_params,          // int
            IntPtr               keys,                // const char*const*
            IntPtr               vals,                // const char*const*
            KVStoreHandle        kvstoreHandle        // KVStoreHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetProfilerConfig( // int
            int                  num_params,          // int
            IntPtr               keys,                // const char*const*
            IntPtr               vals                 // const char*const*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetProcessProfilerState( // int
            int                  state,               // int
            int                  profile_process,     // int
            KVStoreHandle        kvStoreHandle        // KVStoreHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetProfilerState( // int
            int                  state                // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDumpProcessProfile( // int
            int                  finished,            // int
            int                  profile_process,     // int
            KVStoreHandle        kvStoreHandle        // KVStoreHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDumpProfile( // int
            int                  finished             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAggregateProfileStatsPrint( // int
            out IntPtr           out_str,             // const char**
            int                  reset                // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAggregateProfileStatsPrintEx( // int
            out IntPtr           out_str,             // const char**
            int                  reset,               // int
            int                  format,              // int
            int                  sort_by,             // int
            int                  ascending            // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProcessProfilePause( // int
            int                  paused,              // int
            int                  profile_process,     // int
            KVStoreHandle        kvStoreHandle        // KVStoreHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfilePause( // int
            int                  paused               // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileCreateDomain( // int
            string               domain,              // const char*
            out ProfileHandle    @out                 // ProfileHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileCreateTask( // int
            ProfileHandle        domain,              // ProfileHandle
            string               task_name,           // const char*
            out ProfileHandle    @out                 // ProfileHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileCreateFrame( // int
            ProfileHandle        domain,              // ProfileHandle
            string               frame_name,          // const char*
            out ProfileHandle    @out                 // ProfileHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileCreateEvent( // int
            string               event_name,          // const char*
            out ProfileHandle    @out                 // ProfileHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileCreateCounter( // int
            ProfileHandle        domain,              // ProfileHandle
            string               counter_name,        // const char*
            out ProfileHandle    @out                 // ProfileHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileDestroyHandle( // int
            ProfileHandle        frame_handle         // ProfileHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileDurationStart( // int
            ProfileHandle        duration_handle      // ProfileHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileDurationStop( // int
            ProfileHandle        duration_handle      // ProfileHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileSetCounter( // int
            ProfileHandle        counter_handle,      // ProfileHandle
            uint64_t             value                // uint64_t
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileAdjustCounter( // int
            ProfileHandle        counter_handle,      // ProfileHandle
            int64_t              value                // int64_t
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXProfileSetMarker( // int
            ProfileHandle        domain,              // ProfileHandle
            string               instant_marker_name, // const char*
            string               scope                // const char*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetNumOMPThreads( // int
            int                  thread_num           // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXEngineSetBulkSize( // int
            int                  bulk_size,           // int
            IntPtr               prev_bulk_size       // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGetGPUCount( // int
            out int              @out                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGetGPUMemoryInformation( // int
            int                  dev,                 // int
            out int              free_mem,            // int*
            out int              total_mem            // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGetGPUMemoryInformation64( // int
            int                  dev,                 // int
            out uint64_t         free_mem,            // uint64_t*
            out uint64_t         total_mem            // uint64_t*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGetVersion( // int
            out int              @out                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateNone( // int
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreate( // int
            mx_uint[]            shape,               // const mx_uint*
            mx_uint              ndim,                // mx_uint
            int                  dev_type,            // int
            int                  dev_id,              // int
            int                  delay_alloc,         // int
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateEx( // int
            mx_uint[]            shape,               // const mx_uint*
            mx_uint              ndim,                // mx_uint
            int                  dev_type,            // int
            int                  dev_id,              // int
            int                  delay_alloc,         // int
            int                  dtype,               // int
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateEx64( // int
            mx_int64[]           shape,               // const mx_int64*
            int                  ndim,                // int
            int                  dev_type,            // int
            int                  dev_id,              // int
            int                  delay_alloc,         // int
            int                  dtype,               // int
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateSparseEx( // int
            int                  storage_type,        // int
            mx_uint[]            shape,               // const mx_uint*
            mx_uint              ndim,                // mx_uint
            int                  dev_type,            // int
            int                  dev_id,              // int
            int                  delay_alloc,         // int
            int                  dtype,               // int
            mx_uint              num_aux,             // mx_uint
            IntPtr               aux_type,            // int*
            IntPtr               aux_ndims,           // mx_uint*
            mx_uint[]            aux_shape,           // const mx_uint*
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateSparseEx64( // int
            int                  storage_type,        // int
            mx_int64[]           shape,               // const mx_int64*
            int                  ndim,                // int
            int                  dev_type,            // int
            int                  dev_id,              // int
            int                  delay_alloc,         // int
            int                  dtype,               // int
            mx_uint              num_aux,             // mx_uint
            IntPtr               aux_type,            // int*
            IntPtr               aux_ndims,           // int*
            mx_int64[]           aux_shape,           // const mx_int64*
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayLoadFromRawBytes( // int
            IntPtr               buf,                 // const void*
            size_t               size,                // size_t
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySaveRawBytes( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out size_t           out_size,            // size_t*
            out IntPtr           out_buf              // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySave( // int
            string               fname,               // const char*
            mx_uint              num_args,            // mx_uint
            NDArrayHandle[]      args,                // NDArrayHandle*
            string[]             keys                 // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayLoad( // int
            string               fname,               // const char*
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_arr,             // NDArrayHandle**
            out mx_uint          out_name_size,       // mx_uint*
            out IntPtr           out_names            // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayLoad64( // int
            string               fname,               // const char*
            out mx_int64         out_size,            // mx_int64*
            out IntPtr           out_arr,             // NDArrayHandle**
            out mx_int64         out_name_size,       // mx_int64*
            out IntPtr           out_names            // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayLoadFromBuffer( // int
            IntPtr               ndarray_buffer,      // const void*
            size_t               size,                // size_t
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_arr,             // NDArrayHandle**
            out mx_uint          out_name_size,       // mx_uint*
            out IntPtr           out_names            // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayLoadFromBuffer64( // int
            IntPtr               ndarray_buffer,      // const void*
            size_t               size,                // size_t
            out mx_int64         out_size,            // mx_int64*
            out IntPtr           out_arr,             // NDArrayHandle**
            out mx_int64         out_name_size,       // mx_int64*
            out IntPtr           out_names            // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySyncCopyFromCPU( // int
            NDArrayHandle        handle,              // NDArrayHandle
            IntPtr               data,                // const void*
            size_t               size                 // size_t
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySyncCopyToCPU( // int
            NDArrayHandle        handle,              // NDArrayHandle
            IntPtr               data,                // void*
            size_t               size                 // size_t
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySyncCopyFromNDArray( // int
            NDArrayHandle        handle_dst,          // NDArrayHandle
            NDArrayHandle        handle_src,          // const NDArrayHandle
            int                  i                    // const int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySyncCheckFormat( // int
            NDArrayHandle        handle,              // NDArrayHandle
            bool                 full_check           // const bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayWaitToRead( // int
            NDArrayHandle        handle               // NDArrayHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayWaitToWrite( // int
            NDArrayHandle        handle               // NDArrayHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayWaitAll( // int
        
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayFree( // int
            NDArrayHandle        handle               // NDArrayHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySlice( // int
            NDArrayHandle        handle,              // NDArrayHandle
            mx_uint              slice_begin,         // mx_uint
            mx_uint              slice_end,           // mx_uint
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayAt( // int
            NDArrayHandle        handle,              // NDArrayHandle
            mx_uint              idx,                 // mx_uint
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetStorageType( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              out_storage_type     // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayReshape( // int
            NDArrayHandle        handle,              // NDArrayHandle
            int                  ndim,                // int
            IntPtr               dims,                // int*
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayReshape64( // int
            NDArrayHandle        handle,              // NDArrayHandle
            int                  ndim,                // int
            IntPtr               dims,                // dim_t*
            bool                 reverse,             // bool
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetShape( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out mx_uint          out_dim,             // mx_uint*
            out IntPtr           out_pdata            // const mx_uint**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetShape64( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              out_dim,             // int*
            out IntPtr           out_pdata            // const int64_t**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetShapeEx( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              out_dim,             // int*
            out IntPtr           out_pdata            // const int**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetShapeEx64( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              out_dim,             // int*
            out IntPtr           out_pdata            // const mx_int64**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetData( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out IntPtr           out_pdata            // void**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayToDLPack( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out DLManagedTensorHandle out_dlpack           // DLManagedTensorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayFromDLPack( // int
            DLManagedTensorHandle dlpack,              // DLManagedTensorHandle
            out NDArrayHandle    out_handle           // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayFromDLPackEx( // int
            DLManagedTensorHandle dlpack,              // DLManagedTensorHandle
            bool                 transient_handle,    // const bool
            out NDArrayHandle    out_handle           // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCallDLPackDeleter( // int
            DLManagedTensorHandle dlpack               // DLManagedTensorHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetDType( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              out_dtype            // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetAuxType( // int
            NDArrayHandle        handle,              // NDArrayHandle
            mx_uint              i,                   // mx_uint
            out int              out_type             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetAuxType64( // int
            NDArrayHandle        handle,              // NDArrayHandle
            mx_int64             i,                   // mx_int64
            out int              out_type             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetAuxNDArray( // int
            NDArrayHandle        handle,              // NDArrayHandle
            mx_uint              i,                   // mx_uint
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetAuxNDArray64( // int
            NDArrayHandle        handle,              // NDArrayHandle
            mx_int64             i,                   // mx_int64
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetDataNDArray( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetContext( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              out_dev_type,        // int*
            out int              out_dev_id           // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetGrad( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayDetach( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArraySetGradState( // int
            NDArrayHandle        handle,              // NDArrayHandle
            int                  state                // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetGradState( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out int              @out                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXListFunctions( // int
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_array            // FunctionHandle**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXListFunctions64( // int
            out mx_int64         out_size,            // mx_int64*
            out IntPtr           out_array            // FunctionHandle**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGetFunction( // int
            string               name,                // const char*
            out FunctionHandle   @out                 // FunctionHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXFuncGetInfo( // int
            FunctionHandle       fun,                 // FunctionHandle
            out IntPtr           name,                // const char**
            string[]             description,         // const char**
            out mx_uint          num_args,            // mx_uint*
            out IntPtr           arg_names,           // const char***
            out IntPtr           arg_type_infos,      // const char***
            out IntPtr           arg_descriptions,    // const char***
            out IntPtr           return_type          // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXFuncDescribe( // int
            FunctionHandle       fun,                 // FunctionHandle
            out mx_uint          num_use_vars,        // mx_uint*
            out mx_uint          num_scalars,         // mx_uint*
            out mx_uint          num_mutate_vars,     // mx_uint*
            out int              type_mask            // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXFuncInvoke( // int
            FunctionHandle       fun,                 // FunctionHandle
            NDArrayHandle[]      use_vars,            // NDArrayHandle*
            IntPtr               scalar_args,         // mx_float*
            NDArrayHandle[]      mutate_vars          // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXFuncInvokeEx( // int
            FunctionHandle       fun,                 // FunctionHandle
            NDArrayHandle[]      use_vars,            // NDArrayHandle*
            IntPtr               scalar_args,         // mx_float*
            NDArrayHandle[]      mutate_vars,         // NDArrayHandle*
            int                  num_params,          // int
            string[]             param_keys,          // char**
            string[]             param_vals           // char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXImperativeInvoke( // int
            AtomicSymbolCreator  creator,             // AtomicSymbolCreator
            int                  num_inputs,          // int
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            ref int              num_outputs,         // int*
            ref IntPtr           outputs,             // NDArrayHandle**
            int                  num_params,          // int
            string[]             param_keys,          // const char**
            string[]             param_vals           // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXImperativeInvokeEx( // int
            AtomicSymbolCreator  creator,             // AtomicSymbolCreator
            int                  num_inputs,          // int
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            ref int              num_outputs,         // int*
            ref IntPtr           outputs,             // NDArrayHandle**
            int                  num_params,          // int
            string[]             param_keys,          // const char**
            string[]             param_vals,          // const char**
            out IntPtr           out_stypes           // const int**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradSetIsRecording( // int
            int                  is_recording,        // int
            IntPtr               prev                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradSetIsTraining( // int
            int                  is_training,         // int
            IntPtr               prev                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradIsRecording( // int
            out bool             curr                 // bool*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradIsTraining( // int
            out bool             curr                 // bool*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXIsNumpyShape( // int
            out bool             curr                 // bool*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetIsNumpyShape( // int
            int                  is_np_shape,         // int
            IntPtr               prev                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradMarkVariables( // int
            mx_uint              num_var,             // mx_uint
            NDArrayHandle[]      var_handles,         // NDArrayHandle*
            IntPtr               reqs_array,          // mx_uint*
            NDArrayHandle[]      grad_handles         // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradComputeGradient( // int
            mx_uint              num_output,          // mx_uint
            NDArrayHandle[]      output_handles       // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradBackward( // int
            mx_uint              num_output,          // mx_uint
            NDArrayHandle[]      output_handles,      // NDArrayHandle*
            NDArrayHandle[]      ograd_handles,       // NDArrayHandle*
            int                  retain_graph         // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradBackwardEx( // int
            mx_uint              num_output,          // mx_uint
            NDArrayHandle[]      output_handles,      // NDArrayHandle*
            NDArrayHandle[]      ograd_handles,       // NDArrayHandle*
            mx_uint              num_variables,       // mx_uint
            NDArrayHandle[]      var_handles,         // NDArrayHandle*
            int                  retain_graph,        // int
            int                  create_graph,        // int
            int                  is_train,            // int
            IntPtr               grad_handles,        // NDArrayHandle**
            IntPtr               grad_stypes          // int**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXAutogradGetSymbol( // int
            NDArrayHandle        handle,              // NDArrayHandle
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXCreateCachedOp( // int
            SymbolHandle         handle,              // SymbolHandle
            out CachedOpHandle   @out                 // CachedOpHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXCreateCachedOpEx( // int
            SymbolHandle         handle,              // SymbolHandle
            int                  num_flags,           // int
            string[]             keys,                // const char**
            string[]             vals,                // const char**
            out CachedOpHandle   @out                 // CachedOpHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXFreeCachedOp( // int
            CachedOpHandle       handle               // CachedOpHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXInvokeCachedOp( // int
            CachedOpHandle       handle,              // CachedOpHandle
            int                  num_inputs,          // int
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            IntPtr               num_outputs,         // int*
            IntPtr               outputs              // NDArrayHandle**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXInvokeCachedOpEx( // int
            CachedOpHandle       handle,              // CachedOpHandle
            int                  num_inputs,          // int
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            IntPtr               num_outputs,         // int*
            IntPtr               outputs,             // NDArrayHandle**
            out IntPtr           out_stypes           // const int**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXListAllOpNames( // int
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_array            // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXListAllOpNames64( // int
            out mx_int64         out_size,            // mx_int64*
            out IntPtr           out_array            // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListAtomicSymbolCreators( // int
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_array            // AtomicSymbolCreator**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListAtomicSymbolCreators64( // int
            out mx_int64         out_size,            // mx_int64*
            out IntPtr           out_array            // AtomicSymbolCreator**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetAtomicSymbolName( // int
            AtomicSymbolCreator  creator,             // AtomicSymbolCreator
            out IntPtr           name                 // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetInputSymbols( // int
            SymbolHandle         sym,                 // SymbolHandle
            out IntPtr           inputs,              // SymbolHandle**
            IntPtr               input_size           // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCutSubgraph( // int
            SymbolHandle         sym,                 // SymbolHandle
            out IntPtr           inputs,              // SymbolHandle**
            IntPtr               input_size           // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetAtomicSymbolInfo( // int
            AtomicSymbolCreator  creator,             // AtomicSymbolCreator
            out IntPtr           name,                // const char**
            out IntPtr           description,         // const char**
            out mx_uint          num_args,            // mx_uint*
            out IntPtr           arg_names,           // const char***
            out IntPtr           arg_type_infos,      // const char***
            out IntPtr           arg_descriptions,    // const char***
            out IntPtr           key_var_num_args,    // const char**
            out IntPtr           return_type          // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCreateAtomicSymbol( // int
            AtomicSymbolCreator  creator,             // AtomicSymbolCreator
            mx_uint              num_param,           // mx_uint
            string[]             keys,                // const char**
            string[]             vals,                // const char**
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCreateVariable( // int
            string               name,                // const char*
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCreateGroup( // int
            mx_uint              num_symbols,         // mx_uint
            IntPtr               symbols,             // SymbolHandle*
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCreateFromFile( // int
            string               fname,               // const char*
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCreateFromJSON( // int
            string               json,                // const char*
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolRemoveAmpCast( // int
            SymbolHandle         sym_handle,          // SymbolHandle
            IntPtr               ret_sym_handle       // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolSaveToFile( // int
            SymbolHandle         symbol,              // SymbolHandle
            string               fname                // const char*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolSaveToJSON( // int
            SymbolHandle         symbol,              // SymbolHandle
            out IntPtr           out_json             // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolFree( // int
            SymbolHandle         symbol               // SymbolHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCopy( // int
            SymbolHandle         symbol,              // SymbolHandle
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolPrint( // int
            SymbolHandle         symbol,              // SymbolHandle
            out IntPtr           out_str              // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetName( // int
            SymbolHandle         symbol,              // SymbolHandle
            out IntPtr           @out,                // const char**
            out int              success              // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetAttr( // int
            SymbolHandle         symbol,              // SymbolHandle
            string               key,                 // const char*
            out IntPtr           @out,                // const char**
            out int              success              // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolSetAttr( // int
            SymbolHandle         symbol,              // SymbolHandle
            string               key,                 // const char*
            string               value                // const char*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListAttr( // int
            SymbolHandle         symbol,              // SymbolHandle
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           @out                 // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListAttrShallow( // int
            SymbolHandle         symbol,              // SymbolHandle
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           @out                 // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListArguments( // int
            SymbolHandle         symbol,              // SymbolHandle
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_str_array        // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListArguments64( // int
            SymbolHandle         symbol,              // SymbolHandle
            out size_t           out_size,            // size_t*
            out IntPtr           out_str_array        // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListOutputs( // int
            SymbolHandle         symbol,              // SymbolHandle
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_str_array        // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListOutputs64( // int
            SymbolHandle         symbol,              // SymbolHandle
            out size_t           out_size,            // size_t*
            out IntPtr           out_str_array        // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetNumOutputs( // int
            SymbolHandle         symbol,              // SymbolHandle
            out mx_uint          output_count         // mx_uint*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetInternals( // int
            SymbolHandle         symbol,              // SymbolHandle
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetChildren( // int
            SymbolHandle         symbol,              // SymbolHandle
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGetOutput( // int
            SymbolHandle         symbol,              // SymbolHandle
            mx_uint              index,               // mx_uint
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListAuxiliaryStates( // int
            SymbolHandle         symbol,              // SymbolHandle
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_str_array        // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolListAuxiliaryStates64( // int
            SymbolHandle         symbol,              // SymbolHandle
            out size_t           out_size,            // size_t*
            out IntPtr           out_str_array        // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolCompose( // int
            SymbolHandle         sym,                 // SymbolHandle
            string               name,                // const char*
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            IntPtr               args                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolGrad( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_wrt,             // mx_uint
            string[]             wrt,                 // const char**
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShape( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_uint[]            arg_ind_ptr,         // const mx_uint*
            mx_uint[]            arg_shape_data,      // const mx_uint*
            IntPtr               in_shape_size,       // mx_uint*
            IntPtr               in_shape_ndim,       // const mx_uint**
            IntPtr               in_shape_data,       // const mx_uint***
            out mx_uint          out_shape_size,      // mx_uint*
            out IntPtr           out_shape_ndim,      // const mx_uint**
            out IntPtr           out_shape_data,      // const mx_uint***
            IntPtr               aux_shape_size,      // mx_uint*
            IntPtr               aux_shape_ndim,      // const mx_uint**
            IntPtr               aux_shape_data,      // const mx_uint***
            IntPtr               complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShape64( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_int64[]           arg_ind_ptr,         // const mx_int64*
            mx_int64[]           arg_shape_data,      // const mx_int64*
            IntPtr               in_shape_size,       // size_t*
            IntPtr               in_shape_ndim,       // const int**
            IntPtr               in_shape_data,       // const mx_int64***
            out size_t           out_shape_size,      // size_t*
            out IntPtr           out_shape_ndim,      // const int**
            out IntPtr           out_shape_data,      // const mx_int64***
            IntPtr               aux_shape_size,      // size_t*
            IntPtr               aux_shape_ndim,      // const int**
            IntPtr               aux_shape_data,      // const mx_int64***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShapeEx( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_uint[]            arg_ind_ptr,         // const mx_uint*
            int[]                arg_shape_data,      // const int*
            IntPtr               in_shape_size,       // mx_uint*
            IntPtr               in_shape_ndim,       // const int**
            IntPtr               in_shape_data,       // const int***
            out mx_uint          out_shape_size,      // mx_uint*
            out IntPtr           out_shape_ndim,      // const int**
            out IntPtr           out_shape_data,      // const int***
            IntPtr               aux_shape_size,      // mx_uint*
            IntPtr               aux_shape_ndim,      // const int**
            IntPtr               aux_shape_data,      // const int***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShapeEx64( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_int64[]           arg_ind_ptr,         // const mx_int64*
            mx_int64[]           arg_shape_data,      // const mx_int64*
            IntPtr               in_shape_size,       // size_t*
            IntPtr               in_shape_ndim,       // const int**
            IntPtr               in_shape_data,       // const mx_int64***
            out size_t           out_shape_size,      // size_t*
            out IntPtr           out_shape_ndim,      // const int**
            out IntPtr           out_shape_data,      // const mx_int64***
            IntPtr               aux_shape_size,      // size_t*
            IntPtr               aux_shape_ndim,      // const int**
            IntPtr               aux_shape_data,      // const mx_int64***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShapePartial( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_uint[]            arg_ind_ptr,         // const mx_uint*
            mx_uint[]            arg_shape_data,      // const mx_uint*
            IntPtr               in_shape_size,       // mx_uint*
            IntPtr               in_shape_ndim,       // const mx_uint**
            IntPtr               in_shape_data,       // const mx_uint***
            out mx_uint          out_shape_size,      // mx_uint*
            out IntPtr           out_shape_ndim,      // const mx_uint**
            out IntPtr           out_shape_data,      // const mx_uint***
            IntPtr               aux_shape_size,      // mx_uint*
            IntPtr               aux_shape_ndim,      // const mx_uint**
            IntPtr               aux_shape_data,      // const mx_uint***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShapePartial64( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_int64[]           arg_ind_ptr,         // const mx_int64*
            mx_int64[]           arg_shape_data,      // const mx_int64*
            IntPtr               in_shape_size,       // size_t*
            IntPtr               in_shape_ndim,       // const int**
            IntPtr               in_shape_data,       // const mx_int64***
            out size_t           out_shape_size,      // size_t*
            out IntPtr           out_shape_ndim,      // const int**
            out IntPtr           out_shape_data,      // const mx_int64***
            IntPtr               aux_shape_size,      // size_t*
            IntPtr               aux_shape_ndim,      // const int**
            IntPtr               aux_shape_data,      // const mx_int64***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShapePartialEx( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_uint[]            arg_ind_ptr,         // const mx_uint*
            int[]                arg_shape_data,      // const int*
            IntPtr               in_shape_size,       // mx_uint*
            IntPtr               in_shape_ndim,       // const int**
            IntPtr               in_shape_data,       // const int***
            out mx_uint          out_shape_size,      // mx_uint*
            out IntPtr           out_shape_ndim,      // const int**
            out IntPtr           out_shape_data,      // const int***
            IntPtr               aux_shape_size,      // mx_uint*
            IntPtr               aux_shape_ndim,      // const int**
            IntPtr               aux_shape_data,      // const int***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferShapePartialEx64( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            mx_int64[]           arg_ind_ptr,         // const mx_int64*
            mx_int64[]           arg_shape_data,      // const mx_int64*
            IntPtr               in_shape_size,       // size_t*
            IntPtr               in_shape_ndim,       // const int**
            IntPtr               in_shape_data,       // const mx_int64***
            out size_t           out_shape_size,      // size_t*
            out IntPtr           out_shape_ndim,      // const int**
            out IntPtr           out_shape_data,      // const mx_int64***
            IntPtr               aux_shape_size,      // size_t*
            IntPtr               aux_shape_ndim,      // const int**
            IntPtr               aux_shape_data,      // const mx_int64***
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferType( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            int[]                arg_type_data,       // const int*
            IntPtr               in_type_size,        // mx_uint*
            IntPtr               in_type_data,        // const int**
            out mx_uint          out_type_size,       // mx_uint*
            out IntPtr           out_type_data,       // const int**
            IntPtr               aux_type_size,       // mx_uint*
            IntPtr               aux_type_data,       // const int**
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSymbolInferTypePartial( // int
            SymbolHandle         sym,                 // SymbolHandle
            mx_uint              num_args,            // mx_uint
            string[]             keys,                // const char**
            int[]                arg_type_data,       // const int*
            IntPtr               in_type_size,        // mx_uint*
            IntPtr               in_type_data,        // const int**
            out mx_uint          out_type_size,       // mx_uint*
            out IntPtr           out_type_data,       // const int**
            IntPtr               aux_type_size,       // mx_uint*
            IntPtr               aux_type_data,       // const int**
            out int              complete             // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXQuantizeSymbol( // int
            SymbolHandle         sym_handle,          // SymbolHandle
            IntPtr               ret_sym_handle,      // SymbolHandle*
            mx_uint              num_excluded_symbols, // const mx_uint
            string[]             excluded_symbols,    // const char**
            mx_uint              num_offline,         // const mx_uint
            string[]             offline_params,      // const char**
            string               quantized_dtype,     // const char*
            bool                 calib_quantize       // const bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXReducePrecisionSymbol( // int
            SymbolHandle         sym_handle,          // SymbolHandle
            IntPtr               ret_sym_handle,      // SymbolHandle*
            mx_uint              num_args,            // mx_uint
            int[]                arg_type_data,       // const int*
            mx_uint              num_ind_ptr,         // mx_uint
            int[]                ind_ptr,             // const int*
            int[]                target_dtype,        // const int*
            int                  cast_optional_params, // const int
            mx_uint              num_target_dtype_op_names, // const mx_uint
            mx_uint              num_fp32_op_names,   // const mx_uint
            mx_uint              num_widest_dtype_op_names, // const mx_uint
            mx_uint              num_conditional_fp32_op_names, // const mx_uint
            mx_uint              num_excluded_symbols, // const mx_uint
            mx_uint              num_model_params,    // const mx_uint
            string[]             target_dtype_op_names, // const char**
            string[]             fp32_op_names,       // const char**
            string[]             widest_dtype_op_names, // const char**
            string[]             conditional_fp32_op_names, // const char**
            string[]             excluded_symbols,    // const char**
            string[]             conditional_param_names, // const char**
            string[]             conditional_param_vals, // const char**
            string[]             model_param_names,   // const char**
            string[]             arg_names            // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXSetCalibTableToQuantizedSymbol( // int
            SymbolHandle         qsym_handle,         // SymbolHandle
            mx_uint              num_layers,          // const mx_uint
            string[]             layer_names,         // const char**
            IntPtr               low_quantiles,       // const float*
            IntPtr               high_quantiles,      // const float*
            IntPtr               ret_sym_handle       // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGenBackendSubgraph( // int
            SymbolHandle         sym_handle,          // SymbolHandle
            string               backend,             // const char*
            out SymbolHandle     ret_sym_handle       // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXGenAtomicSymbolFromSymbol( // int
            SymbolHandle         sym_handle,          // SymbolHandle
            out SymbolHandle     ret_sym_handle       // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorFree( // int
            ExecutorHandle       handle               // ExecutorHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorPrint( // int
            ExecutorHandle       handle,              // ExecutorHandle
            out IntPtr           out_str              // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorForward( // int
            ExecutorHandle       handle,              // ExecutorHandle
            int                  is_train             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorBackward( // int
            ExecutorHandle       handle,              // ExecutorHandle
            mx_uint              len,                 // mx_uint
            NDArrayHandle[]      head_grads           // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorBackwardEx( // int
            ExecutorHandle       handle,              // ExecutorHandle
            mx_uint              len,                 // mx_uint
            NDArrayHandle[]      head_grads,          // NDArrayHandle*
            int                  is_train             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorOutputs( // int
            ExecutorHandle       handle,              // ExecutorHandle
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           @out                 // NDArrayHandle**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorBind( // int
            SymbolHandle         symbol_handle,       // SymbolHandle
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              len,                 // mx_uint
            NDArrayHandle[]      in_args,             // NDArrayHandle*
            NDArrayHandle[]      arg_grad_store,      // NDArrayHandle*
            IntPtr               grad_req_type,       // mx_uint*
            mx_uint              aux_states_len,      // mx_uint
            NDArrayHandle[]      aux_states,          // NDArrayHandle*
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorBindX( // int
            SymbolHandle         symbol_handle,       // SymbolHandle
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              num_map_keys,        // mx_uint
            string[]             map_keys,            // const char**
            int[]                map_dev_types,       // const int*
            int[]                map_dev_ids,         // const int*
            mx_uint              len,                 // mx_uint
            NDArrayHandle[]      in_args,             // NDArrayHandle*
            NDArrayHandle[]      arg_grad_store,      // NDArrayHandle*
            IntPtr               grad_req_type,       // mx_uint*
            mx_uint              aux_states_len,      // mx_uint
            NDArrayHandle[]      aux_states,          // NDArrayHandle*
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorBindEX( // int
            SymbolHandle         symbol_handle,       // SymbolHandle
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              num_map_keys,        // mx_uint
            string[]             map_keys,            // const char**
            int[]                map_dev_types,       // const int*
            int[]                map_dev_ids,         // const int*
            mx_uint              len,                 // mx_uint
            NDArrayHandle[]      in_args,             // NDArrayHandle*
            NDArrayHandle[]      arg_grad_store,      // NDArrayHandle*
            IntPtr               grad_req_type,       // mx_uint*
            mx_uint              aux_states_len,      // mx_uint
            NDArrayHandle[]      aux_states,          // NDArrayHandle*
            ExecutorHandle       shared_exec,         // ExecutorHandle
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorSimpleBind( // int
            SymbolHandle         symbol_handle,       // SymbolHandle
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              num_g2c_keys,        // const mx_uint
            string[]             g2c_keys,            // const char**
            int[]                g2c_dev_types,       // const int*
            int[]                g2c_dev_ids,         // const int*
            mx_uint              provided_grad_req_list_len, // const mx_uint
            string[]             provided_grad_req_names, // const char**
            string[]             provided_grad_req_types, // const char**
            mx_uint              num_provided_arg_shapes, // const mx_uint
            string[]             provided_arg_shape_names, // const char**
            mx_uint[]            provided_arg_shape_data, // const mx_uint*
            mx_uint[]            provided_arg_shape_idx, // const mx_uint*
            mx_uint              num_provided_arg_dtypes, // const mx_uint
            string[]             provided_arg_dtype_names, // const char**
            int[]                provided_arg_dtypes, // const int*
            mx_uint              num_provided_arg_stypes, // const mx_uint
            string[]             provided_arg_stype_names, // const char**
            int[]                provided_arg_stypes, // const int*
            mx_uint              num_shared_arg_names, // const mx_uint
            string[]             shared_arg_name_list, // const char**
            IntPtr               shared_buffer_len,   // int*
            string[]             shared_buffer_name_list, // const char**
            NDArrayHandle[]      shared_buffer_handle_list, // NDArrayHandle*
            IntPtr               updated_shared_buffer_name_list, // const char***
            IntPtr               updated_shared_buffer_handle_list, // NDArrayHandle**
            IntPtr               num_in_args,         // mx_uint*
            IntPtr               in_args,             // NDArrayHandle**
            IntPtr               arg_grads,           // NDArrayHandle**
            IntPtr               num_aux_states,      // mx_uint*
            IntPtr               aux_states,          // NDArrayHandle**
            ExecutorHandle       shared_exec_handle,  // ExecutorHandle
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorSimpleBindEx( // int
            SymbolHandle         symbol_handle,       // SymbolHandle
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              num_g2c_keys,        // const mx_uint
            string[]             g2c_keys,            // const char**
            int[]                g2c_dev_types,       // const int*
            int[]                g2c_dev_ids,         // const int*
            mx_uint              provided_grad_req_list_len, // const mx_uint
            string[]             provided_grad_req_names, // const char**
            string[]             provided_grad_req_types, // const char**
            mx_uint              num_provided_arg_shapes, // const mx_uint
            string[]             provided_arg_shape_names, // const char**
            int[]                provided_arg_shape_data, // const int*
            mx_uint[]            provided_arg_shape_idx, // const mx_uint*
            mx_uint              num_provided_arg_dtypes, // const mx_uint
            string[]             provided_arg_dtype_names, // const char**
            int[]                provided_arg_dtypes, // const int*
            mx_uint              num_provided_arg_stypes, // const mx_uint
            string[]             provided_arg_stype_names, // const char**
            int[]                provided_arg_stypes, // const int*
            mx_uint              num_shared_arg_names, // const mx_uint
            string[]             shared_arg_name_list, // const char**
            IntPtr               shared_buffer_len,   // int*
            string[]             shared_buffer_name_list, // const char**
            NDArrayHandle[]      shared_buffer_handle_list, // NDArrayHandle*
            IntPtr               updated_shared_buffer_name_list, // const char***
            IntPtr               updated_shared_buffer_handle_list, // NDArrayHandle**
            IntPtr               num_in_args,         // mx_uint*
            IntPtr               in_args,             // NDArrayHandle**
            IntPtr               arg_grads,           // NDArrayHandle**
            IntPtr               num_aux_states,      // mx_uint*
            IntPtr               aux_states,          // NDArrayHandle**
            ExecutorHandle       shared_exec_handle,  // ExecutorHandle
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorReshape( // int
            int                  partial_shaping,     // int
            int                  allow_up_sizing,     // int
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              num_map_keys,        // mx_uint
            string[]             map_keys,            // const char**
            int[]                map_dev_types,       // const int*
            int[]                map_dev_ids,         // const int*
            mx_uint              num_provided_arg_shapes, // const mx_uint
            string[]             provided_arg_shape_names, // const char**
            mx_uint[]            provided_arg_shape_data, // const mx_uint*
            mx_uint[]            provided_arg_shape_idx, // const mx_uint*
            IntPtr               num_in_args,         // mx_uint*
            IntPtr               in_args,             // NDArrayHandle**
            IntPtr               arg_grads,           // NDArrayHandle**
            IntPtr               num_aux_states,      // mx_uint*
            IntPtr               aux_states,          // NDArrayHandle**
            ExecutorHandle       shared_exec,         // ExecutorHandle
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorReshapeEx( // int
            int                  partial_shaping,     // int
            int                  allow_up_sizing,     // int
            int                  dev_type,            // int
            int                  dev_id,              // int
            mx_uint              num_map_keys,        // mx_uint
            string[]             map_keys,            // const char**
            int[]                map_dev_types,       // const int*
            int[]                map_dev_ids,         // const int*
            mx_uint              num_provided_arg_shapes, // const mx_uint
            string[]             provided_arg_shape_names, // const char**
            int[]                provided_arg_shape_data, // const int*
            mx_uint[]            provided_arg_shape_idx, // const mx_uint*
            IntPtr               num_in_args,         // mx_uint*
            IntPtr               in_args,             // NDArrayHandle**
            IntPtr               arg_grads,           // NDArrayHandle**
            IntPtr               num_aux_states,      // mx_uint*
            IntPtr               aux_states,          // NDArrayHandle**
            ExecutorHandle       shared_exec,         // ExecutorHandle
            out ExecutorHandle   @out                 // ExecutorHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorGetOptimizedSymbol( // int
            ExecutorHandle       handle,              // ExecutorHandle
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorSetMonitorCallback( // int
            ExecutorHandle       handle,              // ExecutorHandle
            ExecutorMonitorCallback callback,            // ExecutorMonitorCallback
            IntPtr               callback_handle      // void*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXExecutorSetMonitorCallbackEX( // int
            ExecutorHandle       handle,              // ExecutorHandle
            ExecutorMonitorCallback callback,            // ExecutorMonitorCallback
            IntPtr               callback_handle,     // void*
            bool                 monitor_all          // bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXListDataIters( // int
            out mx_uint          out_size,            // mx_uint*
            out IntPtr           out_array            // DataIterCreator**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterCreateIter( // int
            DataIterCreator      handle,              // DataIterCreator
            mx_uint              num_param,           // mx_uint
            string[]             keys,                // const char**
            string[]             vals,                // const char**
            out DataIterHandle   @out                 // DataIterHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterGetIterInfo( // int
            DataIterCreator      creator,             // DataIterCreator
            out IntPtr           name,                // const char**
            string[]             description,         // const char**
            out mx_uint          num_args,            // mx_uint*
            out IntPtr           arg_names,           // const char***
            out IntPtr           arg_type_infos,      // const char***
            out IntPtr           arg_descriptions     // const char***
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterFree( // int
            DataIterHandle       handle               // DataIterHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterNext( // int
            DataIterHandle       handle,              // DataIterHandle
            out int              @out                 // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterBeforeFirst( // int
            DataIterHandle       handle               // DataIterHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterGetData( // int
            DataIterHandle       handle,              // DataIterHandle
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterGetIndex( // int
            DataIterHandle       handle,              // DataIterHandle
            out IntPtr           out_index,           // uint64_t**
            out uint64_t         out_size             // uint64_t*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterGetPadNum( // int
            DataIterHandle       handle,              // DataIterHandle
            out int              pad                  // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXDataIterGetLabel( // int
            DataIterHandle       handle,              // DataIterHandle
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXInitPSEnv( // int
            mx_uint              num_vars,            // mx_uint
            string[]             keys,                // const char**
            string[]             vals                 // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreCreate( // int
            string               type,                // const char*
            out KVStoreHandle    @out                 // KVStoreHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreSetGradientCompression( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num_params,          // mx_uint
            string[]             keys,                // const char**
            string[]             vals                 // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreFree( // int
            KVStoreHandle        handle               // KVStoreHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreInit( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            int[]                keys,                // const int*
            NDArrayHandle[]      vals                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreInitEx( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            string[]             keys,                // const char**
            NDArrayHandle[]      vals                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePush( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            int[]                keys,                // const int*
            NDArrayHandle[]      vals,                // NDArrayHandle*
            int                  priority             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePushEx( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            string[]             keys,                // const char**
            NDArrayHandle[]      vals,                // NDArrayHandle*
            int                  priority             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePullWithSparse( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            int[]                keys,                // const int*
            NDArrayHandle[]      vals,                // NDArrayHandle*
            int                  priority,            // int
            bool                 ignore_sparse        // bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePullWithSparseEx( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            string[]             keys,                // const char**
            NDArrayHandle[]      vals,                // NDArrayHandle*
            int                  priority,            // int
            bool                 ignore_sparse        // bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePull( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            int[]                keys,                // const int*
            NDArrayHandle[]      vals,                // NDArrayHandle*
            int                  priority             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePullEx( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            string[]             keys,                // const char**
            NDArrayHandle[]      vals,                // NDArrayHandle*
            int                  priority             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePullRowSparse( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            int[]                keys,                // const int*
            NDArrayHandle[]      vals,                // NDArrayHandle*
            IntPtr               row_ids,             // const NDArrayHandle*
            int                  priority             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStorePullRowSparseEx( // int
            KVStoreHandle        handle,              // KVStoreHandle
            mx_uint              num,                 // mx_uint
            string[]             keys,                // const char**
            NDArrayHandle[]      vals,                // NDArrayHandle*
            IntPtr               row_ids,             // const NDArrayHandle*
            int                  priority             // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreSetUpdater( // int
            KVStoreHandle        handle,              // KVStoreHandle
            MXKVStoreUpdater     updater,             // MXKVStoreUpdater
            IntPtr               updater_handle       // void*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreSetUpdaterEx( // int
            KVStoreHandle        handle,              // KVStoreHandle
            MXKVStoreUpdater     updater,             // MXKVStoreUpdater
            MXKVStoreStrUpdater  str_updater,         // MXKVStoreStrUpdater
            IntPtr               updater_handle       // void*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreGetType( // int
            KVStoreHandle        handle,              // KVStoreHandle
            string[]             type                 // const char**
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreGetRank( // int
            KVStoreHandle        handle,              // KVStoreHandle
            IntPtr               ret                  // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreGetGroupSize( // int
            KVStoreHandle        handle,              // KVStoreHandle
            IntPtr               ret                  // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreIsWorkerNode( // int
            IntPtr               ret                  // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreIsServerNode( // int
            IntPtr               ret                  // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreIsSchedulerNode( // int
            IntPtr               ret                  // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreBarrier( // int
            KVStoreHandle        handle               // KVStoreHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreSetBarrierBeforeExit( // int
            KVStoreHandle        handle,              // KVStoreHandle
            int                  barrier_before_exit  // const int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreRunServer( // int
            KVStoreHandle        handle,              // KVStoreHandle
            MXKVStoreServerController controller,          // MXKVStoreServerController
            IntPtr               controller_handle    // void*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreSendCommmandToServers( // int
            KVStoreHandle        handle,              // KVStoreHandle
            int                  cmd_id,              // int
            string               cmd_body             // const char*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXKVStoreGetNumDeadNode( // int
            KVStoreHandle        handle,              // KVStoreHandle
            int                  node_id,             // const int
            IntPtr               number,              // int*
            int                  timeout_sec          // const int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOWriterCreate( // int
            string               uri,                 // const char*
            out RecordIOHandle   @out                 // RecordIOHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOWriterFree( // int
            RecordIOHandle       handle               // RecordIOHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOWriterWriteRecord( // int
            RecordIOHandle       handle,              // RecordIOHandle
            string               buf,                 // const char*
            size_t               size                 // size_t
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOWriterTell( // int
            RecordIOHandle       handle,              // RecordIOHandle
            IntPtr               pos                  // size_t*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOReaderCreate( // int
            string               uri,                 // const char*
            out RecordIOHandle   @out                 // RecordIOHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOReaderFree( // int
            RecordIOHandle       handle               // RecordIOHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOReaderReadRecord( // int
            RecordIOHandle       handle,              // RecordIOHandle
            IntPtr               buf,                 // char const**
            IntPtr               size                 // size_t*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOReaderSeek( // int
            RecordIOHandle       handle,              // RecordIOHandle
            size_t               pos                  // size_t
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRecordIOReaderTell( // int
            RecordIOHandle       handle,              // RecordIOHandle
            IntPtr               pos                  // size_t*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcCreate( // int
            string               name,                // char*
            mx_uint              num_input,           // mx_uint
            mx_uint              num_output,          // mx_uint
            string[]             input_names,         // char**
            string[]             output_names,        // char**
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            NDArrayHandle[]      outputs,             // NDArrayHandle*
            string               kernel,              // char*
            out RtcHandle        @out                 // RtcHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcPush( // int
            RtcHandle            handle,              // RtcHandle
            mx_uint              num_input,           // mx_uint
            mx_uint              num_output,          // mx_uint
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            NDArrayHandle[]      outputs,             // NDArrayHandle*
            mx_uint              gridDimX,            // mx_uint
            mx_uint              gridDimY,            // mx_uint
            mx_uint              gridDimZ,            // mx_uint
            mx_uint              blockDimX,           // mx_uint
            mx_uint              blockDimY,           // mx_uint
            mx_uint              blockDimZ            // mx_uint
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcFree( // int
            RtcHandle            handle               // RtcHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXCustomOpRegister( // int
            string               op_type,             // const char*
            CustomOpPropCreator  creator              // CustomOpPropCreator
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXCustomFunctionRecord( // int
            int                  num_inputs,          // int
            NDArrayHandle[]      inputs,              // NDArrayHandle*
            int                  num_outputs,         // int
            NDArrayHandle[]      outputs,             // NDArrayHandle*
            IntPtr               callbacks            // struct MXCallbackList*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcCudaModuleCreate( // int
            string               source,              // const char*
            int                  num_options,         // int
            string[]             options,             // const char**
            int                  num_exports,         // int
            string[]             exports,             // const char**
            out CudaModuleHandle @out                 // CudaModuleHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcCudaModuleFree( // int
            CudaModuleHandle     handle               // CudaModuleHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcCudaKernelCreate( // int
            CudaModuleHandle     handle,              // CudaModuleHandle
            string               name,                // const char*
            int                  num_args,            // int
            IntPtr               is_ndarray,          // int*
            IntPtr               is_const,            // int*
            IntPtr               arg_types,           // int*
            out CudaKernelHandle @out                 // CudaKernelHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcCudaKernelFree( // int
            CudaKernelHandle     handle               // CudaKernelHandle
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXRtcCudaKernelCall( // int
            CudaKernelHandle     handle,              // CudaKernelHandle
            int                  dev_id,              // int
            IntPtr               args,                // void**
            mx_uint              grid_dim_x,          // mx_uint
            mx_uint              grid_dim_y,          // mx_uint
            mx_uint              grid_dim_z,          // mx_uint
            mx_uint              block_dim_x,         // mx_uint
            mx_uint              block_dim_y,         // mx_uint
            mx_uint              block_dim_z,         // mx_uint
            mx_uint              shared_mem           // mx_uint
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayGetSharedMemHandle( // int
            NDArrayHandle        handle,              // NDArrayHandle
            IntPtr               shared_pid,          // int*
            IntPtr               shared_id            // int*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateFromSharedMem( // int
            int                  shared_pid,          // int
            int                  shared_id,           // int
            mx_uint[]            shape,               // const mx_uint*
            mx_uint              ndim,                // mx_uint
            int                  dtype,               // int
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXStorageEmptyCache( // int
            int                  dev_type,            // int
            int                  dev_id               // int
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXNDArrayCreateFromSharedMemEx( // int
            int                  shared_pid,          // int
            int                  shared_id,           // int
            int[]                shape,               // const int*
            int                  ndim,                // int
            int                  dtype,               // int
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXEnginePushAsync( // int
            EngineAsyncFunc      async_func,          // EngineAsyncFunc
            IntPtr               func_param,          // void*
            EngineFuncParamDeleter deleter,             // EngineFuncParamDeleter
            ContextHandle        ctx_handle,          // ContextHandle
            EngineVarHandle      const_vars_handle,   // EngineVarHandle
            int                  num_const_vars,      // int
            EngineVarHandle      mutable_vars_handle, // EngineVarHandle
            int                  num_mutable_vars,    // int
            EngineFnPropertyHandle prop_handle,         // EngineFnPropertyHandle
            int                  priority,            // int
            string               opr_name,            // const char*
            bool                 wait                 // bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXEnginePushSync( // int
            EngineSyncFunc       sync_func,           // EngineSyncFunc
            IntPtr               func_param,          // void*
            EngineFuncParamDeleter deleter,             // EngineFuncParamDeleter
            ContextHandle        ctx_handle,          // ContextHandle
            EngineVarHandle      const_vars_handle,   // EngineVarHandle
            int                  num_const_vars,      // int
            EngineVarHandle      mutable_vars_handle, // EngineVarHandle
            int                  num_mutable_vars,    // int
            EngineFnPropertyHandle prop_handle,         // EngineFnPropertyHandle
            int                  priority,            // int
            string               opr_name             // const char*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXShallowCopyNDArray( // int
            NDArrayHandle        src,                 // NDArrayHandle
            out NDArrayHandle    @out                 // NDArrayHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXShallowCopySymbol( // int
            SymbolHandle         src,                 // SymbolHandle
            out SymbolHandle     @out                 // SymbolHandle*
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXEnginePushAsyncND( // int
            EngineAsyncFunc      async_func,          // EngineAsyncFunc
            IntPtr               func_param,          // void*
            EngineFuncParamDeleter deleter,             // EngineFuncParamDeleter
            ContextHandle        ctx_handle,          // ContextHandle
            NDArrayHandle[]      const_nds_handle,    // NDArrayHandle*
            int                  num_const_nds,       // int
            NDArrayHandle[]      mutable_nds_handle,  // NDArrayHandle*
            int                  num_mutable_nds,     // int
            EngineFnPropertyHandle prop_handle,         // EngineFnPropertyHandle
            int                  priority,            // int
            string               opr_name,            // const char*
            bool                 wait                 // bool
        );

        /// func
        [DllImport("libmxnet.dll")]
        public static extern int MXEnginePushSyncND( // int
            EngineSyncFunc       sync_func,           // EngineSyncFunc
            IntPtr               func_param,          // void*
            EngineFuncParamDeleter deleter,             // EngineFuncParamDeleter
            ContextHandle        ctx_handle,          // ContextHandle
            NDArrayHandle[]      const_nds_handle,    // NDArrayHandle*
            int                  num_const_nds,       // int
            NDArrayHandle[]      mutable_nds_handle,  // NDArrayHandle*
            int                  num_mutable_nds,     // int
            EngineFnPropertyHandle prop_handle,         // EngineFnPropertyHandle
            int                  priority,            // int
            string               opr_name             // const char*
        );

    }
}
