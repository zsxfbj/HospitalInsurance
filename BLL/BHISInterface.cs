using System;
using System.Runtime.Caching;
using System.Text;
using System.Xml;
using HospitalInsurance.Utility;
using HospitalInsurance.Model.Common;
using HospitalInsurance.Model.DTO;
using HospitalInsurance.Model.VO;
using Newtonsoft.Json;
using WebRegComLib;

namespace HospitalInsurance.BLL
{
    /// <summary>
    /// 医保接口
    /// </summary>
    public class BHISInterface : Singleton<BHISInterface>
    {
        //private readonly static string GatewayUrl = ConfigurationManager.AppSettings["GatewayUrl"].ToString();

        private readonly static ObjectCache SystemCache = MemoryCache.Default;


        //private static readonly HttpClient Client = new HttpClient();

        private const string WebRegClassKey = "webreg-";
  
        #region public PersonVO GetPersonInfo(GetPersonInfoReqDTO req)
        /// <summary>
        /// 获取参保人员信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PersonVO GetPersonInfo(GetPersonInfoReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("GetPersonInfo-" + req.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交获取参保人员信息" };
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
            sb.AppendLine("\t\t<card_no name=\"社保卡卡号\">" + (string.IsNullOrEmpty(req.CardNumber) ? "" : req.CardNumber.Trim()) + "</card_no>");
            sb.AppendLine("\t\t<id_no name=\"公民身份号码\">" + (string.IsNullOrEmpty(req.IdNumber) ? "" : req.IdNumber.Trim()) + "</id_no>");
            sb.AppendLine("\t\t<personname name=\"姓名\">" + (string.IsNullOrEmpty(req.PersonName) ? "" : req.PersonName.Trim()).Trim() + "</personname>");
            sb.AppendLine("\t\t<sex name=\"性别\">" + req.Sex + "</sex>");
            sb.AppendLine("\t\t<birthday name=\"出生日期\">" + (string.IsNullOrEmpty(req.Birthday) ? "" : req.Birthday.Trim()) + "</birthday>");
            sb.AppendLine("\t\t<fundtype name=\"险种类型\">" + (string.IsNullOrEmpty(req.FundType) ? "" : req.FundType.Trim()) + "</fundtype>");
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
            try
            {

                //HttpContent httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
                //HttpResponseMessage response = Client.PostAsync(GatewayUrl + "/ybapi/GetPersonInfo_Web", httpContent).Result;
                //string outXml = response.Content.ReadAsStringAsync().Result;
               
                //string outXml = BTestAction.GetInstance().GetPersonInfoWeb(sb.ToString());
                GetWebRegClass(req.CardNumber).GetPersonInfo_Web(sb.ToString(), out string outXml);
               
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
            catch(Exception e)
            {
                BRequestLog.GetInstance().SaveLog(JsonConvert.SerializeObject(req), e.Message);
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.InterfaceError, ErrorMessage = "HIS接口访问异常" };
            }
            
        }
        #endregion public PersonVO GetPersonInfo(GetPersonInfoReqDTO req)

