using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace ModbusUCBN
{
    public delegate void HandleModbusData(UInt16[] readData);

    class ModbusSerialMaster
    {
        public ModbusSerialMaster(string portName)
            : this(portName, 19200, Parity.Even, 8, StopBits.One) { }

        ~ModbusSerialMaster()
        {
            COMPort.Close();
        }

        public ModbusSerialMaster(string portName, int baudRate, Parity parity, int data, StopBits stopBits)
        {
            COMPort = new SerialPort(portName, baudRate, parity, data, stopBits);
            COMPort.ReadTimeout = ReadAnswerTimeout;
            COMPort.Open();
        }

		public event HandleModbusData ModbusDataReceived;

        public void ModbusRequestRead(byte slaveAddress, UInt16 regAddress, UInt16 regNumber)
        {
            byte[] adu = ModbusADU.CreateRequestADU03(regAddress, regNumber);
            requestFrame = ModbusSerialPDU.CreatePDU(slaveAddress, adu);
            if (CommunicationThread!= null)
                while (CommunicationThread.IsAlive) ;
            CommunicationThread = new Thread(ModbusSendReceive);
            CommunicationThread.Start(regNumber * 2 + 5);
        }

        public void ModbusRequestWrite(byte slaveAddress, UInt16 regAddress, Int16[] values)
        {
            ushort[] uValues = new ushort[values.Length];
            Buffer.BlockCopy(values, 0, uValues, 0, values.Length * 2);
            byte[] adu = ModbusADU.CreateRequestADU10(regAddress, uValues);
            requestFrame = ModbusSerialPDU.CreatePDU(slaveAddress, adu);
            if (CommunicationThread != null)
                while (CommunicationThread.IsAlive) ;
            CommunicationThread = new Thread(ModbusSendReceive);
            CommunicationThread.Start(8);
        }

        private void ModbusSendReceive(object responseSize)
        {
                responseFrame = new byte[(int)responseSize];
                COMPort.DiscardInBuffer();
                COMPort.Write(requestFrame, 0, requestFrame.Length);
                Thread.Sleep(ResponseWaitingDelay);
                COMPort.Read(responseFrame, 0, (int)responseSize);
                byte[] adu = ModbusSerialPDU.extractADU(responseFrame);
                if (responseFrame[1] > (byte)127)
                    throw new ModbusPDUException(String.Format("Modbus application error : function {0:X}, exception {1:X}", responseFrame[1], responseFrame[2]));
                else
				if (responseFrame[1] == 0x03)
				{
					UInt16[] values = ModbusADU.GetValuesFromResponseADU03(adu);
					ModbusDataReceived (values);
				}
            CommunicationThread.Abort();
        }

        SerialPort COMPort;
        Thread CommunicationThread;
        private byte[] requestFrame;
        private byte[] responseFrame;
        const int ReadAnswerTimeout = 300;
        const int ResponseWaitingDelay = 100;
    }
}
