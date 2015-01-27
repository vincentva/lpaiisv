using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ModbusUCBN
{
    public class ModbusPDUException : Exception
    {
        public ModbusPDUException() { }
        public ModbusPDUException(string msg) : base(msg) { }
    }

    class ModbusSerialPDU
    {
        public static byte[] CreatePDU(byte slaveAddress,byte[] frame)
        {
            byte[] newFrame = new byte[frame.Length + 1];
            newFrame[0] = slaveAddress;
            frame.CopyTo(newFrame, 1);
            return AppendCRC(newFrame);
        }

        public static byte[] extractADU(byte[] frame)
        {
            RemoveCRC(frame);
            byte[] adu = new byte[frame.Length - 1];
            Array.Copy(frame, 1, adu, 0, adu.Length);
            return adu;
        }


        static private byte[] AppendCRC(byte[] frame)
        {
            byte[] frameWithCRC = new byte[frame.Length + 2];
            frame.CopyTo(frameWithCRC, 0);
            ushort uicrc = computeCRC(frame);
            frameWithCRC[frame.Length] = (byte)uicrc;
            frameWithCRC[frame.Length+1] = (byte)(uicrc >> 8);
            return frameWithCRC;
        }


        static private void RemoveCRC(byte[] frame)
        {
            if (frame.Length < 2) throw new ModbusPDUException("Frame too short to contain CRC!");

            byte[] frameWithoutCRC = new byte[frame.Length - 2];
            Array.Copy(frame, frameWithoutCRC, frameWithoutCRC.Length);

            ushort crcRecu = (ushort)frame[frameWithoutCRC.Length];
            crcRecu += (ushort)(frame[frameWithoutCRC.Length + 1] * 256);
            ushort computedCRC = computeCRC(frameWithoutCRC);

            if (crcRecu == computedCRC)
               frame = frameWithoutCRC;
            else
                throw new ModbusPDUException("CRC Error.");
        }

        static private ushort computeCRC(byte[] frameWithoutCRC)
        {
            ushort uicrc = 0xFFFF, lsb = 0x01;
            foreach (byte octet in frameWithoutCRC)
            {
                uicrc ^= Convert.ToUInt16(octet);
                for (int i = 0; i < 8; i++)
                {
                    lsb &= uicrc;
                    uicrc >>= 1;
                    if (lsb == 0x01) uicrc ^= 0xA001;
                    lsb = 0x01;
                }
            }
            return uicrc;
        }
    }
}
