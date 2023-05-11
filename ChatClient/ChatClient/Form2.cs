using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form2 : MetroForm
    {
        TcpClient clientSocket; // 소켓
        NetworkStream stream = default(NetworkStream);
        private static char CR = (char)0x0D;
        private static char LF = (char)0x0A;



        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Unicode.GetBytes("pictures" + CR + LF);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
