namespace ModbusExample
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonAddWriteList = new System.Windows.Forms.Button();
            this.buttonClearWriteList = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonReadWrite = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Read";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(104, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(86, 173);
            this.listBox1.TabIndex = 1;
            // 
            // buttonWrite
            // 
            this.buttonWrite.Location = new System.Drawing.Point(196, 70);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(86, 40);
            this.buttonWrite.TabIndex = 2;
            this.buttonWrite.Text = "Write";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 58);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Clear read list";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(288, 12);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(86, 173);
            this.listBox2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(196, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 20);
            this.textBox1.TabIndex = 5;
            // 
            // buttonAddWriteList
            // 
            this.buttonAddWriteList.Location = new System.Drawing.Point(196, 41);
            this.buttonAddWriteList.Name = "buttonAddWriteList";
            this.buttonAddWriteList.Size = new System.Drawing.Size(86, 23);
            this.buttonAddWriteList.TabIndex = 6;
            this.buttonAddWriteList.Text = "Add to write list";
            this.buttonAddWriteList.UseVisualStyleBackColor = true;
            this.buttonAddWriteList.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonClearWriteList
            // 
            this.buttonClearWriteList.Location = new System.Drawing.Point(196, 116);
            this.buttonClearWriteList.Name = "buttonClearWriteList";
            this.buttonClearWriteList.Size = new System.Drawing.Size(86, 24);
            this.buttonClearWriteList.TabIndex = 7;
            this.buttonClearWriteList.Text = "Clear write list";
            this.buttonClearWriteList.UseVisualStyleBackColor = true;
            this.buttonClearWriteList.Click += new System.EventHandler(this.button5_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // buttonReadWrite
            // 
            this.buttonReadWrite.Location = new System.Drawing.Point(196, 160);
            this.buttonReadWrite.Name = "buttonReadWrite";
            this.buttonReadWrite.Size = new System.Drawing.Size(86, 23);
            this.buttonReadWrite.TabIndex = 8;
            this.buttonReadWrite.Text = "Read/Write";
            this.buttonReadWrite.UseVisualStyleBackColor = true;
            this.buttonReadWrite.Click += new System.EventHandler(this.buttonReadWrite_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 195);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonReadWrite);
            this.Controls.Add(this.buttonClearWriteList);
            this.Controls.Add(this.buttonAddWriteList);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonWrite;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonAddWriteList;
        private System.Windows.Forms.Button buttonClearWriteList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonReadWrite;
        private System.Windows.Forms.Button button2;

    }
}

