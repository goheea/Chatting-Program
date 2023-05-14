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
using System.IO.Ports;

namespace MultiChatServer
{
    internal class Client : SocketAsyncEventArgs
    {
        // 메시지는 개행으로 구분한다.
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;
        string url_base = "https://github.com/goheea/MiniServer/blob/main/";
        string tmp = "";
        private Socket socket;
        // 메시지를 모으기 위한 버퍼
        private StringBuilder sb = new StringBuilder();
        private IPEndPoint remoteAddr;
        MenuRecommend menurecommend = new MenuRecommend();
        public delegate void ClientReceiveHandler(Socket sock, String msg); //수신 메시지 이벤트를 위한 델리게이트
        public event ClientReceiveHandler OnReceive;
        public delegate void ClientDisconnectHandler(Socket sock);
        public event ClientDisconnectHandler OnDisconnect;

        public Client(Socket socket)
        {
            try //여기서 this는 SocketAsyncEventArgs를 나타내고, this 뒤는 SocketAsyncEventArgs의 속성을 나타낸다.
            {
                this.socket = socket;
                Console.WriteLine("{0}", socket.Handle);
                // 메모리 버퍼를 초기화 한다. 크기는 1024이다
                base.SetBuffer(new byte[1024], 0, 1024);
                base.UserToken = socket;
                // 메시지가 오면 이벤트를 발생시킨다. (IOCP로 꺼내는 것)
                base.Completed += Client_Completed; //Client_Completed는 속성이 아니라 개발자가 정의한 메소드임.
                // 메시지가 오면 이벤트를 발생시킨다. (IOCP로 넣는 것)
                this.socket.ReceiveAsync(this);
                // 접속 환영 메시지
                remoteAddr = (IPEndPoint)socket.RemoteEndPoint;
                if (remoteAddr != null)
                {
                    //Console.WriteLine($"Client : (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
                    //this.Send("Welcome server!\r\n>");
                }

            }
            catch (Exception e)
            {
                return;
            }

        }
        ~Client()
        {
            socket = null;
        }

        public void Send(String msg)
        {
            byte[] sendData = Encoding.Unicode.GetBytes(msg);
            // Client로 메시지 전송
            if (socket != null) socket.Send(sendData, sendData.Length, SocketFlags.None);
        }

        private void Client_Completed(object sender, SocketAsyncEventArgs e)
        {
            // 접속이 연결되어 있으면...
            if (socket.Connected && base.BytesTransferred > 0)
            {
                // 수신 데이터는 e.Buffer에 있다.
                byte[] data = e.Buffer;
                // 데이터를 string으로 변환한다.
                string msg = Encoding.Unicode.GetString(data);
                // 메모리 버퍼를 초기화 한다. 크기는 1024이다
                base.SetBuffer(new byte[1024], 0, 1024);
                // 버퍼의 공백은 없앤다.
                sb.Append(msg.Trim('\0'));
                // 메시지의 끝이 이스케이프 \r\n의 형태이면 서버에 표시한다.
                if (sb.Length >= 2 && sb[sb.Length - 2] == CR && sb[sb.Length - 1] == LF)
                {
                    Console.WriteLine("{0}", socket);
                    // 개행은 없애고..
                    sb.Length = sb.Length - 2;
                    // string으로 변환한다.
                    msg = sb.ToString();

                    if (OnReceive != null)
                    {
                        if ("메뉴 추천 버튼 클릭$".Equals(msg, StringComparison.OrdinalIgnoreCase))
                        {
                            msg += menurecommend.Recommend();
                            msg += "$";
                            tmp = url_base;
                            tmp += menurecommend.findIndex() + 1;
                            tmp += ".jpg?raw=true";
                            msg += tmp;
                            tmp = "";
                        }
                        Console.WriteLine(msg);
                        OnReceive(this.socket, msg); // 수신 이벤트 발생
                    }

                    // 콘솔에 출력한다.
                    Console.WriteLine(msg);
                    // Client로 Echo를 보낸다.
                    //Send($"Echo - {msg}\r\n>");
                    // 만약 메시지가 exit이면 접속을 끊는다.
                    if ("exit".Equals(msg, StringComparison.OrdinalIgnoreCase))
                    {
                        OnDisconnect(socket);
                        // 접속 종료 메시지
                        Console.WriteLine($"Disconnected : (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
                        // 접속을 중단한다.
                        socket.DisconnectAsync(this);
                        return;
                    }

                    // 버퍼를 비운다.
                    sb.Clear();
                }
                // 메시지가 오면 이벤트를 발생시킨다. (IOCP로 넣는 것)
                this.socket.ReceiveAsync(this);
            }
            else
            {
                OnDisconnect(socket);
                // 접속이 끊겼다..
                Console.WriteLine($"Disconnected : (From: {remoteAddr.Address.ToString()}:{remoteAddr.Port}, Connection time: {DateTime.Now})");
            }


        }

    }

