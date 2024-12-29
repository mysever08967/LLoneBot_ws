using BOT_ReceiveMsg_T;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace WindowsFormsApp1
{
    public partial class MySvrForm : Form
    {
        public static Queue<string> Timergroup = new Queue<string>();
        public static MySvrForm mForm { get; private set; }
        public bool RoomViListisOpen { get; private set; }
        public bool openWS;
        private WebSocketServer WebSocketServer = null;
        private int indexBOXLOG = -1;
        private int Itemsindex = -1;
        private int SVRindex = -1;
        private int PlayerViListItemsindex = -1;
        private int RoomViListItemsindex = -1;
        private int userItemsindex = -1;

        public void WebSocketServerAsync(int port)
        {
            openWS = true;
            WebSocketServer = new WebSocketServer();
            WebSocketServer.Start(port);
        }

        public MySvrForm()
        {
            InitializeComponent();
            mForm = this;
        }

        public void InStart()
        {
            mForm.BOTlist.Items.Clear();
            mForm.TabListGmaneSet.SelectedTab = mForm.TabListGmaneSet.TabPages[1];
            mForm.TabListGmaneSet.SetBounds(135, 20, 772, 452);
            mForm.SetBounds(50, 50, 900, 450);
        }

        public delegate void AppendTextDelegate(System.Windows.Forms.ListViewItem item1);

        //BOT日志列表
        public static void BOT_LoglistADD(string group_name, string group_id, string user_id, string user_name, string msg)
        {
            DateTime now = DateTime.Now;
            string timeString = now.ToString("HH:mm:ss");
            System.Windows.Forms.ListViewItem item1 = new System.Windows.Forms.ListViewItem(
                new[] {
                    timeString,
                    group_name,
                    group_id,
                    user_id,
                    user_name,
                    msg
                });
            Color txtColor1 = new Color();
            txtColor1 = Color.FromArgb(0, 39, 37, 39);
            item1.BackColor = txtColor1;
            item1.ForeColor = Color.Cyan;
            void UpdateLog() => mForm.BOTlist.Items.Add(item1);
            mForm.BOTlist.Invoke(new Action(UpdateLog));
            mForm.indexBOXLOG++;
            if (mForm.indexBOXLOG > 15)
            {
                void UpEnsureVisible() => mForm.BOTlist.EnsureVisible(mForm.BOTlist.Items.Count - 1);
                mForm.BOTlist.Invoke(new Action(UpEnsureVisible));
            }

            if (mForm.indexBOXLOG >= 1000)
            {
                mForm.indexBOXLOG = -1;
                void UpClear() => mForm.BOTlist.Items.Clear();
                mForm.BOTlist.Invoke(new Action(UpClear));
            }
        }

        public static void BOT_group_member_ListViewADD(List<group_member> member_List)
        {
            mForm.group_member_ListView.Items.Clear();
            for (int i = 0; i < member_List.Count; i++)
            {
                System.Windows.Forms.ListViewItem item1 = new System.Windows.Forms.ListViewItem(
                    new[] {
                        member_List[i].user_id,
                        member_List[i].user_name,
                        member_List[i].card,
                        member_List[i].role,
                        member_List[i].join_time,
                        member_List[i].last_sent_time,
                    });
                setGourp = member_List[i].group_id;
                Color txtColor1 = Color.FromArgb(0, 39, 37, 39);
                item1.BackColor = txtColor1;
                item1.ForeColor = Color.Cyan;
                mForm.group_member_ListView.Items.Add(item1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InStart();
        }

        private bool isDragging = false;
        private Point lastPoint;
        private static string setGourp;

        private void MySeverForm_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastPoint = new Point(e.X, e.Y);
        }

        private void MySeverForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(currentPoint.X - lastPoint.X, currentPoint.Y - lastPoint.Y);
            }
        }

        private void MySeverForm_Mouseup(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void BOTLOG_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (mForm.BOTlist.SelectedItems.Count >= 0)
                {
                    Itemsindex = mForm.BOTlist.SelectedItems.Count;
                    BOTLOGcontextMenuStrip.Show(mForm.BOTlist, e.Location);
                }
                else
                {
                    Itemsindex = -1;
                }
            }
        }

        private void button1_Click_2Async(object sender, EventArgs e)
        {
        }

        private void button_WS_start_Click(object sender, EventArgs e)
        {
            if (WebSocketServer != null)
            {
                MessageBox.Show("WebSocketSever: 运行中");
                return;
            }
            if (Tbox_wsProt.Text == null)
            {
                return;
            }
            int wspotr = Convert.ToInt32(Tbox_wsProt.Text);
            if (wspotr <= 0)
            {
                return;
            }
            WebSocketServerAsync(wspotr);
        }

        private void WS_stop_Click(object sender, EventArgs e)
        {
            openWS = false;
            if (WebSocketServer == null)
                return;
            WebSocketServer.stop();
            WebSocketServer = null;
        }

        private void ButLog_Click(object sender, EventArgs e)
        {
            TabListGmaneSet.SelectedTab = TabListGmaneSet.TabPages[0];
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            BOT_Exit();
        }

        private void group_set_Click(object sender, EventArgs e)
        {
            TabListGmaneSet.SelectedTab = TabListGmaneSet.TabPages[2];
        }

        private void BOT_set_Click(object sender, EventArgs e)
        {
            TabListGmaneSet.SelectedTab = TabListGmaneSet.TabPages[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BOT_Exit();
        }

        private void BOT_Exit()
        {
            if (MessageBox.Show("是否退出？", "退出", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                openWS = false;
                WebSocketServer?.stop();
                Close();
            }
        }

        private void butXH_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // 端口编辑框 仅数字输入
        private void Tbox_wsProt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Itemsindex < 1)
            {
                return;
            }
            ListViewItem selectedItem;
            selectedItem = mForm.BOTlist.SelectedItems[0];

            // 获取选中项的文本内容
            //string selectedText = selectedItem.Text;

            // 获取选中项的子项内容
            string subItemText = $"时间: {selectedItem.SubItems[0].Text}\n来源: {selectedItem.SubItems[1].Text}\n详细内容: {selectedItem.SubItems[5].Text}";

            MessageBox.Show(subItemText, "详细信息");
        }

        private void 清空日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mForm.BOTlist.Items.Clear();
            indexBOXLOG = -1;
        }

        private void 复制内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Itemsindex < 1)
            {
                return;
            }
            ListViewItem selectedItem;
            selectedItem = mForm.BOTlist.SelectedItems[0];
            //string selectedText = selectedItem.Text;
            Clipboard.SetText(selectedItem.SubItems[5].Text);
        }

        //退出远程桌面时也保持交互，否则无法加载新窗体
        private void console()
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe", "/c query user")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,   // 创建不显示的窗口
                WindowStyle = ProcessWindowStyle.Hidden // 隐藏窗口样式
            };
            Process process = Process.Start(psi);
            string ot = process.StandardOutput.ReadToEnd();
            if (ot.IndexOf("运行中") != -1)
            {
                string s = ot.Substring(ot.IndexOf("运行中") - 13, 11);
                s = s.Replace(" ", "");
                if (s != null)
                {
                    string Str = $"tscon {s} /password:* /dest:console";
                    //Process.Start("cmd.exe", "/c " + Str);
                    ProcessStartInfo pcmd = new ProcessStartInfo("cmd.exe", "/c " + Str)
                    {
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };
                    Process.Start(pcmd);
                }
            }
        }
    }
}