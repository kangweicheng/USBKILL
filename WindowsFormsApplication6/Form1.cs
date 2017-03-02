using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_DEVICECHANGE = 0x219;
            const int DBT_DEVICEARRIVAL = 0x8000;
            const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

            object ojb = new object();
            try
            {
                // WM_DEVICECHANGE Message : 電腦硬體裝置改變時產生的訊息
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE:
                            break;
                        // DBT_DEVICEARRIVAL Event : 裝置插入並且可以使用時，產生的系統訊息
                        case DBT_DEVICEARRIVAL:
                            shutdown();                            
                            break;
                        // DBT_DEVICEREMOVECOMPLETE Event : 裝置卸載或移除時產生的系統訊息
                        case DBT_DEVICEREMOVECOMPLETE:
                            shutdown();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.WndProc(ref m);
        }
        private void shutdown()
        {
            label1.Text = "SHUT DOWN IN 5 SECS";
            System.Diagnostics.Process.Start("C:\\WINDOWS\\system32\\shutdown.exe", " -s -t 5");

        }
        private void DeviceChange()
        {
            listBox1.Items.Clear();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (di.DriveType == DriveType.Removable)
                {
                    listBox1.Items.Add("偵測到" + di.Name + "抽取式存放裝置"+di.DriveType.ToString());
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeviceChange();
        }
    }
}
