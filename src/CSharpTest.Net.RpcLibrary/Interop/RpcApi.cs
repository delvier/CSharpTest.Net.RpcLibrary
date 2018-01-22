#region Copyright 2010-2014 by Roger Knapp, Licensed under the Apache License, Version 2.0
/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion
using System;
using System.Runtime.InteropServices;
using CSharpTest.Net.RpcLibrary.Interop.Structs;

namespace CSharpTest.Net.RpcLibrary.Interop
{
    /// <summary>
    /// WinAPI imports for RPC
    /// </summary>
    internal static class RpcApi
    {
        #region MIDL_FORMAT_STRINGS

        internal static readonly bool Is64BitProcess;
        internal static readonly byte[] TYPE_FORMAT;
        internal static readonly byte[] FUNC_FORMAT;
        internal static readonly Ptr<Byte[]> FUNC_FORMAT_PTR;

        static RpcApi()
        {
            Is64BitProcess = (IntPtr.Size == 8);
            Log.Verbose("Is64BitProcess = {0}", Is64BitProcess);

            if (Is64BitProcess)
            {
                //TYPE_FORMAT = new byte[]
                //    {
                //        0x00, 0x00, 0x1b, 0x00, 0x01, 0x00, 0x28, 0x00, 0x08, 0x00,
                //        0x01, 0x00, 0x01, 0x5b, 0x11, 0x0c, 0x08, 0x5c, 0x11, 0x14,
                //        0x02, 0x00, 0x12, 0x00, 0x02, 0x00, 0x1b, 0x00, 0x01, 0x00,
                //        0x28, 0x54, 0x18, 0x00, 0x01, 0x00, 0x01, 0x5b, 0x00
                //    };
                //FUNC_FORMAT = new byte[]
                //    {
                //        0x00, 0x68, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x30, 0x00,
                //        0x32, 0x00, 0x00, 0x00, 0x08, 0x00, 0x24, 0x00, 0x47, 0x05,
                //        0x0a, 0x07, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00,
                //        0x48, 0x00, 0x08, 0x00, 0x08, 0x00, 0x0b, 0x00, 0x10, 0x00,
                //        0x02, 0x00, 0x50, 0x21, 0x18, 0x00, 0x08, 0x00, 0x13, 0x20,
                //        0x20, 0x00, 0x12, 0x00, 0x70, 0x00, 0x28, 0x00, 0x10, 0x00,
                //        0x00
                //    };
                TYPE_FORMAT = new byte[]
             {
                    0x00, 0x00, 0x11, 0x04, 0x02, 0x00, 0x30, 0xA0, 0x00,
                    0x00, 0x11, 0x4,  0x2,  0x00, 0x30, 0xe1, 0x00, 0x00, 0x30,
                    0x41, 0x00, 0x00, 0x11, 0x00, 0x02, 0x00, 0x1b, 0x00, 0x01,
                    0x00, 0x29, 0x00, 0x8,  0x00, 0x01, 0x00, 0x1,  0x5b, 0x11,
                    0x00, 0x2,  0x00, 0x1b, 0x00, 0x1,  0x00, 0x29, 0x00, 0x18,
                    0x00, 0x01, 0x00, 0x01, 0x5b, 0x11, 0x0c, 0x8, 0x5c, 0x0
             };

                FUNC_FORMAT = new byte[149]
                    { 
                        /* Procedure RemoteOpen */

                        0x0,		/* 0 */
                        0x48,		/* Old Flags:  */
/*  2 *//*NdrFcLong*/ 0x0, 0x0, 0x0, 0x0,	/* 0 */
/*  6 *//*NdrFcShort*/0x0,0x0,	/* 0 */
/*  8 *//*NdrFcShort*/0x10, 0x0,	/* X64 Stack size/offset = 16 */
/* 10 */	0x32,		/* FC_BIND_PRIMITIVE */
                        0x0,		/* 0 */
/* 12 NdrFcShort*/	0x0, 0x0,	/* X64 Stack size/offset = 0 */
/* 14 NdrFcShort*/	0x0, 0x0,	/* 0 */
/* 16 NdrFcShort*/  0x38, 0x0,	/* 56 */
/* 18 */	0x40,		/* Oi2 Flags:  has ext, */
                        0x1,		/* 1 */
/* 20 */	0xa,		/* 10 */
                        0x1,		/* Ext Flags:  new corr desc, */
/* 22 NdrFcShort*/	0x0 , 0x0,	/* 0 */
/* 24 NdrFcShort*/	0x0 , 0x0,	/* 0 */
/* 26 NdrFcShort*/	0x0 , 0x0,	/* 0 */
/* 28 NdrFcShort*/	0x0 , 0x0,	/* 0 */

                        /* Parameter sessionContex */

/* 30 NdrFcShort*/	 0x10, 0x1,	/* Flags:  out, simple ref, */
/* 32 NdrFcShort*/	 0x8, 0x0,	/* X64 Stack size/offset = 8 */
/* 34 NdrFcShort*/	 0x6, 0x0,	/* Type Offset=6 */

	/* Procedure RemoteClose */

/* 36 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 38 NdrFcLong*/	0x0,0x0,0x0, 0x0,	/* 0 */
/* 42 NdrFcShort*/	 0x1, 0x0 ,	/* 1 */
/* 44 NdrFcShort*/	 0x8, 0x0 ,	/* X64 Stack size/offset = 8 */
/* 46 */	0x30,		/* FC_BIND_CONTEXT */
			0xe0,		/* Ctxt flags:  via ptr, in, out, */
/* 48 NdrFcShort*/	0x0, 0x0,	/* X64 Stack size/offset = 0 */
/* 50 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 52 NdrFcShort*/	0x38, 0x0,	/* 56 */
/* 54 NdrFcShort*/	0x38, 0x0,	/* 56 */
/* 56 */	0x40,		/* Oi2 Flags:  has ext, */
			0x1,		/* 1 */
/* 58 */	0xa,		/* 10 */
			0x1,		/* Ext Flags:  new corr desc, */
/* 60 NdrFcShort*/	0x0,0x0,	/* 0 */
/* 62 NdrFcShort*/	0x0,0x0,	/* 0 */
/* 64 NdrFcShort*/	0x0,0x0,	/* 0 */
/* 66 NdrFcShort*/	0x0,0x0,	/* 0 */

	/* Parameter sessionContex */

/* 68 NdrFcShort (0x118)*/	0x18,0x1,	/* Flags:  in, out, simple ref, */
/* 70 NdrFcShort*/	0x0,0x0,	/* X64 Stack size/offset = 0 */
/* 72 NdrFcShort*/	0xe,0x0,	/* Type Offset=14 */

	/* Procedure RemoteProcessMessage */

/* 74 */	0x0,		/* 0 */
			0x48,		/* Old Flags:  */
/* 76 NdrFcLong*/	0x0,0x0,0x0,0x0,	/* 0 */
/* 80 NdrFcShort*/	0x2, 0x0,	/* 2 */
/* 82 NdrFcShort*/	0x38, 0x0,	/* X64 Stack size/offset = 56 */
/* 84 */	0x30,		/* FC_BIND_CONTEXT */
			0x40,		/* Ctxt flags:  in, */
/* 86 NdrFcShort*/	0x0, 0x0 ,	/* X64 Stack size/offset = 0 */
/* 88 */	0x0,		/* 0 */
			0x0,		/* 0 */
/* 90 NdrFcShort*/	0x32, 0x00,	/* 52 */
/* 92 NdrFcShort*/	0x2c, 0x00,	/* 44 */
/* 94 */	0x47,		/* Oi2 Flags:  srv must size, clt must size, has return, has ext, */
			0x7,		/* 7 */
/* 96 */	0xa,		/* 10 */
			0x7,		/* Ext Flags:  new corr desc, clt corr check, srv corr check, */
/* 98  NdrFcShort*/0x1, 0x0,	/* 1 */
/* 100 NdrFcShort*/0x1, 0x0,	/* 1 */
/* 102 NdrFcShort*/0x0, 0x0,	/* 0 */
/* 104 NdrFcShort*/0x0, 0x0,	/* 0 */

	/* Parameter sessionContex */

/* 106 NdrFcShort*/	0x8, 0x0 ,	/* Flags:  in, */
/* 108 NdrFcShort*/	0x0, 0x0 ,	/* X64 Stack size/offset = 0 */
/* 110 NdrFcShort*/	0x12, 0x00 ,	/* Type Offset=18 */

	/* Parameter inputBufferSize */

/* 112 NdrFcShort*/	0x48, 0x0 ,	/* Flags:  in, base type, */
/* 114 NdrFcShort*/	0x8, 0x0 ,	/* X64 Stack size/offset = 8 */
/* 116 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter inputBuffer */

/* 118 NdrFcShort(0x10b) */	0x0b, 0x1,	/* Flags:  must size, must free, in, simple ref, */
/* 120 NdrFcShort */	0x10, 0x0,	/* X64 Stack size/offset = 16 */
/* 122 NdrFcShort */    0x1a, 0x0,	/* Type Offset=26 */

	/* Parameter outputBufferSize */

/* 124 NdrFcShort*/	0x48, 0x0 ,	/* Flags:  in, base type, */
/* 126 NdrFcShort*/	0x18, 0x0 ,	/* X64 Stack size/offset = 24 */
/* 128 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Parameter outputBuffer */

/* 130 NdrFcShort 0x113 */	0x13, 0x01,	/* Flags:  must size, must free, out, simple ref, */
/* 132 NdrFcShort*/	0x20, 0x0 ,	/* X64 Stack size/offset = 32 */
/* 134 NdrFcShort*/	0x2a, 0x0 ,	/* Type Offset=42 */

	/* Parameter outBufferWritten */

/* 136 NdrFcShort( 0x2150 )*/	0x50, 0x21,	/* Flags:  out, base type, simple ref, srv alloc size=8 */
/* 138 NdrFcShort*/	0x28, 0x0,	/* X64 Stack size/offset = 40 */
/* 140 */	0x8,		/* FC_LONG */
			0x0,		/* 0 */

	/* Return value */

/* 142 NdrFcShort*/	 0x70,0x0 ,	/* Flags:  out, return, base type, */
/* 144 NdrFcShort*/	 0x30,0x0 ,	/* X64 Stack size/offset = 48 */
/* 146 */	0xb,		/* FC_HYPER */
			0x0,		/* 0 */

			0x0
                    };

            }
            else
            {
                TYPE_FORMAT = new byte[]
                    {
                        0x00, 0x00, 0x1b, 0x00, 0x01, 0x00, 0x28, 0x00, 0x04, 0x00,
                        0x01, 0x00, 0x01, 0x5b, 0x11, 0x0c, 0x08, 0x5c, 0x11, 0x14,
                        0x02, 0x00, 0x12, 0x00, 0x02, 0x00, 0x1b, 0x00, 0x01, 0x00,
                        0x28, 0x54, 0x0c, 0x00, 0x01, 0x00, 0x01, 0x5b, 0x00
                    };
                FUNC_FORMAT = new byte[]
                    {
                        0x00, 0x68, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x18, 0x00,
                        0x32, 0x00, 0x00, 0x00, 0x08, 0x00, 0x24, 0x00, 0x47, 0x05,
                        0x08, 0x07, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x48, 0x00,
                        0x04, 0x00, 0x08, 0x00, 0x0b, 0x00, 0x08, 0x00, 0x02, 0x00,
                        0x50, 0x21, 0x0c, 0x00, 0x08, 0x00, 0x13, 0x20, 0x10, 0x00,
                        0x12, 0x00, 0x70, 0x00, 0x14, 0x00, 0x10, 0x00, 0x00
                    };
            }
            FUNC_FORMAT_PTR = new Ptr<byte[]>(FUNC_FORMAT);
        }

