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
    public static class CApi
    {
        public static void MXLoadLib(
            string               path                  // const char *
        )
        {
            var resultCode = CApiDeclaration.MXLoadLib(path);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXLibInfoFeatures(
            out IntPtr           libFeature,           // const struct LibFeature **
            out size_t           size                  // size_t *
        )
        {
            var resultCode = CApiDeclaration.MXLibInfoFeatures(out libFeature, out size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRandomSeed(
            int                  seed                  // int
        )
        {
            var resultCode = CApiDeclaration.MXRandomSeed(seed);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRandomSeedContext(
            int                  seed,                 // int
            int                  dev_type,             // int
            int                  dev_id                // int
        )
        {
            var resultCode = CApiDeclaration.MXRandomSeedContext(seed, dev_type, dev_id);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNotifyShutdown(
        )
        {
            var resultCode = CApiDeclaration.MXNotifyShutdown();
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetProcessProfilerConfig(
            int                  num_params,           // int
            IntPtr               keys,                 // const char *const *
            IntPtr               vals,                 // const char *const *
            IntPtr               kvstoreHandle         // KVStoreHandle
        )
        {
            var resultCode = CApiDeclaration.MXSetProcessProfilerConfig(num_params, keys, vals, kvstoreHandle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetProfilerConfig(
            int                  num_params,           // int
            IntPtr               keys,                 // const char *const *
            IntPtr               vals                  // const char *const *
        )
        {
            var resultCode = CApiDeclaration.MXSetProfilerConfig(num_params, keys, vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetProcessProfilerState(
            int                  state,                // int
            int                  profile_process,      // int
            IntPtr               kvStoreHandle         // KVStoreHandle
        )
        {
            var resultCode = CApiDeclaration.MXSetProcessProfilerState(state, profile_process, kvStoreHandle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetProfilerState(
            int                  state                 // int
        )
        {
            var resultCode = CApiDeclaration.MXSetProfilerState(state);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDumpProcessProfile(
            int                  finished,             // int
            int                  profile_process,      // int
            IntPtr               kvStoreHandle         // KVStoreHandle
        )
        {
            var resultCode = CApiDeclaration.MXDumpProcessProfile(finished, profile_process, kvStoreHandle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDumpProfile(
            int                  finished              // int
        )
        {
            var resultCode = CApiDeclaration.MXDumpProfile(finished);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAggregateProfileStatsPrint(
            out string           out_str,              // const char **
            int                  reset                 // int
        )
        {
            var resultCode = CApiDeclaration.MXAggregateProfileStatsPrint(out out_str, reset);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAggregateProfileStatsPrintEx(
            out string           out_str,              // const char **
            int                  reset,                // int
            int                  format,               // int
            int                  sort_by,              // int
            int                  ascending             // int
        )
        {
            var resultCode = CApiDeclaration.MXAggregateProfileStatsPrintEx(out out_str, reset, format, sort_by, ascending);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProcessProfilePause(
            int                  paused,               // int
            int                  profile_process,      // int
            IntPtr               kvStoreHandle         // KVStoreHandle
        )
        {
            var resultCode = CApiDeclaration.MXProcessProfilePause(paused, profile_process, kvStoreHandle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfilePause(
            int                  paused                // int
        )
        {
            var resultCode = CApiDeclaration.MXProfilePause(paused);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileCreateDomain(
            string               domain,               // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        )
        {
            var resultCode = CApiDeclaration.MXProfileCreateDomain(domain, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileCreateTask(
            ProfileHandle        domain,               // ProfileHandle
            string               task_name,            // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        )
        {
            var resultCode = CApiDeclaration.MXProfileCreateTask(domain, task_name, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileCreateFrame(
            ProfileHandle        domain,               // ProfileHandle
            string               frame_name,           // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        )
        {
            var resultCode = CApiDeclaration.MXProfileCreateFrame(domain, frame_name, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileCreateEvent(
            string               event_name,           // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        )
        {
            var resultCode = CApiDeclaration.MXProfileCreateEvent(event_name, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileCreateCounter(
            ProfileHandle        domain,               // ProfileHandle
            string               counter_name,         // const char *
            out ProfileHandle    @out                  // ProfileHandle *
        )
        {
            var resultCode = CApiDeclaration.MXProfileCreateCounter(domain, counter_name, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileDestroyHandle(
            ProfileHandle        frame_handle          // ProfileHandle
        )
        {
            var resultCode = CApiDeclaration.MXProfileDestroyHandle(frame_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileDurationStart(
            ProfileHandle        duration_handle       // ProfileHandle
        )
        {
            var resultCode = CApiDeclaration.MXProfileDurationStart(duration_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileDurationStop(
            ProfileHandle        duration_handle       // ProfileHandle
        )
        {
            var resultCode = CApiDeclaration.MXProfileDurationStop(duration_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileSetCounter(
            ProfileHandle        counter_handle,       // ProfileHandle
            uint64_t             value                 // uint64_t
        )
        {
            var resultCode = CApiDeclaration.MXProfileSetCounter(counter_handle, value);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileAdjustCounter(
            ProfileHandle        counter_handle,       // ProfileHandle
            IntPtr               value                 // int64_t
        )
        {
            var resultCode = CApiDeclaration.MXProfileAdjustCounter(counter_handle, value);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXProfileSetMarker(
            ProfileHandle        domain,               // ProfileHandle
            string               instant_marker_name,  // const char *
            string               scope                 // const char *
        )
        {
            var resultCode = CApiDeclaration.MXProfileSetMarker(domain, instant_marker_name, scope);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetNumOMPThreads(
            int                  thread_num            // int
        )
        {
            var resultCode = CApiDeclaration.MXSetNumOMPThreads(thread_num);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXEngineSetBulkSize(
            int                  bulk_size,            // int
            out int              prev_bulk_size        // int *
        )
        {
            var resultCode = CApiDeclaration.MXEngineSetBulkSize(bulk_size, out prev_bulk_size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGetGPUCount(
            out int              @out                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXGetGPUCount(out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGetGPUMemoryInformation(
            int                  dev,                  // int
            out int              free_mem,             // int *
            out int              total_mem             // int *
        )
        {
            var resultCode = CApiDeclaration.MXGetGPUMemoryInformation(dev, out free_mem, out total_mem);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGetGPUMemoryInformation64(
            int                  dev,                  // int
            out uint64_t         free_mem,             // uint64_t *
            out uint64_t         total_mem             // uint64_t *
        )
        {
            var resultCode = CApiDeclaration.MXGetGPUMemoryInformation64(dev, out free_mem, out total_mem);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGetVersion(
            out int              @out                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXGetVersion(out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateNone(
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateNone(out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreate(
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreate(shape, ndim, dev_type, dev_id, delay_alloc, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateEx(
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateEx(shape, ndim, dev_type, dev_id, delay_alloc, dtype, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateEx64(
            mx_int64[]           shape,                // const mx_int64 *
            int                  ndim,                 // int
            int                  dev_type,             // int
            int                  dev_id,               // int
            int                  delay_alloc,          // int
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateEx64(shape, ndim, dev_type, dev_id, delay_alloc, dtype, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateSparseEx(
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
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateSparseEx(storage_type, shape, ndim, dev_type, dev_id, delay_alloc, dtype, num_aux, out aux_type, out aux_ndims, aux_shape, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateSparseEx64(
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
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateSparseEx64(storage_type, shape, ndim, dev_type, dev_id, delay_alloc, dtype, num_aux, out aux_type, out aux_ndims, aux_shape, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayLoadFromRawBytes(
            IntPtr               buf,                  // const void *
            size_t               size,                 // size_t
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayLoadFromRawBytes(buf, size, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySaveRawBytes(
            NDArrayHandle        handle,               // NDArrayHandle
            out size_t           out_size,             // size_t *
            out string           out_buf               // const char **
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySaveRawBytes(handle, out out_size, out out_buf);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySave(
            string               fname,                // const char *
            mx_uint              num_args,             // mx_uint
            out NDArrayHandle    args,                 // NDArrayHandle *
            out string           keys                  // const char **
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySave(fname, num_args, out args, out keys);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayLoad(
            string               fname,                // const char *
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_arr,              // NDArrayHandle **
            out mx_uint          out_name_size,        // mx_uint *
            out string[]         out_names             // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayLoad(fname, out out_size, out out_arr, out out_name_size, out out_names);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayLoad64(
            string               fname,                // const char *
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_arr,              // NDArrayHandle **
            out IntPtr           out_name_size,        // mx_int64 *
            out string[]         out_names             // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayLoad64(fname, out out_size, out out_arr, out out_name_size, out out_names);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayLoadFromBuffer(
            IntPtr               ndarray_buffer,       // const void *
            size_t               size,                 // size_t
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_arr,              // NDArrayHandle **
            out mx_uint          out_name_size,        // mx_uint *
            out string[]         out_names             // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayLoadFromBuffer(ndarray_buffer, size, out out_size, out out_arr, out out_name_size, out out_names);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayLoadFromBuffer64(
            IntPtr               ndarray_buffer,       // const void *
            size_t               size,                 // size_t
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_arr,              // NDArrayHandle **
            out IntPtr           out_name_size,        // mx_int64 *
            out string[]         out_names             // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayLoadFromBuffer64(ndarray_buffer, size, out out_size, out out_arr, out out_name_size, out out_names);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySyncCopyFromCPU(
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               data,                 // const void *
            size_t               size                  // size_t
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySyncCopyFromCPU(handle, data, size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySyncCopyToCPU(
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               data,                 // void *
            size_t               size                  // size_t
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySyncCopyToCPU(handle, data, size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySyncCopyFromNDArray(
            NDArrayHandle        handle_dst,           // NDArrayHandle
            IntPtr               handle_src,           // const NDArrayHandle
            IntPtr               i                     // const int
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySyncCopyFromNDArray(handle_dst, handle_src, i);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySyncCheckFormat(
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               full_check            // const bool
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySyncCheckFormat(handle, full_check);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayWaitToRead(
            NDArrayHandle        handle                // NDArrayHandle
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayWaitToRead(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayWaitToWrite(
            NDArrayHandle        handle                // NDArrayHandle
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayWaitToWrite(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayWaitAll(
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayWaitAll();
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayFree(
            NDArrayHandle        handle                // NDArrayHandle
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySlice(
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              slice_begin,          // mx_uint
            mx_uint              slice_end,            // mx_uint
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySlice(handle, slice_begin, slice_end, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayAt(
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              idx,                  // mx_uint
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayAt(handle, idx, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetStorageType(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_storage_type      // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetStorageType(handle, out out_storage_type);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayReshape(
            NDArrayHandle        handle,               // NDArrayHandle
            int                  ndim,                 // int
            out int              dims,                 // int *
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayReshape(handle, ndim, out dims, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayReshape64(
            NDArrayHandle        handle,               // NDArrayHandle
            int                  ndim,                 // int
            out IntPtr           dims,                 // dim_t *
            IntPtr               reverse,              // bool
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayReshape64(handle, ndim, out dims, reverse, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetShape(
            NDArrayHandle        handle,               // NDArrayHandle
            out mx_uint          out_dim,              // mx_uint *
            out IntPtr           out_pdata             // const mx_uint **
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetShape(handle, out out_dim, out out_pdata);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetShape64(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dim,              // int *
            out IntPtr           out_pdata             // const int64_t **
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetShape64(handle, out out_dim, out out_pdata);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetShapeEx(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dim,              // int *
            out IntPtr           out_pdata             // const int **
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetShapeEx(handle, out out_dim, out out_pdata);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetShapeEx64(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dim,              // int *
            out IntPtr           out_pdata             // const mx_int64 **
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetShapeEx64(handle, out out_dim, out out_pdata);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetData(
            NDArrayHandle        handle,               // NDArrayHandle
            out IntPtr           out_pdata             // void **
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetData(handle, out out_pdata);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayToDLPack(
            NDArrayHandle        handle,               // NDArrayHandle
            out IntPtr           out_dlpack            // DLManagedTensorHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayToDLPack(handle, out out_dlpack);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayFromDLPack(
            IntPtr               dlpack,               // DLManagedTensorHandle
            out NDArrayHandle    out_handle            // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayFromDLPack(dlpack, out out_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayFromDLPackEx(
            IntPtr               dlpack,               // DLManagedTensorHandle
            IntPtr               transient_handle,     // const bool
            out NDArrayHandle    out_handle            // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayFromDLPackEx(dlpack, transient_handle, out out_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCallDLPackDeleter(
            IntPtr               dlpack                // DLManagedTensorHandle
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCallDLPackDeleter(dlpack);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetDType(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dtype             // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetDType(handle, out out_dtype);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetAuxType(
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              i,                    // mx_uint
            out int              out_type              // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetAuxType(handle, i, out out_type);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetAuxType64(
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               i,                    // mx_int64
            out int              out_type              // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetAuxType64(handle, i, out out_type);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetAuxNDArray(
            NDArrayHandle        handle,               // NDArrayHandle
            mx_uint              i,                    // mx_uint
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetAuxNDArray(handle, i, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetAuxNDArray64(
            NDArrayHandle        handle,               // NDArrayHandle
            IntPtr               i,                    // mx_int64
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetAuxNDArray64(handle, i, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetDataNDArray(
            NDArrayHandle        handle,               // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetDataNDArray(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetContext(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              out_dev_type,         // int *
            out int              out_dev_id            // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetContext(handle, out out_dev_type, out out_dev_id);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetGrad(
            NDArrayHandle        handle,               // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetGrad(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayDetach(
            NDArrayHandle        handle,               // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayDetach(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArraySetGradState(
            NDArrayHandle        handle,               // NDArrayHandle
            int                  state                 // int
        )
        {
            var resultCode = CApiDeclaration.MXNDArraySetGradState(handle, state);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetGradState(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              @out                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetGradState(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXListFunctions(
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_array             // FunctionHandle **
        )
        {
            var resultCode = CApiDeclaration.MXListFunctions(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXListFunctions64(
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_array             // FunctionHandle **
        )
        {
            var resultCode = CApiDeclaration.MXListFunctions64(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGetFunction(
            string               name,                 // const char *
            out FunctionHandle   @out                  // FunctionHandle *
        )
        {
            var resultCode = CApiDeclaration.MXGetFunction(name, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXFuncGetInfo(
            FunctionHandle       fun,                  // FunctionHandle
            out string           name,                 // const char **
            out string           description,          // const char **
            out mx_uint          num_args,             // mx_uint *
            out string[]         arg_names,            // const char ***
            out string[]         arg_type_infos,       // const char ***
            out string[]         arg_descriptions,     // const char ***
            out string           return_type           // const char **
        )
        {
            var resultCode = CApiDeclaration.MXFuncGetInfo(fun, out name, out description, out num_args, out arg_names, out arg_type_infos, out arg_descriptions, out return_type);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXFuncDescribe(
            FunctionHandle       fun,                  // FunctionHandle
            out mx_uint          num_use_vars,         // mx_uint *
            out mx_uint          num_scalars,          // mx_uint *
            out mx_uint          num_mutate_vars,      // mx_uint *
            out int              type_mask             // int *
        )
        {
            var resultCode = CApiDeclaration.MXFuncDescribe(fun, out num_use_vars, out num_scalars, out num_mutate_vars, out type_mask);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXFuncInvoke(
            FunctionHandle       fun,                  // FunctionHandle
            out NDArrayHandle    use_vars,             // NDArrayHandle *
            out mx_float         scalar_args,          // mx_float *
            out NDArrayHandle    mutate_vars           // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXFuncInvoke(fun, out use_vars, out scalar_args, out mutate_vars);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXFuncInvokeEx(
            FunctionHandle       fun,                  // FunctionHandle
            out NDArrayHandle    use_vars,             // NDArrayHandle *
            out mx_float         scalar_args,          // mx_float *
            out NDArrayHandle    mutate_vars,          // NDArrayHandle *
            int                  num_params,           // int
            string[]             param_keys,           // char **
            string[]             param_vals            // char **
        )
        {
            var resultCode = CApiDeclaration.MXFuncInvokeEx(fun, out use_vars, out scalar_args, out mutate_vars, num_params, param_keys, param_vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXImperativeInvoke(
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs,              // NDArrayHandle **
            int                  num_params,           // int
            string[]             param_keys,           // const char **
            string[]             param_vals            // const char **
        )
        {
            var resultCode = CApiDeclaration.MXImperativeInvoke(creator, num_inputs, inputs, ref num_outputs, ref outputs, num_params, param_keys, param_vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXImperativeInvokeEx(
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs,              // NDArrayHandle **
            int                  num_params,           // int
            string[]             param_keys,           // const char **
            string[]             param_vals,           // const char **
            out IntPtr           out_stypes            // const int **
        )
        {
            var resultCode = CApiDeclaration.MXImperativeInvokeEx(creator, num_inputs, inputs, ref num_outputs, ref outputs, num_params, param_keys, param_vals, out out_stypes);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradSetIsRecording(
            int                  is_recording,         // int
            out int              prev                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradSetIsRecording(is_recording, out prev);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradSetIsTraining(
            int                  is_training,          // int
            out int              prev                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradSetIsTraining(is_training, out prev);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradIsRecording(
            out IntPtr           curr                  // bool *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradIsRecording(out curr);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradIsTraining(
            out IntPtr           curr                  // bool *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradIsTraining(out curr);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXIsNumpyShape(
            out IntPtr           curr                  // bool *
        )
        {
            var resultCode = CApiDeclaration.MXIsNumpyShape(out curr);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetIsNumpyShape(
            int                  is_np_shape,          // int
            out int              prev                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXSetIsNumpyShape(is_np_shape, out prev);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradMarkVariables(
            mx_uint              num_var,              // mx_uint
            out NDArrayHandle    var_handles,          // NDArrayHandle *
            out mx_uint          reqs_array,           // mx_uint *
            out NDArrayHandle    grad_handles          // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradMarkVariables(num_var, out var_handles, out reqs_array, out grad_handles);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradComputeGradient(
            mx_uint              num_output,           // mx_uint
            out NDArrayHandle    output_handles        // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradComputeGradient(num_output, out output_handles);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradBackward(
            mx_uint              num_output,           // mx_uint
            out NDArrayHandle    output_handles,       // NDArrayHandle *
            out NDArrayHandle    ograd_handles,        // NDArrayHandle *
            int                  retain_graph          // int
        )
        {
            var resultCode = CApiDeclaration.MXAutogradBackward(num_output, out output_handles, out ograd_handles, retain_graph);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradBackwardEx(
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
        )
        {
            var resultCode = CApiDeclaration.MXAutogradBackwardEx(num_output, out output_handles, out ograd_handles, num_variables, out var_handles, retain_graph, create_graph, is_train, out grad_handles, out grad_stypes);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXAutogradGetSymbol(
            NDArrayHandle        handle,               // NDArrayHandle
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXAutogradGetSymbol(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXCreateCachedOp(
            SymbolHandle         handle,               // SymbolHandle
            out IntPtr           @out                  // CachedOpHandle *
        )
        {
            var resultCode = CApiDeclaration.MXCreateCachedOp(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXCreateCachedOpEx(
            SymbolHandle         handle,               // SymbolHandle
            int                  num_flags,            // int
            out string           keys,                 // const char **
            out string           vals,                 // const char **
            out IntPtr           @out                  // CachedOpHandle *
        )
        {
            var resultCode = CApiDeclaration.MXCreateCachedOpEx(handle, num_flags, out keys, out vals, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXFreeCachedOp(
            IntPtr               handle                // CachedOpHandle
        )
        {
            var resultCode = CApiDeclaration.MXFreeCachedOp(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXInvokeCachedOp(
            IntPtr               handle,               // CachedOpHandle
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs               // NDArrayHandle **
        )
        {
            var resultCode = CApiDeclaration.MXInvokeCachedOp(handle, num_inputs, inputs, ref num_outputs, ref outputs);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXInvokeCachedOpEx(
            IntPtr               handle,               // CachedOpHandle
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int *
            ref IntPtr           outputs,              // NDArrayHandle **
            out IntPtr           out_stypes            // const int **
        )
        {
            var resultCode = CApiDeclaration.MXInvokeCachedOpEx(handle, num_inputs, inputs, ref num_outputs, ref outputs, out out_stypes);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXListAllOpNames(
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_array             // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXListAllOpNames(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXListAllOpNames64(
            out IntPtr           out_size,             // mx_int64 *
            out string[]         out_array             // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXListAllOpNames64(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListAtomicSymbolCreators(
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_array             // AtomicSymbolCreator **
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListAtomicSymbolCreators(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListAtomicSymbolCreators64(
            out IntPtr           out_size,             // mx_int64 *
            out IntPtr           out_array             // AtomicSymbolCreator **
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListAtomicSymbolCreators64(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetAtomicSymbolName(
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            out string           name                  // const char **
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetAtomicSymbolName(creator, out name);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetInputSymbols(
            SymbolHandle         sym,                  // SymbolHandle
            IntPtr               inputs,               // SymbolHandle **
            IntPtr               input_size            // int *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetInputSymbols(sym, inputs, input_size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCutSubgraph(
            SymbolHandle         sym,                  // SymbolHandle
            IntPtr               inputs,               // SymbolHandle **
            IntPtr               input_size            // int *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCutSubgraph(sym, inputs, input_size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetAtomicSymbolInfo(
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            out string           name,                 // const char **
            out string           description,          // const char **
            out mx_uint          num_args,             // mx_uint *
            out string[]         arg_names,            // const char ***
            out string[]         arg_type_infos,       // const char ***
            out string[]         arg_descriptions,     // const char ***
            out string           key_var_num_args,     // const char **
            out string           return_type           // const char **
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetAtomicSymbolInfo(creator, out name, out description, out num_args, out arg_names, out arg_type_infos, out arg_descriptions, out key_var_num_args, out return_type);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCreateAtomicSymbol(
            AtomicSymbolCreator  creator,              // AtomicSymbolCreator
            mx_uint              num_param,            // mx_uint
            out string           keys,                 // const char **
            out string           vals,                 // const char **
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCreateAtomicSymbol(creator, num_param, out keys, out vals, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCreateVariable(
            string               name,                 // const char *
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCreateVariable(name, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCreateGroup(
            mx_uint              num_symbols,          // mx_uint
            out SymbolHandle     symbols,              // SymbolHandle *
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCreateGroup(num_symbols, out symbols, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCreateFromFile(
            string               fname,                // const char *
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCreateFromFile(fname, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCreateFromJSON(
            string               json,                 // const char *
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCreateFromJSON(json, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolRemoveAmpCast(
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolRemoveAmpCast(sym_handle, out ret_sym_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolSaveToFile(
            SymbolHandle         symbol,               // SymbolHandle
            string               fname                 // const char *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolSaveToFile(symbol, fname);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolSaveToJSON(
            SymbolHandle         symbol,               // SymbolHandle
            out string           out_json              // const char **
        )
        {
            var resultCode = CApiDeclaration.MXSymbolSaveToJSON(symbol, out out_json);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolFree(
            SymbolHandle         symbol                // SymbolHandle
        )
        {
            var resultCode = CApiDeclaration.MXSymbolFree(symbol);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCopy(
            SymbolHandle         symbol,               // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCopy(symbol, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolPrint(
            SymbolHandle         symbol,               // SymbolHandle
            out string           out_str               // const char **
        )
        {
            var resultCode = CApiDeclaration.MXSymbolPrint(symbol, out out_str);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetName(
            SymbolHandle         symbol,               // SymbolHandle
            out string           @out,                 // const char **
            out int              success               // int *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetName(symbol, out @out, out success);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetAttr(
            SymbolHandle         symbol,               // SymbolHandle
            string               key,                  // const char *
            out string           @out,                 // const char **
            out int              success               // int *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetAttr(symbol, key, out @out, out success);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolSetAttr(
            SymbolHandle         symbol,               // SymbolHandle
            string               key,                  // const char *
            string               value                 // const char *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolSetAttr(symbol, key, value);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListAttr(
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         @out                  // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListAttr(symbol, out out_size, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListAttrShallow(
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         @out                  // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListAttrShallow(symbol, out out_size, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListArguments(
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_str_array         // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListArguments(symbol, out out_size, out out_str_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListArguments64(
            SymbolHandle         symbol,               // SymbolHandle
            out size_t           out_size,             // size_t *
            out string[]         out_str_array         // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListArguments64(symbol, out out_size, out out_str_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListOutputs(
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_str_array         // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListOutputs(symbol, out out_size, out out_str_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListOutputs64(
            SymbolHandle         symbol,               // SymbolHandle
            out size_t           out_size,             // size_t *
            out string[]         out_str_array         // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListOutputs64(symbol, out out_size, out out_str_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetNumOutputs(
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          output_count          // mx_uint *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetNumOutputs(symbol, out output_count);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetInternals(
            SymbolHandle         symbol,               // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetInternals(symbol, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetChildren(
            SymbolHandle         symbol,               // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetChildren(symbol, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGetOutput(
            SymbolHandle         symbol,               // SymbolHandle
            mx_uint              index,                // mx_uint
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGetOutput(symbol, index, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListAuxiliaryStates(
            SymbolHandle         symbol,               // SymbolHandle
            out mx_uint          out_size,             // mx_uint *
            out string[]         out_str_array         // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListAuxiliaryStates(symbol, out out_size, out out_str_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolListAuxiliaryStates64(
            SymbolHandle         symbol,               // SymbolHandle
            out size_t           out_size,             // size_t *
            out string[]         out_str_array         // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXSymbolListAuxiliaryStates64(symbol, out out_size, out out_str_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolCompose(
            SymbolHandle         sym,                  // SymbolHandle
            string               name,                 // const char *
            mx_uint              num_args,             // mx_uint
            out string           keys,                 // const char **
            out SymbolHandle     args                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolCompose(sym, name, num_args, out keys, out args);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolGrad(
            SymbolHandle         sym,                  // SymbolHandle
            mx_uint              num_wrt,              // mx_uint
            out string           wrt,                  // const char **
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSymbolGrad(sym, num_wrt, out wrt, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShape(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShape(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShape64(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShape64(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShapeEx(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShapeEx(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShapeEx64(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShapeEx64(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShapePartial(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShapePartial(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShapePartial64(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShapePartial64(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShapePartialEx(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShapePartialEx(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferShapePartialEx64(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferShapePartialEx64(sym, num_args, out keys, arg_ind_ptr, arg_shape_data, out in_shape_size, out in_shape_ndim, out in_shape_data, out out_shape_size, out out_shape_ndim, out out_shape_data, out aux_shape_size, out aux_shape_ndim, out aux_shape_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferType(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferType(sym, num_args, out keys, arg_type_data, out in_type_size, out in_type_data, out out_type_size, out out_type_data, out aux_type_size, out aux_type_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSymbolInferTypePartial(
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
        )
        {
            var resultCode = CApiDeclaration.MXSymbolInferTypePartial(sym, num_args, out keys, arg_type_data, out in_type_size, out in_type_data, out out_type_size, out out_type_data, out aux_type_size, out aux_type_data, out complete);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXQuantizeSymbol(
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle,       // SymbolHandle *
            IntPtr               num_excluded_symbols,  // const mx_uint
            out string           excluded_symbols,     // const char **
            IntPtr               num_offline,          // const mx_uint
            out string           offline_params,       // const char **
            string               quantized_dtype,      // const char *
            IntPtr               calib_quantize        // const bool
        )
        {
            var resultCode = CApiDeclaration.MXQuantizeSymbol(sym_handle, out ret_sym_handle, num_excluded_symbols, out excluded_symbols, num_offline, out offline_params, quantized_dtype, calib_quantize);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXReducePrecisionSymbol(
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
        )
        {
            var resultCode = CApiDeclaration.MXReducePrecisionSymbol(sym_handle, out ret_sym_handle, num_args, arg_type_data, num_ind_ptr, ind_ptr, target_dtype, cast_optional_params, num_target_dtype_op_names, num_fp32_op_names, num_widest_dtype_op_names, num_conditional_fp32_op_names, num_excluded_symbols, num_model_params, out target_dtype_op_names, out fp32_op_names, out widest_dtype_op_names, out conditional_fp32_op_names, out excluded_symbols, out conditional_param_names, out conditional_param_vals, out model_param_names, out arg_names);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXSetCalibTableToQuantizedSymbol(
            SymbolHandle         qsym_handle,          // SymbolHandle
            IntPtr               num_layers,           // const mx_uint
            out string           layer_names,          // const char **
            IntPtr               low_quantiles,        // const float *
            IntPtr               high_quantiles,       // const float *
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXSetCalibTableToQuantizedSymbol(qsym_handle, num_layers, out layer_names, low_quantiles, high_quantiles, out ret_sym_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGenBackendSubgraph(
            SymbolHandle         sym_handle,           // SymbolHandle
            string               backend,              // const char *
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXGenBackendSubgraph(sym_handle, backend, out ret_sym_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXGenAtomicSymbolFromSymbol(
            SymbolHandle         sym_handle,           // SymbolHandle
            out SymbolHandle     ret_sym_handle        // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXGenAtomicSymbolFromSymbol(sym_handle, out ret_sym_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorFree(
            ExecutorHandle       handle                // ExecutorHandle
        )
        {
            var resultCode = CApiDeclaration.MXExecutorFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorPrint(
            ExecutorHandle       handle,               // ExecutorHandle
            out string           out_str               // const char **
        )
        {
            var resultCode = CApiDeclaration.MXExecutorPrint(handle, out out_str);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorForward(
            ExecutorHandle       handle,               // ExecutorHandle
            int                  is_train              // int
        )
        {
            var resultCode = CApiDeclaration.MXExecutorForward(handle, is_train);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorBackward(
            ExecutorHandle       handle,               // ExecutorHandle
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    head_grads            // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXExecutorBackward(handle, len, out head_grads);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorBackwardEx(
            ExecutorHandle       handle,               // ExecutorHandle
            mx_uint              len,                  // mx_uint
            out NDArrayHandle    head_grads,           // NDArrayHandle *
            int                  is_train              // int
        )
        {
            var resultCode = CApiDeclaration.MXExecutorBackwardEx(handle, len, out head_grads, is_train);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorOutputs(
            ExecutorHandle       handle,               // ExecutorHandle
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           @out                  // NDArrayHandle **
        )
        {
            var resultCode = CApiDeclaration.MXExecutorOutputs(handle, out out_size, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorBind(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorBind(symbol_handle, dev_type, dev_id, len, out in_args, out arg_grad_store, out grad_req_type, aux_states_len, out aux_states, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorBindX(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorBindX(symbol_handle, dev_type, dev_id, num_map_keys, out map_keys, map_dev_types, map_dev_ids, len, out in_args, out arg_grad_store, out grad_req_type, aux_states_len, out aux_states, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorBindEX(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorBindEX(symbol_handle, dev_type, dev_id, num_map_keys, out map_keys, map_dev_types, map_dev_ids, len, out in_args, out arg_grad_store, out grad_req_type, aux_states_len, out aux_states, shared_exec, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorSimpleBind(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorSimpleBind(symbol_handle, dev_type, dev_id, num_g2c_keys, out g2c_keys, g2c_dev_types, g2c_dev_ids, provided_grad_req_list_len, out provided_grad_req_names, out provided_grad_req_types, num_provided_arg_shapes, out provided_arg_shape_names, provided_arg_shape_data, provided_arg_shape_idx, num_provided_arg_dtypes, out provided_arg_dtype_names, provided_arg_dtypes, num_provided_arg_stypes, out provided_arg_stype_names, provided_arg_stypes, num_shared_arg_names, out shared_arg_name_list, out shared_buffer_len, out shared_buffer_name_list, out shared_buffer_handle_list, out updated_shared_buffer_name_list, out updated_shared_buffer_handle_list, out num_in_args, out in_args, out arg_grads, out num_aux_states, out aux_states, shared_exec_handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorSimpleBindEx(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorSimpleBindEx(symbol_handle, dev_type, dev_id, num_g2c_keys, out g2c_keys, g2c_dev_types, g2c_dev_ids, provided_grad_req_list_len, out provided_grad_req_names, out provided_grad_req_types, num_provided_arg_shapes, out provided_arg_shape_names, provided_arg_shape_data, provided_arg_shape_idx, num_provided_arg_dtypes, out provided_arg_dtype_names, provided_arg_dtypes, num_provided_arg_stypes, out provided_arg_stype_names, provided_arg_stypes, num_shared_arg_names, out shared_arg_name_list, out shared_buffer_len, out shared_buffer_name_list, out shared_buffer_handle_list, out updated_shared_buffer_name_list, out updated_shared_buffer_handle_list, out num_in_args, out in_args, out arg_grads, out num_aux_states, out aux_states, shared_exec_handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorReshape(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorReshape(partial_shaping, allow_up_sizing, dev_type, dev_id, num_map_keys, out map_keys, map_dev_types, map_dev_ids, num_provided_arg_shapes, out provided_arg_shape_names, provided_arg_shape_data, provided_arg_shape_idx, out num_in_args, out in_args, out arg_grads, out num_aux_states, out aux_states, shared_exec, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorReshapeEx(
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
        )
        {
            var resultCode = CApiDeclaration.MXExecutorReshapeEx(partial_shaping, allow_up_sizing, dev_type, dev_id, num_map_keys, out map_keys, map_dev_types, map_dev_ids, num_provided_arg_shapes, out provided_arg_shape_names, provided_arg_shape_data, provided_arg_shape_idx, out num_in_args, out in_args, out arg_grads, out num_aux_states, out aux_states, shared_exec, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorGetOptimizedSymbol(
            ExecutorHandle       handle,               // ExecutorHandle
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXExecutorGetOptimizedSymbol(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorSetMonitorCallback(
            ExecutorHandle       handle,               // ExecutorHandle
            IntPtr               callback,             // ExecutorMonitorCallback
            IntPtr               callback_handle       // void *
        )
        {
            var resultCode = CApiDeclaration.MXExecutorSetMonitorCallback(handle, callback, callback_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXExecutorSetMonitorCallbackEX(
            ExecutorHandle       handle,               // ExecutorHandle
            IntPtr               callback,             // ExecutorMonitorCallback
            IntPtr               callback_handle,      // void *
            IntPtr               monitor_all           // bool
        )
        {
            var resultCode = CApiDeclaration.MXExecutorSetMonitorCallbackEX(handle, callback, callback_handle, monitor_all);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXListDataIters(
            out mx_uint          out_size,             // mx_uint *
            out IntPtr           out_array             // DataIterCreator **
        )
        {
            var resultCode = CApiDeclaration.MXListDataIters(out out_size, out out_array);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterCreateIter(
            DataIterCreator      handle,               // DataIterCreator
            mx_uint              num_param,            // mx_uint
            out string           keys,                 // const char **
            out string           vals,                 // const char **
            out DataIterHandle   @out                  // DataIterHandle *
        )
        {
            var resultCode = CApiDeclaration.MXDataIterCreateIter(handle, num_param, out keys, out vals, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterGetIterInfo(
            DataIterCreator      creator,              // DataIterCreator
            out string           name,                 // const char **
            out string           description,          // const char **
            out mx_uint          num_args,             // mx_uint *
            out string[]         arg_names,            // const char ***
            out string[]         arg_type_infos,       // const char ***
            out string[]         arg_descriptions      // const char ***
        )
        {
            var resultCode = CApiDeclaration.MXDataIterGetIterInfo(creator, out name, out description, out num_args, out arg_names, out arg_type_infos, out arg_descriptions);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterFree(
            DataIterHandle       handle                // DataIterHandle
        )
        {
            var resultCode = CApiDeclaration.MXDataIterFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterNext(
            DataIterHandle       handle,               // DataIterHandle
            out int              @out                  // int *
        )
        {
            var resultCode = CApiDeclaration.MXDataIterNext(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterBeforeFirst(
            DataIterHandle       handle                // DataIterHandle
        )
        {
            var resultCode = CApiDeclaration.MXDataIterBeforeFirst(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterGetData(
            DataIterHandle       handle,               // DataIterHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXDataIterGetData(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterGetIndex(
            DataIterHandle       handle,               // DataIterHandle
            out IntPtr           out_index,            // uint64_t **
            out uint64_t         out_size              // uint64_t *
        )
        {
            var resultCode = CApiDeclaration.MXDataIterGetIndex(handle, out out_index, out out_size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterGetPadNum(
            DataIterHandle       handle,               // DataIterHandle
            out int              pad                   // int *
        )
        {
            var resultCode = CApiDeclaration.MXDataIterGetPadNum(handle, out pad);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXDataIterGetLabel(
            DataIterHandle       handle,               // DataIterHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXDataIterGetLabel(handle, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXInitPSEnv(
            mx_uint              num_vars,             // mx_uint
            out string           keys,                 // const char **
            out string           vals                  // const char **
        )
        {
            var resultCode = CApiDeclaration.MXInitPSEnv(num_vars, out keys, out vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreCreate(
            string               type,                 // const char *
            out IntPtr           @out                  // KVStoreHandle *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreCreate(type, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreSetGradientCompression(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num_params,           // mx_uint
            out string           keys,                 // const char **
            out string           vals                  // const char **
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreSetGradientCompression(handle, num_params, out keys, out vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreFree(
            IntPtr               handle                // KVStoreHandle
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreInit(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreInit(handle, num, keys, out vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreInitEx(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreInitEx(handle, num, out keys, out vals);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePush(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePush(handle, num, keys, out vals, priority);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePushEx(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePushEx(handle, num, out keys, out vals, priority);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePullWithSparse(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority,             // int
            IntPtr               ignore_sparse         // bool
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePullWithSparse(handle, num, keys, out vals, priority, ignore_sparse);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePullWithSparseEx(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority,             // int
            IntPtr               ignore_sparse         // bool
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePullWithSparseEx(handle, num, out keys, out vals, priority, ignore_sparse);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePull(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePull(handle, num, keys, out vals, priority);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePullEx(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            int                  priority              // int
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePullEx(handle, num, out keys, out vals, priority);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePullRowSparse(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            int[]                keys,                 // const int *
            out NDArrayHandle    vals,                 // NDArrayHandle *
            IntPtr               row_ids,              // const NDArrayHandle *
            int                  priority              // int
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePullRowSparse(handle, num, keys, out vals, row_ids, priority);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStorePullRowSparseEx(
            IntPtr               handle,               // KVStoreHandle
            mx_uint              num,                  // mx_uint
            out string           keys,                 // const char **
            out NDArrayHandle    vals,                 // NDArrayHandle *
            IntPtr               row_ids,              // const NDArrayHandle *
            int                  priority              // int
        )
        {
            var resultCode = CApiDeclaration.MXKVStorePullRowSparseEx(handle, num, out keys, out vals, row_ids, priority);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreSetUpdater(
            IntPtr               handle,               // KVStoreHandle
            IntPtr               updater,              // MXKVStoreUpdater
            IntPtr               updater_handle        // void *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreSetUpdater(handle, updater, updater_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreSetUpdaterEx(
            IntPtr               handle,               // KVStoreHandle
            IntPtr               updater,              // MXKVStoreUpdater
            IntPtr               str_updater,          // MXKVStoreStrUpdater
            IntPtr               updater_handle        // void *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreSetUpdaterEx(handle, updater, str_updater, updater_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreGetType(
            IntPtr               handle,               // KVStoreHandle
            out string           type                  // const char **
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreGetType(handle, out type);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreGetRank(
            IntPtr               handle,               // KVStoreHandle
            out int              ret                   // int *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreGetRank(handle, out ret);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreGetGroupSize(
            IntPtr               handle,               // KVStoreHandle
            out int              ret                   // int *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreGetGroupSize(handle, out ret);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreIsWorkerNode(
            out int              ret                   // int *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreIsWorkerNode(out ret);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreIsServerNode(
            out int              ret                   // int *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreIsServerNode(out ret);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreIsSchedulerNode(
            out int              ret                   // int *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreIsSchedulerNode(out ret);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreBarrier(
            IntPtr               handle                // KVStoreHandle
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreBarrier(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreSetBarrierBeforeExit(
            IntPtr               handle,               // KVStoreHandle
            IntPtr               barrier_before_exit   // const int
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreSetBarrierBeforeExit(handle, barrier_before_exit);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreRunServer(
            IntPtr               handle,               // KVStoreHandle
            IntPtr               controller,           // MXKVStoreServerController
            IntPtr               controller_handle     // void *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreRunServer(handle, controller, controller_handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreSendCommmandToServers(
            IntPtr               handle,               // KVStoreHandle
            int                  cmd_id,               // int
            string               cmd_body              // const char *
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreSendCommmandToServers(handle, cmd_id, cmd_body);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXKVStoreGetNumDeadNode(
            IntPtr               handle,               // KVStoreHandle
            IntPtr               node_id,              // const int
            out int              number,               // int *
            IntPtr               timeout_sec           // const int
        )
        {
            var resultCode = CApiDeclaration.MXKVStoreGetNumDeadNode(handle, node_id, out number, timeout_sec);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOWriterCreate(
            string               uri,                  // const char *
            out IntPtr           @out                  // RecordIOHandle *
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOWriterCreate(uri, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOWriterFree(
            IntPtr               handle                // RecordIOHandle
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOWriterFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOWriterWriteRecord(
            IntPtr               handle,               // RecordIOHandle
            string               buf,                  // const char *
            size_t               size                  // size_t
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOWriterWriteRecord(handle, buf, size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOWriterTell(
            IntPtr               handle,               // RecordIOHandle
            out size_t           pos                   // size_t *
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOWriterTell(handle, out pos);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOReaderCreate(
            string               uri,                  // const char *
            out IntPtr           @out                  // RecordIOHandle *
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOReaderCreate(uri, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOReaderFree(
            IntPtr               handle                // RecordIOHandle
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOReaderFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOReaderReadRecord(
            IntPtr               handle,               // RecordIOHandle
            out IntPtr           buf,                  // char const **
            out size_t           size                  // size_t *
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOReaderReadRecord(handle, out buf, out size);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOReaderSeek(
            IntPtr               handle,               // RecordIOHandle
            size_t               pos                   // size_t
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOReaderSeek(handle, pos);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRecordIOReaderTell(
            IntPtr               handle,               // RecordIOHandle
            out size_t           pos                   // size_t *
        )
        {
            var resultCode = CApiDeclaration.MXRecordIOReaderTell(handle, out pos);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcCreate(
            out IntPtr           name,                 // char *
            mx_uint              num_input,            // mx_uint
            mx_uint              num_output,           // mx_uint
            string[]             input_names,          // char **
            out string           output_names,         // char **
            IntPtr               inputs,               // NDArrayHandle *
            ref NDArrayHandle    outputs,              // NDArrayHandle *
            out IntPtr           kernel,               // char *
            out IntPtr           @out                  // RtcHandle *
        )
        {
            var resultCode = CApiDeclaration.MXRtcCreate(out name, num_input, num_output, input_names, out output_names, inputs, ref outputs, out kernel, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcPush(
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
        )
        {
            var resultCode = CApiDeclaration.MXRtcPush(handle, num_input, num_output, inputs, ref outputs, gridDimX, gridDimY, gridDimZ, blockDimX, blockDimY, blockDimZ);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcFree(
            IntPtr               handle                // RtcHandle
        )
        {
            var resultCode = CApiDeclaration.MXRtcFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXCustomOpRegister(
            string               op_type,              // const char *
            IntPtr               creator               // CustomOpPropCreator
        )
        {
            var resultCode = CApiDeclaration.MXCustomOpRegister(op_type, creator);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXCustomFunctionRecord(
            int                  num_inputs,           // int
            IntPtr               inputs,               // NDArrayHandle *
            ref int              num_outputs,          // int
            ref NDArrayHandle    outputs,              // NDArrayHandle *
            out IntPtr           callbacks             // struct MXCallbackList *
        )
        {
            var resultCode = CApiDeclaration.MXCustomFunctionRecord(num_inputs, inputs, ref num_outputs, ref outputs, out callbacks);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcCudaModuleCreate(
            string               source,               // const char *
            int                  num_options,          // int
            out string           options,              // const char **
            int                  num_exports,          // int
            out string           exports,              // const char **
            out IntPtr           @out                  // CudaModuleHandle *
        )
        {
            var resultCode = CApiDeclaration.MXRtcCudaModuleCreate(source, num_options, out options, num_exports, out exports, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcCudaModuleFree(
            IntPtr               handle                // CudaModuleHandle
        )
        {
            var resultCode = CApiDeclaration.MXRtcCudaModuleFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcCudaKernelCreate(
            IntPtr               handle,               // CudaModuleHandle
            string               name,                 // const char *
            int                  num_args,             // int
            out int              is_ndarray,           // int *
            out int              is_const,             // int *
            out int              arg_types,            // int *
            out IntPtr           @out                  // CudaKernelHandle *
        )
        {
            var resultCode = CApiDeclaration.MXRtcCudaKernelCreate(handle, name, num_args, out is_ndarray, out is_const, out arg_types, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcCudaKernelFree(
            IntPtr               handle                // CudaKernelHandle
        )
        {
            var resultCode = CApiDeclaration.MXRtcCudaKernelFree(handle);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXRtcCudaKernelCall(
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
        )
        {
            var resultCode = CApiDeclaration.MXRtcCudaKernelCall(handle, dev_id, out args, grid_dim_x, grid_dim_y, grid_dim_z, block_dim_x, block_dim_y, block_dim_z, shared_mem);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayGetSharedMemHandle(
            NDArrayHandle        handle,               // NDArrayHandle
            out int              shared_pid,           // int *
            out int              shared_id             // int *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayGetSharedMemHandle(handle, out shared_pid, out shared_id);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateFromSharedMem(
            int                  shared_pid,           // int
            int                  shared_id,            // int
            mx_uint[]            shape,                // const mx_uint *
            mx_uint              ndim,                 // mx_uint
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateFromSharedMem(shared_pid, shared_id, shape, ndim, dtype, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXStorageEmptyCache(
            int                  dev_type,             // int
            int                  dev_id                // int
        )
        {
            var resultCode = CApiDeclaration.MXStorageEmptyCache(dev_type, dev_id);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXNDArrayCreateFromSharedMemEx(
            int                  shared_pid,           // int
            int                  shared_id,            // int
            int[]                shape,                // const int *
            int                  ndim,                 // int
            int                  dtype,                // int
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXNDArrayCreateFromSharedMemEx(shared_pid, shared_id, shape, ndim, dtype, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXEnginePushAsync(
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
        )
        {
            var resultCode = CApiDeclaration.MXEnginePushAsync(async_func, func_param, deleter, ctx_handle, const_vars_handle, num_const_vars, mutable_vars_handle, num_mutable_vars, prop_handle, priority, opr_name, wait);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXEnginePushSync(
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
        )
        {
            var resultCode = CApiDeclaration.MXEnginePushSync(sync_func, func_param, deleter, ctx_handle, const_vars_handle, num_const_vars, mutable_vars_handle, num_mutable_vars, prop_handle, priority, opr_name);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXShallowCopyNDArray(
            NDArrayHandle        src,                  // NDArrayHandle
            out NDArrayHandle    @out                  // NDArrayHandle *
        )
        {
            var resultCode = CApiDeclaration.MXShallowCopyNDArray(src, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXShallowCopySymbol(
            SymbolHandle         src,                  // SymbolHandle
            out SymbolHandle     @out                  // SymbolHandle *
        )
        {
            var resultCode = CApiDeclaration.MXShallowCopySymbol(src, out @out);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXEnginePushAsyncND(
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
        )
        {
            var resultCode = CApiDeclaration.MXEnginePushAsyncND(async_func, func_param, deleter, ctx_handle, out const_nds_handle, num_const_nds, out mutable_nds_handle, num_mutable_nds, prop_handle, priority, opr_name, wait);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

        public static void MXEnginePushSyncND(
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
        )
        {
            var resultCode = CApiDeclaration.MXEnginePushSyncND(sync_func, func_param, deleter, ctx_handle, out const_nds_handle, num_const_nds, out mutable_nds_handle, num_mutable_nds, prop_handle, priority, opr_name);
            if (resultCode != 0)
            {
                var message = CApiDeclaration.MXGetLastError();
                throw new NativeMethodException(resultCode, message);
            }
        }

    }
}
