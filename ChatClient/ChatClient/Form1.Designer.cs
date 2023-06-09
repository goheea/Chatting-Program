﻿
using System;

namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.btn_Login = new System.Windows.Forms.Button();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.menu_start = new System.Windows.Forms.Button();
            this.rt_Message = new System.Windows.Forms.RichTextBox();
            this.menu_viewer = new System.Windows.Forms.PictureBox();
            this.menu_name = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.username_title = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu_viewer)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Logout);
            this.panel1.Controls.Add(this.btn_Login);
            this.panel1.Controls.Add(this.txt_user);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_Port);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_ServerIP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(306, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 98);
            this.panel1.TabIndex = 0;
            // 
            // btn_Logout
            // 
            this.btn_Logout.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Logout.Font = new System.Drawing.Font("AppleSDGothicNeoEB00", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Logout.ForeColor = System.Drawing.Color.DimGray;
            this.btn_Logout.Location = new System.Drawing.Point(546, 18);
            this.btn_Logout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Size = new System.Drawing.Size(83, 62);
            this.btn_Logout.TabIndex = 7;
            this.btn_Logout.Text = "종료";
            this.btn_Logout.UseVisualStyleBackColor = false;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Login.Font = new System.Drawing.Font("AppleSDGothicNeoEB00", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(223)))));
            this.btn_Login.Location = new System.Drawing.Point(418, 18);
            this.btn_Login.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(99, 62);
            this.btn_Login.TabIndex = 6;
            this.btn_Login.Text = "로그인";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            this.btn_Login.MouseEnter += new System.EventHandler(this.btn_Login_MouseEnter);
            this.btn_Login.MouseLeave += new System.EventHandler(this.btn_Login_MouseLeave);
            // 
            // txt_user
            // 
            this.txt_user.Font = new System.Drawing.Font("AppleSDGothicNeoM00", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_user.Location = new System.Drawing.Point(277, 35);
            this.txt_user.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(117, 30);
            this.txt_user.TabIndex = 5;
            this.txt_user.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("AppleSDGothicNeoR00", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(216, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "닉네임";
            // 
            // txt_Port
            // 
            this.txt_Port.Font = new System.Drawing.Font("AppleSDGothicNeoM00", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Port.Location = new System.Drawing.Point(75, 55);
            this.txt_Port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_Port.Size = new System.Drawing.Size(118, 27);
            this.txt_Port.TabIndex = 3;
            this.txt_Port.Text = "7000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("AppleSDGothicNeoR00", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "포트";
            // 
            // txt_ServerIP
            // 
            this.txt_ServerIP.Font = new System.Drawing.Font("AppleSDGothicNeoM00", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ServerIP.Location = new System.Drawing.Point(75, 16);
            this.txt_ServerIP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ServerIP.Name = "txt_ServerIP";
            this.txt_ServerIP.Size = new System.Drawing.Size(118, 27);
            this.txt_ServerIP.TabIndex = 1;
            this.txt_ServerIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("AppleSDGothicNeoR00", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "서버";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(223)))), ((int)(((byte)(239)))));
            this.panel2.Controls.Add(this.btn_Send);
            this.panel2.Controls.Add(this.txt_message);
            this.panel2.Location = new System.Drawing.Point(312, 526);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 62);
            this.panel2.TabIndex = 1;
            // 
            // btn_Send
            // 
            this.btn_Send.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Send.Font = new System.Drawing.Font("AppleSDGothicNeoB00", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Send.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(156)))), ((int)(((byte)(217)))));
            this.btn_Send.Location = new System.Drawing.Point(546, 5);
            this.btn_Send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(79, 51);
            this.btn_Send.TabIndex = 7;
            this.btn_Send.Text = "전송";
            this.btn_Send.UseVisualStyleBackColor = false;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txt_message
            // 
            this.txt_message.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(223)))), ((int)(((byte)(239)))));
            this.txt_message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_message.Font = new System.Drawing.Font("AppleSDGothicNeoR00", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_message.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txt_message.Location = new System.Drawing.Point(21, 18);
            this.txt_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(505, 24);
            this.txt_message.TabIndex = 3;
            this.txt_message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_message_KeyDown);
            // 
            // menu_start
            // 
            this.menu_start.BackColor = System.Drawing.SystemColors.HighlightText;
            this.menu_start.Font = new System.Drawing.Font("AppleSDGothicNeoB00", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.menu_start.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(223)))));
            this.menu_start.Location = new System.Drawing.Point(82, 278);
            this.menu_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.menu_start.Name = "menu_start";
            this.menu_start.Size = new System.Drawing.Size(98, 49);
            this.menu_start.TabIndex = 9;
            this.menu_start.Text = "메뉴 추천";
            this.menu_start.UseVisualStyleBackColor = false;
            this.menu_start.Click += new System.EventHandler(this.button1_Click);
            // 
            // rt_Message
            // 
            this.rt_Message.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rt_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rt_Message.Font = new System.Drawing.Font("AppleSDGothicNeoR00", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rt_Message.Location = new System.Drawing.Point(312, 141);
            this.rt_Message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rt_Message.Name = "rt_Message";
            this.rt_Message.ReadOnly = true;
            this.rt_Message.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rt_Message.Size = new System.Drawing.Size(631, 372);
            this.rt_Message.TabIndex = 2;
            this.rt_Message.Text = "";
            // 
            // menu_viewer
            // 
            this.menu_viewer.Location = new System.Drawing.Point(27, 46);
            this.menu_viewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.menu_viewer.Name = "menu_viewer";
            this.menu_viewer.Size = new System.Drawing.Size(210, 188);
            this.menu_viewer.TabIndex = 3;
            this.menu_viewer.TabStop = false;
            // 
            // menu_name
            // 
            this.menu_name.BackColor = System.Drawing.Color.White;
            this.menu_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.menu_name.Enabled = false;
            this.menu_name.Font = new System.Drawing.Font("AppleSDGothicNeoSB00", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_name.ForeColor = System.Drawing.SystemColors.MenuText;
            this.menu_name.Location = new System.Drawing.Point(26, 241);
            this.menu_name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.menu_name.Name = "menu_name";
            this.menu_name.ReadOnly = true;
            this.menu_name.Size = new System.Drawing.Size(210, 28);
            this.menu_name.TabIndex = 4;
            this.menu_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(156)))), ((int)(((byte)(217)))));
            this.panel3.Controls.Add(this.usernameBox);
            this.panel3.Controls.Add(this.username_title);
            this.panel3.Location = new System.Drawing.Point(11, 105);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 136);
            this.panel3.TabIndex = 10;
            // 
            // usernameBox
            // 
            this.usernameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(156)))), ((int)(((byte)(217)))));
            this.usernameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usernameBox.Font = new System.Drawing.Font("AppleSDGothicNeoR00", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.usernameBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.usernameBox.Location = new System.Drawing.Point(21, 39);
            this.usernameBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.usernameBox.Multiline = true;
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.usernameBox.Size = new System.Drawing.Size(249, 88);
            this.usernameBox.TabIndex = 10;
            // 
            // username_title
            // 
            this.username_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(156)))), ((int)(((byte)(217)))));
            this.username_title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.username_title.Font = new System.Drawing.Font("AppleSDGothicNeoSB00", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_title.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.username_title.Location = new System.Drawing.Point(16, 11);
            this.username_title.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.username_title.Multiline = true;
            this.username_title.Name = "username_title";
            this.username_title.Size = new System.Drawing.Size(242, 32);
            this.username_title.TabIndex = 10;
            this.username_title.Text = "▪▫▪▫접속자 리스트▫▪▫▪";
            this.username_title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.menu_viewer);
            this.panel4.Controls.Add(this.menu_name);
            this.panel4.Controls.Add(this.textBox3);
            this.panel4.Controls.Add(this.menu_start);
            this.panel4.Location = new System.Drawing.Point(15, 256);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(266, 344);
            this.panel4.TabIndex = 11;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("AppleSDGothicNeoSB00", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.textBox3.Location = new System.Drawing.Point(14, 14);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(257, 41);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "오늘의 추천 메뉴는?";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(186)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Controls.Add(this.textBox2);
            this.panel5.Controls.Add(this.textBox1);
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Location = new System.Drawing.Point(-1, 1);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(298, 618);
            this.panel5.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(197, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 62);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(186)))), ((int)(((byte)(255)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Century Gothic", 19.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textBox2.Location = new System.Drawing.Point(109, 38);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(119, 40);
            this.textBox2.TabIndex = 17;
            this.textBox2.Text = "CHAT";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(186)))), ((int)(((byte)(255)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.textBox1.Location = new System.Drawing.Point(43, 36);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 37);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "OUR";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 618);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.rt_Message);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(21, 75, 21, 20);
            this.TransparencyKey = System.Drawing.Color.Orange;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu_viewer)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.RichTextBox rt_Message;
        private System.Windows.Forms.Button btn_Logout;
        private System.Windows.Forms.PictureBox menu_viewer;
        private System.Windows.Forms.TextBox menu_name;
        private System.Windows.Forms.Button menu_start;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox username_title;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
    }
}