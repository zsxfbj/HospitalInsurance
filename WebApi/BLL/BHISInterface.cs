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

        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// 获取参保人员信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PersonVO GetPersonInfo(GetPersonInfoReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("GetPersonInfo-" + req.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交数据" };
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"GetPersonInfo\">");
            //sb.AppendLine("\t\t<card_no name=\"" + req.CardNumber + "\"/>");
            //sb.AppendLine("\t\t<id_no name=\"" + req.IdNumber + "\"/>");
            //sb.AppendLine("\t\t<personname name=\"" + req.PersonName + "\"/>");
            //sb.AppendLine("\t\t<sex name=\"" + req.Sex + "\"/>");
            //sb.AppendLine("\t\t<birthday name=\"" + req.Birthday + "\"/>");
            //sb.AppendLine("\t\t<fundtype name=\"" + req.FundType + "\" />");
            //if (req.InHospital.HasValue)
            //{
            //    sb.AppendLine("\t\t<hospflag name=\"" + req.InHospital.Value + "\" />");
            //}
            //else
            //{
            //    sb.AppendLine("\t\t<hospflag name=\"\" />");
            //}
            sb.AppendLine("\t\t<card_no name=\"社保卡卡号\">" + req.CardNumber.Trim() + "</card_no>");
            sb.AppendLine("\t\t<id_no name=\"公民身份号码\">" + req.IdNumber.Trim() + "</id_no>");
            sb.AppendLine("\t\t<personname name=\"姓名\">" + req.PersonName.Trim() + "</personname>");
            sb.AppendLine("\t\t<sex name=\"性别\">" + req.Sex + "</sex>");
            sb.AppendLine("\t\t<birthday name=\"出生日期\">" + req.Birthday + "</birthday>");
            sb.AppendLine("\t\t<fundtype name=\"险种类型\">" + req.FundType + "</fundtype>");
            if (req.InHospital.HasValue)
            {
                sb.AppendLine("\t\t<hospflag name=\"是否在院\">" + req.InHospital.Value + "</hospflag>");
            }
            else
            {
                sb.AppendLine("\t\t<hospflag name=\"是否在院\" />");
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
            PersonVO personInfo = new PersonVO();

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
                    GetErrorInfo(rootNode);
                }
            }


            BRequestLog.GetInstance().SaveLog(JsonConvert.SerializeObject(req), personInfo);
            return personInfo;
        }

        /// <summary>
        /// 费用分解
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public TradeVO DivideFee(DivideReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("divide-fee-" + req.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交数据" };
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"Divide\">");
            //用户信息
            sb.AppendLine("\t\t<userinfo>");
            sb.AppendLine("\t\t\t<card_no name=\"社保卡号\">" + req.CardNumber + "</card_no>");
            sb.AppendLine("\t\t</userinfo>");
            //交易信息
            sb.AppendLine("\t\t<tradeinfo>");
            sb.AppendLine("\t\t\t<curetype name=\"就诊类型\">" + req.CureType.Trim() + "</curetype>");
            sb.AppendLine("\t\t\t<illtype name=\"就诊方式 0普通 其他未启用\">" + req.IllType.Trim() + "</illtype> ");
            sb.AppendLine("\t\t\t<feeno name=\"收费单据号\">"+ (string.IsNullOrEmpty(req.FeeNumber) ? "" : req.FeeNumber.Trim()) + "</feeno>");
            sb.AppendLine("\t\t\t<operator name=\"收费员\">" + (string.IsNullOrEmpty(req.Operator) ? "" : req.Operator.Trim()) + "</feeno>");
            sb.AppendLine("\t\t</tradeinfo>");
            //处方列表
            sb.AppendLine("\t\t<recipearray>");
            if(req.Recipes != null && req.Recipes.Count > 0)
            {
                foreach (var recipe in req.Recipes)
                {
                    sb.AppendLine("\t\t\t<recipe>");
                    sb.AppendLine("\t\t\t\t<diagnoseno name=\"诊断序号\">" + recipe.DiagnoseNumber.Trim() + "</diagnoseno>");
                    sb.AppendLine("\t\t\t\t<recipeno name=\"处方序号\">" + recipe.RecipeNumber.Trim() + "</recipeno>");
                    sb.AppendLine("\t\t\t\t<recipedate name=\"处方日期时间\">" + recipe.RecipeDate.Trim() + "</recipedate>");
                    sb.AppendLine("\t\t\t\t<diagnosename name=\"诊断名称\">" + recipe.DiagnoseName.Trim() + "</diagnosename>");
                    sb.AppendLine("\t\t\t\t<diagnosecode name=\"诊断名称\">" + recipe.DiagnoseCode.Trim() + "</diagnosecode>");
                    sb.AppendLine("\t\t\t\t<medicalrecord name=\"病历信息\">" + recipe.MedicalRecord.Trim() + "</medicalrecord>");
                    sb.AppendLine("\t\t\t\t<sectioncode name=\"就诊科别代码\">" + recipe.SectionCode.Trim() + "</sectioncode>");
                    sb.AppendLine("\t\t\t\t<sectionname name=\"就诊科别名称\">" + recipe.SectionName.Trim() + "</sectionname>");
                    sb.AppendLine("\t\t\t\t<hissectionname name=\"HIS就诊科别名称\">" + recipe.HisSectionName.Trim() + "</hissectionname>");



                    sb.AppendLine("\t\t\t</recipe>");
                }
            }
            sb.AppendLine("\t\t</recipearray>");
            //结尾部分
            sb.AppendLine("\t</input>");
            sb.AppendLine("</root>");
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
                return personTypeNode.InnerText;
                //return personTypeNode.Attributes[attributeName].Value;
            }
            return null;
        }

        /// <summary>
        /// 读取错误信息并输出
        /// </summary>
        /// <param name="rootNode"></param>
        private void GetErrorInfo(XmlNode rootNode)
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
}