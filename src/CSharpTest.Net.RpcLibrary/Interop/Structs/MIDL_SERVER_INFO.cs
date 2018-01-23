using System;
using System.Runtime.InteropServices;
#pragma warning disable 1591

namespace CSharpTest.Net.RpcLibrary.Interop.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MIDL_SERVER_INFO
    {
        public IntPtr /* PMIDL_STUB_DESC */ pStubDesc;
        public IntPtr /* SERVER_ROUTINE* */ DispatchTable;
        public IntPtr /* PFORMAT_STRING */ ProcString;
        public IntPtr /* unsigned short* */ FmtStringOffset;
        private IntPtr /* STUB_THUNK * */ ThunkTable;
        private IntPtr /* PRPC_SYNTAX_IDENTIFIER */ pTransferSyntax;
        private IntPtr /* ULONG_PTR */ nCount;
        private IntPtr /* PMIDL_SYNTAX_INFO */ pSyntaxInfo;

        internal static Ptr<RPC_SERVER_INTERFACE> Create(RpcHandle handle, Guid iid, Byte[] formatTypes,
                                                         Byte[] formatProc,
                                                         RpcRemoteProcessMessageDelegate remoteProcessMessage)
        {
            Ptr<MIDL_SERVER_INFO> pServer = handle.CreatePtr(new MIDL_SERVER_INFO());

            MIDL_SERVER_INFO temp = new MIDL_SERVER_INFO();
            return temp.Configure(handle, pServer, iid, formatTypes, formatProc, remoteProcessMessage);
        }

        private Ptr<RPC_SERVER_INTERFACE> Configure(RpcHandle handle, Ptr<MIDL_SERVER_INFO> me, Guid iid,
                                                    Byte[] formatTypes,
                                                    Byte[] formatProc, RpcRemoteProcessMessageDelegate remoteProcessMessage)
        {
            Ptr<RPC_SERVER_INTERFACE> svrIface = handle.CreatePtr(new RPC_SERVER_INTERFACE(handle, me, iid));
            Ptr<MIDL_STUB_DESC> stub = handle.CreatePtr(new MIDL_STUB_DESC(handle, svrIface.Handle, formatTypes, true));
            pStubDesc = stub.Handle;

            var serverRoutine = new SERVER_ROUTINE
            {
                RemoteOpen = ServerRoutinesHandlers.RemoteOpenFunctionPtr.Handle,
                RemoteClose = MIDL_STUB_DESC.RemoteCloseFunctionPtr.Handle,
                RemoteProcessMessage = handle.PinFunction(remoteProcessMessage)
            };
            DispatchTable = handle.Pin(serverRoutine);

            ProcString = handle.Pin(formatProc);
            FmtStringOffset = handle.Pin(new ushort[] { 0, 36, 74 });

            ThunkTable = IntPtr.Zero;
            pTransferSyntax = IntPtr.Zero;
            nCount = IntPtr.Zero;
            pSyntaxInfo = IntPtr.Zero;

            //Copy us back into the pinned address
            Marshal.StructureToPtr(this, me.Handle, false);
            return svrIface;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SERVER_ROUTINE
    {
        public IntPtr RemoteOpen;
        public IntPtr RemoteClose;
        public IntPtr RemoteProcessMessage;
    }

    internal delegate ulong RpcRemoteProcessMessageDelegate(IntPtr sessioncontext,
        ulong inputbuffersize, IntPtr inputbufferptr,
        ulong outputbuffersize, out IntPtr outputbufferptr,
        out ulong outbufferwritten);

    public class ServerRoutinesHandlers
    {
        public delegate ulong RemoteProcessMessageDelegate(IntPtr sessionContext,
            ulong inputBufferSize, IntPtr inputBufferPtr,
            ulong outputBufferSize, [Out] out IntPtr outputBufferPtr, out ulong outBufferWritten);
        public static FunctionPtr<RemoteProcessMessageDelegate> RemoteProcessMessageFunctionPtr =
            new FunctionPtr<RemoteProcessMessageDelegate>(RemoteProcessMessageMethod);

        public static ulong RemoteProcessMessageMethod(IntPtr sessioncontext,
            ulong inputbuffersize, IntPtr inputbufferptr,
            ulong outputbuffersize, out IntPtr outputbufferptr, out ulong outbufferwritten)
        {
            var output = RpcApi.Alloc((uint)outputbuffersize);
            var bytes = new byte[] { 0xFF, 0xAA, 0xBB };
            Marshal.Copy(bytes, 0, output, bytes.Length);

            outputbufferptr = output;
            outbufferwritten = (ulong)bytes.Length;
            return 0;
        }

        public delegate uint RemoteOpenDelegate(IntPtr clientHandle, [Out] out IntPtr sessionContext);
        public static FunctionPtr<RemoteOpenDelegate> RemoteOpenFunctionPtr =
            new FunctionPtr<RemoteOpenDelegate>(RemoteOpenMethod);
        public static uint RemoteOpenMethod(IntPtr clientHandle, [Out] out IntPtr sessionContext)
        {
            sessionContext = IntPtr.Zero;
            try
            {
                sessionContext = RpcApi.Alloc((uint)IntPtr.Size);
                return (uint)RpcError.RPC_S_OK;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return (uint)RpcError.RPC_E_FAIL;
            }
        }
    }
}