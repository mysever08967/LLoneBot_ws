using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    partial class MySvrForm
    {

        protected internal System.Windows.Forms.ColumnHeader columnHeader2;

        protected internal System.Windows.Forms.ComboBox G_USERBOX;

        protected internal System.Windows.Forms.ListView group_member_ListView;

        protected internal System.Windows.Forms.Label label5;

        //protected internal new System.Windows.Forms.ColumnHeader Name;

        private System.Windows.Forms.Button BOT_set;

        private System.Windows.Forms.ContextMenuStrip BOTLOGcontextMenuStrip;

        private System.Windows.Forms.Button butExit;

        private System.Windows.Forms.Button ButLog;

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.Button butXH;

        private System.Windows.Forms.ColumnHeader columnHeader1;

        private System.Windows.Forms.ColumnHeader columnHeader3;

        private System.Windows.Forms.ColumnHeader columnHeader4;

        private System.Windows.Forms.ColumnHeader columnHeader5;

        private System.Windows.Forms.ColumnHeader columnHeader6;

        private System.Windows.Forms.ColumnHeader columnHeader7;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button Exit;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;

        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

        private System.Windows.Forms.ToolStripMenuItem 复制内容ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 获取状态ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 获取状态全部玩家ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 拉黑IPToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 拉黑名单ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 清空日志ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 全局屏蔽ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 踢出客户端ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 详细信息ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 修改房名ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 增加人数ToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem 指定死亡ToolStripMenuItem;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySvrForm));
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.G_USERBOX = new System.Windows.Forms.ComboBox();
            this.group_member_ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ButLog = new System.Windows.Forms.Button();
            this.BOT_set = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.butXH = new System.Windows.Forms.Button();
            this.BOTLOGcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.详细信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.全局屏蔽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.指定死亡ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取状态ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拉黑IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拉黑名单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.踢出客户端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取状态全部玩家ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.增加人数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改房名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label_Revsend = new System.Windows.Forms.Label();
            this.BotVer = new System.Windows.Forms.Label();
            this.BOTSET = new System.Windows.Forms.TabPage();
            this.listView_bot = new System.Windows.Forms.ListView();
            this.index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.QQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ws = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tbox_wsProt = new System.Windows.Forms.TextBox();
            this.botText2 = new System.Windows.Forms.Label();
            this.botText = new System.Windows.Forms.Label();
            this.checkBox_BOTAPIMSG = new System.Windows.Forms.CheckBox();
            this.WS_stop = new System.Windows.Forms.Button();
            this.button_WS_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BOTlist = new System.Windows.Forms.ListView();
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.group_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.group_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.User_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.User_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.meg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabListGmaneSet = new System.Windows.Forms.TabControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.BOTLOGcontextMenuStrip.SuspendLayout();
            this.BOTSET.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.TabListGmaneSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Cyan;
            this.label2.Location = new System.Drawing.Point(469, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 24;
            this.label2.Text = "My Sever";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.CausesValidation = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Cyan;
            this.button2.Location = new System.Drawing.Point(482, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 25);
            this.button2.TabIndex = 19;
            this.button2.TabStop = false;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Cyan;
            this.label5.Location = new System.Drawing.Point(167, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = "选择群聊:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // G_USERBOX
            // 
            this.G_USERBOX.AutoCompleteCustomSource.AddRange(new string[] {
            "[群聊] 尚不设置",
            "[群聊] 设置",
            "[群聊] 尚不设置",
            "[群聊] 设置",
            "[群聊] 尚不设置",
            "[群聊] 设置"});
            this.G_USERBOX.BackColor = System.Drawing.Color.MintCream;
            this.G_USERBOX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.G_USERBOX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.G_USERBOX.ForeColor = System.Drawing.SystemColors.WindowText;
            this.G_USERBOX.FormattingEnabled = true;
            this.G_USERBOX.Location = new System.Drawing.Point(244, 389);
            this.G_USERBOX.Name = "G_USERBOX";
            this.G_USERBOX.Size = new System.Drawing.Size(232, 22);
            this.G_USERBOX.TabIndex = 17;
            // 
            // group_member_ListView
            // 
            this.group_member_ListView.HideSelection = false;
            this.group_member_ListView.Location = new System.Drawing.Point(0, 0);
            this.group_member_ListView.Name = "group_member_ListView";
            this.group_member_ListView.Size = new System.Drawing.Size(121, 97);
            this.group_member_ListView.TabIndex = 0;
            this.group_member_ListView.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "    QQ";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "QQ昵称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "群昵称";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "群身份";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "进群时间";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "最后发言时间";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "签到积分";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 100;
            // 
            // ButLog
            // 
            this.ButLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ButLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButLog.ForeColor = System.Drawing.Color.Cyan;
            this.ButLog.Location = new System.Drawing.Point(10, 135);
            this.ButLog.Margin = new System.Windows.Forms.Padding(0);
            this.ButLog.Name = "ButLog";
            this.ButLog.Size = new System.Drawing.Size(120, 24);
            this.ButLog.TabIndex = 12;
            this.ButLog.TabStop = false;
            this.ButLog.Text = "日志";
            this.ButLog.UseVisualStyleBackColor = false;
            this.ButLog.Click += new System.EventHandler(this.ButLog_Click);
            // 
            // BOT_set
            // 
            this.BOT_set.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BOT_set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BOT_set.ForeColor = System.Drawing.Color.Cyan;
            this.BOT_set.Location = new System.Drawing.Point(10, 166);
            this.BOT_set.Margin = new System.Windows.Forms.Padding(0);
            this.BOT_set.Name = "BOT_set";
            this.BOT_set.Size = new System.Drawing.Size(120, 24);
            this.BOT_set.TabIndex = 13;
            this.BOT_set.TabStop = false;
            this.BOT_set.Text = "设置";
            this.BOT_set.UseVisualStyleBackColor = false;
            this.BOT_set.Click += new System.EventHandler(this.BOT_set_Click);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit.ForeColor = System.Drawing.Color.Cyan;
            this.Exit.Location = new System.Drawing.Point(9, 398);
            this.Exit.Margin = new System.Windows.Forms.Padding(0);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(120, 24);
            this.Exit.TabIndex = 19;
            this.Exit.TabStop = false;
            this.Exit.Text = "退出";
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // butExit
            // 
            this.butExit.BackColor = System.Drawing.Color.Transparent;
            this.butExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butExit.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butExit.ForeColor = System.Drawing.Color.Cyan;
            this.butExit.Location = new System.Drawing.Point(866, 0);
            this.butExit.Margin = new System.Windows.Forms.Padding(0);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(33, 18);
            this.butExit.TabIndex = 21;
            this.butExit.TabStop = false;
            this.butExit.Text = "❌";
            this.butExit.UseVisualStyleBackColor = false;
            this.butExit.Click += new System.EventHandler(this.button2_Click);
            // 
            // butXH
            // 
            this.butXH.BackColor = System.Drawing.Color.Transparent;
            this.butXH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butXH.Font = new System.Drawing.Font("宋体", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butXH.ForeColor = System.Drawing.Color.Cyan;
            this.butXH.Location = new System.Drawing.Point(830, 0);
            this.butXH.Margin = new System.Windows.Forms.Padding(0);
            this.butXH.Name = "butXH";
            this.butXH.Size = new System.Drawing.Size(33, 18);
            this.butXH.TabIndex = 22;
            this.butXH.TabStop = false;
            this.butXH.Text = "-";
            this.butXH.UseVisualStyleBackColor = false;
            this.butXH.Click += new System.EventHandler(this.butXH_Click);
            // 
            // BOTLOGcontextMenuStrip
            // 
            this.BOTLOGcontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.详细信息ToolStripMenuItem,
            this.清空日志ToolStripMenuItem,
            this.复制内容ToolStripMenuItem});
            this.BOTLOGcontextMenuStrip.Name = "BOTLOGcontextMenuStrip";
            this.BOTLOGcontextMenuStrip.Size = new System.Drawing.Size(125, 70);
            // 
            // 详细信息ToolStripMenuItem
            // 
            this.详细信息ToolStripMenuItem.Name = "详细信息ToolStripMenuItem";
            this.详细信息ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.详细信息ToolStripMenuItem.Text = "详细信息";
            this.详细信息ToolStripMenuItem.Click += new System.EventHandler(this.详细信息ToolStripMenuItem_Click);
            // 
            // 清空日志ToolStripMenuItem
            // 
            this.清空日志ToolStripMenuItem.Name = "清空日志ToolStripMenuItem";
            this.清空日志ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.清空日志ToolStripMenuItem.Text = "清空日志";
            this.清空日志ToolStripMenuItem.Click += new System.EventHandler(this.清空日志ToolStripMenuItem_Click);
            // 
            // 复制内容ToolStripMenuItem
            // 
            this.复制内容ToolStripMenuItem.Name = "复制内容ToolStripMenuItem";
            this.复制内容ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.复制内容ToolStripMenuItem.Text = "复制内容";
            this.复制内容ToolStripMenuItem.Click += new System.EventHandler(this.复制内容ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(32, 19);
            // 
            // 全局屏蔽ToolStripMenuItem
            // 
            this.全局屏蔽ToolStripMenuItem.Name = "全局屏蔽ToolStripMenuItem";
            this.全局屏蔽ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 指定死亡ToolStripMenuItem
            // 
            this.指定死亡ToolStripMenuItem.Name = "指定死亡ToolStripMenuItem";
            this.指定死亡ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 获取状态ToolStripMenuItem
            // 
            this.获取状态ToolStripMenuItem.Name = "获取状态ToolStripMenuItem";
            this.获取状态ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 拉黑IPToolStripMenuItem
            // 
            this.拉黑IPToolStripMenuItem.Name = "拉黑IPToolStripMenuItem";
            this.拉黑IPToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 拉黑名单ToolStripMenuItem
            // 
            this.拉黑名单ToolStripMenuItem.Name = "拉黑名单ToolStripMenuItem";
            this.拉黑名单ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 踢出客户端ToolStripMenuItem
            // 
            this.踢出客户端ToolStripMenuItem.Name = "踢出客户端ToolStripMenuItem";
            this.踢出客户端ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 获取状态全部玩家ToolStripMenuItem
            // 
            this.获取状态全部玩家ToolStripMenuItem.Name = "获取状态全部玩家ToolStripMenuItem";
            this.获取状态全部玩家ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(32, 19);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(32, 19);
            // 
            // 增加人数ToolStripMenuItem
            // 
            this.增加人数ToolStripMenuItem.Name = "增加人数ToolStripMenuItem";
            this.增加人数ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 修改房名ToolStripMenuItem
            // 
            this.修改房名ToolStripMenuItem.Name = "修改房名ToolStripMenuItem";
            this.修改房名ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // label_Revsend
            // 
            this.label_Revsend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.label_Revsend.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label_Revsend.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Revsend.ForeColor = System.Drawing.Color.Cyan;
            this.label_Revsend.Location = new System.Drawing.Point(0, 20);
            this.label_Revsend.Name = "label_Revsend";
            this.label_Revsend.Size = new System.Drawing.Size(140, 15);
            this.label_Revsend.TabIndex = 56;
            this.label_Revsend.Text = "收:0  发:0";
            this.label_Revsend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BotVer
            // 
            this.BotVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.BotVer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BotVer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BotVer.ForeColor = System.Drawing.Color.Cyan;
            this.BotVer.Location = new System.Drawing.Point(0, 46);
            this.BotVer.Name = "BotVer";
            this.BotVer.Size = new System.Drawing.Size(140, 15);
            this.BotVer.TabIndex = 57;
            this.BotVer.Text = "版本:待获取";
            this.BotVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BOTSET
            // 
            this.BOTSET.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.BOTSET.Controls.Add(this.listView_bot);
            this.BOTSET.Controls.Add(this.Tbox_wsProt);
            this.BOTSET.Controls.Add(this.botText2);
            this.BOTSET.Controls.Add(this.botText);
            this.BOTSET.Controls.Add(this.checkBox_BOTAPIMSG);
            this.BOTSET.Controls.Add(this.WS_stop);
            this.BOTSET.Controls.Add(this.button_WS_start);
            this.BOTSET.Controls.Add(this.label1);
            this.BOTSET.Location = new System.Drawing.Point(4, 4);
            this.BOTSET.Name = "BOTSET";
            this.BOTSET.Padding = new System.Windows.Forms.Padding(3);
            this.BOTSET.Size = new System.Drawing.Size(764, 427);
            this.BOTSET.TabIndex = 1;
            this.BOTSET.Text = "框架设置";
            // 
            // listView_bot
            // 
            this.listView_bot.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listView_bot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.listView_bot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_bot.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.index,
            this.QQ,
            this.name,
            this.ws});
            this.listView_bot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(60)))));
            this.listView_bot.FullRowSelect = true;
            this.listView_bot.GridLines = true;
            this.listView_bot.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView_bot.HideSelection = false;
            this.listView_bot.Location = new System.Drawing.Point(1, 53);
            this.listView_bot.Margin = new System.Windows.Forms.Padding(0);
            this.listView_bot.Name = "listView_bot";
            this.listView_bot.Size = new System.Drawing.Size(760, 426);
            this.listView_bot.TabIndex = 29;
            this.listView_bot.UseCompatibleStateImageBehavior = false;
            this.listView_bot.View = System.Windows.Forms.View.Details;
            // 
            // index
            // 
            this.index.Width = 30;
            // 
            // QQ
            // 
            this.QQ.Width = 100;
            // 
            // name
            // 
            this.name.Width = 120;
            // 
            // ws
            // 
            this.ws.Width = 100;
            // 
            // Tbox_wsProt
            // 
            this.Tbox_wsProt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.Tbox_wsProt.ForeColor = System.Drawing.Color.Aqua;
            this.Tbox_wsProt.Location = new System.Drawing.Point(71, 12);
            this.Tbox_wsProt.MaxLength = 5;
            this.Tbox_wsProt.Name = "Tbox_wsProt";
            this.Tbox_wsProt.Size = new System.Drawing.Size(80, 21);
            this.Tbox_wsProt.TabIndex = 8;
            this.Tbox_wsProt.TabStop = false;
            this.Tbox_wsProt.Text = "3002";
            this.Tbox_wsProt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tbox_wsProt.WordWrap = false;
            this.Tbox_wsProt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbox_wsProt_KeyPress);
            // 
            // botText2
            // 
            this.botText2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.botText2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.botText2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.botText2.ForeColor = System.Drawing.Color.Cyan;
            this.botText2.Location = new System.Drawing.Point(651, 8);
            this.botText2.Name = "botText2";
            this.botText2.Size = new System.Drawing.Size(101, 410);
            this.botText2.TabIndex = 28;
            this.botText2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // botText
            // 
            this.botText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.botText.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.botText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.botText.ForeColor = System.Drawing.Color.Cyan;
            this.botText.Location = new System.Drawing.Point(516, 8);
            this.botText.Name = "botText";
            this.botText.Size = new System.Drawing.Size(116, 410);
            this.botText.TabIndex = 27;
            this.botText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_BOTAPIMSG
            // 
            this.checkBox_BOTAPIMSG.AutoSize = true;
            this.checkBox_BOTAPIMSG.ForeColor = System.Drawing.Color.Cyan;
            this.checkBox_BOTAPIMSG.Location = new System.Drawing.Point(352, 17);
            this.checkBox_BOTAPIMSG.Name = "checkBox_BOTAPIMSG";
            this.checkBox_BOTAPIMSG.Size = new System.Drawing.Size(96, 16);
            this.checkBox_BOTAPIMSG.TabIndex = 26;
            this.checkBox_BOTAPIMSG.Text = "原始收发消息";
            this.checkBox_BOTAPIMSG.UseVisualStyleBackColor = true;
            // 
            // WS_stop
            // 
            this.WS_stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.WS_stop.CausesValidation = false;
            this.WS_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WS_stop.ForeColor = System.Drawing.Color.Cyan;
            this.WS_stop.Location = new System.Drawing.Point(233, 11);
            this.WS_stop.Name = "WS_stop";
            this.WS_stop.Size = new System.Drawing.Size(70, 23);
            this.WS_stop.TabIndex = 3;
            this.WS_stop.TabStop = false;
            this.WS_stop.Text = "BOT_退出";
            this.WS_stop.UseVisualStyleBackColor = false;
            this.WS_stop.Click += new System.EventHandler(this.WS_stop_Click);
            // 
            // button_WS_start
            // 
            this.button_WS_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.button_WS_start.CausesValidation = false;
            this.button_WS_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_WS_start.ForeColor = System.Drawing.Color.Cyan;
            this.button_WS_start.Location = new System.Drawing.Point(157, 11);
            this.button_WS_start.Name = "button_WS_start";
            this.button_WS_start.Size = new System.Drawing.Size(70, 23);
            this.button_WS_start.TabIndex = 1;
            this.button_WS_start.TabStop = false;
            this.button_WS_start.Text = "BOT_登录";
            this.button_WS_start.UseVisualStyleBackColor = false;
            this.button_WS_start.Click += new System.EventHandler(this.button_WS_start_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 11;
            this.label1.Text = "WS Port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.tabPage1.Controls.Add(this.BOTlist);
            this.tabPage1.ForeColor = System.Drawing.Color.LightGray;
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(764, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "BOT_日志";
            // 
            // BOTlist
            // 
            this.BOTlist.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.BOTlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.BOTlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BOTlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Time,
            this.group_name,
            this.group_ID,
            this.User_ID,
            this.User_name,
            this.meg});
            this.BOTlist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.BOTlist.FullRowSelect = true;
            this.BOTlist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.BOTlist.HideSelection = false;
            this.BOTlist.Location = new System.Drawing.Point(0, 0);
            this.BOTlist.Margin = new System.Windows.Forms.Padding(0);
            this.BOTlist.Name = "BOTlist";
            this.BOTlist.Size = new System.Drawing.Size(760, 426);
            this.BOTlist.TabIndex = 0;
            this.BOTlist.UseCompatibleStateImageBehavior = false;
            this.BOTlist.View = System.Windows.Forms.View.Details;
            this.BOTlist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BOTLOG_MouseUp);
            // 
            // Time
            // 
            this.Time.Text = "  时间";
            // 
            // group_name
            // 
            this.group_name.Text = "来源群";
            this.group_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.group_name.Width = 120;
            // 
            // group_ID
            // 
            this.group_ID.Text = "群号";
            this.group_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.group_ID.Width = 120;
            // 
            // User_ID
            // 
            this.User_ID.Text = "QQ";
            this.User_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.User_ID.Width = 120;
            // 
            // User_name
            // 
            this.User_name.Text = "昵称";
            this.User_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.User_name.Width = 120;
            // 
            // meg
            // 
            this.meg.Text = "             消息内容";
            this.meg.Width = 190;
            // 
            // TabListGmaneSet
            // 
            this.TabListGmaneSet.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabListGmaneSet.Controls.Add(this.tabPage1);
            this.TabListGmaneSet.Controls.Add(this.BOTSET);
            this.TabListGmaneSet.Cursor = System.Windows.Forms.Cursors.Default;
            this.TabListGmaneSet.ItemSize = new System.Drawing.Size(6, 15);
            this.TabListGmaneSet.Location = new System.Drawing.Point(135, 20);
            this.TabListGmaneSet.Margin = new System.Windows.Forms.Padding(0);
            this.TabListGmaneSet.Multiline = true;
            this.TabListGmaneSet.Name = "TabListGmaneSet";
            this.TabListGmaneSet.Padding = new System.Drawing.Point(0, 0);
            this.TabListGmaneSet.SelectedIndex = 0;
            this.TabListGmaneSet.Size = new System.Drawing.Size(772, 450);
            this.TabListGmaneSet.TabIndex = 11;
            this.TabListGmaneSet.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(140, 451);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // MySvrForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(39)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(945, 478);
            this.Controls.Add(this.BotVer);
            this.Controls.Add(this.label_Revsend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butXH);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.BOT_set);
            this.Controls.Add(this.ButLog);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.TabListGmaneSet);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MySvrForm";
            this.Text = "MySever";
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MySeverForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MySeverForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MySeverForm_Mouseup);
            this.BOTLOGcontextMenuStrip.ResumeLayout(false);
            this.BOTSET.ResumeLayout(false);
            this.BOTSET.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.TabListGmaneSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private Label label2;
        protected internal Label label_Revsend;
        protected internal Label BotVer;
        private TabPage BOTSET;
        protected internal TextBox Tbox_wsProt;
        protected internal Label botText2;
        protected internal Label botText;
        protected internal CheckBox checkBox_BOTAPIMSG;
        private Button WS_stop;
        private Button button_WS_start;
        protected internal Label label1;
        private TabPage tabPage1;
        protected internal ListView BOTlist;
        private ColumnHeader Time;
        protected internal ColumnHeader group_name;
        private ColumnHeader group_ID;
        private ColumnHeader User_ID;
        private ColumnHeader User_name;
        private ColumnHeader meg;
        protected internal TabControl TabListGmaneSet;
        private PictureBox pictureBox2;
        protected internal ListView listView_bot;
        private ColumnHeader index;
        private ColumnHeader QQ;
        private ColumnHeader name;
        private ColumnHeader ws;
    }
}

