using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MAME.Core
{
    public unsafe class Memory
    {
        //public static byte[] mainrom, audiorom, mainram, audioram;
        //public static byte[] ;

        static byte[] mainrom, audiorom, mainram, audioram;
        public static void memory_reset()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                    CPS.sbyte0 = -1;
                    CPS.short1 = -1;
                    CPS.short2 = -1;
                    CPS.sbyte3 = -1;
                    break;
                case "CPS2":
                    CPS.short0 = -1;
                    CPS.short1 = -1;
                    CPS.short2 = -1;
                    break;
                case "Data East":
                    Dataeast.byte1 = 0xff;
                    Dataeast.byte2 = 0xff;
                    break;
                case "Tehkan":
                    Tehkan.byte0 = 0;
                    Tehkan.byte1 = 0;
                    Tehkan.byte2 = 0;
                    break;
                case "Neo Geo":
                    Neogeo.short0 = unchecked((short)0xff00);
                    Neogeo.short1 = -1;
                    Neogeo.short2 = -1;
                    Neogeo.short3 = 0x3f;
                    Neogeo.short4 = unchecked((short)0xffc0);
                    Neogeo.short5 = unchecked((short)0xffff);
                    Neogeo.short6 = 0x03;
                    break;
                case "Namco System 1":
                    Namcos1.byte0 = 0xff;
                    Namcos1.byte1 = 0xff;
                    Namcos1.byte2 = 0xff;
                    switch (Machine.sName)
                    {
                        case "faceoff":
                            Namcos1.byte00 = 0xff;
                            Namcos1.byte01 = 0xff;
                            Namcos1.byte02 = 0xff;
                            Namcos1.byte03 = 0xff;
                            break;
                    }
                    break;
                case "IGS011":
                    switch (Machine.sName)
                    {
                        case "drgnwrld":
                        case "drgnwrldv30":
                        case "drgnwrldv21":
                        case "drgnwrldv21j":
                        case "drgnwrldv20j":
                        case "drgnwrldv10c":
                        case "drgnwrldv11h":
                        case "drgnwrldv40k":
                            IGS011.sbyte0 = -1;
                            IGS011.sbyte1 = -1;
                            IGS011.sbyte2 = -1;
                            IGS011.sbytec = -1;
                            break;
                        case "lhb":
                        case "lhbv33c":
                        case "dbc":
                        case "ryukobou":
                            IGS011.bkey0 = 0xff;
                            IGS011.bkey1 = 0xff;
                            IGS011.bkey2 = 0xff;
                            IGS011.bkey3 = 0xff;
                            IGS011.bkey4 = 0xff;
                            IGS011.sbytec = -1;
                            break;
                    }
                    break;
                case "PGM":
                    PGM.short0 = -1;
                    PGM.short1 = -1;
                    PGM.short2 = -1;
                    PGM.short3 = 0xff;
                    PGM.short4 = 0;
                    break;
                case "M72":
                    M72.ushort0 = 0xffff;
                    M72.ushort1 = 0xffff;
                    break;
                case "M92":
                    M92.ushort0 = 0xffff;
                    M92.ushort1 = 0xff7f;
                    M92.ushort2 = 0xffff;
                    break;
                case "Taito":
                    Taito.sbyte0 = unchecked((sbyte)0xf3);
                    Taito.sbyte1 = -1;
                    Taito.sbyte2 = -1;
                    switch (Machine.sName)
                    {
                        case "tokio":
                        case "tokioo":
                        case "tokiou":
                        case "tokiob":
                            Taito.sbyte0 = unchecked((sbyte)0xd3);
                            break;
                        case "sboblbobl":
                            Taito.sbyte0 = unchecked((sbyte)0x73);
                            break;
                        case "opwolf":
                            Taito.sbyte0 = unchecked((sbyte)0xfc);
                            break;
                        case "opwolfp":
                            Taito.sbyte2 = 0x3e;
                            Taito.sbyte3 = 0;
                            break;
                    }
                    break;
                case "Taito B":
                    Taitob.sbyte0 = -1;
                    Taitob.sbyte1 = -1;
                    Taitob.sbyte2 = -1;
                    Taitob.sbyte3 = -1;
                    Taitob.sbyte4 = -1;
                    Taitob.sbyte5 = -1;
                    break;
                case "Konami 68000":
                    Konami68000.sbyte0 = -1;
                    Konami68000.sbyte1 = -1;
                    Konami68000.sbyte2 = -1;
                    Konami68000.sbyte3 = -1;
                    Konami68000.sbyte4 = -1;
                    switch (Machine.sName)
                    {
                        case "lgtnfght":
                        case "lgtnfghta":
                        case "lgtnfghtu":
                        case "trigon":
                            Konami68000.sbyte0 = unchecked((sbyte)0xfb);
                            break;
                        case "prmrsocr":
                        case "prmrsocrj":
                            Konami68000.sbyte0 = unchecked((sbyte)0xef);
                            Konami68000.bytee = 0xfe;
                            break;
                    }
                    break;
                case "Capcom":
                    switch (Machine.sName)
                    {
                        case "gng":
                        case "gnga":
                        case "gngbl":
                        case "gngprot":
                        case "gngblita":
                        case "gngc":
                        case "gngt":
                        case "makaimur":
                        case "makaimurc":
                        case "makaimurg":
                        case "diamond":
                            Capcom.bytes = 0xff;
                            Capcom.byte1 = 0xff;
                            Capcom.byte2 = 0xff;
                            break;
                        case "sf":
                            Capcom.short0 = -1;
                            Capcom.short1 = -1;
                            break;
                        case "sfua":
                        case "sfj":
                            Capcom.short1 = -1;
                            Capcom.short2 = -1;
                            Capcom.shortc = -1;
                            break;
                        case "sfjan":
                        case "sfan":
                        case "sfp":
                            Capcom.short0 = -1;
                            Capcom.shortc = -1;
                            Capcom.sbyte1 = 0;
                            Capcom.sbyte2 = 0;
                            Capcom.sbyte3 = 0;
                            Capcom.sbyte4 = 0;
                            break;
                    }
                    break;
            }
        }
        public static void memory_reset2()
        {
            switch (Machine.sBoard)
            {
                case "CPS-1":
                case "CPS-1(QSound)":
                    CPS.sbyte0_old = 0;
                    CPS.short1_old = 0;
                    CPS.short2_old = 0;
                    CPS.sbyte3_old = 0;
                    break;
                case "CPS2":
                    CPS.short0_old = 0;
                    CPS.short1_old = 0;
                    CPS.short2_old = 0;
                    break;
                case "Data East":
                    Dataeast.byte1_old = 0;
                    Dataeast.byte2_old = 0;
                    break;
                case "Tehkan":
                    Tehkan.byte0_old = 0;
                    Tehkan.byte1_old = 0;
                    Tehkan.byte2_old = 0;
                    break;
                case "Neo Geo":
                    Neogeo.short0_old = 0;
                    Neogeo.short1_old = 0;
                    Neogeo.short2_old = 0;
                    Neogeo.short3_old = 0;
                    Neogeo.short4_old = 0;
                    break;
                case "Namco System 1":
                    Namcos1.byte0_old = 0;
                    Namcos1.byte1_old = 0;
                    Namcos1.byte2_old = 0;
                    break;
                case "IGS011":
                    IGS011.sbyte0_old = 0;
                    IGS011.sbyte1_old = 0;
                    IGS011.sbyte2_old = 0;
                    IGS011.sbytec_old = 0;
                    break;
                case "PGM":
                    PGM.short0_old = 0;
                    PGM.short1_old = 0;
                    PGM.short2_old = 0;
                    PGM.short3_old = 0;
                    PGM.short4_old = 0;
                    break;
                case "M72":
                    M72.ushort0_old = 0;
                    M72.ushort1_old = 0;
                    break;
                case "M92":
                    M92.ushort0_old = 0;
                    M92.ushort1_old = 0;
                    M92.ushort2_old = 0;
                    break;
                case "Taito":
                    Taito.sbyte0_old = 0;
                    Taito.sbyte1_old = 0;
                    Taito.sbyte2_old = 0;
                    Taito.sbyte3_old = 0;
                    break;
                case "Taito B":
                    Taitob.dswb_old = 0;
                    Taitob.sbyte0_old = 0;
                    Taitob.sbyte1_old = 0;
                    Taitob.sbyte2_old = 0;
                    Taitob.sbyte3_old = 0;
                    Taitob.sbyte4_old = 0;
                    Taitob.sbyte5_old = 0;
                    break;
                case "Konami 68000":
                    Konami68000.sbyte0_old = 0;
                    Konami68000.sbyte1_old = 0;
                    Konami68000.sbyte2_old = 0;
                    Konami68000.sbyte3_old = 0;
                    Konami68000.sbyte4_old = 0;
                    switch (Machine.sName)
                    {
                        case "prmrsocr":
                        case "prmrsocrj":
                            Konami68000.bytee = 0;
                            break;
                    }
                    break;
                case "Capcom":
                    switch (Machine.sName)
                    {
                        case "gng":
                        case "gnga":
                        case "gngbl":
                        case "gngprot":
                        case "gngblita":
                        case "gngc":
                        case "gngt":
                        case "makaimur":
                        case "makaimurc":
                        case "makaimurg":
                        case "diamond":
                            Capcom.bytes_old = 0;
                            Capcom.byte1_old = 0;
                            Capcom.byte2_old = 0;
                            break;
                        case "sf":
                            Capcom.short0_old = 0;
                            Capcom.short1_old = 0;
                            Capcom.shorts_old = 0;
                            break;
                        case "sfua":
                        case "sfj":
                            Capcom.short1_old = 0;
                            Capcom.short2_old = 0;
                            Capcom.shortc_old = 0;
                            break;
                        case "sfjan":
                        case "sfan":
                        case "sfp":
                            Capcom.short0_old = 0;
                            Capcom.shortc_old = 0;
                            Capcom.sbyte1_old = 0;
                            Capcom.sbyte2_old = 0;
                            Capcom.sbyte3_old = 0;
                            Capcom.sbyte4_old = 0;
                            break;
                    }
                    break;
            }
        }

        static GCHandle mainrom_handle;
        public static byte* mainrom_Ptr;
        public static int mainrom_Lenght;
        public static bool mainrom_IsNull => mainrom == null;

        public static void Set_mainrom(byte[] data)
        {
            Release_mainrom();
            mainrom = data;
            mainrom_handle = GCHandle.Alloc(mainrom, GCHandleType.Pinned);
            mainrom_Ptr = (byte*)mainrom_handle.AddrOfPinnedObject();
            mainrom_Lenght = data.Length;
        }
        static void Release_mainrom()
        {
            if (mainrom != null)
            {
                if (mainrom_handle.IsAllocated)
                    mainrom_handle.Free();
            }
            mainrom = null;
            mainrom_handle = default;
            mainrom_Ptr = null;
            mainrom_Lenght = default;
        }


        static GCHandle audiorom_handle;
        public static byte* audiorom_Ptr;

        public static bool audiorom_IsNull => audiorom == null;

        public static void Set_audiorom(byte[] data)
        {
            Release_audiorom();
            audiorom = data;
            audiorom_handle = GCHandle.Alloc(audiorom, GCHandleType.Pinned);
            audiorom_Ptr = (byte*)audiorom_handle.AddrOfPinnedObject();
        }
        static void Release_audiorom()
        {
            if (audiorom != null)
            {
                if (audiorom_handle.IsAllocated)
                    audiorom_handle.Free();
            }
            audiorom = null;
            audiorom_handle = default;
            audiorom_Ptr = null;
        }


        static GCHandle mainram_handle;
        public static byte* mainram_Ptr;
        public static int mainram_Lenght;
        public static bool mainram_IsNull => mainram == null;

        public static void Set_mainram(byte[] data)
        {
            Release_mainram();
            mainram = data;
            mainram_handle = GCHandle.Alloc(mainram, GCHandleType.Pinned);
            mainram_Ptr = (byte*)mainram_handle.AddrOfPinnedObject();
            mainram_Lenght = data.Length;
        }
        static void Release_mainram()
        {
            if (mainram != null)
            {
                if (mainram_handle.IsAllocated)
                    mainram_handle.Free();
            }
            mainram = null;
            mainram_handle = default;
            mainram_Ptr = null;
            mainram_Lenght = default;
        }


        static GCHandle audioram_handle;
        public static byte* audioram_Ptr;
        public static int audioram_Lenght;
        public static bool audioram_IsNull => audioram == null;

        public static void Set_audioram(byte[] data)
        {
            Release_audioram();
            audioram = data;
            audioram_handle = GCHandle.Alloc(audioram, GCHandleType.Pinned);
            audioram_Ptr = (byte*)audioram_handle.AddrOfPinnedObject();
            audioram_Lenght = data.Length;
        }
        static void Release_audioram()
        {
            if (audioram != null)
            {
                if (audioram_handle.IsAllocated)
                    audioram_handle.Free();
            }
            audioram = null;
            audioram_handle = default;
            audioram_Ptr = null;
            audioram_Lenght = default;
        }

    }

    public unsafe static class AxiMemoryEx
    {
        static HashSet<GCHandle> GCHandles = new HashSet<GCHandle>();

        public static void Init()
        {
            FreeAllGCHandle();
            set_TempBuffer = new byte[0x20000];
        }

        public static void GetObjectPtr(this object srcObj, ref GCHandle handle, ref uint* ptr)
        {
            GetObjectPtr(srcObj, ref handle, out IntPtr intptr);
            ptr = (uint*)intptr;
        }
        public static void GetObjectPtr(this object srcObj, ref GCHandle handle, ref ushort* ptr)
        {
            GetObjectPtr(srcObj, ref handle, out IntPtr intptr);
            ptr = (ushort*)intptr;
        }
        public static void GetObjectPtr(this object srcObj, ref GCHandle handle, ref int* ptr)
        {
            GetObjectPtr(srcObj, ref handle, out IntPtr intptr);
            ptr = (int*)intptr;
        }
        public static void GetObjectPtr(this object srcObj, ref GCHandle handle, ref byte* ptr)
        {
            GetObjectPtr(srcObj, ref handle, out IntPtr intptr);
            ptr = (byte*)intptr;
        }

        static void GetObjectPtr(this object srcObj, ref GCHandle handle, out IntPtr intptr)
        {
            ReleaseGCHandle(ref handle);
            handle = GCHandle.Alloc(srcObj, GCHandleType.Pinned);
            GCHandles.Add(handle);
            intptr = handle.AddrOfPinnedObject();
        }

        public static void ReleaseGCHandle(this ref GCHandle handle)
        {
            if (handle.IsAllocated)
                handle.Free();
            GCHandles.Remove(handle);
        }

        public static void FreeAllGCHandle()
        {
            foreach (var handle in GCHandles)
            {
                if (handle.IsAllocated)
                    handle.Free();
            }
            GCHandles.Clear();
        }

        #region 指针化 TempBuffer
        static byte[] TempBuffer_src;
        static GCHandle TempBuffer_handle;
        public static byte* TempBuffer;
        public static byte[] set_TempBuffer
        {
            set
            {
                TempBuffer_handle.ReleaseGCHandle();
                TempBuffer_src = value;
                TempBuffer_src.GetObjectPtr(ref TempBuffer_handle, ref TempBuffer);
            }
        }
        #endregion

        public static void Write(this BinaryWriter bw, byte* buffer, int index, int count)
        {
            // 使用指针复制数据到临时数组
            Buffer.MemoryCopy(buffer + index, TempBuffer, count, count);
            // 使用BinaryWriter写入临时数组
            bw.Write(TempBuffer, 0, count);
        }

    }

    public unsafe static class AxiArray
    {

        public static void Copy(byte* src, int srcindex, byte* target, int targetindex, int count)
        {
            Buffer.MemoryCopy(&src[srcindex], target, targetindex, count);
        }

        public static void Copy(byte* src, byte* target, int index, int count)
        {
            Buffer.MemoryCopy(src, target, index, count);
        }

        public static void Copy(ushort* src, ushort* target, int index, int count)
        {
            long destinationStartIndexInBytes = index * sizeof(ushort);
            long totalBytesToCopy = count * sizeof(ushort);
            Buffer.MemoryCopy(src, target, destinationStartIndexInBytes, totalBytesToCopy);
        }
        public static void Copy(ushort* src, ushort* target, int count)
        {
            long totalBytesToCopy = count * sizeof(ushort);
            Buffer.MemoryCopy(src, target, 0, totalBytesToCopy);
        }
        public static void Copy(byte* src, byte* target, int count)
        {
            Buffer.MemoryCopy(src, target, 0, count);
        }
        public static void Clear(byte* data, int index, int lenght)
        {
            for (int i = index; i < lenght; i++, index++)
            {
                data[index] = 0;
            }
        }
        public static void Clear(ushort* data, int index, int lenght)
        {
            for (int i = index; i < lenght; i++, index++)
            {
                data[index] = 0;
            }
        }
    }

}