using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CCWin;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace VideoListShow
{
    public partial class Main : Skin_Mac
    {
        List<Video> allVideos = new List<Video>();
        List<Button> allButtons;
        float dpiX, dpiY;
        string ffmpegPath = Application.StartupPath + "\\ffmpeg\\ffmpeg.exe";
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            init();
            LookFile();
            CreateThumb();
            CreateImageButtons();
        }

        private void init()
        {
            Graphics graphics = this.CreateGraphics();
            dpiX = (graphics.DpiX)/96;
            dpiY = (graphics.DpiY)/96;
            //设定按字体来缩放控件
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //设定字体大小为12px     
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Size = new Size((int)(1366*dpiX),(int)(768*dpiY));
            label1.Location = new Point((this.Width / 2) - (label1.Text.Length * 35), (int)(83*dpiY));
            Images_pnl.Location = new Point((int)(100*dpiX),(int)(200*dpiY));
            Images_pnl.Size = new Size((int)(1366 * dpiX), (int)(768 * dpiY)); ;
        }


        //检索视频文件
        public void LookFile()
        {
            string startPath = "Videos";
            string pathname = Application.StartupPath + "\\" + startPath + "\\";
            if (pathname.Trim().Length == 0)//判断文件名不为空
            {
                return;
            }
            DirectoryInfo _dir = new DirectoryInfo(pathname);
            FileInfo[] fileInfos;
            try
            {
                fileInfos = _dir.GetFiles();
                foreach (FileInfo _file in fileInfos)
                {
                    if (Path.GetExtension(_file.Name).Equals(".mp4") ||
                        Path.GetExtension(_file.Name).Equals(".avi") ||
                        Path.GetExtension(_file.Name).Equals(".wmv")||
                        Path.GetExtension(_file.Name).Equals(".MP4") ||
                        Path.GetExtension(_file.Name).Equals(".AVI") ||
                        Path.GetExtension(_file.Name).Equals(".WMV"))
                    {
                        Video _v = new Video(_file);
                        allVideos.Add(_v);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            }

        //使用ffmpeg获取视频缩略图
        private void CreateThumb()
        {
            Process _process = new Process();
            foreach (Video _v in allVideos)
            {
                if (File.Exists(Application.StartupPath + "\\Thumbs\\" + _v.fileInfo.Name.Split('.')[0] + ".jpg"))
                {
                    _v.thumbPath = Application.StartupPath + "\\Thumbs\\" + _v.fileInfo.Name.Split('.')[0] + ".jpg";
                    continue;
                }
                string outPut = "";
                string error = "";
                string command = string.Format("\"{0}\" -i \"{1}\" -ss {2} -vframes 1 -r 1 -ac 1 -ab 2 -s {3}*{4} -f image2 \"{5}\"", ffmpegPath, _v.fileInfo.FullName, 15, 640, 360, Application.StartupPath + "\\Thumbs\\" + _v.fileInfo.Name.Split('.')[0] + ".jpg");
                ExecuteFFMpegCommand(_process, command, out outPut, out error);
                _v.thumbPath = Application.StartupPath + "\\Thumbs\\" + _v.fileInfo.Name.Split('.')[0] + ".jpg";
            }

        }

        //创建按钮和标签
        private void CreateImageButtons()
        {
            allButtons = new List<Button>();
            int count = 0;
            foreach (Video _v  in allVideos)
            {
                Button btn = new Button();
                btn.Parent = Images_pnl;
                //btn.Size = new Size((int)(240*dpiX), (int)(135 * dpiY));
                btn.Size = new Size((int)(270 * dpiX), (int)(151 * dpiY));
                //btn.Location = new Point((int)((100 + 285 * count)*dpiX), (int)(200*dpiY));
                btn.Location = new Point((int)(285 * (count%4)*dpiX), (int)((count/4)*200*dpiY));
                //btn.BackColor = Color.Blue;
                btn.Name = _v.fileInfo.Name.Split('.')[0];
                //btn.FlatAppearance.BorderColor = Color.White;
                btn.BackgroundImage = ReadImageFile(_v.thumbPath);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.Click += new EventHandler(btn_Click);
                this.Controls.Add(btn);
                allButtons.Add(btn);
                Label lbl = new Label();
                lbl.Parent = Images_pnl;
                lbl.Font = new Font("微软雅黑", 11.0f, FontStyle.Regular);
                //lbl.Location = new Point((int)((100 + 300 * count) * dpiX), (int)((200+150) * dpiY));
                lbl.Location = new Point((int)(285 * (count % 4) * dpiX), (int)((((count / 4) * 200)+152) * dpiY));
                //lbl.AutoSize = true;
                lbl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#505050");
                lbl.Text = _v.fileInfo.Name.Split('.')[0];
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Size = new Size((int)(270 * dpiX), (int)(30 * dpiY));
                Images_pnl.Controls.Add(btn);
                Images_pnl.Controls.Add(lbl);
                count++;
            }
        }

        public static Bitmap ReadImageFile(string path,int retryCount = 0)
        {
            Bitmap bit;
            Image result;
            retryCount++;
            if (path.Length == 0)
            {
                return null;
            }
            try {
                FileStream fs = File.OpenRead(path); //OpenRead
                int filelength = 0;
                filelength = (int)fs.Length; //获得文件长度 
                Byte[] image = new Byte[filelength]; //建立一个字节数组 
                fs.Read(image, 0, filelength); //按字节流读取 
                result = System.Drawing.Image.FromStream(fs);
                fs.Close();
                bit = new Bitmap(result);
                return bit;
            }
            catch(Exception e)
            {
                if(retryCount < 5)
                {
                    System.Threading.Thread.Sleep(1000);
                    return ReadImageFile(path, retryCount);
                }
                else
                {
                    return null;
                }
            }
        }

        //点击按钮
        void btn_Click(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name;
            foreach (Video _v in allVideos)
            {
                if (_v.fileInfo.Name.Split('.')[0].Equals(name))
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = _v.fileInfo.FullName;
                    info.Arguments = "";
                    Process.Start(info);
                }
            }
        }

        private void Images_pnl_ControlAdded(object sender, ControlEventArgs e)
        {
            this.Images_pnl.VerticalScroll.Enabled = true;
            this.Images_pnl.VerticalScroll.Visible = true;
            Images_pnl.AutoScroll = true;
            this.Images_pnl.Scroll += Images_pnl_Scroll;
        }

        private void Images_pnl_Scroll(object sender, ScrollEventArgs e)
        {
            this.Images_pnl.VerticalScroll.Value = e.NewValue;
        }

        //执行ffmpeg指令
        private void ExecuteFFMpegCommand(Process pc,string command,out string output, out string error)
        {
            try
            {
                //创建进程
                pc.StartInfo.FileName = command;
                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.RedirectStandardOutput = true;
                pc.StartInfo.RedirectStandardError = true;
                pc.StartInfo.CreateNoWindow = true;
                pc.Start();
                //准备读出输出流及错误流
                string outputData = string.Empty;
                string errorData = string.Empty;
                pc.BeginOutputReadLine();
                pc.BeginErrorReadLine();
                pc.OutputDataReceived += (ss, ee) =>
                {
                    outputData += ee.Data;
                };
                pc.ErrorDataReceived += (ss, ee) =>
                {
                    errorData += ee.Data;
                };
                //等待执行结束后退出
                pc.WaitForExit();
                //关闭进程
                pc.Close();
                //返回结果
                output = outputData;
                error = errorData;
            }
            catch (Exception)
            {
                output = null;
                error = null;
            }
        }
    }
}