        #endregion

        #region Memory Utils

        [DllImport("Kernel32.dll", EntryPoint = "LocalFree", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr LocalFree(IntPtr memHandle);

        internal static void Free(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                Log.Verbose("LocalFree({0})", ptr);
                LocalFree(ptr);
            }
        }

        private const UInt32 LPTR = 0x0040;

        [DllImport("Kernel32.dll", EntryPoint = "LocalAlloc", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr LocalAlloc(UInt32 flags, UInt32 nBytes);

        internal static IntPtr Alloc(uint size)
        {
            IntPtr ptr = LocalAlloc(LPTR, size);
            Log.Verbose("{0} = LocalAlloc({1})", ptr, size);
            return ptr;
        }

        [DllImport("Rpcrt4.dll", EntryPoint = "NdrServerCall2", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern void NdrServerCall2(IntPtr ptr);

        internal delegate void ServerEntryPoint(IntPtr ptr);

        internal static FunctionPtr<ServerEntryPoint> ServerEntry = new FunctionPtr<ServerEntryPoint>(NdrServerCall2);

        internal static FunctionPtr<LocalAlloc> AllocPtr = new FunctionPtr<LocalAlloc>(Alloc);
        internal static FunctionPtr<LocalFree> FreePtr = new FunctionPtr<LocalFree>(Free);

        #endregion
    }
}