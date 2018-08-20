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

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using LibSVMsharp;
using LibSVMsharp.Helpers;

namespace use_SVM
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        

        private void btnSVM_Click(object sender, EventArgs e)
        {
            SVMProblem problem = SVMProblemHelper.Load(@"train.txt");        //訓練樣本
            SVMProblem testProblem = SVMProblemHelper.Load(@"train.txt");    //測試樣本
            
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(testProblem.Length, 128); //建立圖片
            img.SetValue(255); //設為全白
            double res = 0.0;
            //SVM的模板
            SVMParameter parameter = new SVMParameter();
            parameter.Type = SVMType.C_SVC;
            parameter.Kernel = SVMKernelType.LINEAR;
            //SVM模型
            SVMModel model = SVM.Train(problem, parameter);

            double[] target = new double[testProblem.Length];
            //上色
            for (int i = img.Height-1, k = 0; i > 0; --i)
            {
                for (int j = 0; j < img.Width; ++j)
                {
                    if (k >= testProblem.Length)
                        break;
                    //SVM預測
                    res = SVM.Predict(model, testProblem.X[k]);
                    target[k] = res;
                    k++;

                    if (res == 1)
                    {
                        for (int count = 100; count > 0; count--)
                            img[i - count, j] = new Bgr(255, 0, 0);
                    }
                    else
                    if (res == 2)
                    {
                        for (int count = 100; count > 0; count--)
                            img[i - count, j] = new Bgr(0, 255, 0);
                    }
                    else
                    if (res == 3)
                    {
                        for (int count = 100; count > 0; count--)
                            img[i - count, j] = new Bgr(0, 0, 255);
                    }
                    else
                    if (res == 4)
                    {
                        for (int count = 100; count > 0; count--)
                            img[i - count, j] = new Bgr(0, 0, 0);
                    }
                    else
                    if (res == 0)
                    {
                        for (int count = 100; count > 0; count--)
                            img[i - count, j] = new Bgr(127, 127, 127);
                    }
                }
            }
            //輸出圖片
            ImageViewer output = new ImageViewer(img, "123");
            output.Show();
            //算出準確率
            double accuracy = SVMHelper.EvaluateClassificationProblem(testProblem, target);
            lblAccuracy.Text = "準確率： " + accuracy;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            //讀取R點Index與病徵
            using (System.IO.StreamReader file = new System.IO.StreamReader("106_ann.txt"))
            {
                string line = "";
                string temp = "";
                double[,] arrDat; //電位陣列
                int[] type; //類型
                int[] samplenum; //設定sample陣列
                int count = 0; //資料數量
                int k = 0; //終止迴圈計數
                int typeCount = 0; //type計數
                int samplenumCount = 0; //sample計數
                Boolean done = true;

                //計算數量
                while ((line = file.ReadLine()) != null)
                {
                    count++;
                }
                count--;//不算第一條

                arrDat = new double[count, 2]; //設定電位陣列
                type = new int[count];  //設定類型陣列
                samplenum = new int[count]; //設定sample陣列

                file.ReadToEnd();  //標頭拉到尾
                file.BaseStream.Seek(0, SeekOrigin.Begin);  //標頭重新回到最開始

                file.ReadLine(); //去掉第一條

                while ((line = file.ReadLine()) != null)
                {
                    //時間
                    line = line.TrimStart();
                    string[] annLine = line.Split(' ');

                    //類型、sample
                    annLine[0] = " ";
                    line = "";
                    while(done)
                    {
                        if(annLine[k] == "N" || annLine[k] == "V" || annLine[k] == "~" || annLine[k] == "+")
                        {
                            if (annLine[k] == "N")    //type N 是1
                                type[typeCount] = 1;
                            else if(annLine[k] == "V") // type V 是 2
                                type[typeCount] = 2;
                            else if(annLine[k] == "A") // type A 是 3
                                type[typeCount] = 3;
                            else if(annLine[k] == "~") // type + 是 4
                                type[typeCount] = 4;
                            else
                                type[typeCount] = 0;
                            //Console.WriteLine(type[typeCount]);
                            typeCount++;
                            done = false;
                        }
                        else
                        {
                            line += annLine[k];
                            k++;
                        }
                    }
                    k = 0;
                    done = true;

                    //取出sample
                    line = line.TrimStart();
                    annLine = line.Split(' ');
                    samplenum[samplenumCount] = Int32.Parse(annLine[0]);
                    //Console.WriteLine(samplenum[samplenumCount]);
                    samplenumCount++;
                }

                file.Close();

                var lines = File.ReadLines("106_dat.txt");

                k = 0;
                for (int i = 0, datCount = 0;;i++)
                {
                    if (i >= samplenumCount)
                        break;
                    datCount = samplenum[i];
                    foreach (var datLine in lines)
                    {

                        if (datCount == 0)
                        {
                            temp = datLine.TrimStart();
                            temp = temp.Replace("  ", " ");
                            string[] datTempLine = temp.Split(' ');
                            //取出左右電位
                            arrDat[k, 0] = double.Parse(datTempLine[1]);
                            arrDat[k, 1] = double.Parse(datTempLine[2]);
                            //Console.WriteLine(arrDat[k, 1]);
                            k++;
                            break;
                        }
                        
                        datCount--;
                    }
                }

                if (File.Exists("train.txt"))
                    File.Delete("train.txt");

                for(int i = 0; i < count; i++)
                {
                    File.AppendAllText("train.txt", type[i] + " 1:" + arrDat[i, 0] + " 2:" + arrDat[i, 1] + "\n");
                }

            }

        }
    }
}
