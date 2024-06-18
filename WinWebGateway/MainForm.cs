using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using HospitalInsurance.BLL;
using HospitalInsurance.Model.Common;
using HospitalInsurance.Model.DTO;
using HospitalInsurance.Model.VO.Api;
using Newtonsoft.Json;

namespace WinWebGateway
{
    public partial class MainForm : Form
    {
        private HttpListener listener;

        public MainForm()
        {
            InitializeComponent();

           startHttpListen();           
            
        }
 

        private void startHttpListen()
        {
            try
            {
                listener =  new HttpListener();
                tbResult.AppendText( "监听状态：正在启动\r\n");
                listener.Prefixes.Add(System.Configuration.ConfigurationManager.AppSettings["listen-url"]);  //添加需要监听的url范围
                listener.Start(); //开始监听端口，接收客户端请求;
                listener.BeginGetContext(ListenerHandle, listener);
                tbResult.AppendText("监听状态：已启动\r\n");
            }
            catch
            {
                tbResult.AppendText("监听状态：监听失败\r\n");
            }
        }


        /// <summary>
        /// 监听回调函数
        /// </summary>
        private void ListenerHandle(IAsyncResult result)
        {
            try
            {
                if (listener.IsListening)
                {
                    listener.BeginGetContext(ListenerHandle, result);
                    HttpListenerContext context = listener.EndGetContext(result);
                    HttpListenerResponse response = context.Response;
                    HttpListenerRequest request = context.Request;

                    context.Response.AppendHeader("Access-Control-Allow-Origin", "*");//后台跨域请求，通常设置为配置文件
                    context.Response.AppendHeader("Access-Control-Allow-Credentials", "true"); //后台跨域请求
                    response.StatusCode = 200;
                    response.ContentType = "application/json;charset=UTF-8";
                    context.Response.AddHeader("Content-type", "text/plain");//添加响应头信息
                    context.Response.AddHeader("Content-type", "application/x-www-form-urlencoded");//添加响应头信息

                    response.ContentEncoding = Encoding.UTF8;
                    tbResult.AppendText("监听到Web请求地址：" + request.Url.PathAndQuery);
                    tbResult.AppendText(System.Environment.NewLine);
                    //解析Request请求                    
                    string responseString ="";

                    switch (request.HttpMethod)
                    {
                        case "POST":
                            {
                               
                                Stream stream = request.InputStream;
                                StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                                string content = reader.ReadToEnd();

                                // json字符串
                                if (request.Url.PathAndQuery.StartsWith("/api/webyb/divide"))
                                {
                                    tbResult.AppendText("费用分解请求：" + content);
                                    tbResult.AppendText(System.Environment.NewLine);
                                    ApiResult<string> apiResult = new ApiResult<string>();
                                    apiResult.Code = HospitalInsurance.Enums.ResultCodeEnum.Success;
                                    apiResult.Data = BHISInterface.GetInstance().DivideFee(JsonConvert.DeserializeObject<DivideReqDTO>(content));
                                    responseString = JsonConvert.SerializeObject(apiResult);
                                    tbResult.AppendText("费用分解返回内容：" + responseString);
                                    tbResult.AppendText(Environment.NewLine);
                                }
                                else if (request.Url.AbsolutePath.StartsWith("/api/webyb/refundment")) 
                                {
                                    tbResult.AppendText("退费请求：" + content);
                                    tbResult.AppendText(System.Environment.NewLine);
                                    ApiResult<string> apiResult = new ApiResult<string>();
                                    apiResult.Code = HospitalInsurance.Enums.ResultCodeEnum.Success;
                                    apiResult.Data = BHISInterface.GetInstance().GetRefundTrade(JsonConvert.DeserializeObject<RefundmentReqDTO>(content));
                                    responseString = JsonConvert.SerializeObject(apiResult);
                                    tbResult.AppendText("退费返回内容：" + responseString);
                                    tbResult.AppendText(Environment.NewLine);
                                }
                            }
                            break;
                        case "GET":
                            {

                                string url = request.Url.PathAndQuery;
                                if (url.StartsWith("/api/webyb/trade-detail"))
                                {
                                    string requestId = request.Url.PathAndQuery.Substring(url.LastIndexOf("/") + 1);
                                    if (url.Contains("?"))
                                    {
                                        requestId = requestId.Substring(0, requestId.IndexOf("?"));
                                    }
                                    tbResult.AppendText("查询请求处理结果：" + requestId);
                                    tbResult.AppendText(System.Environment.NewLine);
                                    ApiResult<TradeDetailVO> apiResult = new ApiResult<TradeDetailVO>();
                                    apiResult.Code = HospitalInsurance.Enums.ResultCodeEnum.Success;
                                    apiResult.Data = BHISInterface.GetInstance().GetTradeDetail(requestId);
                                    responseString = JsonConvert.SerializeObject(apiResult);
                                    tbResult.AppendText("查询请求处理返回内容：" + responseString);
                                    tbResult.AppendText(Environment.NewLine);
                                }

                            }
                            break;
                    }
                   
                
                    
                    byte[] buffers = System.Text.Encoding.UTF8.GetBytes(responseString);

                    response.ContentLength64 = buffers.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffers, 0, buffers.Length);
                    // You must close the output stream.
                    output.Close();

                }

            }
            catch (Exception ex)
            {
                tbResult.AppendText(ex.Message);
                tbResult.AppendText(System.Environment.NewLine);
            }
        }



    }
}
