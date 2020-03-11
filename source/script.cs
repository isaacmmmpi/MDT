using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wineforever;
using static Wineforever.Coordinate.client;
using C3D;
namespace Mocap_Data_Transformation
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<string>> MOCAP_DATA = new Dictionary<string, List<string>>();
        List<List<Point3D>> MOCAP_FRAMES = new List<List<Point3D>>();
        double SCALE = 30;
        int POINTS_COUNT = 17;
        int CURRENT_FRAME = 0;
        string Debug = "";
        double wheel = 0;
        //初始化
        public void Initialization()
        {
            var winSize = new float[] { 0,0,RenderWindow.Width,RenderWindow.Height};
            Wineforever.Coordinate.client.SetDistance(1.35);
            Wineforever.Coordinate.client.SetBackground(Color.LightYellow);
            Wineforever.Coordinate.client.Initialization("3D", winSize, true, false);
            Wineforever.Coordinate.client.SetOriginalPoint(RenderWindow.Width/2, RenderWindow.Height/2);
            //其他
            CheckBox.Parent = RenderWindow;
        }
        //渲染
        public void Render()
        {
            if (MOCAP_FRAMES.Count > 0)
            {
                for (int i = 0; i < POINTS_COUNT; i++)
                {
                    Wineforever.Coordinate.client.DrawPoint(MOCAP_FRAMES[CURRENT_FRAME][i].X, MOCAP_FRAMES[CURRENT_FRAME][i].Y, MOCAP_FRAMES[CURRENT_FRAME][i].Z, Brushes.Yellow);
                    Wineforever.Coordinate.client.DrawString(MOCAP_FRAMES[CURRENT_FRAME][i], i.ToString(), Brushes.Black);
                }
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][0], MOCAP_FRAMES[CURRENT_FRAME][1], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][0], MOCAP_FRAMES[CURRENT_FRAME][4], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][1], MOCAP_FRAMES[CURRENT_FRAME][2], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][4], MOCAP_FRAMES[CURRENT_FRAME][5], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][2], MOCAP_FRAMES[CURRENT_FRAME][3], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][5], MOCAP_FRAMES[CURRENT_FRAME][6], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][0], MOCAP_FRAMES[CURRENT_FRAME][7], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][7], MOCAP_FRAMES[CURRENT_FRAME][8], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][9], MOCAP_FRAMES[CURRENT_FRAME][10], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][8], MOCAP_FRAMES[CURRENT_FRAME][9], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][8], MOCAP_FRAMES[CURRENT_FRAME][10], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][8], MOCAP_FRAMES[CURRENT_FRAME][11], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][8], MOCAP_FRAMES[CURRENT_FRAME][14], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][11], MOCAP_FRAMES[CURRENT_FRAME][12], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][14], MOCAP_FRAMES[CURRENT_FRAME][15], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][12], MOCAP_FRAMES[CURRENT_FRAME][13], Brushes.Red);
                Wineforever.Coordinate.client.DrawLine(MOCAP_FRAMES[CURRENT_FRAME][15], MOCAP_FRAMES[CURRENT_FRAME][16], Brushes.Red);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            //重绘
            var winSize = new float[] { 0, 0, RenderWindow.Width, RenderWindow.Height };
            Wineforever.Coordinate.client.Initialization("3D", winSize, true, CheckBox.Checked
                );
            //刷新帧
            if (CURRENT_FRAME >= MOCAP_FRAMES.Count - 1)
            {
                CURRENT_FRAME = 0;
            }
            else
                CURRENT_FRAME++;
            Render();
            RenderWindow.Image = Wineforever.Coordinate.client.Show();
            //其他
            PrintWindow.Text = Debug;
            PrintWindow.Select(this.PrintWindow.TextLength, 0);//光标定位到文本最后
            PrintWindow.ScrollToCaret();//滚动到光标处
            try
            {
                SCALE = double.Parse(inputbox_scale.Text);
                POINTS_COUNT = int.Parse(inputbox_pointsCount.Text);
            }
            catch (Exception)
            {
                SCALE = 30;
                POINTS_COUNT = 17;
            }        }
        //视角移动
        Point Mouse_Point = new Point();
        private void RenderWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                RotationTimer.Enabled = false;
            if (e.Button == MouseButtons.Right)
                MoveTimer.Enabled = false;
        }
        private void RotationTimer_Tick(object sender, EventArgs e)
        {
            int x = RenderWindow.PointToClient(Control.MousePosition).X;
            int y = RenderWindow.PointToClient(Control.MousePosition).Y;
            int b = Mouse_Point.Y - y;
            int c = Mouse_Point.X - x;
            Wineforever.Coordinate.client.SetRotation(180, b, -c);
        }
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            int x = RenderWindow.PointToClient(Control.MousePosition).X;
            int y = RenderWindow.PointToClient(Control.MousePosition).Y;
            Wineforever.Coordinate.client.SetOriginalPoint(x, y);
        }
        private void RenderWindow_MouseDown(object sender, MouseEventArgs e)
        {
            //记录按下时鼠标坐标
            Mouse_Point.X = RenderWindow.PointToClient(Control.MousePosition).X;
            Mouse_Point.Y = RenderWindow.PointToClient(Control.MousePosition).Y;
            if (e.Button == MouseButtons.Left)
            {
                RotationTimer.Enabled = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                MoveTimer.Enabled = true;
            }
        }
        private void RenderWindow_MouseWheel(object sender, MouseEventArgs e)
        {
            wheel += e.Delta * 0.1;
            Wineforever.Coordinate.client.SetDistance(wheel / 48);
        }
        //文件操作
        private void SaveToWF(string fileName)
        {
            var Log = "正在复制文件...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            File.Copy(fileName, System.AppDomain.CurrentDomain.BaseDirectory + "NumpyData.npy", true);
            #region BarCode
            string bar_code = @"def load(filepath):
    f = open(filepath,encoding='utf-8')
    data = f.read()
    return data

def save(data,filepath):
    with open(filepath,'w', encoding='utf-8') as f:
        f.write(data)

def load_from_sheet(filepath):
    data = load(filepath)
    FLAG = [False,False]
    res = ['','']
    Data = {}
    for i in range(len(data)-1):
        if data[i-1]=='［':
            FLAG[0] = True
            res[0] = ''
        elif data[i-1] == '　' and data[i] != '　' and data[i] != '［':
            FLAG[1] = True
            res[1] = ''
        if FLAG[0]:
            res[0] += data[i]
        elif FLAG[1]:
            res[1] += data[i]
        if data[i+1] == '］':
            FLAG[0] = False
            Data[res[0]] = []
        elif data[i+1] == '　' and FLAG[1] and data[i] != '］':
            FLAG[1] = False
            Data[res[0]].append(res[1])
    return Data

def save_to_sheet(data,filepath):
    Data = []
    for key, value in data.items():
        Data.append('［' + key + '］')
        for i in range(len(value)):
            if value[i] == '':
                Data.append('　' + ' ' + '　')
            else:
                Data.append('　' + value[i] + '　')
    Data = ''.join(Data)
    save(Data,filepath)";
            #endregion
            #region SaveCode
            string save_code = @"
import numpy as np
from Wineforever_PyString import save_to_sheet

print(""Iniliazation..."")

o= {}
o['Time Series'] = []
o['Node'] = []
o['Dim'] = []
o['Point'] = []

print(""Load..."")

i = np.load('./NumpyData.npy')

print(""Output..."")

print(""Time Series Length:%s"" %  i.shape[0])
print(""Nodes Length:%s"" %  i.shape[1])
print(""Dim Num:%s"" %  i.shape[2])
for x in range(0,i.shape[0]):
  for y in range(0,i.shape[1]):
    for z in range(0,i.shape[2]):
      o['Point'].append(str(i[x,y,z]))
      o['Dim'] .append(str(z))
      o['Node'] .append(str(y))
      o['Time Series'] .append(str(x))

print(""Save..."")

filepath = 'boneData.wf'
save_to_sheet(o,filepath)

print(""Done!"")";
            #endregion
            Log = "正在写入Python文件...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            Wineforever.String.client.Save(bar_code, System.AppDomain.CurrentDomain.BaseDirectory + "Wineforever_PyString.py");
            Wineforever.String.client.Save(save_code, System.AppDomain.CurrentDomain.BaseDirectory + "SaveToWF.py");
            Log = "正在执行格式转换...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            try
            {
                System.Diagnostics.Process exep = new System.Diagnostics.Process();
                exep.StartInfo.UseShellExecute = true;
                exep.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                exep.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory + "SaveToWF.py";
                exep.Start();
                exep.WaitForExit();
            }
            catch (Exception)
            {
                Debug += "未检测到Python，请先配置环境！";
            }
            Log = "正在导入文件...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            MOCAP_DATA = Wineforever.String.client.LoadFromSheet(System.AppDomain.CurrentDomain.BaseDirectory + "boneData.wf");
            Log = "导入成功!";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
        }
        private void Input_Btn_Click(object sender, EventArgs e)
        {
            MOCAP_DATA.Clear();
            openFileDialog.ShowDialog();
            var fileName = openFileDialog.FileName;
            if (fileName != "")
            {
                //将 .NPY 文件转换成 .WF
                if (Path.GetExtension(fileName).ToLower() == ".npy")
                {
                    SaveToWF(fileName);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".wf")
                {
                    MOCAP_DATA = Wineforever.String.client.LoadFromSheet(fileName);
                }
                MOCAPDATA_LOAD();
                Create_Btn.Enabled = true;
                inputbox_scale.Enabled = true;
                inputbox_pointsCount.Enabled = true;
            }
        }
        private void MOCAPDATA_LOAD()
        {
            MOCAP_FRAMES.Clear();
            //读取数据
            var res_Count = int.Parse(MOCAP_DATA["Time Series"].Last()) + 1;
            var Log = "正在读取数据...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            for (int n = 0; n < res_Count; n++)
            {
                var quary_list = new List<KeyValuePair<string, string>>() {//设置查询条件
                new KeyValuePair<string, string>("Time Series", n.ToString()),
                 };
                var quary_res = Wineforever.String.client.Query(MOCAP_DATA, quary_list);//调用
                var points = new List<Point3D>();
                for (int i = 0; i < POINTS_COUNT; i++)//节点数
                {
                    points.Add(new Point3D(double.Parse(quary_res[i * 3 + 0]["Point"]) * SCALE, double.Parse(quary_res[i * 3 + 1]["Point"]) * SCALE, double.Parse(quary_res[i * 3 + 2]["Point"]) * SCALE));
                }
                MOCAP_FRAMES.Add(points);
            }
            CURRENT_FRAME = 0;//初始化动画
            Log = "读取成功!";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
        }
        private void Create_Btn_Click(object sender, EventArgs e)
        {
            var Log = "正在创建C3D对象...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            C3DFile file = C3DFile.Create();//创建C3D对象
            Log = "正在录入动作捕捉点数据...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            for (int n = 0; n < MOCAP_FRAMES.Count; n++)//时间序列
            {
                C3DPoint3DData[] c3DPoints = new C3DPoint3DData[POINTS_COUNT];
                var Points = MOCAP_FRAMES[n];
                for (int i = 0; i < POINTS_COUNT; i++)//节点数
                {
                    C3DPoint3DData point3DData = new C3DPoint3DData() { X = (float)Points[i].X * (float)SCALE, Y = (float)Points[i].Y * (float)SCALE, Z = (float)Points[i].Z * (float)SCALE, Residual = 0, CameraMask = 0 };
                    c3DPoints[i] = point3DData;
                }
                C3DFrame c3DFrame = new C3DFrame(c3DPoints);
                file.AllFrames.Add(c3DFrame);
            }
            Log = "正在设置头信息...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            file.Header.PointCount = (ushort)POINTS_COUNT;
            file.Header.FirstFrameIndex = 1;
            file.Header.LastFrameIndex = (ushort)(MOCAP_FRAMES.Count - 1);
            file.Header.IsSupport4CharsLabel = false;
            file.Parameters.SetGroup(1, "POINT", "");
            file.Parameters[1].Add("FRAMES", "").SetData<Int16>((short)(MOCAP_FRAMES.Count - 1));
            file.Parameters[1].Add("USED", "").SetData<Int16>((short)POINTS_COUNT);
            Log = "正在保存到C3D文件...";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
            file.SaveTo(System.AppDomain.CurrentDomain.BaseDirectory + "output.c3d");
            Log = "保存成功!已在程序根目录下生成'output.c3d'文件!";
            Debug += DateTime.Now + ":" + Log + "\r\n\r\n";
        }
    }
}
