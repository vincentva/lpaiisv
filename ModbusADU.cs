using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusUCBN
{
    class ModbusADU
    {
        static public byte[] CreateRequestADU03(UInt16 regAddress,UInt16 regNumber)
        {
            byte[] bytesRegAddress = UInt16ToBytes(regAddress);
            byte[] bytesRegNumber = UInt16ToBytes(regNumber);
            byte[] adu = new byte[5];
            adu[0] = 0x03;
            bytesRegAddress.CopyTo(adu, 1);
            bytesRegNumber.CopyTo(adu, 3);
            return adu;
        }

        static public byte[] CreateRequestADU10(UInt16 regAddress, UInt16[] regValues)
        {
            UInt16 regNumber = (UInt16)regValues.Length;
            byte[] bytesRegAddress = UInt16ToBytes(regAddress);
            byte[] bytesRegNumber = UInt16ToBytes(regNumber);
            byte[] adu = new byte[6+regValues.Length*2];
            adu[0] = 0x10;
            bytesRegAddress.CopyTo(adu, 1);
            bytesRegNumber.CopyTo(adu, 3);
            adu[5] = (byte) (regNumber * 2);
            byte[] temp = new byte[2];
            int i = 6;
            foreach (UInt16 value in regValues)
            {
                temp = UInt16ToBytes(value);
                temp.CopyTo(adu, i);
                i += 2;
            }
            return adu;
        }

        static public UInt16[] GetValuesFromResponseADU03(byte[] adu)
        {
            int N = (int)adu[1] / 2; // where adu[1] = Modbus ADU byte count
            UInt16[] values = new UInt16[N];
            byte[] temp = new byte[2];
            for (int i = 1; i <= N; i++) {
                Array.Copy(adu, i * 2, temp, 0, 2);
                values[i - 1] = BytesToUInt16(temp);
            }
            return values;
        }
        
        static private byte[] UInt16ToBytes(UInt16 uinteger)
        {
            byte[] bytesArray = new byte[2];
            bytesArray[1] = (byte)uinteger;
            bytesArray[0] = (byte)(uinteger >> 8);
            return bytesArray;
        }

        static private UInt16 BytesToUInt16(byte[] bytesArray)
        {
            UInt16 uinteger;
            uinteger = (UInt16)bytesArray[1];
            uinteger += (UInt16) (bytesArray[0]*256);
            return uinteger;
        }
    }
}
