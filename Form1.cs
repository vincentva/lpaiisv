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
            HandleModbusData traitementDonneesRecues = new HandleModbusData(DisplayModbusData);
            ModbusMaster1.ModbusDataReceived += delegate(UInt16[] donneesLues) { Invoke(traitementDonneesRecues, donneesLues ); };
        }

        ModbusSerialMaster ModbusMaster1;
        public UInt16[] readValues;

        private void button1_Click(object sender, EventArgs e)
        {
            ModbusMaster1.ModbusRequestRead(1, 24, 8);
        }

        void DisplayModbusData(UInt16[] values)
        {
            foreach (object value in values)
                listBox1.Items.Add(value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int NbItemsToSend = Math.Min(8,listBox2.Items.Count);
            Int16[] testValues = new Int16[NbItemsToSend];
            int idx = 0;
            for (idx = 0; idx < NbItemsToSend;idx++ )
                {
                    testValues[idx] = unchecked((Int16)(UInt16)listBox2.Items[idx]);
                }
            ModbusMaster1.ModbusRequestWrite(1,16,testValues);
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
            UInt16 value;
            if (UInt16.TryParse(textBox1.Text, out value))
                listBox2.Items.Add(value);
        }

        
    }
}
