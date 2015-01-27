using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModbusUCBN;

namespace ModbusExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ModbusMaster1 = new ModbusSerialMaster("COM9");
            HandleModbusData DisplayModbusDataDelegate = new HandleModbusData(DisplayModbusData);
            ModbusMaster1.ModbusDataReceived += delegate(Int16[] donneesLues) { Invoke(DisplayModbusDataDelegate, donneesLues ); };

            HandleModbusError HandleCommErrorDelegate = new HandleModbusError(HandleCommError);
            ModbusMaster1.ModbusError += delegate(Exception ex) { Invoke(HandleCommErrorDelegate,ex); };
        }

        ModbusSerialMaster ModbusMaster1;
        
        private void HandleCommError(Exception ex)
        {
            MessageBox.Show(ex.Message);
            ModbusMaster1.CommException = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readOutputValues();
        }

        private void DisplayModbusData(Int16[] values)
        {
            foreach (object value in values)
                listBox1.Items.Add(value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            writeInputValues();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int idx = listBox1.Items.Count; idx > 0; idx--)
                listBox1.Items.RemoveAt(idx - 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int idx = listBox2.Items.Count; idx > 0; idx--)
                listBox2.Items.RemoveAt(idx - 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Int16 value;
            if (Int16.TryParse(textBox1.Text, out value))
                listBox2.Items.Add(value);
        }

        private void buttonReadWrite_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            button1_Click(sender, e);
        }

        private void readOutputValues()
        {
            try
            {
                ModbusMaster1.ModbusRequestRead(1, 24, 8);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void writeInputValues()
        {
            int NbItemsToSend = Math.Min(8, listBox2.Items.Count);
            if (NbItemsToSend > 0)
            {
                Int16[] testValues = new Int16[NbItemsToSend];
                int idx = 0;
                for (idx = 0; idx < NbItemsToSend; idx++)
                {
                    Int16 temp = (Int16)listBox2.Items[idx];
                    testValues[idx] = unchecked((Int16)temp);
                }
                try
                {
                    ModbusMaster1.ModbusRequestWrite(1, 16, testValues);
                 }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            short[] testValues = {1,2,3,4};
            ModbusMaster1.ModbusRequestWrite(1,24,testValues);
        }

        
    }
}