    // 서버 Event로 SocketAsyncEventArgs를 상속받았다.
    class Server : SocketAsyncEventArgs
    {
        public delegate void ClientReceiveHandler(Socket sock, String msg); //수신 메시지 이벤트를 위한 델리게이트
        public event ClientReceiveHandler OnReceive;
        public delegate void ClientDisconnectHandler(Socket sock);
        public event ClientDisconnectHandler OnDisconnect;
        public delegate void ClientConnectHandler(Socket sock);
        public event ClientConnectHandler OnConnect;

        private Socket socket;
        public List<Socket> clientSocketList = new List<Socket>();//클라이언트 소켓을 관리하는 리스트, 소켓과 접속 아이디를 관리하자.
        public Dictionary<Socket, Client> clientList = new Dictionary<Socket, Client>();//클라이언트 소켓을 관리하는 리스트, 소켓과 접속 아이디를 관리하자.

        public Server(Socket socket)
        {
            this.socket = socket;
            base.UserToken = socket;
            // Client로부터 Accept이 되면 이벤트를 발생시킨다. (IOCP로 꺼내는 것)
            base.Completed += Server_Completed;
        }

        public void SocketClose()
        {
            foreach (var client in clientSocketList)
            {
                if (client.Connected) client.Disconnect(false);
                client.Dispose();
            }
            foreach (var client in clientList)
            {
                Client c = client.Value;
                c.Dispose();
            }
            clientSocketList.Clear();
            clientList.Clear();
        }

        // Client가 접속하면 이벤트를 발생한다.
        private void Server_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                // 접속이 완료되면, Client Event를 생성하여 Receive이벤트를 생성한다.
                var client = new Client(e.AcceptSocket);
                Console.WriteLine("{0}", e.AcceptSocket.Handle);
                client.OnReceive += ClientReceive;
                client.OnDisconnect += ClientDisconnect;
                clientSocketList.Add(e.AcceptSocket);
                clientList.Add(e.AcceptSocket, client);
                // 서버 Event에 cilent를 제거한다.
                e.AcceptSocket = null;
                // Client로부터 Accept이 되면 이벤트를 발생시킨다. (IOCP로 넣는 것)
                this.socket.AcceptAsync(e);
                if (OnConnect != null)
                {
                    OnConnect(this.socket);
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void ClientDisconnect(Socket sock)
        {

            if (OnDisconnect != null)
                OnDisconnect(sock);
            Console.WriteLine("{0}", sock.Handle);
            clientList.Remove(sock);
            clientSocketList.Remove(sock);
        }

        private void ClientReceive(Socket sock, string msg)
        {
            if (OnReceive != null)
                OnReceive(sock, msg);
        }
        public void SendAllMessage(String msg)
        {
            foreach (var client in clientList)
            {
                Client c = client.Value;
                c.Send(msg);
            }
        }
    }

    class ServerProgram : Socket
    {
        public delegate void ClientReceiveHandler(Socket sock, String msg); //수신 메시지 이벤트를 위한 델리게이트
        public event ClientReceiveHandler OnReceive;
        public delegate void ClientDisconnectHandler(Socket sock);
        public event ClientDisconnectHandler OnDisconnect;
        public delegate void ClientConnectHandler(Socket sock);
        public event ClientConnectHandler OnConnect;

        private bool _disposed = false;


        public Server serverSocket;
        public IPEndPoint ipEndPoint;
        public ServerProgram(int port) : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) //생성자 선언
        {
            ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            try
            {
                base.Bind(ipEndPoint);
                base.Listen(20);
                // 비동기 소켓으로 Server 클래스를 선언한다. (IOCP로 집어넣는것)
                serverSocket = new Server(this);
                serverSocket.OnConnect += ClientConnect;
                serverSocket.OnDisconnect += ClientDisConnect;
                serverSocket.OnReceive += ClientRecieve;

                base.AcceptAsync(serverSocket);
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void ClientRecieve(Socket sock, string msg)
        {
            if (OnReceive != null)
                OnReceive(sock, msg);
        }

        private void ClientDisConnect(Socket sock)
        {
            if (OnDisconnect != null)
                OnDisconnect(sock);
        }

        private void ClientConnect(Socket sock)
        {
            if (OnConnect != null)
                OnConnect(sock);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;
            try
            {
                if (serverSocket != null)
                {
                    serverSocket.SocketClose();
                    base.Disconnect(true);

                    base.Shutdown(SocketShutdown.Both);
                    base.Close();
                    base.Dispose();

                    GC.SuppressFinalize(serverSocket);
                }
                //serverSocket = null;
            }
            catch (Exception ex)
            {
                //
            }
            finally
            {
                //base.Close(0);
            }
            _disposed = true;
        }

        public void SendMessage(String msg)
        {
            if (serverSocket != null)
                serverSocket.SendAllMessage(msg);
        }
    }

}