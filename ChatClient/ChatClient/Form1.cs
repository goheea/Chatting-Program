using MetroFramework.Forms;
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
using System.Net; //WebClient
using System.IO; //MemoryStream
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatClient
{
    public partial class Form1 : MetroForm
    {
        TcpClient clientSocket; // 소켓
        NetworkStream stream = default(NetworkStream);
        string stSendMessage = "";
        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;
        bool bThreadExit = false;
        string menuresult = "";
        string menuimage = "";
        string tmp = "";
        List<string> names = new List<string>();
        String curDate = DateTime.Now.ToString("HH:mm:ss");

        public Form1()
        {
            InitializeComponent();
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

            //usernameBox.AppendText(txt_user.Text + Environment.NewLine);
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
                if (message.EndsWith("님이 입장하셨습니다."))
                {
                    int start = message.IndexOf(']') + 2;
                    int end = message.IndexOf("님이");
                    string name = message.Substring(start, end - start);
                    names.Add(name);
                    usernameBox.Clear();
                    foreach (string n in names)
                    {
                        usernameBox.AppendText(n + Environment.NewLine);
                    }
                }
                if(message.EndsWith("님이 대화방을 나갔습니다."))
{
                    int start = message.IndexOf(']') + 2;
                    int end = message.IndexOf("님이");
                    string name = message.Substring(start, end - start);
                    names.Remove(name);
                    usernameBox.Clear();
                    foreach (string n in names)
                    {
                        usernameBox.AppendText(n + Environment.NewLine);
                    }
                }

                else
                {
                    DisplayText(message);
                    //----------------폼2추가부분
                    show_alert1(message);
                    //--------------------------
                }
                /*
                string input = rt_Message.Text;
                List<string> names = new List<string>();
                if (input.EndsWith("님이 입장하셨습니다."))
                {
                    int start = input.IndexOf(']') + 2;
                    int end = input.IndexOf("님이");
                    string name = input.Substring(start, end - start);
                    names.Add(name);
                }
                */
                
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

        private void useboxClear(string message)
        {
            if (usernameBox.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                usernameBox.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    usernameBox.Clear();
                    foreach (string n in names)
                    {
                        usernameBox.AppendText(n + Environment.NewLine);
                    }
                }));
            }
            else
                usernameBox.Clear();
                foreach (string n in names)
                {
                    usernameBox.AppendText(n + Environment.NewLine);
                }
        }
        /*
        private void Displayuserlist(string message)
        {
            if (rt_Message.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                rt_Message.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    usernameBox.AppendText(txt_user.Text + Environment.NewLine);
                }));
            }
            else
                usernameBox.AppendText(txt_user.Text + Environment.NewLine);
        }
        */
        private void Displayusername(string message)
        {
            if (rt_Message.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                rt_Message.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    usernameBox.AppendText(txt_user.Text + Environment.NewLine);
                }));
            }
            else
                usernameBox.AppendText(txt_user.Text + Environment.NewLine);
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

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            rt_Message.AppendText("[ " + curDate + " ] " + txt_user.Text + " 님이 대화방을 나갔습니다.");
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
            /*
            Point parentPoint = this.Location;
            Form2 newform2 = new Form2();
            newform2.StartPosition = FormStartPosition.Manual;
            newform2.Location = new Point(parentPoint.X + 625, parentPoint.Y + 50);
            newform2.Show();
            */
            byte[] buffer = Encoding.Unicode.GetBytes("메뉴 추천 버튼 클릭$" + CR + LF);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }


        //-----------------------------------------------폼2추가 부분
        //private void show_alert(string msg)
        //{
        //    Form2 alert = new Form2();
        //    alert.ShowDialog();
        //}

        private void show_alert1(string message)
        {
            Form2 alert = new Form2();
            //alert.Location = new Point();
            if (alert.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                alert.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    alert.Show();

                }));
            }
            else
            {
                alert.Show();
                
            }
                
        }



        //--------------------------------------------------------------


    }
}
