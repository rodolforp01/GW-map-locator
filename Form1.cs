using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace GWMaping_box
{
    public partial class Form1 : Form
    {
        private const int PROCESS_ALL_ACCESS = 33773329;

        public Form1()
        {
            InitializeComponent();
        }

        private void LocationX(object sender, EventArgs e) { }
        private void LocationY(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e) 
        {
            GotoLoc(Conversions.ToInteger(this.textBox1.Text), Conversions.ToInteger(this.textBox2.Text)); //converts the text to ints // AND CALLS THE FUNCTION
        }

        private void GotoLoc(int x, int y)
        {
           
            Process me = Process.GetProcessesByName("GodsWar").FirstOrDefault();
            int processID = me.Id;

            int handle = OpenProcess(PROCESS_ALL_ACCESS, 0, processID); //handle to the process
            checked
            {
                int handle2 = handle;
                int addr = 22504132;
                int buffer = 0;
                int numberOfBytes = 4;
                int last = 0;
                ReadProcessMemory(handle2, addr, ref buffer, numberOfBytes, ref last);
                int newNum = buffer + 112;
                int newNum2 = buffer + 116;
                Activateit(); //activates window
                MoveMouse();
                Thread.Sleep(100);
                //handle2
                //newNum
                //int x
                //NumberOfBytes = 4
                //same last =0;
                WriteProcessMemory(handle2, newNum, ref x, numberOfBytes, ref last);
                //handle2
                //newNum2
                //int y
                //NumberOfBytes
                // same 0
                WriteProcessMemory(handle2, newNum2, ref y, numberOfBytes, ref last);
                MouseEvent();

            }
               

        }

        public void MouseEvent()
        {
            mouse_event(2, 0, 0, 0, 1);
            mouse_event(4, 0, 0, 0, 1);
        }

        public void MoveMouse()
        {
            Process[] gw = Process.GetProcessesByName("GodsWar");
            foreach (Process ok in gw)
            {
                int process = ok.Id;


                Rectangle rectangle = default(Rectangle);
                IntPtr mainHandle;
                mainHandle = Process.GetProcessById(process).MainWindowHandle;

                GetWindowRect(mainHandle, ref rectangle);
                
                SetCursorPos(rectangle.X, rectangle.Y);

                SetCursorPos(rectangle.X + 727, rectangle.Y + 108);//click on the map
                MouseEvent();
                SetCursorPos(rectangle.X + 657, rectangle.Y + 154); //change the position
                break;
            }
        }

        public void Activateit()
        {
            Process[] me1 = Process.GetProcessesByName("GodsWar");
            SetForegroundWindow(me1[0].MainWindowHandle); //activates the window
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern void mouse_event(int int_33, int int_34, int int_35, int int_36, int int_37);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int SetCursorPos(int int_33, int int_34);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr inta, ref Rectangle whatever);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int OpenProcess(int int_33, int int_34, int int_35);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int ReadProcessMemory(int int_33, int int_34, ref int int_35, int int_36, ref int int_37);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int WriteProcessMemory(int int_33, int int_34, ref int int_35, int int_36, ref int int_37);

    }
}
