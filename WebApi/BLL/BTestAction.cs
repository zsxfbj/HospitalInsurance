using HospitalInsurance.Utility;

namespace HospitalInsurance.WebApi.BLL
{

    /// <summary>
    /// 
    /// </summary>
    public class BTestAction : Singleton<BTestAction>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public string GetPersonInfoWeb(string requestData)
        {
            string outXml = "<?xml version=\"1.0\" encoding=\"utf16\" standalone=\"yes\" ?> \r\n<root version=\"2.003\">\r\n\t<state success=\"true\">\r\n\t\t<error no=\"1\" info=\"读卡失败,请插入社保卡!\" /> \r\n\t\t<error no=\"2\" info=\"该卡片已经挂失,不能使用!\" /> \r\n\t\t<warning no=\"1\" info=\"与医保中心通讯中断,不能取得个人帐户,定点医疗机构等信息,请联系网络管理员查看网络运行状况\" /> \r\n\t</state>\r\n\t<output name=\"输出部分\">\r\n\t\t<ic>\r\n\t\t\t<card_no name=\"110512343210\"/> \r\n\t\t\t<ic_no name=\"110512343211\"/> \r\n\t\t\t<id_no name=\"110101199901011244\"/> \r\n\t\t\t<personname name=\"测试\"/> \r\n\t\t\t<sex name=\"2\"/> \r\n\t\t\t<birthday name=\"19990101\"/> \r\n\t\t\t<fromhosp name=\"\" /> \r\n\t\t\t<fromhospdate name=\"18991230\" /> \r\n\t\t\t<fundtype name=\"3\" /> \r\n\t\t</ic>\r\n\t\t <net>\r\n\t\t\t<persontype name=\"3\" /> \r\n\t\t\t<isinredlist name=\"true\"/> \r\n\t\t\t<isspecifiedhosp name=\"0\"/> \r\n\t\t\t<ischronichosp name=\"true\"/> \r\n\t\t\t<personcount name=\"1207.12\" /> \r\n\t\t\t<chroniccode name=\"947\"/> \r\n\t\t</net>\r\n\t</output>\r\n</root>";

            BRequestLog.GetInstance().SaveLog(requestData, outXml);
            return outXml;

        }
    }
}