﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration; //참조 추가를 해야함(어셈블리탭 -> System.Configuration.dll)
using MySql.Data.MySqlClient; //솔루션용 nuget패키지 관리자에서 MySql.Data를 설치해야함.
using System.Runtime.InteropServices;
using System.IO;

namespace ChatClient
{
    public partial class Form1 : MetroForm
    {
        public MySqlConnection conn = new MySqlConnection("Server=192.168.100.249;Port=3306;Database=chatting_program;Uid=admin;Pwd=admin1234!");
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
        List<string> name_list = new List<string>();
        public bool test = false;
        string name_tmp = "";
        const string msgPlaceholder = "메세지를 입력하세요.";

        public Form1()
        {
            InitializeComponent();
            txt_message.ForeColor = Color.DimGray; // 처음 Placeholder 색 지정
            txt_message.Text = msgPlaceholder; // 처음 Placeholder 글 설정(메세지를 입력하세요)
            txt_message.GotFocus += RemovePlaceholder; // 텍스트박스 커서 Focus 여부에 따라 이벤트 지정
            txt_message.LostFocus += SetPlaceholder;
        }
        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == msgPlaceholder)
            { //텍스트박스 내용이 Placeholder이고 박스에 포커스가 잡히면,
                txt.ForeColor = Color.Black; //사용자 입력은 진한 글씨로,
                txt.Text = string.Empty; //플레이스홀더는 지운다.
            }
        }
        private void SetPlaceholder(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text)) //사용자 입력값이 하나도 없는 경우에 포커스 잃으면 Placeholder 적용해주기
            {                
                txt.ForeColor = Color.DimGray;
                if (txt == txt_message)
                {
                    txt.Text = msgPlaceholder;
                }
            }
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            bThreadExit = false;
            int Port = Int32.Parse(txt_Port.Text);
            clientSocket = new TcpClient();
            stream = default(NetworkStream);

            DataSet ds = new DataSet();
            string query = "SELECT name from chatting_program.user_names";
            MySqlDataAdapter adpt = new MySqlDataAdapter(query, conn);
            adpt.Fill(ds, "name");
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    name_list.Add((string)r["name"]);
                }
            }

            try
            {
                clientSocket.Connect(txt_ServerIP.Text, Port); // 접속 IP 및 포트
                if (txt_user.Text != "")
                {
                    foreach (string st in name_list)
                    {
                        if (st == txt_user.Text)
                        {
                            throw new Exception("예외를 던집니다.");
                        }
                    }
                }
                else
                {
                    throw new Exception("예외를 던집니다.");
                }
                stream = clientSocket.GetStream();
                conn.Open();
            }
            catch (Exception e2)
            {
                if (e2 is SocketException)
                {
                    MessageBox.Show("서버가 실행중이 아닙니다.", "연결 실패!");
                    return;
                }
                else
                {
                    MessageBox.Show("사용자명이 공란이거나 중복된 사용자명입니다.", "다시 입력해주세요!");
                    return;
                }
            }

            btn_Login.Enabled = false;
            txt_user.Enabled = false;
            menu_viewer.Enabled = false;
            menu_name.Enabled = false;

            name_tmp = txt_user.Text;
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
                int BUFFERSIZE = 10000000; //사진을 받기 위해서 버퍼 사이즈 크기 크게 주기
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
                    byte[] byteArray = Convert.FromBase64String(menuimage);
                    MemoryStream ms = new MemoryStream(byteArray, 0, byteArray.Length);
                    ms.Write(byteArray, 0, byteArray.Length);
                    Image image = Image.FromStream(ms, true);
                    DisplayMenuImage(image);
                    DisplayMenuText(menuresult);
                }

                else
                {
                    DisplayText(message);
                    if (message.EndsWith("님이 입장하셨습니다.") || message.EndsWith("님이 대화방을 나갔습니다."))
                    {
                        DisplayName();
                    }
                    else
                    {
                        int startIndex = message.IndexOf(']');  // ']' 인덱스값
                        int colonIndex = message.IndexOf(':', startIndex); // startIndex 이후의 ':' 인덱스 찾기
                        string name0 = message.Substring(startIndex + 2, colonIndex - (startIndex + 2)-1); //']'+1 인덱스 값 부터 콜론인덱스 사이의 값만큼 문자열 반환
                        if(name0 != name_tmp)
                        {
                            show_alert1(message);
                        }
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
                    rt_Message.SelectionStart = rt_Message.TextLength; //자동스크롤
                    rt_Message.ScrollToCaret();

                }));
            }
            else
            {
                rt_Message.AppendText(message + Environment.NewLine);
                rt_Message.SelectionStart = rt_Message.TextLength; //자동스크롤
                rt_Message.ScrollToCaret();
            }
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

        private void DisplayMenuImage(Image image)
        {
            if (menu_viewer.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                menu_viewer.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    menu_viewer.Image = image;
                    menu_viewer.SizeMode = PictureBoxSizeMode.StretchImage;
                }));
            }
            else
            {
                menu_viewer.Image = image;
                menu_viewer.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void DisplayName()
        {
            if (usernameBox.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
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
        private void btn_Login_MouseEnter(object sender, EventArgs e)
        {

            btn_Login.BackColor = Color.DeepSkyBlue; // 원하는 색상으로 변경
            btn_Login.ForeColor = Color.White; // 원하는 색상으로 변경
        }
        private void btn_Login_MouseLeave(object sender, EventArgs e)
        {
            btn_Login.BackColor = Color.White; 
            btn_Login.ForeColor = Color.DodgerBlue; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Unicode.GetBytes("메뉴 추천 버튼 클릭$" + CR + LF);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private void show_alert1(string message)           //호출
        {


            IntPtr foregroundWindowHandle = GetForegroundWindow();  //윈도우 창 꺼져있거나 뒤로 가 있을 시의 상태를 비교하여 조건절을 실행
            IntPtr currentWindowHandle = this.Handle;
            //bool hasFocus = (foregroundWindowHandle == currentWindowHandle);
            bool hasFocus = (foregroundWindowHandle == currentWindowHandle);

            if (this.WindowState == FormWindowState.Minimized || hasFocus == false)
            {


                Form2 alert = new Form2();          //폼2 객체 생성
                string text = message;


                //알림창에 들어갈 내용 입력
                int startIndex = text.IndexOf(']');  // ']' 인덱스값
                int colonIndex = text.IndexOf(':', startIndex); // startIndex 이후의 ':' 인덱스 찾기
                string name = text.Substring(startIndex + 1, colonIndex - (startIndex + 1)); //']'+1 인덱스 값 부터 콜론인덱스 사이의 값만큼 문자열 반환
                string message_1 = text.Substring(colonIndex + 1); //이름 뒤 ':'값 뒤의 메시지 출력
                //메시지가 18자 이상 왔을 시 알림창 내용 생략
                if (message_1.Length > 18)
                {
                    message_1 = message_1.Substring(0, 18) + "...";   //내용 생략 
                }

                //라벨에 값 추가
                alert.label1.Text = " ";
                alert.label1.Text = name;               //이름 표시
                alert.label2.Text = " ";
                alert.label2.Text = message_1;          //내용 표시

                //알림창 x,y포지션 값
                alert.StartPosition = FormStartPosition.Manual;
                alert.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - alert.Width - 10, Screen.PrimaryScreen.WorkingArea.Height - alert.Height - 10);

                //알림창 띄우기
                alert.ShowDialog();
                //그 후 타이머 2초 
            }
        }
    }
    //-----------------------------------------------------------------------
    public static class FlashWindow
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);
        public const uint FLASHW_STOP = 0;
        public const uint FLASHW_CAPTION = 1;
        public const uint FLASHW_TRAY = 2;      //구현해보고 싶어서 찾아보고 이 값만 뽑아씀, 알림창이 뜰 시에만 아이콘이 깜빡인다.
        public const uint FLASHW_ALL = 3;
        public const uint FLASHW_TIMER = 4;
        public const uint FLASHW_TIMERNOFG = 12;
        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public uint dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }
        public static bool Flash(System.Windows.Forms.Form form)
        {
            if (Win2000OrLater)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL | FLASHW_TIMERNOFG, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }
        public static bool Flash(System.Windows.Forms.Form form, uint count)
        {
            if (Win2000OrLater)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL, count, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }
        private static FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }
        public static bool Tray(System.Windows.Forms.Form form)
        {
            if (Win2000OrLater)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_TRAY, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }
        public static bool TrayAndWindow(System.Windows.Forms.Form form)
        {
            if (Win2000OrLater)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        public static bool Stop(System.Windows.Forms.Form form)
        {
            if (Win2000OrLater)
            {
                FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_STOP, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        private static bool Win2000OrLater
        {
            get { return System.Environment.OSVersion.Version.Major >= 5; }
        }

    }
}