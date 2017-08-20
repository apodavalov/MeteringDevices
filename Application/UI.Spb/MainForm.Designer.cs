namespace MeteringDevices.UI.Spb
{
    partial class MainForm
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
            this.tb_Day = new System.Windows.Forms.TextBox();
            this.lb_Day = new System.Windows.Forms.Label();
            this.bt_Send = new System.Windows.Forms.Button();
            this.tb_Night = new System.Windows.Forms.TextBox();
            this.lb_Night = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_Day
            // 
            this.tb_Day.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Day.Location = new System.Drawing.Point(167, 5);
            this.tb_Day.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_Day.Name = "tb_Day";
            this.tb_Day.Size = new System.Drawing.Size(144, 20);
            this.tb_Day.TabIndex = 7;
            this.tb_Day.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // lb_Day
            // 
            this.lb_Day.AutoSize = true;
            this.lb_Day.Location = new System.Drawing.Point(9, 7);
            this.lb_Day.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Day.Name = "lb_Day";
            this.lb_Day.Size = new System.Drawing.Size(158, 13);
            this.lb_Day.TabIndex = 1;
            this.lb_Day.Text = "Дневное энергопотребление:";
            // 
            // bt_Send
            // 
            this.bt_Send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Send.Location = new System.Drawing.Point(11, 58);
            this.bt_Send.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bt_Send.Name = "bt_Send";
            this.bt_Send.Size = new System.Drawing.Size(299, 24);
            this.bt_Send.TabIndex = 13;
            this.bt_Send.Text = "Отправить!";
            this.bt_Send.UseVisualStyleBackColor = true;
            this.bt_Send.Click += new System.EventHandler(this.bt_Send_Click);
            // 
            // tb_Night
            // 
            this.tb_Night.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Night.Location = new System.Drawing.Point(167, 28);
            this.tb_Night.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_Night.Name = "tb_Night";
            this.tb_Night.Size = new System.Drawing.Size(144, 20);
            this.tb_Night.TabIndex = 8;
            this.tb_Night.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // lb_Night
            // 
            this.lb_Night.AutoSize = true;
            this.lb_Night.Location = new System.Drawing.Point(15, 30);
            this.lb_Night.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Night.Name = "lb_Night";
            this.lb_Night.Size = new System.Drawing.Size(150, 13);
            this.lb_Night.TabIndex = 2;
            this.lb_Night.Text = "Ночное энергопотребление:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 92);
            this.Controls.Add(this.lb_Night);
            this.Controls.Add(this.tb_Night);
            this.Controls.Add(this.bt_Send);
            this.Controls.Add(this.lb_Day);
            this.Controls.Add(this.tb_Day);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Показания приборов учёта (Спб)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Day;
        private System.Windows.Forms.Label lb_Day;
        private System.Windows.Forms.Button bt_Send;
        private System.Windows.Forms.TextBox tb_Night;
        private System.Windows.Forms.Label lb_Night;
    }
}

