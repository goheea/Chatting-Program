using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; //참조 추가를 해야함(어셈블리탭 -> System.Configuration.dll)
using MySql.Data.MySqlClient; //솔루션용 nuget패키지 관리자에서 MySql.Data를 설치해야함.
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MultiChatServer
{
    public partial class Form1 : MetroForm
    {

        public Dictionary<Socket, string> clientSocketList = new Dictionary<Socket, string>();//클라이언트 소켓을 관리하는 리스트, 소켓과 접속 아이디를 관리하자.
        //string은 김지원, Socket 클래스는 잘 모르겠음.
        ServerProgram multiServer;
        MenuRecommend menurecommend;
        int serverPort;
        public MySqlConnection conn = new MySqlConnection("Server=localhost;Port=3306;Database=chatting_program;Uid=admin;Pwd=1234");

        public Form1()
        {
            InitializeComponent();
        }


        private void allClientSend(string message, string username, bool flag)
        {
            String curDate = DateTime.Now.ToString("HH:mm:ss"); // 현재 날짜 받기

            String sendMsg;

            byte[] buffer = null;
            if (flag)
            {
                if (message.Equals("disConnect"))
                {
                    string query = "delete from chatting_program.user_names where name = \"" + username + "\"";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    sendMsg = "[ " + curDate + " ] " + username + "님이 대화방을 나갔습니다.";
                }
                else
                    sendMsg = "[ " + curDate + " ] " + username + " : " + message;

            }
            else
            {

                sendMsg = message;

            }
            displayMessage(sendMsg);
            multiServer.SendMessage(sendMsg);


        }

        private void displayMessage(string text)
        {
            if (txt_Message.InvokeRequired) // 다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                txt_Message.BeginInvoke((MethodInvoker)delegate
                {
                    txt_Message.AppendText(text + Environment.NewLine);
                    txt_Message.SelectionStart = txt_Message.TextLength; //자동스크롤
                    txt_Message.ScrollToCaret();
                });
            }
            else
                txt_Message.AppendText(text + Environment.NewLine);
                txt_Message.SelectionStart = txt_Message.TextLength; //자동스크롤
                txt_Message.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs ev)
        {
            txt_ServerPort.Enabled = false;
            button1.Enabled = false;
            try
            {
                serverPort = Int32.Parse(txt_ServerPort.Text.ToString()); // 소켓 번호 설정
            }
            catch (FormatException e)
            {
                //textbox 에 숫자 외의 문자인 경우
                serverPort = 1004;
            }
            conn.Open();
            menurecommend = new MenuRecommend();
            multiServer = new ServerProgram(serverPort);
            multiServer.OnConnect += clientConnected;
            multiServer.OnDisconnect += clientDisconncted;
            multiServer.OnReceive += clientReceive;

        }

        private void clientReceive(Socket sock, String msg)
        {
            int index = msg.IndexOf("$");
            String curDate = DateTime.Now.ToString("HH:mm:ss"); // 현재 날짜 받기
            String stCmd = "";
            String stData = "";
            String sendMsg = "";
            string image_url = "";
            if (index > 0)
            {
                stCmd = msg.Substring(0, index); //$를 기준으로 앞 부분을 cmd로 
                stData = msg.Substring(index + 1);

            }
            if (stCmd.ToUpper() == "Login".ToUpper())
            {
                clientSocketList[sock] = stData; // Login 사용자명 셋팅
                sendMsg = "[ " + curDate + " ] " + stData + "님이 입장하셨습니다.";
                string query = "insert into chatting_program.user_names (name) value (\"" + stData + "\")";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            else
            {
                sendMsg = "[ " + curDate + " ] " + stCmd + " : " + stData;
            }
            if (stCmd != "")
            {
                displayMessage(sendMsg);
                if (multiServer != null)
                {
                    if (stCmd.Contains("메뉴 추천 버튼 클릭"))
                    {
                        multiServer.SendMessage(msg);
                    }
                    else
                    {
                        multiServer.SendMessage(sendMsg);
                    }
                }
            }

        }

        private void clientDisconncted(Socket sock)
        {
            String userName;
            if (clientSocketList.ContainsKey(sock))
            {
                userName = clientSocketList[sock];
                allClientSend("disConnect", userName, true);
                clientSocketList.Remove(sock);
            }
        }

        private void clientConnected(Socket sock)
        {
            clientSocketList.Add(sock, "");
        }
    }
}
