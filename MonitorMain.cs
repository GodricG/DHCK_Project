using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DHCKProject_V1;
namespace Server_V1
{
    public partial class MonitorMain : Form
    {
        public delegate void ArgsMonitorToMain(string args);
        public static ArgsMonitorToMain ArgsEvent;
        private string iniPath = Path.Combine(Environment.CurrentDirectory,
            "CameraConfigurations\\camconfig.ini");
        private bool m_bInitSDK = false;
        private string ichannel = 1.ToString();
        uint iLastErr = CHCNetSDK.NET_DVR_GetLastError();
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
        InitCam initCam1 = new InitCam();
        InitCam initCam2 = new InitCam();
        InitCam initCam3 = new InitCam();
        InitCam initCam4 = new InitCam();
        InitCam initCam5 = new InitCam();
        InitCam initCam6 = new InitCam();

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
            cam1 = getCamConf(iniPath, "region1_cam1");
            cam2 = getCamConf(iniPath, "region1_cam2");
            cam3 = getCamConf(iniPath, "region1_cam3");
            cam4 = getCamConf(iniPath, "region1_cam4");
            cam5 = getCamConf(iniPath, "region1_cam5");
            cam6 = getCamConf(iniPath, "region1_cam6");
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
            SetValueForInitCam(ref initCam1, ref userID_1, dev1, pictureBox1,ref IsSetted1);
            SetValueForInitCam(ref initCam2, ref userID_2, dev2, pictureBox2,ref IsSetted2);
            SetValueForInitCam(ref initCam3, ref userID_3, dev3, pictureBox3,ref IsSetted3);
            SetValueForInitCam(ref initCam4, ref userID_4, dev4, pictureBox4,ref IsSetted4);
            SetValueForInitCam(ref initCam5, ref userID_5, dev5, pictureBox5,ref IsSetted5);
            SetValueForInitCam(ref initCam6, ref userID_6, dev6, pictureBox6,ref IsSetted6);
            Thread cam1Thread = new Thread(initCam);
            Thread cam2Thread = new Thread(initCam);
            Thread cam3Thread = new Thread(initCam);
            Thread cam4Thread = new Thread(initCam);
            Thread cam5Thread = new Thread(initCam);
            Thread cam6Thread = new Thread(initCam);
            cam1Thread.Start(initCam1);
            cam2Thread.Start(initCam2);
            cam3Thread.Start(initCam3);
            cam4Thread.Start(initCam4);
            cam5Thread.Start(initCam5);
            cam6Thread.Start(initCam6);
        }
        
        private void SetValueForInitCam(ref InitCam initCam,ref Int32 userId,DevInfo devInfo,PictureBox pictureBox,ref bool isSetted)
        {
            initCam.DevInfo = new DevInfo();
            if (!isSetted)
            {
                initCam.UserId = userId;
                initCam.m_lRealHandle = -1;
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
                loginButton(ref camInfo.DevInfo, ref camInfo.UserId, camInfo.m_lRealHandle);
                Thread.Sleep(2000);
                previewBtn(camInfo.UserId, ref camInfo.m_lRealHandle, camInfo.PictureBox, camInfo.DevInfo.StreamType);
            }
            else
            {
                MessageBox.Show("Configure caminfo.ini file.");
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
            ArgsEvent("Size autochanged.");
        }

        private int getCamNums()
        {
            string filepath = Path.Combine(Environment.CurrentDirectory, "CameraConfigurations\\camconfig.ini");
            return Int32.Parse(IniFileDll.IniFileDll.Read("camNums", "number", null, filepath));
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
        private void loginButton(ref DevInfo dev, ref Int32 userID, Int32 realHandler)
        {
            if (userID > 0)
            {
                return;
            }
            if (checkInfo(ref dev))
            {
                string tmp = "Please input IP, Port, User name, Password and StreamType!";
                MessageBox.Show(tmp);
                return;
            }
            if (userID < 0)
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
                    
                    ArgsEvent(tmp);

                }

            }
            
        }
        private void previewBtn(Int32 userID, ref Int32 realHandler, PictureBox Wnd, string streamType)
        {
            if (realHandler > 0)
            {
                return;
            }

            if (realHandler < 0)
            {
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

                if (realHandler < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    MessageBox.Show(str);
                    return;
                }
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
