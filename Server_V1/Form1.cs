using System;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Server_V1
{
    //主界面
    public partial class Form1 : Form //主界面
    {
        public delegate void ArgsMainToMonitor(int args);//从主界面向视频界面传参数的代理类
        public static ArgsMainToMonitor argsMainToMonitor;
        public delegate bool InitCamDelegate(string camNums, string CamNumSection, MonitorMain.IniFile iniFile);//将ini配置文件信息传递给监控MonitorMain类
        public static InitCamDelegate StartCamEvent;//StartCamEvent：启动相机的代理函数
        private bool IsOpen_Node0_1_1 = false;//判断节点是否已经打开
        private bool IsOpen_Node0_1_2 = false;
        private bool IsOpen_Node0_1_3 = false;
        private bool IsOpen_Node0_1_4 = false;
        private bool IsOpen_Node0_1_5 = false;
        private bool IsOpen_Node0_1_6 = false;
        private bool Moni1IsInit = false;//判断某个节点的实例是否初始化
        private bool Moni2IsInit = false;
        private bool Moni3IsInit = false;
        private bool Moni4IsInit = false;
        private bool Moni5IsInit = false;
        private bool Moni6IsInit = false;
        private string camNums = "camNums";//指定ini文件的section部分
        private string CamNumSection1 = "region1_number";//region1-6：监控节1-6的相机数量
        private string CamNumSection2 = "region2_number";//camNums和CamNumSectionX需要配合使用
        private string CamNumSection3 = "region3_number";
        private string CamNumSection4 = "region4_number";
        private string CamNumSection5 = "region5_number";
        private string CamNumSection6 = "region6_number";
        MonitorMain.IniFile sec1IniFile = new MonitorMain.IniFile();//指定读取ini配置文件中的相应regionX_camX文件
        MonitorMain.IniFile sec2IniFile = new MonitorMain.IniFile();
        MonitorMain.IniFile sec3IniFile = new MonitorMain.IniFile();
        MonitorMain.IniFile sec4IniFile = new MonitorMain.IniFile();


        MonitorMain monitorWnd = new MonitorMain();//初始化监控点窗口实例
        MonitorMain SecondMoni = new MonitorMain();
        MonitorMain ThirdMoni = new MonitorMain();
        MonitorMain FourthMoni = new MonitorMain();
        DHCKSQLServerDatabase databaseWnd = new DHCKSQLServerDatabase();
        SysConfig sysConfigForm = new SysConfig();//初始化系统配置窗口实例
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            sec1IniFile.Section1 = "region1_cam1";
            sec1IniFile.Section2 = "region1_cam2";
            sec1IniFile.Section3 = "region1_cam3";
            sec1IniFile.Section4 = "region1_cam4";
            sec1IniFile.Section5 = "region1_cam5";
            sec1IniFile.Section6 = "region1_cam6";
            sec2IniFile.Section1 = "region2_cam1";
            sec2IniFile.Section2 = "region2_cam2";
            sec2IniFile.Section3 = "region2_cam3";
            sec2IniFile.Section4 = "region2_cam4";
            sec2IniFile.Section5 = "region2_cam5";
            sec2IniFile.Section6 = "region2_cam6";
            sec3IniFile.Section1 = "region3_cam1";
            sec3IniFile.Section2 = "region3_cam2";
            sec3IniFile.Section3 = "region3_cam3";
            sec3IniFile.Section4 = "region3_cam4";
            sec3IniFile.Section5 = "region3_cam5";
            sec3IniFile.Section6 = "region3_cam6";
            sec4IniFile.Section1 = "region4_cam1";
            sec4IniFile.Section2 = "region4_cam2";
            sec4IniFile.Section3 = "region4_cam3";
            sec4IniFile.Section4 = "region4_cam4";
            sec4IniFile.Section5 = "region4_cam5";
            sec4IniFile.Section6 = "region4_cam6";
        }
        
        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnIndex_Click(object sender, EventArgs e)
        {
            InitWnd(monitorWnd);
            monitorWnd.Show();
            monitorWnd.BringToFront();
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            splitContainer2.SplitterDistance = splitContainer2.Panel2.Width / 6;
            splitContainer3.SplitterDistance = splitContainer2.Panel1.Height / 2;
            treeViewMain.ExpandAll();
            MonitorMain.ArgsEvent = MessageToListBox;
            DHCKSQLServerDatabase.SqlServerDatabase sqlconn = new DHCKSQLServerDatabase.SqlServerDatabase();
            sqlconn.SetToDefaultDatabase();
            Thread OpenDbThread = new Thread(sqlconn.OpenDatabase);
            //OpenDbThread.Start();
        }

        private bool InitWnd(MonitorMain monitorMain)
        {
            monitorMain.MdiParent = this;
            splitContainer2.Panel2.Controls.Add(monitorMain);
            monitorMain.Parent = splitContainer2.Panel2;
            monitorMain.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(monitorMain);
            monitorMain.Size = splitContainer2.Panel2.Size;
            monitorMain.Show();
            monitorMain.BringToFront();
            return true;
        }
        private bool InitDBWnd(DHCKSQLServerDatabase DbWnd)
        {
            DbWnd.MdiParent = this;
            splitContainer2.Panel2.Controls.Add(DbWnd);
            DbWnd.Parent = splitContainer2.Panel2;
            DbWnd.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(DbWnd);
            DbWnd.Size = splitContainer2.Panel2.Size;
            DbWnd.Show();
            DbWnd.BringToFront();
            return true;
        }

        private void TreeViewMain_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            



            if (e.Node.Name == "Node0-1-1")
            {
                if (!IsOpen_Node0_1_1)
                {
                    listBoxMessage.Items.Insert(0, "监控点1");
                    if (!Moni1IsInit)
                    {
                        Moni1IsInit = InitWnd(monitorWnd);
                    }
                    //IsOpen_Node0_1_1 = StartCamEvent(camNums, CamNumSection1,sec1IniFile);
                }
                argsMainToMonitor(1);
                monitorWnd.Show();
                monitorWnd.BringToFront();
            }

            if (e.Node.Name =="Node0-1-2")
            {
                if (!IsOpen_Node0_1_2)
                {
                    listBoxMessage.Items.Insert(0, "监控点2");
                    if (!Moni2IsInit)
                    {
                        Moni2IsInit = InitWnd(SecondMoni);
                    }
                    //IsOpen_Node0_1_2 = StartCamEvent(camNums, CamNumSection2, sec2IniFile);
                }
                argsMainToMonitor(2);
                SecondMoni.Show();
                SecondMoni.BringToFront();
            }
            if (e.Node.Name == "Node0-1-3")
            {
                if (!IsOpen_Node0_1_3)
                {
                    listBoxMessage.Items.Insert(0, "监控点3");
                    if (!Moni3IsInit)
                    {
                        Moni3IsInit = InitWnd(ThirdMoni);
                    }
                    //IsOpen_Node0_1_3 = StartCamEvent(camNums, CamNumSection3, sec3IniFile);
                }
                argsMainToMonitor(3);
                ThirdMoni.Show();
                ThirdMoni.BringToFront();
            }
            if (e.Node.Name == "Node0-1-4")
            {
                if (!IsOpen_Node0_1_4)
                {
                    listBoxMessage.Items.Insert(0, "监控点4");
                    if (!Moni4IsInit)
                    {
                        Moni4IsInit = InitWnd(FourthMoni);
                    }
                    //IsOpen_Node0_1_4 = StartCamEvent(camNums, CamNumSection4, sec4IniFile);
                }
                argsMainToMonitor(4);
                FourthMoni.Show();
                FourthMoni.BringToFront();
            }
        }

        private void MessageToListBox(string msg)
        {
            listBoxMessage.Items.Add(msg);
        }

        private void BtnSystem_Click(object sender, EventArgs e)
        {
            sysConfigForm.MdiParent = this;
            sysConfigForm.Parent = splitContainer2.Panel2;

            sysConfigForm.TopLevel = false;
            this.splitContainer2.Panel2.Controls.Add(sysConfigForm);
            sysConfigForm.Show();
            sysConfigForm.BringToFront();
            sysConfigForm.Size = splitContainer2.Panel2.Size;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            InitDBWnd(databaseWnd);
            databaseWnd.Show();
            //databaseWnd.BringToFront();
        }
    }
    //数据库类，功能不全
    
}
