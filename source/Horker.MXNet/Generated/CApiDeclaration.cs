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

namespace Horker.MXNet.Core
{
    public static class CApiDeclaration
    {
        [DllImport("libmxnet.dll", EntryPoint = "MXGetLastError")]
        public static extern string MXGetLastError( // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXLoadLib")]
        public static extern int MXLoadLib( // int
            string               path                  // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXLibInfoFeatures")]
        public static extern int MXLibInfoFeatures( // int
            out IntPtr           libFeature,           // const struct LibFeature **
            out size_t           size                  // size_t *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRandomSeed")]
        public static extern int MXRandomSeed( // int
            int                  seed                  // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRandomSeedContext")]
        public static extern int MXRandomSeedContext( // int
            int                  seed,                 // int
            int                  dev_type,             // int
            int                  dev_id                // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNotifyShutdown")]
        public static extern int MXNotifyShutdown( // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetProcessProfilerConfig")]
        public static extern int MXSetProcessProfilerConfig( // int
            int                  num_params,           // int
            IntPtr               keys,                 // const char *const *
            IntPtr               vals,                 // const char *const *
            IntPtr               kvstoreHandle         // KVStoreHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetProfilerConfig")]
        public static extern int MXSetProfilerConfig( // int
            int                  num_params,           // int
            IntPtr               keys,                 // const char *const *
            IntPtr               vals                  // const char *const *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetProcessProfilerState")]
        public static extern int MXSetProcessProfilerState( // int
            int                  state,                // int
            int                  profile_process,      // int
            IntPtr               kvStoreHandle         // KVStoreHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetProfilerState")]
        public static extern int MXSetProfilerState( // int
            int                  state                 // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDumpProcessProfile")]
        public static extern int MXDumpProcessProfile( // int
            int                  finished,             // int
            int                  profile_process,      // int
            IntPtr               kvStoreHandle         // KVStoreHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDumpProfile")]
        public static extern int MXDumpProfile( // int
            int                  finished              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAggregateProfileStatsPrint")]
        public static extern int MXAggregateProfileStatsPrint( // int
            out string           out_str,              // const char **
            int                  reset                 // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAggregateProfileStatsPrintEx")]
        public static extern int MXAggregateProfileStatsPrintEx( // int
            out string           out_str,              // const char **
            int                  reset,                // int
            int                  format,               // int
            int                  sort_by,              // int
            int                  ascending             // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProcessProfilePause")]
        public static extern int MXProcessProfilePause( // int
            int                  paused,               // int
            int                  profile_process,      // int
            IntPtr               kvStoreHandle         // KVStoreHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfilePause")]
        public static extern int MXProfilePause( // int
            int                  paused                // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileCreateDomain")]
        public static extern int MXProfileCreateDomain( // int
            string               domain,               // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileCreateTask")]
        public static extern int MXProfileCreateTask( // int
            ProfileHandle        domain,               // ProfileHandle
            string               task_name,            // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileCreateFrame")]
        public static extern int MXProfileCreateFrame( // int
            ProfileHandle        domain,               // ProfileHandle
            string               frame_name,           // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileCreateEvent")]
        public static extern int MXProfileCreateEvent( // int
            string               event_name,           // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileCreateCounter")]
        public static extern int MXProfileCreateCounter( // int
            ProfileHandle        domain,               // ProfileHandle
            string               counter_name,         // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileDestroyHandle")]
        public static extern int MXProfileDestroyHandle( // int
            ProfileHandle        frame_handle          // ProfileHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileDurationStart")]
        public static extern int MXProfileDurationStart( // int
            ProfileHandle        duration_handle       // ProfileHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileDurationStop")]
        public static extern int MXProfileDurationStop( // int
            ProfileHandle        duration_handle       // ProfileHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileSetCounter")]
        public static extern int MXProfileSetCounter( // int
            ProfileHandle        counter_handle,       // ProfileHandle
            uint64_t             value                 // uint64_t
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileAdjustCounter")]
        public static extern int MXProfileAdjustCounter( // int
            ProfileHandle        counter_handle,       // ProfileHandle
            IntPtr               value                 // int64_t
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXProfileSetMarker")]
        public static extern int MXProfileSetMarker( // int
            ProfileHandle        domain,               // ProfileHandle
            string               instant_marker_name,  // const char *
            string               scope                 // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetNumOMPThreads")]
        public static extern int MXSetNumOMPThreads( // int
            int                  thread_num            // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXEngineSetBulkSize")]
        public static extern int MXEngineSetBulkSize( // int
            int                  bulk_size,            // int
            out int              prev_bulk_size        // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGetGPUCount")]
        public static extern int MXGetGPUCount( // int
            out int              @out                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGetGPUMemoryInformation")]
        public static extern int MXGetGPUMemoryInformation( // int
            int                  dev,                  // int
            out int              free_mem,             // int *
            out int              total_mem             // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGetGPUMemoryInformation64")]
        public static extern int MXGetGPUMemoryInformation64( // int
            int                  dev,                  // int
            out uint64_t         free_mem,             // uint64_t *
            out uint64_t         total_mem             // uint64_t *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGetVersion")]
        public static extern int MXGetVersion( // int
            out int              @out                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateNone")]
        public static extern int MXNDArrayCreateNone( // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreate")]
        public static extern int MXNDArrayCreate( // int
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateEx")]
        public static extern int MXNDArrayCreateEx( // int
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateEx64")]
        public static extern int MXNDArrayCreateEx64( // int
            mx_int64[]           shape,                // const mx_int64 *
            int                  ndim,                 // int
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateSparseEx")]
        public static extern int MXNDArrayCreateSparseEx( // int
            int                  storage_type,         // int
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            int                  dtype,                // int
            mx_uint              num_aux,              // mx_uint
            out int              aux_type,             // int *
            out mx_uint          aux_ndims,            // mx_uint *
            mx_uint[]            aux_shape,            // const mx_uint *
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateSparseEx64")]
        public static extern int MXNDArrayCreateSparseEx64( // int
            int                  storage_type,         // int
            mx_int64[]           shape,                // const mx_int64 *
            int                  ndim,                 // int
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            int                  dtype,                // int
            mx_uint              num_aux,              // mx_uint
            out int              aux_type,             // int *
            out int              aux_ndims,            // int *
            mx_int64[]           aux_shape,            // const mx_int64 *
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayLoadFromRawBytes")]
        public static extern int MXNDArrayLoadFromRawBytes( // int
            IntPtr               buf,                  // const void *
            size_t               size,                 // size_t
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySaveRawBytes")]
        public static extern int MXNDArraySaveRawBytes( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out size_t           out_size,             // size_t *
            out string           out_buf               // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySave")]
        public static extern int MXNDArraySave( // int
            string               fname,                // const char *
            mx_uint              num_args,             // mx_uint
            out NDArrayHandle    args,                 // NDArrayHandle *
            out string           keys                  // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayLoad")]
        public static extern int MXNDArrayLoad( // int
            string               fname,                // const char *
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_arr,              // NDArrayHandle **
            out mx_uint          out_name_size,        // mx_uint *
            out string[]         out_names             // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayLoad64")]
        public static extern int MXNDArrayLoad64( // int
            string               fname,                // const char *
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_arr,              // NDArrayHandle **
            out IntPtr           out_name_size,        // mx_int64 *
            out string[]         out_names             // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayLoadFromBuffer")]
        public static extern int MXNDArrayLoadFromBuffer( // int
            IntPtr               ndarray_buffer,       // const void *
            size_t               size,                 // size_t
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_arr,              // NDArrayHandle **
            out mx_uint          out_name_size,        // mx_uint *
            out string[]         out_names             // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayLoadFromBuffer64")]
        public static extern int MXNDArrayLoadFromBuffer64( // int
            IntPtr               ndarray_buffer,       // const void *
            size_t               size,                 // size_t
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_arr,              // NDArrayHandle **
            out IntPtr           out_name_size,        // mx_int64 *
            out string[]         out_names             // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySyncCopyFromCPU")]
        public static extern int MXNDArraySyncCopyFromCPU( // int
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               data,                 // const void *
            size_t               size                  // size_t
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySyncCopyToCPU")]
        public static extern int MXNDArraySyncCopyToCPU( // int
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               data,                 // void *
            size_t               size                  // size_t
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySyncCopyFromNDArray")]
        public static extern int MXNDArraySyncCopyFromNDArray( // int
            NDArrayHandle        handle_dst,           // NDArrayHandle
            IntPtr               handle_src,           // const NDArrayHandle
            IntPtr               i                     // const int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySyncCheckFormat")]
        public static extern int MXNDArraySyncCheckFormat( // int
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               full_check            // const bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayWaitToRead")]
        public static extern int MXNDArrayWaitToRead( // int
            NDArrayHandle        handle                // NDArrayHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayWaitToWrite")]
        public static extern int MXNDArrayWaitToWrite( // int
            NDArrayHandle        handle                // NDArrayHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayWaitAll")]
        public static extern int MXNDArrayWaitAll( // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayFree")]
        public static extern int MXNDArrayFree( // int
            NDArrayHandle        handle                // NDArrayHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySlice")]
        public static extern int MXNDArraySlice( // int
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              slice_begin,          // mx_uint
            mx_uint              slice_end,            // mx_uint
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayAt")]
        public static extern int MXNDArrayAt( // int
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              idx,                  // mx_uint
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetStorageType")]
        public static extern int MXNDArrayGetStorageType( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_storage_type      // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayReshape")]
        public static extern int MXNDArrayReshape( // int
            NDArrayHandle        handle,               // NDArrayHandle
            int                  ndim,                 // int
            out int              dims,                 // int *
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayReshape64")]
        public static extern int MXNDArrayReshape64( // int
            NDArrayHandle        handle,               // NDArrayHandle
            int                  ndim,                 // int
            out IntPtr           dims,                 // dim_t *
            IntPtr               reverse,              // bool
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetShape")]
        public static extern int MXNDArrayGetShape( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out mx_uint          out_dim,              // mx_uint *
            out IntPtr           out_pdata             // const mx_uint **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetShape64")]
        public static extern int MXNDArrayGetShape64( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dim,              // int *
            out IntPtr           out_pdata             // const int64_t **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetShapeEx")]
        public static extern int MXNDArrayGetShapeEx( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dim,              // int *
            out IntPtr           out_pdata             // const int **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetShapeEx64")]
        public static extern int MXNDArrayGetShapeEx64( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dim,              // int *
            out IntPtr           out_pdata             // const mx_int64 **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetData")]
        public static extern int MXNDArrayGetData( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out IntPtr           out_pdata             // void **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayToDLPack")]
        public static extern int MXNDArrayToDLPack( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out IntPtr           out_dlpack            // DLManagedTensorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayFromDLPack")]
        public static extern int MXNDArrayFromDLPack( // int
            IntPtr               dlpack,               // DLManagedTensorHandle
            out NDArrayHandle    out_handle            // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayFromDLPackEx")]
        public static extern int MXNDArrayFromDLPackEx( // int
            IntPtr               dlpack,               // DLManagedTensorHandle
            IntPtr               transient_handle,     // const bool
            out NDArrayHandle    out_handle            // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCallDLPackDeleter")]
        public static extern int MXNDArrayCallDLPackDeleter( // int
            IntPtr               dlpack                // DLManagedTensorHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetDType")]
        public static extern int MXNDArrayGetDType( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dtype             // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetAuxType")]
        public static extern int MXNDArrayGetAuxType( // int
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              i,                    // mx_uint
            out int              out_type              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetAuxType64")]
        public static extern int MXNDArrayGetAuxType64( // int
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               i,                    // mx_int64
            out int              out_type              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetAuxNDArray")]
        public static extern int MXNDArrayGetAuxNDArray( // int
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              i,                    // mx_uint
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetAuxNDArray64")]
        public static extern int MXNDArrayGetAuxNDArray64( // int
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               i,                    // mx_int64
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetDataNDArray")]
        public static extern int MXNDArrayGetDataNDArray( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetContext")]
        public static extern int MXNDArrayGetContext( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dev_type,         // int *
            out int              out_dev_id            // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetGrad")]
        public static extern int MXNDArrayGetGrad( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayDetach")]
        public static extern int MXNDArrayDetach( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArraySetGradState")]
        public static extern int MXNDArraySetGradState( // int
            NDArrayHandle        handle,               // NDArrayHandle
            int                  state                 // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetGradState")]
        public static extern int MXNDArrayGetGradState( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              @out                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXListFunctions")]
        public static extern int MXListFunctions( // int
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_array             // FunctionHandle **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXListFunctions64")]
        public static extern int MXListFunctions64( // int
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_array             // FunctionHandle **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGetFunction")]
        public static extern int MXGetFunction( // int
            string               name,                 // const char *
            out FunctionHandle   @out                  // FunctionHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXFuncGetInfo")]
        public static extern int MXFuncGetInfo( // int
            FunctionHandle       fun,                  // FunctionHandle
            out string           name,                 // const char **
            out string           description,          // const char **
            out mx_uint          num_args,             // mx_uint *
            out string[]         arg_names,            // const char ***
            out string[]         arg_type_infos,       // const char ***
            out string[]         arg_descriptions,     // const char ***
            out string           return_type           // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXFuncDescribe")]
        public static extern int MXFuncDescribe( // int
            FunctionHandle       fun,                  // FunctionHandle
            out mx_uint          num_use_vars,         // mx_uint *
            out mx_uint          num_scalars,          // mx_uint *
            out mx_uint          num_mutate_vars,      // mx_uint *
            out int              type_mask             // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXFuncInvoke")]
        public static extern int MXFuncInvoke( // int
            FunctionHandle       fun,                  // FunctionHandle
            out NDArrayHandle    use_vars,             // NDArrayHandle *
            out mx_float         scalar_args,          // mx_float *
            out NDArrayHandle    mutate_vars           // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXFuncInvokeEx")]
        public static extern int MXFuncInvokeEx( // int
            FunctionHandle       fun,                  // FunctionHandle
            out NDArrayHandle    use_vars,             // NDArrayHandle *
            out mx_float         scalar_args,          // mx_float *
            out NDArrayHandle    mutate_vars,          // NDArrayHandle *
            int                  num_params,           // int
            string[]             param_keys,           // char **
            string[]             param_vals            // char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXImperativeInvoke")]
        public static extern int MXImperativeInvoke( // int
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs,              // NDArrayHandle **
            int                  num_params,           // int
            string[]             param_keys,           // const char **
            string[]             param_vals            // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXImperativeInvokeEx")]
        public static extern int MXImperativeInvokeEx( // int
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs,              // NDArrayHandle **
            int                  num_params,           // int
            string[]             param_keys,           // const char **
            string[]             param_vals,           // const char **
            out IntPtr           out_stypes            // const int **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradSetIsRecording")]
        public static extern int MXAutogradSetIsRecording( // int
            int                  is_recording,         // int
            out int              prev                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradSetIsTraining")]
        public static extern int MXAutogradSetIsTraining( // int
            int                  is_training,          // int
            out int              prev                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradIsRecording")]
        public static extern int MXAutogradIsRecording( // int
            out IntPtr           curr                  // bool *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradIsTraining")]
        public static extern int MXAutogradIsTraining( // int
            out IntPtr           curr                  // bool *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXIsNumpyShape")]
        public static extern int MXIsNumpyShape( // int
            out IntPtr           curr                  // bool *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetIsNumpyShape")]
        public static extern int MXSetIsNumpyShape( // int
            int                  is_np_shape,          // int
            out int              prev                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradMarkVariables")]
        public static extern int MXAutogradMarkVariables( // int
            mx_uint              num_var,              // mx_uint
            out NDArrayHandle    var_handles,          // NDArrayHandle *
            out mx_uint          reqs_array,           // mx_uint *
            out NDArrayHandle    grad_handles          // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradComputeGradient")]
        public static extern int MXAutogradComputeGradient( // int
            mx_uint              num_output,           // mx_uint
            out NDArrayHandle    output_handles        // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradBackward")]
        public static extern int MXAutogradBackward( // int
            mx_uint              num_output,           // mx_uint
            out NDArrayHandle    output_handles,       // NDArrayHandle *
            out NDArrayHandle    ograd_handles,        // NDArrayHandle *
            int                  retain_graph          // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradBackwardEx")]
        public static extern int MXAutogradBackwardEx( // int
            mx_uint              num_output,           // mx_uint
            out NDArrayHandle    output_handles,       // NDArrayHandle *
            out NDArrayHandle    ograd_handles,        // NDArrayHandle *
            mx_uint              num_variables,        // mx_uint
            out NDArrayHandle    var_handles,          // NDArrayHandle *
            int                  retain_graph,         // int
            int                  create_graph,         // int
            int                  is_train,             // int
            out IntPtr           grad_handles,         // NDArrayHandle **
            out IntPtr           grad_stypes           // int **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXAutogradGetSymbol")]
        public static extern int MXAutogradGetSymbol( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXCreateCachedOp")]
        public static extern int MXCreateCachedOp( // int
            SymbolHandle         handle,               // SymbolHandle
            out IntPtr           @out                  // CachedOpHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXCreateCachedOpEx")]
        public static extern int MXCreateCachedOpEx( // int
            SymbolHandle         handle,               // SymbolHandle
            int                  num_flags,            // int
            out string           keys,                 // const char **
            out string           vals,                 // const char **
            out IntPtr           @out                  // CachedOpHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXFreeCachedOp")]
        public static extern int MXFreeCachedOp( // int
            IntPtr               handle                // CachedOpHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXInvokeCachedOp")]
        public static extern int MXInvokeCachedOp( // int
            IntPtr               handle,               // CachedOpHandle
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs               // NDArrayHandle **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXInvokeCachedOpEx")]
        public static extern int MXInvokeCachedOpEx( // int
            IntPtr               handle,               // CachedOpHandle
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs,              // NDArrayHandle **
            out IntPtr           out_stypes            // const int **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXListAllOpNames")]
        public static extern int MXListAllOpNames( // int
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_array             // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXListAllOpNames64")]
        public static extern int MXListAllOpNames64( // int
            out IntPtr           out_size,             // mx_int64 *
            out string[]         out_array             // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListAtomicSymbolCreators")]
        public static extern int MXSymbolListAtomicSymbolCreators( // int
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_array             // AtomicSymbolCreator **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListAtomicSymbolCreators64")]
        public static extern int MXSymbolListAtomicSymbolCreators64( // int
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_array             // AtomicSymbolCreator **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetAtomicSymbolName")]
        public static extern int MXSymbolGetAtomicSymbolName( // int
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            out string           name                  // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetInputSymbols")]
        public static extern int MXSymbolGetInputSymbols( // int
            SymbolHandle         sym,                  // SymbolHandle
            IntPtr               inputs,               // SymbolHandle **
            IntPtr               input_size            // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCutSubgraph")]
        public static extern int MXSymbolCutSubgraph( // int
            SymbolHandle         sym,                  // SymbolHandle
            IntPtr               inputs,               // SymbolHandle **
            IntPtr               input_size            // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetAtomicSymbolInfo")]
        public static extern int MXSymbolGetAtomicSymbolInfo( // int
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            out string           name,                 // const char **
            out string           description,          // const char **
            out mx_uint          num_args,             // mx_uint *
            out string[]         arg_names,            // const char ***
            out string[]         arg_type_infos,       // const char ***
            out string[]         arg_descriptions,     // const char ***
            out string           key_var_num_args,     // const char **
            out string           return_type           // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCreateAtomicSymbol")]
        public static extern int MXSymbolCreateAtomicSymbol( // int
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            mx_uint              num_param,            // mx_uint
            out string           keys,                 // const char **
            out string           vals,                 // const char **
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCreateVariable")]
        public static extern int MXSymbolCreateVariable( // int
            string               name,                 // const char *
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCreateGroup")]
        public static extern int MXSymbolCreateGroup( // int
            mx_uint              num_symbols,          // mx_uint
            out SymbolHandle     symbols,              // SymbolHandle *
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCreateFromFile")]
        public static extern int MXSymbolCreateFromFile( // int
            string               fname,                // const char *
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCreateFromJSON")]
        public static extern int MXSymbolCreateFromJSON( // int
            string               json,                 // const char *
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolRemoveAmpCast")]
        public static extern int MXSymbolRemoveAmpCast( // int
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolSaveToFile")]
        public static extern int MXSymbolSaveToFile( // int
            SymbolHandle         symbol,               // SymbolHandle
            string               fname                 // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolSaveToJSON")]
        public static extern int MXSymbolSaveToJSON( // int
            SymbolHandle         symbol,               // SymbolHandle
            out string           out_json              // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolFree")]
        public static extern int MXSymbolFree( // int
            SymbolHandle         symbol                // SymbolHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCopy")]
        public static extern int MXSymbolCopy( // int
            SymbolHandle         symbol,               // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolPrint")]
        public static extern int MXSymbolPrint( // int
            SymbolHandle         symbol,               // SymbolHandle
            out string           out_str               // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetName")]
        public static extern int MXSymbolGetName( // int
            SymbolHandle         symbol,               // SymbolHandle
            out string           @out,                 // const char **
            out int              success               // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetAttr")]
        public static extern int MXSymbolGetAttr( // int
            SymbolHandle         symbol,               // SymbolHandle
            string               key,                  // const char *
            out string           @out,                 // const char **
            out int              success               // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolSetAttr")]
        public static extern int MXSymbolSetAttr( // int
            SymbolHandle         symbol,               // SymbolHandle
            string               key,                  // const char *
            string               value                 // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListAttr")]
        public static extern int MXSymbolListAttr( // int
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         @out                  // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListAttrShallow")]
        public static extern int MXSymbolListAttrShallow( // int
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         @out                  // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListArguments")]
        public static extern int MXSymbolListArguments( // int
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_str_array         // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListArguments64")]
        public static extern int MXSymbolListArguments64( // int
            SymbolHandle         symbol,               // SymbolHandle
            out size_t           out_size,             // size_t *
            out string[]         out_str_array         // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListOutputs")]
        public static extern int MXSymbolListOutputs( // int
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_str_array         // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListOutputs64")]
        public static extern int MXSymbolListOutputs64( // int
            SymbolHandle         symbol,               // SymbolHandle
            out size_t           out_size,             // size_t *
            out string[]         out_str_array         // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetNumOutputs")]
        public static extern int MXSymbolGetNumOutputs( // int
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          output_count          // mx_uint *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetInternals")]
        public static extern int MXSymbolGetInternals( // int
            SymbolHandle         symbol,               // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetChildren")]
        public static extern int MXSymbolGetChildren( // int
            SymbolHandle         symbol,               // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGetOutput")]
        public static extern int MXSymbolGetOutput( // int
            SymbolHandle         symbol,               // SymbolHandle
            mx_uint              index,                // mx_uint
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListAuxiliaryStates")]
        public static extern int MXSymbolListAuxiliaryStates( // int
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_str_array         // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolListAuxiliaryStates64")]
        public static extern int MXSymbolListAuxiliaryStates64( // int
            SymbolHandle         symbol,               // SymbolHandle
            out size_t           out_size,             // size_t *
            out string[]         out_str_array         // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolCompose")]
        public static extern int MXSymbolCompose( // int
            SymbolHandle         sym,                  // SymbolHandle
            string               name,                 // const char *
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            out SymbolHandle     args                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolGrad")]
        public static extern int MXSymbolGrad( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_wrt,              // mx_uint
            out string           wrt,                  // const char **
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShape")]
        public static extern int MXSymbolInferShape( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_uint[]            arg_ind_ptr,          // const mx_uint *
            mx_uint[]            arg_shape_data,       // const mx_uint *
            out mx_uint          in_shape_size,        // mx_uint *
            out IntPtr           in_shape_ndim,        // const mx_uint **
            out IntPtr           in_shape_data,        // const mx_uint ***
            out mx_uint          out_shape_size,       // mx_uint *
            out IntPtr           out_shape_ndim,       // const mx_uint **
            out IntPtr           out_shape_data,       // const mx_uint ***
            out mx_uint          aux_shape_size,       // mx_uint *
            out IntPtr           aux_shape_ndim,       // const mx_uint **
            out IntPtr           aux_shape_data,       // const mx_uint ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShape64")]
        public static extern int MXSymbolInferShape64( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_int64[]           arg_ind_ptr,          // const mx_int64 *
            mx_int64[]           arg_shape_data,       // const mx_int64 *
            out size_t           in_shape_size,        // size_t *
            out IntPtr           in_shape_ndim,        // const int **
            out IntPtr           in_shape_data,        // const mx_int64 ***
            out size_t           out_shape_size,       // size_t *
            out IntPtr           out_shape_ndim,       // const int **
            out IntPtr           out_shape_data,       // const mx_int64 ***
            out size_t           aux_shape_size,       // size_t *
            out IntPtr           aux_shape_ndim,       // const int **
            out IntPtr           aux_shape_data,       // const mx_int64 ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShapeEx")]
        public static extern int MXSymbolInferShapeEx( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_uint[]            arg_ind_ptr,          // const mx_uint *
            int[]                arg_shape_data,       // const int *
            out mx_uint          in_shape_size,        // mx_uint *
            out IntPtr           in_shape_ndim,        // const int **
            out IntPtr           in_shape_data,        // const int ***
            out mx_uint          out_shape_size,       // mx_uint *
            out IntPtr           out_shape_ndim,       // const int **
            out IntPtr           out_shape_data,       // const int ***
            out mx_uint          aux_shape_size,       // mx_uint *
            out IntPtr           aux_shape_ndim,       // const int **
            out IntPtr           aux_shape_data,       // const int ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShapeEx64")]
        public static extern int MXSymbolInferShapeEx64( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_int64[]           arg_ind_ptr,          // const mx_int64 *
            mx_int64[]           arg_shape_data,       // const mx_int64 *
            out size_t           in_shape_size,        // size_t *
            out IntPtr           in_shape_ndim,        // const int **
            out IntPtr           in_shape_data,        // const mx_int64 ***
            out size_t           out_shape_size,       // size_t *
            out IntPtr           out_shape_ndim,       // const int **
            out IntPtr           out_shape_data,       // const mx_int64 ***
            out size_t           aux_shape_size,       // size_t *
            out IntPtr           aux_shape_ndim,       // const int **
            out IntPtr           aux_shape_data,       // const mx_int64 ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShapePartial")]
        public static extern int MXSymbolInferShapePartial( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_uint[]            arg_ind_ptr,          // const mx_uint *
            mx_uint[]            arg_shape_data,       // const mx_uint *
            out mx_uint          in_shape_size,        // mx_uint *
            out IntPtr           in_shape_ndim,        // const mx_uint **
            out IntPtr           in_shape_data,        // const mx_uint ***
            out mx_uint          out_shape_size,       // mx_uint *
            out IntPtr           out_shape_ndim,       // const mx_uint **
            out IntPtr           out_shape_data,       // const mx_uint ***
            out mx_uint          aux_shape_size,       // mx_uint *
            out IntPtr           aux_shape_ndim,       // const mx_uint **
            out IntPtr           aux_shape_data,       // const mx_uint ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShapePartial64")]
        public static extern int MXSymbolInferShapePartial64( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_int64[]           arg_ind_ptr,          // const mx_int64 *
            mx_int64[]           arg_shape_data,       // const mx_int64 *
            out size_t           in_shape_size,        // size_t *
            out IntPtr           in_shape_ndim,        // const int **
            out IntPtr           in_shape_data,        // const mx_int64 ***
            out size_t           out_shape_size,       // size_t *
            out IntPtr           out_shape_ndim,       // const int **
            out IntPtr           out_shape_data,       // const mx_int64 ***
            out size_t           aux_shape_size,       // size_t *
            out IntPtr           aux_shape_ndim,       // const int **
            out IntPtr           aux_shape_data,       // const mx_int64 ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShapePartialEx")]
        public static extern int MXSymbolInferShapePartialEx( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_uint[]            arg_ind_ptr,          // const mx_uint *
            int[]                arg_shape_data,       // const int *
            out mx_uint          in_shape_size,        // mx_uint *
            out IntPtr           in_shape_ndim,        // const int **
            out IntPtr           in_shape_data,        // const int ***
            out mx_uint          out_shape_size,       // mx_uint *
            out IntPtr           out_shape_ndim,       // const int **
            out IntPtr           out_shape_data,       // const int ***
            out mx_uint          aux_shape_size,       // mx_uint *
            out IntPtr           aux_shape_ndim,       // const int **
            out IntPtr           aux_shape_data,       // const int ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferShapePartialEx64")]
        public static extern int MXSymbolInferShapePartialEx64( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            mx_int64[]           arg_ind_ptr,          // const mx_int64 *
            mx_int64[]           arg_shape_data,       // const mx_int64 *
            out size_t           in_shape_size,        // size_t *
            out IntPtr           in_shape_ndim,        // const int **
            out IntPtr           in_shape_data,        // const mx_int64 ***
            out size_t           out_shape_size,       // size_t *
            out IntPtr           out_shape_ndim,       // const int **
            out IntPtr           out_shape_data,       // const mx_int64 ***
            out size_t           aux_shape_size,       // size_t *
            out IntPtr           aux_shape_ndim,       // const int **
            out IntPtr           aux_shape_data,       // const mx_int64 ***
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferType")]
        public static extern int MXSymbolInferType( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            int[]                arg_type_data,        // const int *
            out mx_uint          in_type_size,         // mx_uint *
            out IntPtr           in_type_data,         // const int **
            out mx_uint          out_type_size,        // mx_uint *
            out IntPtr           out_type_data,        // const int **
            out mx_uint          aux_type_size,        // mx_uint *
            out IntPtr           aux_type_data,        // const int **
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSymbolInferTypePartial")]
        public static extern int MXSymbolInferTypePartial( // int
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            int[]                arg_type_data,        // const int *
            out mx_uint          in_type_size,         // mx_uint *
            out IntPtr           in_type_data,         // const int **
            out mx_uint          out_type_size,        // mx_uint *
            out IntPtr           out_type_data,        // const int **
            out mx_uint          aux_type_size,        // mx_uint *
            out IntPtr           aux_type_data,        // const int **
            out int              complete              // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXQuantizeSymbol")]
        public static extern int MXQuantizeSymbol( // int
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle,       // SymbolHandle *
            IntPtr               num_excluded_symbols,  // const mx_uint
            out string           excluded_symbols,     // const char **
            IntPtr               num_offline,          // const mx_uint
            out string           offline_params,       // const char **
            string               quantized_dtype,      // const char *
            IntPtr               calib_quantize        // const bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXReducePrecisionSymbol")]
        public static extern int MXReducePrecisionSymbol( // int
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle,       // SymbolHandle *
            mx_uint              num_args,             // mx_uint
            int[]                arg_type_data,        // const int *
            mx_uint              num_ind_ptr,          // mx_uint
            int[]                ind_ptr,              // const int *
            int[]                target_dtype,         // const int *
            IntPtr               cast_optional_params,  // const int
            IntPtr               num_target_dtype_op_names,  // const mx_uint
            IntPtr               num_fp32_op_names,    // const mx_uint
            IntPtr               num_widest_dtype_op_names,  // const mx_uint
            IntPtr               num_conditional_fp32_op_names,  // const mx_uint
            IntPtr               num_excluded_symbols,  // const mx_uint
            IntPtr               num_model_params,     // const mx_uint
            out string           target_dtype_op_names,  // const char **
            out string           fp32_op_names,        // const char **
            out string           widest_dtype_op_names,  // const char **
            out string           conditional_fp32_op_names,  // const char **
            out string           excluded_symbols,     // const char **
            out string           conditional_param_names,  // const char **
            out string           conditional_param_vals,  // const char **
            out string           model_param_names,    // const char **
            out string           arg_names             // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXSetCalibTableToQuantizedSymbol")]
        public static extern int MXSetCalibTableToQuantizedSymbol( // int
            SymbolHandle         qsym_handle,          // SymbolHandle
            IntPtr               num_layers,           // const mx_uint
            out string           layer_names,          // const char **
            IntPtr               low_quantiles,        // const float *
            IntPtr               high_quantiles,       // const float *
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGenBackendSubgraph")]
        public static extern int MXGenBackendSubgraph( // int
            SymbolHandle         sym_handle,           // SymbolHandle
            string               backend,              // const char *
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXGenAtomicSymbolFromSymbol")]
        public static extern int MXGenAtomicSymbolFromSymbol( // int
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorFree")]
        public static extern int MXExecutorFree( // int
            ExecutorHandle       handle                // ExecutorHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorPrint")]
        public static extern int MXExecutorPrint( // int
            ExecutorHandle       handle,               // ExecutorHandle
            out string           out_str               // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorForward")]
        public static extern int MXExecutorForward( // int
            ExecutorHandle       handle,               // ExecutorHandle
            int                  is_train              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorBackward")]
        public static extern int MXExecutorBackward( // int
            ExecutorHandle       handle,               // ExecutorHandle
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    head_grads            // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorBackwardEx")]
        public static extern int MXExecutorBackwardEx( // int
            ExecutorHandle       handle,               // ExecutorHandle
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    head_grads,           // NDArrayHandle *
            int                  is_train              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorOutputs")]
        public static extern int MXExecutorOutputs( // int
            ExecutorHandle       handle,               // ExecutorHandle
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           @out                  // NDArrayHandle **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorBind")]
        public static extern int MXExecutorBind( // int
            SymbolHandle         symbol_handle,        // SymbolHandle
            int                  dev_type,             // int
            int                  dev_id,               // int
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    in_args,              // NDArrayHandle *
            out NDArrayHandle    arg_grad_store,       // NDArrayHandle *
            out mx_uint          grad_req_type,        // mx_uint *
            mx_uint              aux_states_len,       // mx_uint
            out NDArrayHandle    aux_states,           // NDArrayHandle *
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorBindX")]
        public static extern int MXExecutorBindX( // int
            SymbolHandle         symbol_handle,        // SymbolHandle
            int                  dev_type,             // int
            int                  dev_id,               // int
            mx_uint              num_map_keys,         // mx_uint
            out string           map_keys,             // const char **
            int[]                map_dev_types,        // const int *
            int[]                map_dev_ids,          // const int *
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    in_args,              // NDArrayHandle *
            out NDArrayHandle    arg_grad_store,       // NDArrayHandle *
            out mx_uint          grad_req_type,        // mx_uint *
            mx_uint              aux_states_len,       // mx_uint
            out NDArrayHandle    aux_states,           // NDArrayHandle *
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorBindEX")]
        public static extern int MXExecutorBindEX( // int
            SymbolHandle         symbol_handle,        // SymbolHandle
            int                  dev_type,             // int
            int                  dev_id,               // int
            mx_uint              num_map_keys,         // mx_uint
            out string           map_keys,             // const char **
            int[]                map_dev_types,        // const int *
            int[]                map_dev_ids,          // const int *
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    in_args,              // NDArrayHandle *
            out NDArrayHandle    arg_grad_store,       // NDArrayHandle *
            out mx_uint          grad_req_type,        // mx_uint *
            mx_uint              aux_states_len,       // mx_uint
            out NDArrayHandle    aux_states,           // NDArrayHandle *
            ExecutorHandle       shared_exec,          // ExecutorHandle
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorSimpleBind")]
        public static extern int MXExecutorSimpleBind( // int
            SymbolHandle         symbol_handle,        // SymbolHandle
            int                  dev_type,             // int
            int                  dev_id,               // int
            IntPtr               num_g2c_keys,         // const mx_uint
            out string           g2c_keys,             // const char **
            int[]                g2c_dev_types,        // const int *
            int[]                g2c_dev_ids,          // const int *
            IntPtr               provided_grad_req_list_len,  // const mx_uint
            out string           provided_grad_req_names,  // const char **
            out string           provided_grad_req_types,  // const char **
            IntPtr               num_provided_arg_shapes,  // const mx_uint
            out string           provided_arg_shape_names,  // const char **
            mx_uint[]            provided_arg_shape_data,  // const mx_uint *
            mx_uint[]            provided_arg_shape_idx,  // const mx_uint *
            IntPtr               num_provided_arg_dtypes,  // const mx_uint
            out string           provided_arg_dtype_names,  // const char **
            int[]                provided_arg_dtypes,  // const int *
            IntPtr               num_provided_arg_stypes,  // const mx_uint
            out string           provided_arg_stype_names,  // const char **
            int[]                provided_arg_stypes,  // const int *
            IntPtr               num_shared_arg_names,  // const mx_uint
            out string           shared_arg_name_list,  // const char **
            out int              shared_buffer_len,    // int *
            out string           shared_buffer_name_list,  // const char **
            out NDArrayHandle    shared_buffer_handle_list,  // NDArrayHandle *
            out string[]         updated_shared_buffer_name_list,  // const char ***
            out IntPtr           updated_shared_buffer_handle_list,  // NDArrayHandle **
            out mx_uint          num_in_args,          // mx_uint *
            out IntPtr           in_args,              // NDArrayHandle **
            out IntPtr           arg_grads,            // NDArrayHandle **
            out mx_uint          num_aux_states,       // mx_uint *
            out IntPtr           aux_states,           // NDArrayHandle **
            ExecutorHandle       shared_exec_handle,   // ExecutorHandle
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorSimpleBindEx")]
        public static extern int MXExecutorSimpleBindEx( // int
            SymbolHandle         symbol_handle,        // SymbolHandle
            int                  dev_type,             // int
            int                  dev_id,               // int
            IntPtr               num_g2c_keys,         // const mx_uint
            out string           g2c_keys,             // const char **
            int[]                g2c_dev_types,        // const int *
            int[]                g2c_dev_ids,          // const int *
            IntPtr               provided_grad_req_list_len,  // const mx_uint
            out string           provided_grad_req_names,  // const char **
            out string           provided_grad_req_types,  // const char **
            IntPtr               num_provided_arg_shapes,  // const mx_uint
            out string           provided_arg_shape_names,  // const char **
            int[]                provided_arg_shape_data,  // const int *
            mx_uint[]            provided_arg_shape_idx,  // const mx_uint *
            IntPtr               num_provided_arg_dtypes,  // const mx_uint
            out string           provided_arg_dtype_names,  // const char **
            int[]                provided_arg_dtypes,  // const int *
            IntPtr               num_provided_arg_stypes,  // const mx_uint
            out string           provided_arg_stype_names,  // const char **
            int[]                provided_arg_stypes,  // const int *
            IntPtr               num_shared_arg_names,  // const mx_uint
            out string           shared_arg_name_list,  // const char **
            out int              shared_buffer_len,    // int *
            out string           shared_buffer_name_list,  // const char **
            out NDArrayHandle    shared_buffer_handle_list,  // NDArrayHandle *
            out string[]         updated_shared_buffer_name_list,  // const char ***
            out IntPtr           updated_shared_buffer_handle_list,  // NDArrayHandle **
            out mx_uint          num_in_args,          // mx_uint *
            out IntPtr           in_args,              // NDArrayHandle **
            out IntPtr           arg_grads,            // NDArrayHandle **
            out mx_uint          num_aux_states,       // mx_uint *
            out IntPtr           aux_states,           // NDArrayHandle **
            ExecutorHandle       shared_exec_handle,   // ExecutorHandle
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorReshape")]
        public static extern int MXExecutorReshape( // int
            int                  partial_shaping,      // int
            int                  allow_up_sizing,      // int
            int                  dev_type,             // int
            int                  dev_id,               // int
            mx_uint              num_map_keys,         // mx_uint
            out string           map_keys,             // const char **
            int[]                map_dev_types,        // const int *
            int[]                map_dev_ids,          // const int *
            IntPtr               num_provided_arg_shapes,  // const mx_uint
            out string           provided_arg_shape_names,  // const char **
            mx_uint[]            provided_arg_shape_data,  // const mx_uint *
            mx_uint[]            provided_arg_shape_idx,  // const mx_uint *
            out mx_uint          num_in_args,          // mx_uint *
            out IntPtr           in_args,              // NDArrayHandle **
            out IntPtr           arg_grads,            // NDArrayHandle **
            out mx_uint          num_aux_states,       // mx_uint *
            out IntPtr           aux_states,           // NDArrayHandle **
            ExecutorHandle       shared_exec,          // ExecutorHandle
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorReshapeEx")]
        public static extern int MXExecutorReshapeEx( // int
            int                  partial_shaping,      // int
            int                  allow_up_sizing,      // int
            int                  dev_type,             // int
            int                  dev_id,               // int
            mx_uint              num_map_keys,         // mx_uint
            out string           map_keys,             // const char **
            int[]                map_dev_types,        // const int *
            int[]                map_dev_ids,          // const int *
            IntPtr               num_provided_arg_shapes,  // const mx_uint
            out string           provided_arg_shape_names,  // const char **
            int[]                provided_arg_shape_data,  // const int *
            mx_uint[]            provided_arg_shape_idx,  // const mx_uint *
            out mx_uint          num_in_args,          // mx_uint *
            out IntPtr           in_args,              // NDArrayHandle **
            out IntPtr           arg_grads,            // NDArrayHandle **
            out mx_uint          num_aux_states,       // mx_uint *
            out IntPtr           aux_states,           // NDArrayHandle **
            ExecutorHandle       shared_exec,          // ExecutorHandle
            out ExecutorHandle   @out                  // ExecutorHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorGetOptimizedSymbol")]
        public static extern int MXExecutorGetOptimizedSymbol( // int
            ExecutorHandle       handle,               // ExecutorHandle
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorSetMonitorCallback")]
        public static extern int MXExecutorSetMonitorCallback( // int
            ExecutorHandle       handle,               // ExecutorHandle
            IntPtr               callback,             // ExecutorMonitorCallback
            IntPtr               callback_handle       // void *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXExecutorSetMonitorCallbackEX")]
        public static extern int MXExecutorSetMonitorCallbackEX( // int
            ExecutorHandle       handle,               // ExecutorHandle
            IntPtr               callback,             // ExecutorMonitorCallback
            IntPtr               callback_handle,      // void *
            IntPtr               monitor_all           // bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXListDataIters")]
        public static extern int MXListDataIters( // int
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_array             // DataIterCreator **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterCreateIter")]
        public static extern int MXDataIterCreateIter( // int
            DataIterCreator      handle,               // DataIterCreator
            mx_uint              num_param,            // mx_uint
            out string           keys,                 // const char **
            out string           vals,                 // const char **
            out DataIterHandle   @out                  // DataIterHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterGetIterInfo")]
        public static extern int MXDataIterGetIterInfo( // int
            DataIterCreator      creator,              // DataIterCreator
            out string           name,                 // const char **
            out string           description,          // const char **
            out mx_uint          num_args,             // mx_uint *
            out string[]         arg_names,            // const char ***
            out string[]         arg_type_infos,       // const char ***
            out string[]         arg_descriptions      // const char ***
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterFree")]
        public static extern int MXDataIterFree( // int
            DataIterHandle       handle                // DataIterHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterNext")]
        public static extern int MXDataIterNext( // int
            DataIterHandle       handle,               // DataIterHandle
            out int              @out                  // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterBeforeFirst")]
        public static extern int MXDataIterBeforeFirst( // int
            DataIterHandle       handle                // DataIterHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterGetData")]
        public static extern int MXDataIterGetData( // int
            DataIterHandle       handle,               // DataIterHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterGetIndex")]
        public static extern int MXDataIterGetIndex( // int
            DataIterHandle       handle,               // DataIterHandle
            out IntPtr           out_index,            // uint64_t **
            out uint64_t         out_size              // uint64_t *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterGetPadNum")]
        public static extern int MXDataIterGetPadNum( // int
            DataIterHandle       handle,               // DataIterHandle
            out int              pad                   // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXDataIterGetLabel")]
        public static extern int MXDataIterGetLabel( // int
            DataIterHandle       handle,               // DataIterHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXInitPSEnv")]
        public static extern int MXInitPSEnv( // int
            mx_uint              num_vars,             // mx_uint
            out string           keys,                 // const char **
            out string           vals                  // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreCreate")]
        public static extern int MXKVStoreCreate( // int
            string               type,                 // const char *
            out IntPtr           @out                  // KVStoreHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreSetGradientCompression")]
        public static extern int MXKVStoreSetGradientCompression( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num_params,           // mx_uint
            out string           keys,                 // const char **
            out string           vals                  // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreFree")]
        public static extern int MXKVStoreFree( // int
            IntPtr               handle                // KVStoreHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreInit")]
        public static extern int MXKVStoreInit( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreInitEx")]
        public static extern int MXKVStoreInitEx( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePush")]
        public static extern int MXKVStorePush( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePushEx")]
        public static extern int MXKVStorePushEx( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePullWithSparse")]
        public static extern int MXKVStorePullWithSparse( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority,             // int
            IntPtr               ignore_sparse         // bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePullWithSparseEx")]
        public static extern int MXKVStorePullWithSparseEx( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority,             // int
            IntPtr               ignore_sparse         // bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePull")]
        public static extern int MXKVStorePull( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePullEx")]
        public static extern int MXKVStorePullEx( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePullRowSparse")]
        public static extern int MXKVStorePullRowSparse( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            IntPtr               row_ids,              // const NDArrayHandle *
            int                  priority              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStorePullRowSparseEx")]
        public static extern int MXKVStorePullRowSparseEx( // int
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            IntPtr               row_ids,              // const NDArrayHandle *
            int                  priority              // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreSetUpdater")]
        public static extern int MXKVStoreSetUpdater( // int
            IntPtr               handle,               // KVStoreHandle
            IntPtr               updater,              // MXKVStoreUpdater
            IntPtr               updater_handle        // void *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreSetUpdaterEx")]
        public static extern int MXKVStoreSetUpdaterEx( // int
            IntPtr               handle,               // KVStoreHandle
            IntPtr               updater,              // MXKVStoreUpdater
            IntPtr               str_updater,          // MXKVStoreStrUpdater
            IntPtr               updater_handle        // void *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreGetType")]
        public static extern int MXKVStoreGetType( // int
            IntPtr               handle,               // KVStoreHandle
            out string           type                  // const char **
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreGetRank")]
        public static extern int MXKVStoreGetRank( // int
            IntPtr               handle,               // KVStoreHandle
            out int              ret                   // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreGetGroupSize")]
        public static extern int MXKVStoreGetGroupSize( // int
            IntPtr               handle,               // KVStoreHandle
            out int              ret                   // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreIsWorkerNode")]
        public static extern int MXKVStoreIsWorkerNode( // int
            out int              ret                   // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreIsServerNode")]
        public static extern int MXKVStoreIsServerNode( // int
            out int              ret                   // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreIsSchedulerNode")]
        public static extern int MXKVStoreIsSchedulerNode( // int
            out int              ret                   // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreBarrier")]
        public static extern int MXKVStoreBarrier( // int
            IntPtr               handle                // KVStoreHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreSetBarrierBeforeExit")]
        public static extern int MXKVStoreSetBarrierBeforeExit( // int
            IntPtr               handle,               // KVStoreHandle
            IntPtr               barrier_before_exit   // const int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreRunServer")]
        public static extern int MXKVStoreRunServer( // int
            IntPtr               handle,               // KVStoreHandle
            IntPtr               controller,           // MXKVStoreServerController
            IntPtr               controller_handle     // void *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreSendCommmandToServers")]
        public static extern int MXKVStoreSendCommmandToServers( // int
            IntPtr               handle,               // KVStoreHandle
            int                  cmd_id,               // int
            string               cmd_body              // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXKVStoreGetNumDeadNode")]
        public static extern int MXKVStoreGetNumDeadNode( // int
            IntPtr               handle,               // KVStoreHandle
            IntPtr               node_id,              // const int
            out int              number,               // int *
            IntPtr               timeout_sec           // const int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOWriterCreate")]
        public static extern int MXRecordIOWriterCreate( // int
            string               uri,                  // const char *
            out IntPtr           @out                  // RecordIOHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOWriterFree")]
        public static extern int MXRecordIOWriterFree( // int
            IntPtr               handle                // RecordIOHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOWriterWriteRecord")]
        public static extern int MXRecordIOWriterWriteRecord( // int
            IntPtr               handle,               // RecordIOHandle
            string               buf,                  // const char *
            size_t               size                  // size_t
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOWriterTell")]
        public static extern int MXRecordIOWriterTell( // int
            IntPtr               handle,               // RecordIOHandle
            out size_t           pos                   // size_t *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOReaderCreate")]
        public static extern int MXRecordIOReaderCreate( // int
            string               uri,                  // const char *
            out IntPtr           @out                  // RecordIOHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOReaderFree")]
        public static extern int MXRecordIOReaderFree( // int
            IntPtr               handle                // RecordIOHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOReaderReadRecord")]
        public static extern int MXRecordIOReaderReadRecord( // int
            IntPtr               handle,               // RecordIOHandle
            out IntPtr           buf,                  // char const **
            out size_t           size                  // size_t *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOReaderSeek")]
        public static extern int MXRecordIOReaderSeek( // int
            IntPtr               handle,               // RecordIOHandle
            size_t               pos                   // size_t
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRecordIOReaderTell")]
        public static extern int MXRecordIOReaderTell( // int
            IntPtr               handle,               // RecordIOHandle
            out size_t           pos                   // size_t *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcCreate")]
        public static extern int MXRtcCreate( // int
            out IntPtr           name,                 // char *
            mx_uint              num_input,            // mx_uint
            mx_uint              num_output,           // mx_uint
            string[]             input_names,          // char **
            out string           output_names,         // char **
            IntPtr               inputs,               // NDArrayHandle *
            ref NDArrayHandle    outputs,              // NDArrayHandle *
            out IntPtr           kernel,               // char *
            out IntPtr           @out                  // RtcHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcPush")]
        public static extern int MXRtcPush( // int
            IntPtr               handle,               // RtcHandle
            mx_uint              num_input,            // mx_uint
            mx_uint              num_output,           // mx_uint
            IntPtr               inputs,               // NDArrayHandle *
            ref NDArrayHandle    outputs,              // NDArrayHandle *
            mx_uint              gridDimX,             // mx_uint
            mx_uint              gridDimY,             // mx_uint
            mx_uint              gridDimZ,             // mx_uint
            mx_uint              blockDimX,            // mx_uint
            mx_uint              blockDimY,            // mx_uint
            mx_uint              blockDimZ             // mx_uint
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcFree")]
        public static extern int MXRtcFree( // int
            IntPtr               handle                // RtcHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXCustomOpRegister")]
        public static extern int MXCustomOpRegister( // int
            string               op_type,              // const char *
            IntPtr               creator               // CustomOpPropCreator
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXCustomFunctionRecord")]
        public static extern int MXCustomFunctionRecord( // int
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int
            ref NDArrayHandle    outputs,              // NDArrayHandle *
            out IntPtr           callbacks             // struct MXCallbackList *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcCudaModuleCreate")]
        public static extern int MXRtcCudaModuleCreate( // int
            string               source,               // const char *
            int                  num_options,          // int
            out string           options,              // const char **
            int                  num_exports,          // int
            out string           exports,              // const char **
            out IntPtr           @out                  // CudaModuleHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcCudaModuleFree")]
        public static extern int MXRtcCudaModuleFree( // int
            IntPtr               handle                // CudaModuleHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcCudaKernelCreate")]
        public static extern int MXRtcCudaKernelCreate( // int
            IntPtr               handle,               // CudaModuleHandle
            string               name,                 // const char *
            int                  num_args,             // int
            out int              is_ndarray,           // int *
            out int              is_const,             // int *
            out int              arg_types,            // int *
            out IntPtr           @out                  // CudaKernelHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcCudaKernelFree")]
        public static extern int MXRtcCudaKernelFree( // int
            IntPtr               handle                // CudaKernelHandle
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXRtcCudaKernelCall")]
        public static extern int MXRtcCudaKernelCall( // int
            IntPtr               handle,               // CudaKernelHandle
            int                  dev_id,               // int
            out IntPtr           args,                 // void **
            mx_uint              grid_dim_x,           // mx_uint
            mx_uint              grid_dim_y,           // mx_uint
            mx_uint              grid_dim_z,           // mx_uint
            mx_uint              block_dim_x,          // mx_uint
            mx_uint              block_dim_y,          // mx_uint
            mx_uint              block_dim_z,          // mx_uint
            mx_uint              shared_mem            // mx_uint
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayGetSharedMemHandle")]
        public static extern int MXNDArrayGetSharedMemHandle( // int
            NDArrayHandle        handle,               // NDArrayHandle
            out int              shared_pid,           // int *
            out int              shared_id             // int *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateFromSharedMem")]
        public static extern int MXNDArrayCreateFromSharedMem( // int
            int                  shared_pid,           // int
            int                  shared_id,            // int
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXStorageEmptyCache")]
        public static extern int MXStorageEmptyCache( // int
            int                  dev_type,             // int
            int                  dev_id                // int
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXNDArrayCreateFromSharedMemEx")]
        public static extern int MXNDArrayCreateFromSharedMemEx( // int
            int                  shared_pid,           // int
            int                  shared_id,            // int
            int[]                shape,                // const int *
            int                  ndim,                 // int
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXEnginePushAsync")]
        public static extern int MXEnginePushAsync( // int
            IntPtr               async_func,           // EngineAsyncFunc
            IntPtr               func_param,           // void *
            IntPtr               deleter,              // EngineFuncParamDeleter
            IntPtr               ctx_handle,           // ContextHandle
            IntPtr               const_vars_handle,    // EngineVarHandle
            int                  num_const_vars,       // int
            IntPtr               mutable_vars_handle,  // EngineVarHandle
            int                  num_mutable_vars,     // int
            IntPtr               prop_handle,          // EngineFnPropertyHandle
            int                  priority,             // int
            string               opr_name,             // const char *
            IntPtr               wait                  // bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXEnginePushSync")]
        public static extern int MXEnginePushSync( // int
            IntPtr               sync_func,            // EngineSyncFunc
            IntPtr               func_param,           // void *
            IntPtr               deleter,              // EngineFuncParamDeleter
            IntPtr               ctx_handle,           // ContextHandle
            IntPtr               const_vars_handle,    // EngineVarHandle
            int                  num_const_vars,       // int
            IntPtr               mutable_vars_handle,  // EngineVarHandle
            int                  num_mutable_vars,     // int
            IntPtr               prop_handle,          // EngineFnPropertyHandle
            int                  priority,             // int
            string               opr_name              // const char *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXShallowCopyNDArray")]
        public static extern int MXShallowCopyNDArray( // int
            NDArrayHandle        src,                  // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXShallowCopySymbol")]
        public static extern int MXShallowCopySymbol( // int
            SymbolHandle         src,                  // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXEnginePushAsyncND")]
        public static extern int MXEnginePushAsyncND( // int
            IntPtr               async_func,           // EngineAsyncFunc
            IntPtr               func_param,           // void *
            IntPtr               deleter,              // EngineFuncParamDeleter
            IntPtr               ctx_handle,           // ContextHandle
            out NDArrayHandle    const_nds_handle,     // NDArrayHandle *
            int                  num_const_nds,        // int
            out NDArrayHandle    mutable_nds_handle,   // NDArrayHandle *
            int                  num_mutable_nds,      // int
            IntPtr               prop_handle,          // EngineFnPropertyHandle
            int                  priority,             // int
            string               opr_name,             // const char *
            IntPtr               wait                  // bool
        );

        [DllImport("libmxnet.dll", EntryPoint = "MXEnginePushSyncND")]
        public static extern int MXEnginePushSyncND( // int
            IntPtr               sync_func,            // EngineSyncFunc
            IntPtr               func_param,           // void *
            IntPtr               deleter,              // EngineFuncParamDeleter
            IntPtr               ctx_handle,           // ContextHandle
            out NDArrayHandle    const_nds_handle,     // NDArrayHandle *
            int                  num_const_nds,        // int
            out NDArrayHandle    mutable_nds_handle,   // NDArrayHandle *
            int                  num_mutable_nds,      // int
            IntPtr               prop_handle,          // EngineFnPropertyHandle
            int                  priority,             // int
            string               opr_name              // const char *
        );

    }
}
