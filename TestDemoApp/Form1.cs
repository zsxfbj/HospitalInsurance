using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalInsurance.BLL;
using HospitalInsurance.Model.DTO;
using Newtonsoft.Json;

namespace HospitalInsurance.TestDemoApp
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient Client = new HttpClient();

        private static readonly String appDoc = Environment.CurrentDirectory;

        private const string UrlFileName = "url.txt";

        private const string GetPersonReqFileName = "GetPersonReq.json";

        private const string GetPersonRespFileName = "GetPersonResp.json";

        private const string LogPageReqFileName = "LogPageReq.json";

        

        public Form1()
        {
            InitializeComponent();

           

            string fileName= appDoc + "\\" + UrlFileName;
            if(File.Exists (fileName) )
            {
                using(StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    tbGatewayUrl.Text = sr.ReadToEnd ().Trim();
                }
            }

            fileName = appDoc + "\\" + GetPersonReqFileName;
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    tbGetPersonReq.Text = sr.ReadToEnd().Trim();
                }
            }

            fileName = appDoc + "\\" + GetPersonRespFileName;
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    tbGetPersonResp.Text = sr.ReadToEnd().Trim();
                }
            }

            fileName = appDoc + "\\" + LogPageReqFileName;
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    tbLogPageReq.Text = sr.ReadToEnd().Trim();
                }
            }
        }


        private void SaveGatewayUrl()
        {
            string fileName = appDoc + "\\" + UrlFileName;
            FileInfo fileInfo = new FileInfo(fileName);
            if(fileInfo.Exists )
            {
                fileInfo.Delete();
            }
           
            using (StreamWriter sw = fileInfo.CreateText())
            {
                sw.WriteLine(tbGatewayUrl.Text.Trim());
            }          
        }

        private void SaveGetPersonReq()
        {
            string fileName = appDoc + "\\" + GetPersonReqFileName;
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            using (StreamWriter sw = fileInfo.CreateText())
            {
                sw.WriteLine(tbGetPersonReq.Text.Trim());
            }
        }

        private void SaveGetPersonResp()
        {
            string fileName = appDoc + "\\" + GetPersonRespFileName;
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            using (StreamWriter sw = fileInfo.CreateText())
            {
                sw.WriteLine(tbGetPersonResp.Text.Trim());
            }
        }

        private void SaveLogPageReq()
        {
            string fileName = appDoc + "\\" + LogPageReqFileName;
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }

            using (StreamWriter sw = fileInfo.CreateText())
            {
                sw.WriteLine(tbLogPageReq.Text.Trim());
            }
        }

        private void btnSubmitGetPerson_Click(object sender, EventArgs e)
        {
            SaveGatewayUrl();
            SaveGetPersonReq();
            try
            {
                tbGetPersonResp.Text = JsonConvert.SerializeObject(BHISInterface.GetInstance().GetPersonInfo(JsonConvert.DeserializeObject<GetPersonInfoReqDTO>(tbGetPersonReq.Text)));
                
                //HttpContent httpContent = new StringContent(tbGetPersonReq.Text, Encoding.UTF8, "application/json");
                //string url = tbGatewayUrl.Text.Trim() + "/api/net-yb/get-person";
                //HttpResponseMessage response = Client.PostAsync(url, httpContent).Result;
                //tbGetPersonResp.AppendText("请求地址：" + url);
                //tbGetPersonResp.AppendText(Environment.NewLine);
                //tbGetPersonResp.AppendText(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                tbGetPersonResp.Text = ex.Message;
               
            }

                SaveGetPersonResp();
        }

        private void btnLogPageReq_Click(object sender, EventArgs e)
        {
            SaveGatewayUrl();
            SaveLogPageReq();
            tbLogPageResp.Text = "";
            try
            {              
                HttpContent httpContent = new StringContent(tbLogPageReq.Text, Encoding.UTF8, "application/json");
                string url = tbGatewayUrl.Text.Trim() + "/api/request-log/page";
                HttpResponseMessage response = Client.PostAsync(url, httpContent).Result;
                tbLogPageResp.AppendText("请求地址：" + url);
                tbLogPageResp.AppendText(Environment.NewLine);
                tbLogPageResp.AppendText(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                tbLogPageResp.Text = ex.Message;

            }
        }

       
    }
}
