using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelTriggerBot
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]

        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LEFTUP = 0x004;
        private const int LEFTDOWN = 0x008;

        Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        Graphics graphics;

        Object oldCol;
        Object newCol;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread Start = new Thread(Main);
            Start.Start();
        }
        private void Main()
        {
            while (true)
            {
                if (GetAsyncKeyState(Keys.XButton2)<0)
                {
                    oldCol = GrabColor();
                    Thread.Sleep(2);
                    newCol = GrabColor();
                    if (oldCol.ToString() != newCol.ToString())
                    {
                        Thread.Sleep(10);
                       //  mouse_event(LEFTDOWN, 0, 0, 0, 0);
                        Thread.Sleep(1);
                        MessageBox.Show("did work anyways");
                      //  mouse_event(LEFTUP, 0, 0, 0, 0);
                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(2);
                }
                Thread.Sleep(5);
            }
        }

        Object GrabColor()
        {
            graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            object pixel = bitmap.GetPixel(961, 541);
            return pixel;
        }
    }
}