        #region public TradeVO DivideFee(DivideReqDTO req)
        /// <summary>
        /// 费用分解
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public TradeVO DivideFee(DivideReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("divide-fee-" + req.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交费用分解数据" };
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"Divide\">");
            //用户信息
            sb.AppendLine("\t\t<userinfo>");
            sb.AppendLine("\t\t\t<card_no name=\"社保卡号\">" + (string.IsNullOrEmpty(req.CardNumber) ? "" : req.CardNumber.Trim()) + "</card_no>");
            sb.AppendLine("\t\t</userinfo>");

            //交易信息
            sb.AppendLine("\t\t<tradeinfo>");
            sb.AppendLine("\t\t\t<curetype name=\"就诊类型\">" + (string.IsNullOrEmpty(req.CureType) ? "" : req.CureType.Trim()) + "</curetype>");
            sb.AppendLine("\t\t\t<illtype name=\"就诊方式 0普通 其他未启用\">" + (string.IsNullOrEmpty(req.IllType) ? "" : req.IllType.Trim()) + "</illtype> ");
            sb.AppendLine("\t\t\t<feeno name=\"收费单据号\">" + (string.IsNullOrEmpty(req.FeeNumber) ? "" : req.FeeNumber.Trim()) + "</feeno>");
            sb.AppendLine("\t\t\t<operator name=\"收费员\">" + (string.IsNullOrEmpty(req.Operator) ? "" : req.Operator.Trim()) + "</feeno>");
            sb.AppendLine("\t\t</tradeinfo>");

            //处方列表
            sb.AppendLine("\t\t<recipearray>");
            if (req.Recipes != null && req.Recipes.Count > 0)
            {
                foreach (var recipe in req.Recipes)
                {
                    sb.AppendLine("\t\t\t<recipe>");
                    sb.AppendLine("\t\t\t\t<diagnoseno name=\"诊断序号\">" + (string.IsNullOrEmpty(recipe.DiagnoseNumber) ? "" : recipe.DiagnoseNumber.Trim()) + "</diagnoseno>");
                    sb.AppendLine("\t\t\t\t<recipeno name=\"处方序号\">" + (string.IsNullOrEmpty(recipe.RecipeNumber) ? "" : recipe.RecipeNumber.Trim()) + "</recipeno>");
                    sb.AppendLine("\t\t\t\t<recipedate name=\"处方日期时间\">" + (string.IsNullOrEmpty(recipe.RecipeDate) ? "" : recipe.RecipeDate.Trim()) + "</recipedate>");
                    sb.AppendLine("\t\t\t\t<diagnosename name=\"诊断名称\">" + (string.IsNullOrEmpty(recipe.DiagnoseName) ? "" : recipe.DiagnoseName.Trim()) + "</diagnosename>");
                    sb.AppendLine("\t\t\t\t<diagnosecode name=\"诊断名称\">" + (string.IsNullOrEmpty(recipe.DiagnoseCode) ? "" : recipe.DiagnoseCode.Trim()) + "</diagnosecode>");
                    sb.AppendLine("\t\t\t\t<medicalrecord name=\"病历信息\">" + (string.IsNullOrEmpty(recipe.MedicalRecord) ? "" : recipe.MedicalRecord.Trim()) + "</medicalrecord>");
                    sb.AppendLine("\t\t\t\t<sectioncode name=\"就诊科别代码\">" + (string.IsNullOrEmpty(recipe.SectionCode) ? "" : recipe.SectionCode.Trim()) + "</sectioncode>");
                    sb.AppendLine("\t\t\t\t<sectionname name=\"就诊科别名称\">" + (string.IsNullOrEmpty(recipe.SectionName) ? "" : recipe.SectionName.Trim()) + "</sectionname>");
                    sb.AppendLine("\t\t\t\t<hissectionname name=\"HIS就诊科别名称\">" + (string.IsNullOrEmpty(recipe.HisSectionName) ? "" : recipe.HisSectionName.Trim()) + "</hissectionname>");
                    sb.AppendLine("\t\t\t\t<drid name=\"医师编号\">" + (string.IsNullOrEmpty(recipe.DoctorId) ? "" : recipe.DoctorId.Trim()) + "</drid>");
                    sb.AppendLine("\t\t\t\t<drname name=\"医师姓名\">" + (string.IsNullOrEmpty(recipe.DoctorName) ? "" : recipe.DoctorName.Trim()) + "</drname>");
                    sb.AppendLine("\t\t\t\t<recipetype name=\"处方类别（1：医保内处方 2：医保外处方）\">" + recipe.RecipeType + "</recipetype>");
                    sb.AppendLine("\t\t\t\t<helpmedicineflag name=\"代开药标识\">" + (string.IsNullOrEmpty(recipe.HelpMedicineFlag) ? "" : recipe.HelpMedicineFlag.Trim()) + "</helpmedicineflag>");
                    sb.AppendLine("\t\t\t\t<remark name=\"代开药标识\">" + (string.IsNullOrEmpty(recipe.Remark) ? "" : recipe.Remark.Trim()) + "</remark>");
                    sb.AppendLine("\t\t\t\t<registertradeno name=\"挂号交易流水号\">" + (string.IsNullOrEmpty(recipe.RegisterTradeNumber) ? "" : recipe.RegisterTradeNumber.Trim()) + "</registertradeno>");
                    sb.AppendLine("\t\t\t\t<billstype name=\"单据类型（1-挂号）\">" + (string.IsNullOrEmpty(recipe.BillsType) ? "" : recipe.BillsType.Trim()) + "</billstype>");
                    sb.AppendLine("\t\t\t</recipe>");
                }
            }
            sb.AppendLine("\t\t</recipearray>");

            //费用明细列表
            sb.AppendLine("\t\t<feeitemarray>");
            if (req.FeeItems != null && req.FeeItems.Count > 0)
            {
                foreach (var item in req.FeeItems)
                {
                    sb.AppendLine("\t\t\t<feeitem itemno=\"" + item.ItemNumber + "\" recipeno=\"" + (string.IsNullOrEmpty(item.RecipeNumber) ? "" : item.RecipeNumber.Trim()) + "\" hiscode=\"" + (string.IsNullOrEmpty(item.HisCode) ? "" : item.HisCode.Trim()) + "\" itemname=\"" + (string.IsNullOrEmpty(item.ItemName) ? "" : item.ItemName.Trim()) + "\" itemtype=\"" + item.ItemType + "\" unitprice=\"" + item.UnitPrice.ToString("#0.####") + "\" count=\"" + item.Count.ToString("#0.##") + "\" fee=\"" + item.Fee.ToString("#0.####") + "\" dose=\"" + (string.IsNullOrEmpty(item.Dose) ? "" : item.Dose.Trim()) + "\" specification=\"" + (string.IsNullOrEmpty(item.Specification) ? "" : item.Specification.Trim()) + "\" unit=\"" + (string.IsNullOrEmpty(item.Unit) ? "" : item.Unit.Trim()) + "\" howtouse=\"" + (string.IsNullOrEmpty(item.HowToUse) ? "" : item.HowToUse.Trim()) + "\" dosage=\"" + (string.IsNullOrEmpty(item.Dosage) ? "" : item.Dosage.Trim()) + "\" packaging=\"" + (string.IsNullOrEmpty(item.Packaging) ? "" : item.Packaging.Trim()) + "\" minpackage=\"" + (string.IsNullOrEmpty(item.MinPackage) ? "" : item.MinPackage.Trim()) + "\" conversion=\"" + item.Conversion + "\" days=\"" + item.Days + "\" babyflag=\"" + (string.IsNullOrEmpty(item.BabyFlag) ? "" : item.BabyFlag.Trim()) + "\" drugapprovalnumber=\"" + (string.IsNullOrEmpty(item.DrugApprovalNumber) ? "" : item.DrugApprovalNumber.Trim()) + "\"/>");
                }
            }
            sb.AppendLine("\t\t</feeitemarray>");

            //结尾部分
            sb.AppendLine("\t</input>");
            sb.AppendLine("</root>");

            try
            {
                //HttpContent httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
                //HttpResponseMessage response = Client.PostAsync(GatewayUrl + "/ybapi/Divide_Web", httpContent).Result;
                //string outXml = response.Content.ReadAsStringAsync().Result;

                //string outXml = BTestAction.GetInstance().GetPersonInfoWeb(sb.ToString());
                GetWebRegClass(req.CardNumber).Divide_Web(sb.ToString(), out string outXml);
                //记录医保网关的请求日志
                BRequestLog.GetInstance().SaveLog(sb.ToString(), outXml);

                //定义输出结果
                TradeVO trade = new TradeVO
                {
                    CardNumber = req.CardNumber
                };

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

                        //交易信息
                        XmlNode tradeNode = outPutNode.SelectSingleNode("tradeinfo");
                        if (tradeNode != null)
                        {
                            //交易流水号
                            trade.TradeNumber = GetSubNodeValue(tradeNode, "tradeno");

                            //收费单据号
                            trade.FeeNumber = GetSubNodeValue(tradeNode, "feeno");

                            //交易日期
                            trade.TradeDate = GetSubNodeValue(tradeNode, "tradedate");                           
                        }

                        //费用明细
                        XmlNode feeItemsNode = outPutNode.SelectSingleNode("feeitemarray");
                        if (feeItemsNode != null)
                        {
                            trade.FeeItems = new System.Collections.Generic.List<FeeItemVO>();
                            foreach(XmlNode feeItemNode in feeItemsNode.ChildNodes)
                            {                              
                                //添加到计费明细项
                                trade.FeeItems.Add(GetFeeItemVO(feeItemNode));    
                            }                            
                        }

                        //汇总支付信息
                        XmlNode summaryPayNode = outPutNode.SelectSingleNode("sumpay");
                        if(summaryPayNode != null)
                        {
                            trade.SummaryPay = GetSummaryPay(summaryPayNode);
                        }

                        //支付信息
                        XmlNode paymentNode = outPutNode.SelectSingleNode("payinfo");
                        if(paymentNode != null)
                        {
                            trade.Payment = GetPayment(paymentNode);
                        }

                        //分类汇总信息
                        XmlNode medicineCatalogNode = outPutNode.SelectSingleNode("medicatalog");
                        if(medicineCatalogNode != null)
                        {
                            GetMedicineCatalogVO(trade, medicineCatalogNode);
                        }

                        //新单据分类汇总信息
                        XmlNode medicineCatalog2Node = outPutNode.SelectSingleNode("medicatalog2");
                        if (medicineCatalog2Node != null)
                        {
                            GetMedicineCatalog2VO(trade, medicineCatalog2Node);
                        }
                    }
                    else
                    {
                        GetErrorInfo(rootNode);
                    }
                }

