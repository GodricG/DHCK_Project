using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DHCKProject_V1;

namespace Server_V1
{
    public partial class MonitorMain : Form
    {
        public struct IniFile
        {
            public string Section1 { get; set; }
            public string Section2 { get; set; }
            public string Section3 { get; set; }
            public string Section4 { get; set; }
            public string Section5 { get; set; }
            public string Section6 { get; set; }
        }
        public delegate void ArgsMonitorToMain(string args);
        public static ArgsMonitorToMain ArgsEvent;
        
        private string iniPath = Path.Combine(Environment.CurrentDirectory,  "CameraConfigurations\\camconfig.ini");

        private string recordDirPath = Path.Combine(Environment.CurrentDirectory, "VideoDirectory");
        private bool m_bInitSDK = false;
        private string ichannel = "1";
        uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
        private string cam1Section = "region1_cam1";
        private string cam2Section = "region1_cam2";
        private string cam3Section = "region1_cam3";
        private string cam4Section = "region1_cam4";
        private string cam5Section = "region1_cam5";
        private string cam6Section = "region1_cam6";
        private int nowRegion;
        private string CamNumSection;
        private bool IsSetted1 = false;
        private bool IsSetted2 = false;
        private bool IsSetted3 = false;
        private bool IsSetted4 = false;
        private bool IsSetted5 = false;
        private bool IsSetted6 = false;
        DevInfo dev1 = new DevInfo();
        DevInfo dev2 = new DevInfo();
        DevInfo dev3 = new DevInfo();
        DevInfo dev4 = new DevInfo();
        DevInfo dev5 = new DevInfo();
        DevInfo dev6 = new DevInfo();
        CamConf cam1 = new CamConf();
        CamConf cam2 = new CamConf();
        CamConf cam3 = new CamConf();
        CamConf cam4 = new CamConf();
        CamConf cam5 = new CamConf();
        CamConf cam6 = new CamConf();
        private Int32 userID_1 = -1;
        private Int32 userID_2 = -1;
        private Int32 userID_3 = -1;
        private Int32 userID_4 = -1;
        private Int32 userID_5 = -1;
        private Int32 userID_6 = -1;
        private Int32 m_RealHandle1 = -1;
        private Int32 m_RealHandle2 = -1;
        private Int32 m_RealHandle3 = -1;
        private Int32 m_RealHandle4 = -1;
        private Int32 m_RealHandle5 = -1;
        private Int32 m_RealHandle6 = -1;
        private bool Cam1IsRecord = false;
        private bool Cam2IsRecord = false;
        private bool Cam3IsRecord = false;
        private bool Cam4IsRecord = false;
        private bool Cam5IsRecord = false;
        private bool Cam6IsRecord = false;
        InitCam initCam1 = new InitCam();
        InitCam initCam2 = new InitCam();
        InitCam initCam3 = new InitCam();
        InitCam initCam4 = new InitCam();
        InitCam initCam5 = new InitCam();
        InitCam initCam6 = new InitCam();
        private bool IsLoginAll = false;
        private bool IsLiveAll = false;
        private bool IsRecordAll = false;
        private bool IsActivatedLiveAll = false;
        private bool IsLiveAllButtonEnabled = false;
        private bool IsActivatedRecordAll = false;
        public IniFile SecFile = new IniFile();
        private string str;
        public MonitorMain()
        {
            InitializeComponent();
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "K:\\SdkLog\\", true);
            }

            
            cam1 = getCamConf(iniPath, cam1Section);
            cam2 = getCamConf(iniPath, cam2Section);
            cam3 = getCamConf(iniPath, cam3Section);
            cam4 = getCamConf(iniPath, cam4Section);
            cam5 = getCamConf(iniPath, cam5Section);
            cam6 = getCamConf(iniPath, cam6Section);
            GetDevInfo(ref dev1, cam1);
            GetDevInfo(ref dev2, cam2);
            GetDevInfo(ref dev3, cam3);
            GetDevInfo(ref dev4, cam4);
            GetDevInfo(ref dev5, cam5);
            GetDevInfo(ref dev6, cam6);
        }
        public void MonitorMain_Load(object sender, EventArgs e)
        {
            
            AutoSized();
            SetValueForInitCam(ref initCam1, ref userID_1, ref m_RealHandle1, dev1, pictureBox1, ref IsSetted1);
            SetValueForInitCam(ref initCam2, ref userID_2, ref m_RealHandle2, dev2, pictureBox2,ref IsSetted2);
            SetValueForInitCam(ref initCam3, ref userID_3, ref m_RealHandle3, dev3, pictureBox3,ref IsSetted3);
            SetValueForInitCam(ref initCam4, ref userID_4, ref m_RealHandle4, dev4, pictureBox4,ref IsSetted4);
            SetValueForInitCam(ref initCam5, ref userID_5, ref m_RealHandle5, dev5, pictureBox5,ref IsSetted5);
            SetValueForInitCam(ref initCam6, ref userID_6, ref m_RealHandle6, dev6, pictureBox6,ref IsSetted6);
            Form1.StartCamEvent = StartCamEvent;
            Form1.argsMainToMonitor = GetRegionNum;
        }

        public bool StartCamEvent(string camNums,string section,IniFile iniFile)
        {
            cam1Section = iniFile.Section1;
            cam2Section = iniFile.Section2;
            cam3Section = iniFile.Section3;
            cam4Section = iniFile.Section4;
            cam5Section = iniFile.Section5;
            cam6Section = iniFile.Section6;
            int camNumbers = int.Parse(IniFileDll.IniFileDll.Read(camNums, section, null, iniPath));
            switch (camNumbers)
            {
                case 1:
                    Thread cam11Thread = new Thread(initCam);
                    cam11Thread.Start(initCam1);
                    break;
                case 2:
                    Thread cam21Thread = new Thread(initCam);
                    Thread cam22Thread = new Thread(initCam);
                    cam21Thread.Start(initCam1);
                    cam22Thread.Start(initCam2);
                    break;
                case 3:
                    Thread cam31Thread = new Thread(initCam);
                    Thread cam32Thread = new Thread(initCam);
                    Thread cam33Thread = new Thread(initCam);
                    cam31Thread.Start(initCam1);
                    cam32Thread.Start(initCam2);
                    cam33Thread.Start(initCam3);
                    break;
                case 4:
                    Thread cam41Thread = new Thread(initCam);
                    Thread cam42Thread = new Thread(initCam);
                    Thread cam43Thread = new Thread(initCam);
                    Thread cam44Thread = new Thread(initCam);
                    cam41Thread.Start(initCam1);
                    cam42Thread.Start(initCam2);
                    cam43Thread.Start(initCam3);
                    cam44Thread.Start(initCam4);
                    break;
                case 5:
                    Thread cam51Thread = new Thread(initCam);
                    Thread cam52Thread = new Thread(initCam);
                    Thread cam53Thread = new Thread(initCam);
                    Thread cam54Thread = new Thread(initCam);
                    Thread cam55Thread = new Thread(initCam);
                    cam51Thread.Start(initCam1);
                    cam52Thread.Start(initCam2);
                    cam53Thread.Start(initCam3);
                    cam54Thread.Start(initCam4);
                    cam55Thread.Start(initCam5);
                    break;
                case 6:
                    Thread cam61Thread = new Thread(initCam);
                    Thread cam62Thread = new Thread(initCam);
                    Thread cam63Thread = new Thread(initCam);
                    Thread cam64Thread = new Thread(initCam);
                    Thread cam65Thread = new Thread(initCam);
                    Thread cam66Thread = new Thread(initCam);
                    cam61Thread.Start(initCam1);
                    cam62Thread.Start(initCam2);
                    cam63Thread.Start(initCam3);
                    cam64Thread.Start(initCam4);
                    cam65Thread.Start(initCam5);
                    cam66Thread.Start(initCam6);
                    break;
            }
            return true;
        }
        private void SetValueForInitCam(ref InitCam initCam,ref Int32 userId, ref Int32 realHandle,DevInfo devInfo,PictureBox pictureBox,ref bool isSetted)
        {
            initCam.DevInfo = new DevInfo();
            if (!isSetted)
            {
                initCam.UserId = userId;
                initCam.m_lRealHandle = realHandle;
                isSetted = true;
            }
            initCam.DevInfo = devInfo;
            initCam.PictureBox = pictureBox;
        }
        private void initCam(object obj)
        {
            InitCam camInfo = obj as InitCam;
            if (camInfo.DevInfo.IP != "")
            {
                
                //loginButton(ref camInfo.DevInfo, ref camInfo.UserId);//, camInfo.m_lRealHandle);
                ArgsEvent("Initialize camera...");
                Thread.Sleep(2000);
                //previewBtn(camInfo.UserId, ref camInfo.m_lRealHandle, camInfo.PictureBox, camInfo.DevInfo.StreamType);
                
            }
            else
            {
                return;
            }
        }

        private void GetDevInfo(ref DevInfo devInfo, CamConf camConf)
        {
            devInfo.UserID = camConf.UserID;
            devInfo.IP = camConf.IPAddress;
            devInfo.StreamType = 0.ToString();
            devInfo.Port = camConf.Port;
            devInfo.Password = camConf.Pwd;
        }
        
        public void AutoSized()
        {
            splitContainer1.SplitterDistance = Height / 3 * 2;
            splitContainer2.SplitterDistance = splitContainer1.Panel1.Width / 3 * 2;
            splitContainer3.SplitterDistance = splitContainer2.Panel2.Height / 2;
            splitContainer4.SplitterDistance = splitContainer1.Panel2.Width / 3;
            splitContainer5.SplitterDistance = splitContainer4.Panel2.Width / 2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer2.IsSplitterFixed = true;
            splitContainer3.IsSplitterFixed = true;
            splitContainer4.IsSplitterFixed = true;
            splitContainer5.IsSplitterFixed = true;
        }

        private int getCamNums(string section)
        {
            CamNumSection = "region" + nowRegion.ToString() + "_number";
            //var File = Path.Combine(Environment.CurrentDirectory, "CameraConfigurations\\camconfig.ini");
            return Int32.Parse(IniFileDll.IniFileDll.Read("camNums", CamNumSection, null, iniPath));
        }

        public CamConf getCamConf(string filePath, string section)
        {
            CamConf tmp = new CamConf();
            string def = null;
            tmp.IPAddress = IniFileDll.IniFileDll.Read(section, "IPAddress", def, filePath);
            tmp.Port = IniFileDll.IniFileDll.Read(section, "Port", def, filePath);
            tmp.UserID = IniFileDll.IniFileDll.Read(section, "UserID", def, filePath);
            tmp.Pwd = IniFileDll.IniFileDll.Read(section, "Pwd", def, filePath);
            return tmp;
        }
        private static bool checkInfo(ref DevInfo e)
        {
            return (e.getUserID() == "" || e.getPassword() == "" || e.getStreamType() == "" || e.getIP() == "" ||
            e.getPort() == "");
        }
        
        private void SaveVideo(DevInfo dev, ref bool ifRecord, string path, ref Int32 userID, ref Int32 realHandler, ToolStripMenuItem recordToolStripMenuItem)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string VideoName = path +"\\"+ dev.getIP() + "_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" +
                               DateTime.Now.Hour + "-" + DateTime.Now.Minute + ".mp4";

            if (!ifRecord)
            {
                int iChannel_1 = Int16.Parse(ichannel);
                
                CHCNetSDK.NET_DVR_MakeKeyFrame(userID, iChannel_1);
                bool ret = CHCNetSDK.NET_DVR_SaveRealData(userID, VideoName);
                if (!ret)
                {
                    uint ilastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_SaveRealData failed, error code= " + ilastErr;
                    MessageBox.Show(str);
                    
                    return;
                }
                else
                {
                    
                    recordToolStripMenuItem.Text = "Stop recording";
                    
                    ifRecord = true;
                }

            }
            else
            {
                bool ret = CHCNetSDK.NET_DVR_StopSaveRealData(realHandler);
                if (!ret)
                {
                    uint ilastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopSaveRealData failed, error code= " + ilastErr;
                    MessageBox.Show(str);
                    
                    return;
                }
                else
                {
                    str = "Successful to stop recording and the saved file is " + VideoName;
                    recordToolStripMenuItem.Text = "Record this";
                    MessageBox.Show(str);
                    ifRecord = false;
                }
            }
        }

        private string RecordFilePath(int region, int cam)
        {
            string PartialPath = "region" + region.ToString() + "\\camera" + cam.ToString();
            return Path.Combine(recordDirPath, PartialPath);
        }

        private void GetRegionNum(int regionNum)
        {
            nowRegion = regionNum;
        }
        //PictureBox1-this
        
        //PictureBox1-all
        private void recordAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartRecordAll();
        }

        private void StartRecordAll()
        {
            IsRecordAll = true;
            if (getCamNums(CamNumSection) >= 1)
            {
                SaveVideo(dev1, ref Cam1IsRecord, RecordFilePath(nowRegion, 1), ref userID_1, ref m_RealHandle1, recordToolStripMenuItem);
                if (getCamNums(CamNumSection) >= 2)
                {
                    SaveVideo(dev2, ref Cam2IsRecord, RecordFilePath(nowRegion, 2), ref userID_2, ref m_RealHandle2, recordThisToolStripMenuItem);
                    if (getCamNums(CamNumSection) >= 3)
                    {
                        SaveVideo(dev3, ref Cam3IsRecord, RecordFilePath(nowRegion, 3), ref userID_3, ref m_RealHandle3, recordThisToolStripMenuItem1);
                        if (getCamNums(CamNumSection) >= 4)
                        {
                            SaveVideo(dev4, ref Cam4IsRecord, RecordFilePath(nowRegion, 4), ref userID_4, ref m_RealHandle4, recordThisToolStripMenuItem3);
                            if (getCamNums(CamNumSection) >= 5)
                            {
                                SaveVideo(dev5, ref Cam5IsRecord, RecordFilePath(nowRegion, 5), ref userID_5, ref m_RealHandle5, recordThisToolStripMenuItem4);
                                if (getCamNums(CamNumSection) >= 6)
                                {
                                    SaveVideo(dev6, ref Cam6IsRecord, RecordFilePath(nowRegion, 6), ref userID_6, ref m_RealHandle6, recordThisToolStripMenuItem5);
                                }
                            }
                        }
                    }
                }
            }
            IsRecordAll = false;
            

        }
        private void recordThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveVideo(dev2, ref Cam2IsRecord, RecordFilePath(nowRegion, 2), ref userID_2, ref m_RealHandle2, recordThisToolStripMenuItem);
        }

        private void recordThisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveVideo(dev3, ref Cam3IsRecord, RecordFilePath(nowRegion, 3), ref userID_3, ref m_RealHandle3, recordThisToolStripMenuItem1);
        }

        private void recordThisToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SaveVideo(dev4, ref Cam4IsRecord, RecordFilePath(nowRegion, 4), ref userID_4, ref m_RealHandle4, recordThisToolStripMenuItem3);
        }

        private void recordThisToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SaveVideo(dev5, ref Cam5IsRecord, RecordFilePath(nowRegion, 5), ref userID_5, ref m_RealHandle5, recordThisToolStripMenuItem4);
        }

        private void recordThisToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SaveVideo(dev6, ref Cam6IsRecord, RecordFilePath(nowRegion, 6), ref userID_6, ref m_RealHandle6, recordThisToolStripMenuItem5);

        }

        private void recordAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StartRecordAll();
        }

        private void recordThisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            recordAllToolStripMenuItem1_Click(sender, e);
        }

        private void recordAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StartRecordAll();
        }

        private void recordAllToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            StartRecordAll();
        }

        private void recordAllToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            StartRecordAll();
        }

        private void loginToolStripMenuItem1_Click(object sender, EventArgs e) //contextMenuStrip1_login
        {
            if(getCamNums(CamNumSection) >=1)
            loginButton(ref dev1, ref userID_1,ref m_RealHandle1, loginToolStripMenuItem1, liveViewToolStripMenuItem);
        }
        private void loginButton(ref DevInfo dev, ref Int32 userID,ref Int32 realHandler,ToolStripMenuItem toolStripMenuItem,ToolStripMenuItem ifActivateLive)
        {
            if (userID >= 0)
            {
                if (realHandler >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");

                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(userID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Logout failed, error code= " + iLastErr;
                    MessageBox.Show(str);

                    return;
                }

                ArgsEvent("Logout succeeded");
                userID = -1;
                toolStripMenuItem.Text = "Login";
                if (IsActivatedLiveAll)
                {
                    IsActivatedLiveAll = IfactivateLiveAll(IsActivatedLiveAll);
                    ifActivateLive.Enabled = false;
                }
                
            }
            //if (checkInfo(ref dev))
            //{
            //    string tmp = "Please input IP, Port, User name, Password and StreamType!";
            //    MessageBox.Show(tmp);
            //    return;
            //}
            else
            {
                //string DVRIPAddress = textBoxIP.Text; //设备IP地址或者域名
                //Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);//设备服务端口号
                //string DVRUserName = textBoxUserName.Text;//设备登录用户名
                //string DVRPassword = textBoxPassword.Text;//设备登录密码
                string DVRIPAddress = dev.getIP(); //设备IP地址或者域名
                Int16 DVRPortNumber = Int16.Parse(dev.getPort());//设备服务端口号
                string DVRUserName = dev.getUserID();//设备登录用户名
                string DVRPassword = dev.getPassword();//设备登录密码

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                //登录设备 Login the device
                userID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (userID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                    MessageBox.Show(str);

                    return;
                }
                else
                {

                    //登录成功
                    string tmp = dev.getIP() + " login success!";
                    toolStripMenuItem.Text = "Logout";
                    if (!IsActivatedLiveAll)
                    {
                        IsActivatedLiveAll = IfactivateLiveAll(IsActivatedLiveAll);
                        
                    }
                    ArgsEvent(tmp);
                    ifActivateLive.Enabled = true;
                    return;
                }

            }

        }

        private void liveViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previewBtn(userID_1, ref m_RealHandle1, pictureBox1, liveViewToolStripMenuItem, recordToolStripMenuItem, "0");
        }
        private void previewBtn(Int32 userID, ref Int32 realHandler, PictureBox Wnd, ToolStripMenuItem contextMenuStrip, ToolStripMenuItem recordButton, string streamType)
        {
            if (realHandler >= 0)
            {
                if (!CHCNetSDK.NET_DVR_StopRealPlay(realHandler))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                realHandler = -1;
                contextMenuStrip.Text = "Live this";
                recordButton.Enabled = false;
                Wnd.Refresh();

            }

            else
            {
                if (userID < 0)
                {
                    MessageBox.Show("Please login first");
                    return;
                }
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = Wnd.Handle;//预览窗口
                //lpPreviewInfo.hPlayWnd = imageBoxShow.Handle;
                lpPreviewInfo.lChannel = Int16.Parse(ichannel);//预te览的设备通道
                lpPreviewInfo.dwStreamType = uint.Parse(streamType);//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;

                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                realHandler = CHCNetSDK.NET_DVR_RealPlay_V40(userID, ref lpPreviewInfo,
                    null, pUser);
                ArgsEvent("Live view succeed.");
                contextMenuStrip.Text = "Stop living this";
                recordButton.Enabled = true;
                if (realHandler < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
            }

        }
        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveVideo(dev1, ref Cam1IsRecord, RecordFilePath(nowRegion, 1), ref userID_1, ref m_RealHandle1, recordToolStripMenuItem);
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (getCamNums(CamNumSection) >= 2)
                loginButton(ref dev2, ref userID_2,ref m_RealHandle2, loginToolStripMenuItem, liveViewToolStripMenuItem1);
        }

        private void liveViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            previewBtn(userID_2, ref m_RealHandle2, pictureBox2, liveViewToolStripMenuItem1, recordThisToolStripMenuItem, "0");
        }

        private void loginToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (getCamNums(CamNumSection) >= 3)
                loginButton(ref dev3,ref userID_3,ref m_RealHandle3, loginToolStripMenuItem2, liveThisToolStripMenuItem);
        }

        private void liveThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previewBtn(userID_3,ref m_RealHandle3,pictureBox3, liveThisToolStripMenuItem, recordThisToolStripMenuItem1,"0");
        }

        private void loginToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (getCamNums(CamNumSection) >= 4)
                loginButton(ref dev4,ref userID_4,ref m_RealHandle4, loginToolStripMenuItem3, liveThisToolStripMenuItem1);
        }

        private void liveThisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            previewBtn(userID_4,ref m_RealHandle4,pictureBox4, liveThisToolStripMenuItem1, recordThisToolStripMenuItem3,"0");
        }

        private void loginToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (getCamNums(CamNumSection) >= 5)
                loginButton(ref dev5,ref userID_5,ref m_RealHandle5, loginToolStripMenuItem4, liveThisToolStripMenuItem2);
        }

        private void liveThisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            previewBtn(userID_5,ref m_RealHandle5,pictureBox5, liveThisToolStripMenuItem2, recordThisToolStripMenuItem4,"0");
        }

        private void loginToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (getCamNums(CamNumSection) >= 6)
                loginButton(ref dev6,ref userID_6,ref m_RealHandle6, loginToolStripMenuItem5, liveThisToolStripMenuItem3);
        }

        private void liveThisToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            previewBtn(userID_6,ref m_RealHandle6,pictureBox6, liveThisToolStripMenuItem3, recordThisToolStripMenuItem5,"0");
        }

        private void liveAllToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            LiveAll();
        }

        private bool LiveAll()
        {
            if (getCamNums(CamNumSection) >= 1)
            {
                previewBtn(userID_1, ref m_RealHandle1, pictureBox1, liveViewToolStripMenuItem, recordToolStripMenuItem, "0");
                if (getCamNums(CamNumSection) >= 2)
                {
                    previewBtn(userID_2, ref m_RealHandle2, pictureBox2, liveViewToolStripMenuItem1, recordThisToolStripMenuItem, "0");
                    if (getCamNums(CamNumSection) >= 3)
                    {
                        previewBtn(userID_3, ref m_RealHandle3, pictureBox3, liveThisToolStripMenuItem, recordThisToolStripMenuItem1, "0");
                        if (getCamNums(CamNumSection) >= 4)
                        {
                            previewBtn(userID_4, ref m_RealHandle4, pictureBox4, liveThisToolStripMenuItem1, recordThisToolStripMenuItem3, "0");
                            if (getCamNums(CamNumSection) >= 5)
                            {
                                previewBtn(userID_5, ref m_RealHandle5, pictureBox5, liveThisToolStripMenuItem2, recordThisToolStripMenuItem4,"0");
                                if (getCamNums(CamNumSection) >= 6)
                                {
                                    previewBtn(userID_6, ref m_RealHandle6, pictureBox6, liveThisToolStripMenuItem3, recordThisToolStripMenuItem5,"0");
                                }
                            }
                        }
                    }
                }
            }

            if (!IsActivatedRecordAll)
            {
                IsActivatedRecordAll=EnableRecordButton(IsActivatedRecordAll);
            }
            else
            {
                IsActivatedRecordAll = EnableRecordButton(IsActivatedRecordAll);
            }
            return true;
        }

        //private void previewAll()
        //{
        //    previewBtn(userID_1, ref m_RealHandle1, pictureBox1, liveViewToolStripMenuItem, "0");
        //    previewBtn(userID_2, ref m_RealHandle2, pictureBox2, liveViewToolStripMenuItem1, "0");
        //    previewBtn(userID_3, ref m_RealHandle3, pictureBox3, liveThisToolStripMenuItem, "0");
        //    previewBtn(userID_4, ref m_RealHandle4, pictureBox4, liveThisToolStripMenuItem1, "0");
        //    previewBtn(userID_5, ref m_RealHandle5, pictureBox5, liveThisToolStripMenuItem2, "0");
        //    previewBtn(userID_6, ref m_RealHandle6, pictureBox6, liveThisToolStripMenuItem3, "0");
        //}

        private void viewAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LiveAll();
        }

        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LiveAll();
        }

        private void liveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LiveAll();
        }

        private void liveAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LiveAll();
        }

        private void liveAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LiveAll();
        }

        private bool LoginAll()
        {
            if (getCamNums(CamNumSection) >= 1)
            {
                
                loginButton(ref dev1, ref userID_1, ref m_RealHandle1, loginToolStripMenuItem1, liveViewToolStripMenuItem);
                if (getCamNums(CamNumSection) >= 2)
                {
                    loginButton(ref dev2, ref userID_2, ref m_RealHandle2, loginToolStripMenuItem, liveViewToolStripMenuItem1);
                    if (getCamNums(CamNumSection) >= 3)
                    {
                        loginButton(ref dev3, ref userID_3, ref m_RealHandle3, loginToolStripMenuItem2, liveThisToolStripMenuItem);
                        if (getCamNums(CamNumSection) >= 4)
                        {
                            loginButton(ref dev4, ref userID_4, ref m_RealHandle4, loginToolStripMenuItem3, liveThisToolStripMenuItem1);
                            if (getCamNums(CamNumSection) >= 5)
                            {
                                loginButton(ref dev5, ref userID_5, ref m_RealHandle5, loginToolStripMenuItem4, liveThisToolStripMenuItem2);
                                if (getCamNums(CamNumSection) >= 6)
                                {
                                    loginButton(ref dev6, ref userID_6, ref m_RealHandle6, loginToolStripMenuItem5, liveThisToolStripMenuItem3);
                                }
                            }
                        }
                    }
                }
            }

            if (!IsLiveAllButtonEnabled)
            {
                IsLiveAllButtonEnabled = IfactivateLiveAll(IsLiveAllButtonEnabled);
            }
            else
            {
                IsLiveAllButtonEnabled = IfactivateLiveAll(IsLiveAllButtonEnabled);
            }
            return true;
        }

        private bool EnableRecordButton(bool IsRecordButtonEnabled)
        {
            if (!IsRecordButtonEnabled)
            {
                recordAllToolStripMenuItem.Enabled = true;
                recordAllToolStripMenuItem1.Enabled = true;
                recordThisToolStripMenuItem2.Enabled = true;
                recordAllToolStripMenuItem2.Enabled = true;
                recordAllToolStripMenuItem3.Enabled = true;
                recordAllToolStripMenuItem4.Enabled = true;
                return true;
            }
            else
            {
                recordAllToolStripMenuItem.Enabled = false;
                recordAllToolStripMenuItem1.Enabled = false;
                recordThisToolStripMenuItem2.Enabled = false;
                recordAllToolStripMenuItem2.Enabled = false;
                recordAllToolStripMenuItem3.Enabled = false;
                recordAllToolStripMenuItem4.Enabled = false;
                return false;
            }
        }
        private void LoginAllbutton(ref DevInfo dev, ref Int32 userID, ToolStripMenuItem toolStripMenuItem, ToolStripMenuItem ifActivateLive)
        {
            string DVRIPAddress = dev.getIP(); //设备IP地址或者域名
            Int16 DVRPortNumber = Int16.Parse(dev.getPort());//设备服务端口号
            string DVRUserName = dev.getUserID();//设备登录用户名
            string DVRPassword = dev.getPassword();//设备登录密码

            CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

            //登录设备 Login the device
            userID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
            if (userID < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                MessageBox.Show(str);

                return;
            }
            else
            {

                //登录成功
                string tmp = dev.getIP() + "login success!";
                toolStripMenuItem.Text = "Logout";
                if (!IsActivatedLiveAll)
                {
                    IsActivatedLiveAll = IfactivateLiveAll(IsActivatedLiveAll);

                }

                ArgsEvent(tmp);
                ifActivateLive.Enabled = true;
                return;
            }
        }
        private void loginAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginAll();
        }

        private void loginAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LoginAll();
        }

        private void loginAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LoginAll();
        }

        private void loginAllToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            LoginAll();
        }

        private void loginAllToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            LoginAll();
        }

        private void loginAllToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            LoginAll();
        }

        private bool IfactivateLiveAll(bool isactivatedLiveAll)
        {
            if (!isactivatedLiveAll)
            {
                viewAllToolStripMenuItem.Enabled = true;
                viewAllToolStripMenuItem1.Enabled = true;
                liveAllToolStripMenuItem.Enabled = true;
                liveAllToolStripMenuItem1.Enabled = true;
                liveAllToolStripMenuItem2.Enabled = true;
                liveAllToolStripMenuItem3.Enabled = true;
                return true;
            }
            else
            {
                viewAllToolStripMenuItem.Enabled = false;
                viewAllToolStripMenuItem1.Enabled = false;
                liveAllToolStripMenuItem.Enabled = false;
                liveAllToolStripMenuItem1.Enabled = false;
                liveAllToolStripMenuItem2.Enabled = false;
                liveAllToolStripMenuItem3.Enabled = false;
                return false;
            }
        }
    }
    
    public class DevInfo
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string StreamType { get; set; }
        public string getIP()
        {
            return IP;
        }

        public string getPort()
        {
            return Port;
        }

        public string getUserID()
        {
            return UserID;
        }

        public string getPassword()
        {
            return Password;
        }

        public string getStreamType()
        {
            return StreamType;
        }
    }

    public class InitCam
    {
        public DevInfo DevInfo;
        public Int32 UserId;
        public PictureBox PictureBox { get; set; }
        public int m_lRealHandle;
    }

    public class CamConf
    {
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public string UserID { get; set; }
        public string Pwd { get; set; }
    }
}
