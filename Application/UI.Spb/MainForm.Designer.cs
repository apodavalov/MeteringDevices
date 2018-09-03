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
            this.lb_KitchenHot = new System.Windows.Forms.Label();
            this.tb_KitchenHot = new System.Windows.Forms.TextBox();
            this.lb_KitchenCold = new System.Windows.Forms.Label();
            this.tb_KitchenCold = new System.Windows.Forms.TextBox();
            this.lb_BathroomHot = new System.Windows.Forms.Label();
            this.tb_BathroomHot = new System.Windows.Forms.TextBox();
            this.lb_BathroomCold = new System.Windows.Forms.Label();
            this.tb_BathroomCold = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_Day
            // 
            this.tb_Day.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Day.Location = new System.Drawing.Point(167, 5);
            this.tb_Day.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Day.Name = "tb_Day";
            this.tb_Day.Size = new System.Drawing.Size(144, 20);
            this.tb_Day.TabIndex = 2;
            this.tb_Day.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // lb_Day
            // 
            this.lb_Day.AutoSize = true;
            this.lb_Day.Location = new System.Drawing.Point(5, 8);
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
            this.bt_Send.Location = new System.Drawing.Point(11, 155);
            this.bt_Send.Margin = new System.Windows.Forms.Padding(2);
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
            this.tb_Night.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Night.Name = "tb_Night";
            this.tb_Night.Size = new System.Drawing.Size(144, 20);
            this.tb_Night.TabIndex = 4;
            this.tb_Night.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // lb_Night
            // 
            this.lb_Night.AutoSize = true;
            this.lb_Night.Location = new System.Drawing.Point(13, 31);
            this.lb_Night.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_Night.Name = "lb_Night";
            this.lb_Night.Size = new System.Drawing.Size(150, 13);
            this.lb_Night.TabIndex = 3;
            this.lb_Night.Text = "Ночное энергопотребление:";
            // 
            // lb_KitchenHot
            // 
            this.lb_KitchenHot.AutoSize = true;
            this.lb_KitchenHot.Location = new System.Drawing.Point(48, 78);
            this.lb_KitchenHot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_KitchenHot.Name = "lb_KitchenHot";
            this.lb_KitchenHot.Size = new System.Drawing.Size(115, 13);
            this.lb_KitchenHot.TabIndex = 7;
            this.lb_KitchenHot.Text = "Горячая вода (кухня):";
            // 
            // tb_KitchenHot
            // 
            this.tb_KitchenHot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_KitchenHot.Location = new System.Drawing.Point(167, 75);
            this.tb_KitchenHot.Margin = new System.Windows.Forms.Padding(2);
            this.tb_KitchenHot.Name = "tb_KitchenHot";
            this.tb_KitchenHot.Size = new System.Drawing.Size(144, 20);
            this.tb_KitchenHot.TabIndex = 8;
            // 
            // lb_KitchenCold
            // 
            this.lb_KitchenCold.AutoSize = true;
            this.lb_KitchenCold.Location = new System.Drawing.Point(40, 55);
            this.lb_KitchenCold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_KitchenCold.Name = "lb_KitchenCold";
            this.lb_KitchenCold.Size = new System.Drawing.Size(123, 13);
            this.lb_KitchenCold.TabIndex = 5;
            this.lb_KitchenCold.Text = "Холодная вода (кухня):";
            // 
            // tb_KitchenCold
            // 
            this.tb_KitchenCold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_KitchenCold.Location = new System.Drawing.Point(167, 52);
            this.tb_KitchenCold.Margin = new System.Windows.Forms.Padding(2);
            this.tb_KitchenCold.Name = "tb_KitchenCold";
            this.tb_KitchenCold.Size = new System.Drawing.Size(144, 20);
            this.tb_KitchenCold.TabIndex = 6;
            // 
            // lb_BathroomHot
            // 
            this.lb_BathroomHot.AutoSize = true;
            this.lb_BathroomHot.Location = new System.Drawing.Point(40, 125);
            this.lb_BathroomHot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_BathroomHot.Name = "lb_BathroomHot";
            this.lb_BathroomHot.Size = new System.Drawing.Size(123, 13);
            this.lb_BathroomHot.TabIndex = 11;
            this.lb_BathroomHot.Text = "Горячая вода (ванная):";
            // 
            // tb_BathroomHot
            // 
            this.tb_BathroomHot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_BathroomHot.Location = new System.Drawing.Point(167, 122);
            this.tb_BathroomHot.Margin = new System.Windows.Forms.Padding(2);
            this.tb_BathroomHot.Name = "tb_BathroomHot";
            this.tb_BathroomHot.Size = new System.Drawing.Size(144, 20);
            this.tb_BathroomHot.TabIndex = 12;
            // 
            // lb_BathroomCold
            // 
            this.lb_BathroomCold.AutoSize = true;
            this.lb_BathroomCold.Location = new System.Drawing.Point(32, 102);
            this.lb_BathroomCold.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_BathroomCold.Name = "lb_BathroomCold";
            this.lb_BathroomCold.Size = new System.Drawing.Size(131, 13);
            this.lb_BathroomCold.TabIndex = 9;
            this.lb_BathroomCold.Text = "Холодная вода (ванная):";
            // 
            // tb_BathroomCold
            // 
            this.tb_BathroomCold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_BathroomCold.Location = new System.Drawing.Point(167, 99);
            this.tb_BathroomCold.Margin = new System.Windows.Forms.Padding(2);
            this.tb_BathroomCold.Name = "tb_BathroomCold";
            this.tb_BathroomCold.Size = new System.Drawing.Size(144, 20);
            this.tb_BathroomCold.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 189);
            this.Controls.Add(this.lb_BathroomHot);
            this.Controls.Add(this.tb_BathroomHot);
            this.Controls.Add(this.lb_BathroomCold);
            this.Controls.Add(this.tb_BathroomCold);
            this.Controls.Add(this.lb_KitchenHot);
            this.Controls.Add(this.tb_KitchenHot);
            this.Controls.Add(this.lb_KitchenCold);
            this.Controls.Add(this.tb_KitchenCold);
            this.Controls.Add(this.lb_Night);
            this.Controls.Add(this.tb_Night);
            this.Controls.Add(this.bt_Send);
            this.Controls.Add(this.lb_Day);
            this.Controls.Add(this.tb_Day);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Label lb_KitchenHot;
        private System.Windows.Forms.TextBox tb_KitchenHot;
        private System.Windows.Forms.Label lb_KitchenCold;
        private System.Windows.Forms.TextBox tb_KitchenCold;
        private System.Windows.Forms.Label lb_BathroomHot;
        private System.Windows.Forms.TextBox tb_BathroomHot;
        private System.Windows.Forms.Label lb_BathroomCold;
        private System.Windows.Forms.TextBox tb_BathroomCold;
    }
}