                BRequestLog.GetInstance().SaveLog(JsonConvert.SerializeObject(req), trade);
                return trade;
            }
            catch (Exception e)
            {
                BRequestLog.GetInstance().SaveLog(JsonConvert.SerializeObject(req), e.Message);
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.InterfaceError, ErrorMessage = "HIS接口访问异常" };
            }
        }
        #endregion public TradeVO DivideFee(DivideReqDTO req)

        #region public TradeResultVO TradeConfirm(string cardNumber)
        /// <summary>
        /// 门诊交易确认
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public TradeResultVO TradeConfirm(string cardNumber)
        {
            if(string.IsNullOrEmpty(cardNumber) || cardNumber.Trim().Length != 12)
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RequestParamterError, ErrorMessage = "社保卡号格式错误" };
            }

            if (BActionCheck.GetInstance().IsRepeat("trade-" + cardNumber.Trim()))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交门诊交易确认查询请求" };
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"Divide\">");
            //用户信息
            sb.AppendLine("\t\t<userinfo>");
            sb.AppendLine("\t\t\t<card_no name=\"社保卡号\">" + cardNumber.Trim()  + "</card_no>");
            sb.AppendLine("\t\t</userinfo>");
            //结尾部分
            sb.AppendLine("\t</input>");
            sb.AppendLine("</root>");
            try
            {
                //HttpContent httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
                //HttpResponseMessage response = Client.PostAsync(GatewayUrl + "/ybapi/Trade_Web", httpContent).Result;
                //string outXml = response.Content.ReadAsStringAsync().Result;

                //string outXml = BTestAction.GetInstance().GetPersonInfoWeb(sb.ToString());
                GetWebRegClass(cardNumber).Trade_Web(out string outXml);
                //记录医保网关的请求日志
                BRequestLog.GetInstance().SaveLog(sb.ToString(), outXml);

                //定义输出结果
                TradeResultVO tradeResult = new TradeResultVO
                {
                    CardNumber = cardNumber.Trim()
                };

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

                        decimal.TryParse(GetSubNodeValue(outPutNode, "personcountaftersub"), out decimal amount);
                        tradeResult.PersonAccountAfterSubtractAmount = amount;
                        tradeResult.CertId = GetSubNodeValue(outPutNode, "certid");
                        tradeResult.Sign = GetSubNodeValue(outPutNode, "sign");
                    }
                    else
                    {
                        GetErrorInfo(rootNode);
                    }
                }

                BRequestLog.GetInstance().SaveLog("社保卡号：" + cardNumber, tradeResult);
                return tradeResult;
            }
            catch (Exception e)
            {
                BRequestLog.GetInstance().SaveLog("社保卡号：" + cardNumber, e.Message);
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.InterfaceError, ErrorMessage = "HIS接口访问异常" };
            }
        }
        #endregion public TradeResultVO TradeConfirm(string cardNumber)

        #region public RefundTradeVO GetRefundTrade(RefundFeeReqDTO req)
        /// <summary>
        /// 退费交易请求
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public RefundTradeVO GetRefundTrade(RefundFeeReqDTO req)
        {
            if (BActionCheck.GetInstance().IsRepeat("refundment-" + req.CardNumber))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交退费分解请求" };
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"Refundment\">");
            //用户信息
            sb.AppendLine("\t\t<userinfo>");
            sb.AppendLine("\t\t\t<card_no name=\"社保卡号\">" + (string.IsNullOrEmpty(req.CardNumber) ? "" : req.CardNumber.Trim()) + "</card_no>");
            sb.AppendLine("\t\t</userinfo>");
            //交易信息
            sb.AppendLine("\t\t<tradeinfo>");
            sb.AppendLine("\t\t\t<tradeno name=\"交易流水号\">" + (string.IsNullOrEmpty(req.TradeNumber) ? "" : req.TradeNumber.Trim()) + "</tradeno>");
            sb.AppendLine("\t\t\t<operator name=\"收费员\">" + (string.IsNullOrEmpty(req.Operator) ? "" : req.Operator.Trim()) + "</operator>");
            sb.AppendLine("\t\t</tradeinfo>");
            //结尾部分
            sb.AppendLine("\t</input>");
            sb.AppendLine("</root>");

            try
            {
                //HttpContent httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
                // HttpResponseMessage response = Client.PostAsync(GatewayUrl + "/ybapi/Refundment_Web", httpContent).Result;
                //string outXml = response.Content.ReadAsStringAsync().Result;
                GetWebRegClass(req.CardNumber).Refundment_Web(sb.ToString(), out string outXml);
                
                //记录医保网关的请求日志
                BRequestLog.GetInstance().SaveLog(sb.ToString(), outXml);

                //定义输出结果
                RefundTradeVO refundTrade = new RefundTradeVO
                {
                    CardNumber = req.CardNumber.Trim()
                };

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

                        XmlNode fullTradeNode = outPutNode.SelectSingleNode("fulltrade");
                        if (fullTradeNode == null)
                        {
                            ServiceException serviceException = new ServiceException
                            {
                                ResultCode = Enums.ResultCodeEnum.InterfaceError,
                                ErrorMessage = "无有效结果输出"
                            };
                            throw serviceException;
                        }

                        //交易信息
                        XmlNode tradeNode = outPutNode.SelectSingleNode("tradeinfo");
                        if (tradeNode != null)
                        {
                            //交易流水号
                            refundTrade.TradeNumber = GetSubNodeValue(tradeNode, "tradeno");

                            //交易流水号
                            refundTrade.IllType = GetSubNodeValue(tradeNode, "illtype");

                            //医保应用号
                            refundTrade.IcNumber = GetSubNodeValue(tradeNode, "ic_no");

                            //交易流水号
                            refundTrade.CureType = GetSubNodeValue(tradeNode, "curetype");

                            //交易日期
                            refundTrade.TradeDate = GetSubNodeValue(tradeNode, "tradedate");
                        }

                        //费用明细
                        XmlNode feeItemsNode = outPutNode.SelectSingleNode("feeitemarray");
                        if (feeItemsNode != null)
                        {
                            refundTrade.FeeItems = new System.Collections.Generic.List<FeeItemVO>();
                            foreach (XmlNode feeItemNode in feeItemsNode.ChildNodes)
                            {
                                //添加到计费明细项
                                refundTrade.FeeItems.Add(GetFeeItemVO(feeItemNode));
                            }
                        }

                        //汇总支付信息
                        XmlNode summaryPayNode = outPutNode.SelectSingleNode("sumpay");
                        if (summaryPayNode != null)
                        {
                            refundTrade.SummaryPay = GetSummaryPay(summaryPayNode);
                        }

                        //支付信息
                        XmlNode paymentNode = outPutNode.SelectSingleNode("payinfo");
                        if (paymentNode != null)
                        {
                            refundTrade.Payment = GetPayment(paymentNode);
                        }
                    }
                    else
                    {
                        GetErrorInfo(rootNode);
                    }
                }

             
                return refundTrade;
            }
            catch (Exception e)
            {
                BRequestLog.GetInstance().SaveLog(JsonConvert.SerializeObject(req), e.Message);
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.InterfaceError, ErrorMessage = "HIS接口访问异常" };
            }
        }
        #endregion public RefundTradeVO GetRefundTrade(RefundFeeReqDTO req)

        #region public TradeStateVO GetTradeState(string tradeNumber)
        /// <summary>
        /// 交易查询及回退
        /// </summary>
        /// <param name="tradeNumber"></param>
        /// <returns></returns>
        /// <exception cref="ServiceException"></exception>
        public TradeStateVO GetTradeState(string tradeNumber)
        {
            if (string.IsNullOrEmpty(tradeNumber) || tradeNumber.Trim().Length > 22)
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RequestParamterError, ErrorMessage = "交易流水号格式错误" };
            }
            if (BActionCheck.GetInstance().IsRepeat("comfirm-trade-state-" + tradeNumber.Trim()))
            {
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.RepeatAction, ErrorMessage = "频繁的提交交易状态确认查询请求" };
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?>");
            sb.AppendLine("<root version=\"2.003\">");
            sb.AppendLine("\t<input name=\"Divide\">");            
            //用户信息            
            sb.AppendLine("\t\t<tradeno name=\"交易流水号\">" + tradeNumber.Trim() + "</tradeno>");         
            //结尾部分
            sb.AppendLine("\t</input>");
            sb.AppendLine("</root>");

            try
            {
                //HttpContent httpContent = new StringContent(sb.ToString(), Encoding.UTF8, "application/xml");
                //HttpResponseMessage response = Client.PostAsync(GatewayUrl + "/ybapi/CommitTradeState_Web", httpContent).Result;
                //string outXml = response.Content.ReadAsStringAsync().Result;
                GetWebRegClass("").CommitTradeState_Web(tradeNumber, out string outXml);
                //string outXml = BTestAction.GetInstance().GetPersonInfoWeb(sb.ToString());
                //记录医保网关的请求日志
                BRequestLog.GetInstance().SaveLog(sb.ToString(), outXml);

                //定义输出结果
                TradeStateVO tradeState = new TradeStateVO()
                {
                    TradeNumber = tradeNumber.Trim()
                };

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

                        tradeState.State = GetSubNodeValue(outPutNode, "tradestate");
                        if(tradeState.State.Equals("ok", StringComparison.OrdinalIgnoreCase))
                        {
                            tradeState.StateName = "交易成功";
                        }
                        else
                        {
                            tradeState.StateName = "交易撤销";
                        }                        
                    }
                    else
                    {
                        GetErrorInfo(rootNode);
                    }
                }

                BRequestLog.GetInstance().SaveLog("交易流水号：" + tradeNumber, tradeState);
                return tradeState;
            }
            catch (Exception e)
            {
                BRequestLog.GetInstance().SaveLog("交易流水号：" + tradeNumber, e.Message);
                throw new ServiceException { ResultCode = Enums.ResultCodeEnum.InterfaceError, ErrorMessage = "HIS接口访问异常" };
            }
        }
        #endregion public TradeStateVO GetTradeState(string tradeNumber)

        #region Private Methods

        #region private FeeItemVO GetFeeItemVO(XmlNode feeItemNode)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="feeItemNode"></param>
        /// <returns></returns>
        private FeeItemVO GetFeeItemVO(XmlNode feeItemNode)
        {
            FeeItemVO feeItem = new FeeItemVO();

            int.TryParse(feeItemNode.Attributes["itemno"].Value, out int itemNumber);

            feeItem.ItemNumber = itemNumber;
            feeItem.RecipeNumber = feeItemNode.Attributes["recipeno"].Value;
            feeItem.HisCode = feeItemNode.Attributes["hiscode"].Value;
            feeItem.ItemCode = feeItemNode.Attributes["itemcode"].Value;
            feeItem.ItemName = feeItemNode.Attributes["itemname"].Value;

            int.TryParse(feeItemNode.Attributes["itemtype"].Value, out int itemType);
            feeItem.ItemType = itemType;

            decimal.TryParse(feeItemNode.Attributes["unitprice"].Value, out decimal unitPrice);
            feeItem.UnitPrice = unitPrice;

            decimal.TryParse(feeItemNode.Attributes["count"].Value, out decimal count);
            feeItem.Count = count;

            decimal.TryParse(feeItemNode.Attributes["fee"].Value, out decimal fee);
            feeItem.Fee = fee;

            decimal.TryParse(feeItemNode.Attributes["feein"].Value, out decimal inInsuranceFee);
            feeItem.InInsuranceFee = inInsuranceFee;

            decimal.TryParse(feeItemNode.Attributes["feeout"].Value, out decimal outInsuranceFee);
            feeItem.OutInsuranceFee = outInsuranceFee;

            decimal.TryParse(feeItemNode.Attributes["selfpay2"].Value, out decimal selfPayFee);
            feeItem.SelfPayFee = selfPayFee;

            int.TryParse(feeItemNode.Attributes["state"].Value, out int state);
            feeItem.State = state;

            feeItem.FeeType = feeItemNode.Attributes["fee_type"].Value;

            decimal.TryParse(feeItemNode.Attributes["preferentialfee"].Value, out decimal preferentialFee);
            feeItem.PreferentialFee = preferentialFee;

            int.TryParse(feeItemNode.Attributes["preferentialscale"].Value, out int scale);
            feeItem.PreferentialScale = scale;

            return feeItem;
        }
        #endregion private FeeItemVO GetFeeItemVO(XmlNode feeItemNode)

        #region private SummaryPayVO GetSummaryPay(XmlNode summaryPayNode)
        /// <summary>
        /// 
        /// </summary>        
        /// <param name="summaryPayNode"></param>
        private SummaryPayVO GetSummaryPay(XmlNode summaryPayNode)
        {
            SummaryPayVO vo = new SummaryPayVO();

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "feeall"), out decimal totalAmount);
            vo.TotalAmount = totalAmount;

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "fund"), out decimal fundAmount);
            vo.FundAmount = fundAmount;

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "cash"), out decimal cashAmount);
            vo.CashAmount = cashAmount;

            decimal.TryParse(GetSubNodeValue(summaryPayNode, "personcountpay"), out decimal personAccountAmount);
            vo.PersonAccountAmount = personAccountAmount;

            return vo;
        }
        #endregion private SummaryPayVO GetSummaryPay(XmlNode summaryPayNode)

        #region private PaymentVO GetPayment(XmlNode paymentNode)
        /// <summary>
        /// 获取支付信息结果
        /// </summary>
        /// <param name="paymentNode"></param>
        private PaymentVO GetPayment(XmlNode paymentNode)
        {
            PaymentVO vo = new PaymentVO();

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzfee"), out decimal totalAmount);
            vo.TotalAmount = totalAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzfeein"), out decimal inInsuranceAmount);
            vo.InInsuranceAmount = inInsuranceAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzpayfirst"), out decimal firstPayAmount);
            vo.FirstPayAmount = firstPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzselfpay2"), out decimal selfPayAmount);
            vo.SelfPayAmount = selfPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzbigpay"), out decimal bigPayAmount);
            vo.BigPayAmount = bigPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzbigselfpay"), out decimal selfBigPayAmount);
            vo.SelfBigPayAmount = selfBigPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "mzoutofbig"), out decimal outOfBigPayAmount);
            vo.OutOfBigPayAmount = outOfBigPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "bcpay"), out decimal supplementaryPayAmount);
            vo.SupplementaryPayAmount = supplementaryPayAmount;

            decimal.TryParse(GetSubNodeValue(paymentNode, "jcbz"), out decimal militaryPayAmount);
            vo.MilitaryPayAmount = militaryPayAmount;

            return vo;
        }
        #endregion private PaymentVO GetPayment(XmlNode paymentNode)

        #region private void GetMedicineCatalogVO(TradeVO trade, XmlNode medicineCatalogNode)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="medicineCatalogNode"></param>
        private void GetMedicineCatalogVO(TradeVO trade, XmlNode medicineCatalogNode)
        {
            trade.MedicineCatalog = new MedicineCatalogVO();

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "medicine"), out decimal medicineFee);
            trade.MedicineCatalog.MedicineFee = medicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "tmedicine"), out decimal chineseMedicineFee);
            trade.MedicineCatalog.ChineseMedicineFee = chineseMedicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "therb"), out decimal chineseHerbalDrinkFee);
            trade.MedicineCatalog.ChineseHerbalDrinkFee = chineseHerbalDrinkFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "examine"), out decimal examineFee);
            trade.MedicineCatalog.ExamineFee = examineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "ct"), out decimal ctFee);
            trade.MedicineCatalog.CtFee = ctFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "mri"), out decimal mriFee);
            trade.MedicineCatalog.MriFee = mriFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "ultrasonic"), out decimal ultrasonicFee);
            trade.MedicineCatalog.UltrasonicFee = ultrasonicFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "labexam"), out decimal labExamFee);
            trade.MedicineCatalog.LabExamFee = labExamFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "xray"), out decimal xrayFee);
            trade.MedicineCatalog.XRayFee = xrayFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "treatment"), out decimal treatmentFee);
            trade.MedicineCatalog.TreatmentFee = treatmentFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "material"), out decimal materialFee);
            trade.MedicineCatalog.MaterialFee = materialFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "operation"), out decimal operationFee);
            trade.MedicineCatalog.OperationFee = operationFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "oxygen"), out decimal oxygenFee);
            trade.MedicineCatalog.OxygenFee = oxygenFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "bloodt"), out decimal bloodTransfusionFee);
            trade.MedicineCatalog.BloodTransfusionFee = bloodTransfusionFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "orthodontics"), out decimal orthodonticsFee);
            trade.MedicineCatalog.OrthodonticsFee = orthodonticsFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "prosthesis"), out decimal prosthesisFee);
            trade.MedicineCatalog.ProsthesisFee = prosthesisFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "forensic_expertise"), out decimal forensicExpertiseFee);
            trade.MedicineCatalog.ForensicExpertiseFee = forensicExpertiseFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalogNode, "other"), out decimal otherFee);
            trade.MedicineCatalog.OtherFee = otherFee;
        }
        #endregion private void GetMedicineCatalogVO(TradeVO trade, XmlNode medicineCatalogNode)

        #region private void GetMedicineCatalog2VO(TradeVO trade, XmlNode medicineCatalog2Node)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="medicineCatalog2Node"></param>
        private void GetMedicineCatalog2VO(TradeVO trade, XmlNode medicineCatalog2Node)
        {
            trade.MedicineCatalog2 = new MedicineCatalog2VO();

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "diagnosis"), out decimal diagnosisFee);
            trade.MedicineCatalog2.DiagnosisFee = diagnosisFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "examine"), out decimal examineFee);
            trade.MedicineCatalog2.ExamineFee = examineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "labexam"), out decimal labExamFee);
            trade.MedicineCatalog2.LabExamFee = labExamFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "treatment"), out decimal treatmentFee);
            trade.MedicineCatalog2.TreatmentFee = treatmentFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "material"), out decimal materialFee);
            trade.MedicineCatalog2.MaterialFee = materialFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "operation"), out decimal operationFee);
            trade.MedicineCatalog2.OperationFee = operationFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "medicine"), out decimal medicineFee);
            trade.MedicineCatalog2.MedicineFee = medicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "tmedicine"), out decimal chineseMedicineFee);
            trade.MedicineCatalog2.ChineseMedicineFee = chineseMedicineFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "therb"), out decimal chineseHerbalDrinkFee);
            trade.MedicineCatalog2.ChineseHerbalDrinkFee = chineseHerbalDrinkFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "medicalservice"), out decimal medicalServiceFee);
            trade.MedicineCatalog2.MedicalServiceFee = medicalServiceFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "commonservice"), out decimal commonServiceFee);
            trade.MedicineCatalog2.CommonServiceFee = commonServiceFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "registfee"), out decimal registFee);
            trade.MedicineCatalog2.RegistFee = registFee;

            decimal.TryParse(GetSubNodeValue(medicineCatalog2Node, "otheropfee"), out decimal otherOperationFee);
            trade.MedicineCatalog2.OtherOperationFee = otherOperationFee;
        }
        #endregion private void GetMedicineCatalog2VO(TradeVO trade, XmlNode medicineCatalog2Node)

        #region private string GetSubNodeValue(XmlNode parentNode, string nodeName)
        /// <summary>
        /// 查询指定节点下，指定名称节点的值
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns></returns>
        private string GetSubNodeValue(XmlNode parentNode, string nodeName)
        {
            XmlNode personTypeNode = parentNode.SelectSingleNode(nodeName);
            if (personTypeNode != null)
            {
                return personTypeNode.InnerText;
                //return personTypeNode.Attributes[attributeName].Value;
            }
            return null;
        }
        #endregion private string GetSubNodeValue(XmlNode parentNode, string nodeName)

        #region private void GetErrorInfo(XmlNode rootNode)
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
        #endregion private void GetErrorInfo(XmlNode rootNode)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        private WebRegClass GetWebRegClass(string cardNumber)
        {
            string key = WebRegClassKey + cardNumber;

            if (SystemCache.Contains(key))
            {                
                return (WebRegClass)SystemCache.Get(key);
            }
            WebRegClass webReg = new WebRegClass();
            var cacheItemPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(4),
            };
            SystemCache.Set(key, webReg, cacheItemPolicy);
            return webReg;
        }

        #endregion Private Methods
    }
}