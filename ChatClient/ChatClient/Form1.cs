﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; //참조 추가를 해야함(어셈블리탭 -> System.Configuration.dll)
using MySql.Data.MySqlClient; //솔루션용 nuget패키지 관리자에서 MySql.Data를 설치해야함.

namespace ChatClient
{
    public partial class Form1 : MetroForm
    {
        public MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=chatting_program;Uid=root;Pwd=1234");
        TcpClient clientSocket; // 소켓
        NetworkStream stream = default(NetworkStream);
        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;
        bool bThreadExit = false;
        string menuresult = "";
        string menuimage = "";
        string tmp = "";
        String curDate = DateTime.Now.ToString("HH:mm:ss");

        //지수
        //public virtual string PlaceholderText { get; set; }
        public bool test = false;
        TextBox[] txtList;
        const string msgPlaceholder = "메세지를 입력하세요.";

        public Form1()
        {
            InitializeComponent();

            txtList = new TextBox[] {txt_message};
            foreach (var txt in txtList)
            {
                txt.ForeColor = Color.DarkGray; //처음 Placeholder 색 지정
                if (txt == txt_message) txt.Text = msgPlaceholder; //처음 Placeholder 글 설정
                txt.GotFocus += RemovePlaceholder; //텍스트박스 커서 Focus 여부에 따라 이벤트 지정
                txt.LostFocus += SetPlaceholder;
            }
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == msgPlaceholder)
            { //텍스트박스 내용이 사용자가 입력한 값이 아닌 Placeholder일 경우에만, 커서 포커스일때 빈칸으로 만들기
                txt.ForeColor = Color.Black; //사용자 입력 진한 글씨
                txt.Text = string.Empty;
            }
        }
        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text)) //사용자 입력값이 하나도 없는 경우에 포커스 잃으면 Placeholder 적용해주기
            {                
                txt.ForeColor = Color.DarkGray;
                if (txt == txt_message) { txt.Text = msgPlaceholder;}
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            bThreadExit = false;
            int Port = Int32.Parse(txt_Port.Text);
            clientSocket = new TcpClient();
            stream = default(NetworkStream);
            try
            {
                clientSocket.Connect(txt_ServerIP.Text, Port); // 접속 IP 및 포트
                stream = clientSocket.GetStream();
                conn.Open();
            }
            catch (Exception e2)
            {
                MessageBox.Show("서버가 실행중이 아닙니다.", "연결 실패!");
                return;
            }
            btn_Login.Enabled = false;
            txt_user.Enabled = false;
            menu_viewer.Enabled = false;
            menu_name.Enabled = false;

            byte[] buffer = Encoding.Unicode.GetBytes("Login$" + txt_user.Text + CR + LF);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();

            Thread t_handler = new Thread(GetMessage);
            t_handler.IsBackground = true;
            t_handler.Start();
        }

        public void GetMessage() //서버측에 전송하기 전 클라이언트측의 입력 데이터 가져옴
        {
            while (!bThreadExit)
            {
                stream = clientSocket.GetStream();
                int BUFFERSIZE = clientSocket.ReceiveBufferSize;
                byte[] buffer = new byte[BUFFERSIZE];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.Unicode.GetString(buffer, 0, bytes);
                Console.WriteLine("메시지는 {0}", message);
                

                if (message.Contains("메뉴 추천 버튼 클릭"))
                {
                    menuresult = message.Remove(0, 12);
                    tmp = menuresult;
                    menuresult = menuresult.Remove(menuresult.IndexOf("$"));
                    menuimage = tmp.Remove(0, tmp.IndexOf("$") + 1);
                    DisplayMenuText(menuresult);
                    DisplayMenuImage(menuimage);
                }

                else
                {
                    DisplayText(message);
                    if (message.EndsWith("님이 입장하셨습니다.") || message.EndsWith("님이 대화방을 나갔습니다."))
                    {
                        if (message.EndsWith("님이 입장하셨습니다."))
                        {
                            DisplayName();
                        }
                        if (message.EndsWith("님이 대화방을 나갔습니다."))
                        {
                            DisplayName();
                        }
                    }
                    else
                    {
                        show_alert1(message);
                    }
                }
            }
        }

        private void DisplayText(string message)
        {
            if (rt_Message.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                rt_Message.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    rt_Message.AppendText(message + Environment.NewLine);
                    
                }));
            }
            else
                rt_Message.AppendText(message + Environment.NewLine);
        }

        private void DisplayMenuText(string message)
        {
            if (menu_name.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                menu_name.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    menu_name.Text = message;
                }));
            }
            else
                menu_name.Text = message;
        }

        private void DisplayMenuImage(string message)
        {
            if (menu_viewer.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                menu_viewer.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    menu_viewer.ImageLocation = message;
                    menu_viewer.SizeMode = PictureBoxSizeMode.StretchImage;
                }));
            }
            else
            {
                menu_viewer.ImageLocation = message;
                menu_viewer.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void DisplayName()
        {
            if (usernameBox.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                //usernameBox.Clear();
                usernameBox.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    usernameBox.Clear();
                    DataSet ds = new DataSet();
                    string query = "SELECT name from chatting_program.user_names";
                    MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                    adpt.Fill(ds, "name");
                    if(ds.Tables.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            usernameBox.AppendText(r["name"] + Environment.NewLine);
                        }
                    }
                }));
            }
            else
            {
                usernameBox.Clear();
                DataSet ds = new DataSet();
                string query = "SELECT name from chatting_program.user_names";
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
                adpt.Fill(ds, "name");
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        usernameBox.AppendText(r["name"] + Environment.NewLine);
                    }
                }
            }
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Unicode.GetBytes("exit" + CR + LF);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            bThreadExit = true;
            Thread.Sleep(1000);

            Application.Exit();

        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(txt_user.Text + "$" + txt_message.Text + CR + LF); //김지원$안녕하세요
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            txt_message.Text = "";
        }

        private void txt_message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btn_Send_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Unicode.GetBytes("메뉴 추천 버튼 클릭$" + CR + LF);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        private void show_alert1(string message)                      //비동기 메소드............await
        {
            Form2 alert = new Form2();
            string text = message;

            int startIndex = text.IndexOf(']');  // ']' 인덱스값
            int colonIndex = text.IndexOf(':', startIndex); // startIndex 이후의 ':' 인덱스 찾기
            string name = text.Substring(startIndex + 1, colonIndex - (startIndex + 1)); //']'+1 인덱스 값 부터 콜론인덱스 사이의 값만큼 문자열 반환
            string message_1 = text.Substring(colonIndex + 1); //이름 뒤 ':'값 뒤의 메시지 출력
            if (message_1.Length > 15)
            {
                message_1 = message_1.Substring(0, 25) + "...";   //내용 생략 
            }
            else
            {
                message_1 = message_1;
            }

            alert.label1.Text = " ";
            alert.label1.Text = name;               //이름 표시
            alert.label2.Text = " ";
            alert.label2.Text = message_1;          //내용 표시
            alert.StartPosition = FormStartPosition.Manual;
            alert.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - alert.Width, Screen.PrimaryScreen.WorkingArea.Height - alert.Height);

            alert.ShowDialog();
        }
    }
}
