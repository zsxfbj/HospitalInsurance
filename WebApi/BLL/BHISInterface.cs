using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Xml;
using HospitalInsurance.Utility;
using HospitalInsurance.WebApi.Model.Common;
using HospitalInsurance.WebApi.Model.DTO;
using HospitalInsurance.WebApi.Model.VO;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.BLL
{
    /// <summary>
    /// 医保接口
    /// </summary>
    public class BHISInterface : Singleton<BHISInterface>
    {
        private readonly static string GatewayUrl = ConfigurationManager.AppSettings["GatewayUrl"].ToString();

        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PersonInfoVO GetPersonInfo(GetPersonInfoReqDTO req)
        {
            if(BActionCheck.GetInstance().IsRepeat("GetPersonInfo-" + req.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交数据" };
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"GetPersonInfo\">");
            sb.AppendLine("\t\t<card_no name=\"" + req.CardNumber + "\"/>");
            sb.AppendLine("\t\t<id_no name=\"" + req.IdNumber + "\"/>");
            sb.AppendLine("\t\t<personname name=\"" + req.PersonName + "\"/>");
            sb.AppendLine("\t\t<sex name=\"" + req.Sex + "\"/>");
            sb.AppendLine("\t\t<birthday name=\"" + req.Birthday + "\"/>");
            sb.AppendLine("\t\t<fundtype name=\"" + req.FundType + "\" />");
            if (req.InHospital.HasValue)
            {
                sb.AppendLine("\t\t<hospflag name=\"" + req.InHospital.Value + "\" />");
            }
            sb.AppendLine("\t</input>");
            sb.AppendLine("</root>");
            //HttpContent httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");       
            //HttpResponseMessage response = client.PostAsync(GatewayUrl + "/ybapi/GetPersonInfo_Web", httpContent).Result;
            //string outXml = response.Content.ReadAsStringAsync().Result;

            string outXml = BTestAction.GetInstance().GetPersonInfoWeb(sb.ToString());
            //记录医保网关的请求日志
            BRequestLog.GetInstance().SaveLog(sb.ToString(), outXml);

            //定义输出结果
            PersonInfoVO personInfo = new PersonInfoVO();

            XmlDocument document = new XmlDocument();
            document.LoadXml(outXml);
            //获取根节点
            XmlNode rootNode = document.SelectSingleNode("root");
            if (rootNode != null)
            {
                //查询状态
                XmlNode stateNode = rootNode.SelectSingleNode("state");
                if (stateNode.Attributes["success"].Value.Equals("true", System.StringComparison.OrdinalIgnoreCase))
                {
                    //查询具体返回数据
                    XmlNode outPutNode = rootNode.SelectSingleNode("output");
                    if (outPutNode == null)
                    {
                        ServiceException serviceException = new ServiceException
                        {
                            ResultCode = Enums.ResultCodeEnum.InterfaceError,
                            ErrorMessage = "无有效结果输出"
                        };
                        throw serviceException;
                    }

                    XmlNode icNode = outPutNode.SelectSingleNode("ic");
                    if (icNode != null)
                    {
                        //社保卡号
                        personInfo.CardNumber = GetSubNodeValue(icNode, "card_no");

                        //身份证号
                        personInfo.IdNumber = GetSubNodeValue(icNode, "id_no");

                        //医保号
                        personInfo.IcNumber = GetSubNodeValue(icNode, "ic_no");

                        //姓名
                        personInfo.PersonName = GetSubNodeValue(icNode, "personname");

                        //性别
                        string sex = GetSubNodeValue(icNode, "sex");
                        int.TryParse(sex, out int value);
                        personInfo.Sex = value;

                        //出生日期
                        personInfo.Birthday = GetSubNodeValue(icNode, "birthday");

                        //转诊医院编码
                        personInfo.FromHospital = GetSubNodeValue(icNode, "fromhosp");

                        //转诊时限
                        personInfo.FromHospitalDate = GetSubNodeValue(icNode, "fromhospdate");

                        //险种类型
                        personInfo.FundType = GetSubNodeValue(icNode, "fundtype");

                    }

                    XmlNode netNode = outPutNode.SelectSingleNode("net");
                    if (netNode != null)
                    {
                        //参保人员类别
                        personInfo.PersonType = GetSubNodeValue(netNode, "persontype");

                        //是否在红名单
                        personInfo.IsInRedList = GetSubNodeValue(netNode, "isinredlist");

                        //本人定点医院状态
                        personInfo.IsSpecifiedHospital = GetSubNodeValue(netNode, "isspecifiedhosp");

                        //是否本人慢病定点医院
                        personInfo.IsChronicHospital = GetSubNodeValue(netNode, "ischronichosp");

                        //个人帐户余额
                        string personCount = GetSubNodeValue(netNode, "personcount");
                        decimal.TryParse(personCount, out decimal value);
                        personInfo.PersonCount = value;

                        //慢病编码
                        personInfo.ChronicCode = GetSubNodeValue(netNode, "chroniccode");
                    }
                }
                else
                {
                    XmlNodeList errorNodes = rootNode.SelectNodes("error");
                    if (errorNodes != null && errorNodes.Count > 0)
                    {
                        StringBuilder errorMessage = new StringBuilder();
                        foreach (XmlNode errorNode in errorNodes)
                        {
                            errorMessage.AppendLine(errorNode.Attributes["no"].Value + "、" + errorNode.Attributes["info"].Value);
                        }

                        ServiceException serviceException = new ServiceException
                        {
                            ResultCode = Enums.ResultCodeEnum.InterfaceError,
                            ErrorMessage = errorMessage.ToString()
                        };
                        throw serviceException;
                    }
                    else
                    {
                        XmlNodeList warnNodes = rootNode.SelectNodes("warning");
                        StringBuilder errorMessage = new StringBuilder();
                        foreach (XmlNode warnNode in warnNodes)
                        {
                            errorMessage.AppendLine(warnNode.Attributes["no"].Value + "、" + warnNode.Attributes["info"].Value);
                        }

                        ServiceException serviceException = new ServiceException
                        {
                            ResultCode = Enums.ResultCodeEnum.InterfaceError,
                            ErrorMessage = errorMessage.ToString()
                        };
                        throw serviceException;
                    }
                }
            }


            BRequestLog.GetInstance().SaveLog(JsonConvert.SerializeObject(req), personInfo);
            return personInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeName"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        private string GetSubNodeValue(XmlNode parentNode, string nodeName, string attributeName = "name")
        {
            XmlNode personTypeNode = parentNode.SelectSingleNode(nodeName);
            if (personTypeNode != null)
            {
                return personTypeNode.Attributes[attributeName].Value;
            }
            return null;
        }
    }
}