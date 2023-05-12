namespace ChatClient
{
    partial class Form2
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
            this.startbutton = new System.Windows.Forms.Button();
            this.exitbutton = new System.Windows.Forms.Button();
            this.metroUserControl1 = new MetroFramework.Controls.MetroUserControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menu_name = new MetroFramework.Controls.MetroTextBox();
            this.title = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // startbutton
            // 
            this.startbutton.BackColor = System.Drawing.Color.AliceBlue;
            this.startbutton.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.startbutton.Location = new System.Drawing.Point(110, 303);
            this.startbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startbutton.Name = "startbutton";
            this.startbutton.Size = new System.Drawing.Size(122, 45);
            this.startbutton.TabIndex = 3;
            this.startbutton.Text = "추천 받기";
            this.startbutton.UseVisualStyleBackColor = false;
            this.startbutton.Click += new System.EventHandler(this.startbutton_Click);
            // 
            // exitbutton
            // 
            this.exitbutton.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.exitbutton.ForeColor = System.Drawing.Color.Black;
            this.exitbutton.Location = new System.Drawing.Point(272, 334);
            this.exitbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.exitbutton.Name = "exitbutton";
            this.exitbutton.Size = new System.Drawing.Size(51, 30);
            this.exitbutton.TabIndex = 5;
            this.exitbutton.Text = "종료";
            this.exitbutton.UseVisualStyleBackColor = true;
            this.exitbutton.Click += new System.EventHandler(this.exitbutton_Click);
            // 
            // metroUserControl1
            // 
            this.metroUserControl1.Location = new System.Drawing.Point(340, 258);
            this.metroUserControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.metroUserControl1.Name = "metroUserControl1";
            this.metroUserControl1.Size = new System.Drawing.Size(7, 6);
            this.metroUserControl1.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(59, 82);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 161);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // menu_name
            // 
            this.menu_name.BackColor = System.Drawing.Color.White;
            this.menu_name.CustomBackground = true;
            this.menu_name.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.menu_name.ForeColor = System.Drawing.Color.Black;
            this.menu_name.Location = new System.Drawing.Point(105, 247);
            this.menu_name.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.menu_name.Name = "menu_name";
            this.menu_name.Size = new System.Drawing.Size(122, 34);
            this.menu_name.Style = MetroFramework.MetroColorStyle.White;
            this.menu_name.TabIndex = 10;
            this.menu_name.Text = "음식 이름";
            this.menu_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.menu_name.UseStyleColors = true;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.White;
            this.title.CustomBackground = true;
            this.title.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.title.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.title.ForeColor = System.Drawing.Color.Black;
            this.title.Location = new System.Drawing.Point(40, 44);
            this.title.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(262, 34);
            this.title.Style = MetroFramework.MetroColorStyle.White;
            this.title.TabIndex = 11;
            this.title.Text = "오늘의 날씨와 어울리는 음식은?";
            this.title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.title.UseStyleColors = true;
            // 
            // Form2
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(339, 388);
            this.ControlBox = false;
            this.Controls.Add(this.exitbutton);
            this.Controls.Add(this.startbutton);
            this.Controls.Add(this.title);
            this.Controls.Add(this.menu_name);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.metroUserControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Padding = new System.Windows.Forms.Padding(18, 48, 18, 16);
            this.Resizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TextAlign = System.Windows.Forms.VisualStyles.HorizontalAlign.Center;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startbutton;
        private System.Windows.Forms.Button exitbutton;
        private MetroFramework.Controls.MetroUserControl metroUserControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroTextBox menu_name;
        private MetroFramework.Controls.MetroTextBox title;
    }
}