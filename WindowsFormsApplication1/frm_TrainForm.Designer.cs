﻿namespace BackTestSystem
{
    partial class frm_TrainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Train = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ddl_MLFunc = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_BegExpect = new System.Windows.Forms.TextBox();
            this.txt_DataLength = new System.Windows.Forms.TextBox();
            this.txt_LearnDeep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_begT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_endT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1883, 1025);
            this.splitContainer1.SplitterDistance = 370;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_LearnDeep);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ddl_MLFunc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 338);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "训练方法";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_endT);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_begT);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btn_Train);
            this.panel1.Location = new System.Drawing.Point(1415, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 312);
            this.panel1.TabIndex = 1;
            // 
            // btn_Train
            // 
            this.btn_Train.Location = new System.Drawing.Point(128, 207);
            this.btn_Train.Name = "btn_Train";
            this.btn_Train.Size = new System.Drawing.Size(129, 45);
            this.btn_Train.TabIndex = 0;
            this.btn_Train.Text = "训练";
            this.btn_Train.UseVisualStyleBackColor = true;
            this.btn_Train.Click += new System.EventHandler(this.btn_Train_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "训练方法";
            // 
            // ddl_MLFunc
            // 
            this.ddl_MLFunc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_MLFunc.FormattingEnabled = true;
            this.ddl_MLFunc.Location = new System.Drawing.Point(147, 38);
            this.ddl_MLFunc.Name = "ddl_MLFunc";
            this.ddl_MLFunc.Size = new System.Drawing.Size(310, 32);
            this.ddl_MLFunc.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_DataLength);
            this.groupBox2.Controls.Add(this.txt_BegExpect);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(522, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(887, 337);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据集";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "起始期号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(381, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "期数";
            // 
            // txt_BegExpect
            // 
            this.txt_BegExpect.Location = new System.Drawing.Point(133, 33);
            this.txt_BegExpect.Name = "txt_BegExpect";
            this.txt_BegExpect.Size = new System.Drawing.Size(238, 35);
            this.txt_BegExpect.TabIndex = 2;
            this.txt_BegExpect.Text = "590963";
            // 
            // txt_DataLength
            // 
            this.txt_DataLength.Location = new System.Drawing.Point(468, 33);
            this.txt_DataLength.Name = "txt_DataLength";
            this.txt_DataLength.Size = new System.Drawing.Size(238, 35);
            this.txt_DataLength.TabIndex = 3;
            this.txt_DataLength.Text = "10000";
            // 
            // txt_LearnDeep
            // 
            this.txt_LearnDeep.Location = new System.Drawing.Point(146, 89);
            this.txt_LearnDeep.Name = "txt_LearnDeep";
            this.txt_LearnDeep.Size = new System.Drawing.Size(311, 35);
            this.txt_LearnDeep.TabIndex = 4;
            this.txt_LearnDeep.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "学习深度";
            // 
            // txt_begT
            // 
            this.txt_begT.Location = new System.Drawing.Point(145, 30);
            this.txt_begT.Name = "txt_begT";
            this.txt_begT.Size = new System.Drawing.Size(238, 35);
            this.txt_begT.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "开始时间";
            // 
            // txt_endT
            // 
            this.txt_endT.Location = new System.Drawing.Point(145, 71);
            this.txt_endT.Name = "txt_endT";
            this.txt_endT.Size = new System.Drawing.Size(238, 35);
            this.txt_endT.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "起始期号";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 37;
            this.dataGridView1.Size = new System.Drawing.Size(1883, 651);
            this.dataGridView1.TabIndex = 0;
            // 
            // frm_TrainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1883, 1025);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frm_TrainForm";
            this.Text = "机器学习训练平台";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Train;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddl_MLFunc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DataLength;
        private System.Windows.Forms.TextBox txt_BegExpect;
        private System.Windows.Forms.TextBox txt_LearnDeep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_endT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_begT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}