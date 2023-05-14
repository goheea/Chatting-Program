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

namespace MultiChatServer
{
    public partial class Form1 : MetroForm
    {

        public Dictionary<Socket, string> clientSocketList = new Dictionary<Socket, string>();//클라이언트 소켓을 관리하는 리스트, 소켓과 접속 아이디를 관리하자.
        //string은 김지원, Socket 클래스는 잘 모르겠음.
        ServerProgram multiServer;
        MenuRecommend menurecommend;
        int serverPort;

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
                    sendMsg = "[ " + curDate + " ] " + username + "님이 대화방을 나갔습니다.";


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
        /*바꾸기전
        private void displayMessage(string text)
        {
            if (txt_Message.InvokeRequired) //다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                txt_Message.BeginInvoke(new MethodInvoker(delegate   ///델리게이트로 넘겨서 실행
                {
                    txt_Message.AppendText(text + Environment.NewLine);
                }));
            }
            else
                txt_Message.AppendText(text + Environment.NewLine);

        }
        */
        private void displayMessage(string text)
        {//지수: 여기서 txt_Message 디버깅에 크로스스레드 오류가 나옴.
            if (txt_Message.InvokeRequired) // 다른 쓰레드에서 실행되어 Invoke가 필요한 상태라면 
            {
                txt_Message.BeginInvoke((MethodInvoker)delegate
                {
                    txt_Message.AppendText(text + Environment.NewLine);
                });
            }
            else
                txt_Message.AppendText(text + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs ev)
        {
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
            }
            /*지수지수: 
            else if (stCmd.ToUpper() == "exit".ToUpper())
            {
                clientSocketList[sock] = stData; // Login 사용자명 셋팅
                sendMsg = "[ " + curDate + " ] " + stData + "님이 대화방을 나갔습니다.";
            }
            */
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
