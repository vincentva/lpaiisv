using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace ModbusUCBN
{
    public delegate void HandleModbusData(Int16[] readData);
    public delegate void HandleModbusError(Exception ex);

    struct ModbusParam
    {
        public ModbusParam(int r, byte[] requestADU)
        {
            ResponseSize = r;
            RequestADU = requestADU;
        }
        public int ResponseSize;
        public byte[] RequestADU;
    }

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
        public event HandleModbusError ModbusError;

        public void ModbusRequestRead(byte slaveAddress, UInt16 regAddress, UInt16 regNumber)
        {
            byte[] adu = ModbusADU.CreateRequestADU03(regAddress, regNumber);
            byte[] requestFrame = ModbusSerialPDU.CreatePDU(slaveAddress, adu);
            /*if (CommunicationThread!= null)
                while (CommunicationThread.IsAlive) ;*/
            ModbusParam param = new ModbusParam(regNumber * 2 + 5, requestFrame);
            Thread CommunicationThread = new Thread(ModbusSendReceive);
            CommunicationThread.Start(param);
        }

        public void ModbusRequestWrite(byte slaveAddress, UInt16 regAddress, Int16[] values)
        {
            ushort[] uValues = new ushort[values.Length];
            Buffer.BlockCopy(values, 0, uValues, 0, values.Length * 2);
            byte[] adu = ModbusADU.CreateRequestADU10(regAddress, uValues);
            byte[] requestFrame = ModbusSerialPDU.CreatePDU(slaveAddress, adu);
            /*if (CommunicationThread != null)
                while (CommunicationThread.IsAlive) ;*/
            Thread CommunicationThread = new Thread(ModbusSendReceive);
            ModbusParam param = new ModbusParam(8, requestFrame);
            CommunicationThread.Start(param);
        }

        private void ModbusSendReceive(object param)
        {
            lock (this)
            {
                try
                {
                    ModbusParam parameters;
                    if (param is ModbusParam)
                        parameters = (ModbusParam)param;
                    else
                        throw new ModbusPDUException("Wrong parametrization of ModbusSendReceive method.");
                    byte[] responseFrame = new byte[(int)parameters.ResponseSize];
                    COMPort.DiscardInBuffer();
                    COMPort.Write(parameters.RequestADU, 0, parameters.RequestADU.Length);
                    Thread.Sleep(ResponseWaitingDelay);
                    COMPort.Read(responseFrame, 0, (int)parameters.ResponseSize);
                    byte[] adu = ModbusSerialPDU.extractADU(responseFrame);
                    if (responseFrame[1] > (byte)127)
                        throw new ModbusPDUException(String.Format("Modbus application error : function {0:X}, exception {1:X}", responseFrame[1], responseFrame[2]));
                    else
                        if (responseFrame[1] == 0x03)
                        {
                            Int16[] values = GetValuesFromResponseADU03(adu);
                            ModbusDataReceived(values);
                        }
                }
                catch (Exception ex)
                {
                    commException = ex;
                    ModbusError(ex);
                }
            }
        }

        private Int16[] GetValuesFromResponseADU03(byte[] adu)
        {
            int N = (int)adu[1] / 2; // where adu[1] = Modbus ADU byte count
            Int16[] values = new Int16[N];
            byte[] temp = new byte[2];
            for (int i = 1; i <= N; i++)
            {
                Array.Copy(adu, i * 2, temp, 0, 2);
                values[i - 1] = unchecked((Int16)(temp[1] + (temp[0] << 8)));
            }
            return values;
        }

        SerialPort COMPort;
        //Thread CommunicationThread;
        //private byte[] requestFrame;
        //private byte[] responseFrame;
        //int requestPending = 0;
        Exception commException = null;
        public Exception CommException
        {
            get { return commException; }
            set {
                if (value == null) commException = null;
                else throw new ModbusPDUException("CommException can only be set to null.");
            }
        }
        const int ReadAnswerTimeout = 300;
        const int ResponseWaitingDelay = 100;
    }
}
