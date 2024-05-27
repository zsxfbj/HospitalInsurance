using System;
using System.Collections.Generic;
using HospitalInsurance.Utility;
using HospitalInsurance.Model;
using HospitalInsurance.Model.Common;
using HospitalInsurance.Model.DTO;

namespace HospitalInsurance.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class BRequestLog : Singleton<BRequestLog>
    {
        private const string CollectionName = "request_log";

        /// <summary>
        /// 新增日志记录
        /// </summary>
        /// <param name="requestData"></param>
        /// <param name="respData"></param>
        public void SaveLog(string requestData, Object respData)
        {
            //RequestLog log = new RequestLog();

            //log.ClientIp = HttpContextUtil.GetClientIp();
            //log.UserAgent = HttpContextUtil.GetUserAgent();
            //log.RequestUrl = HttpContextUtil.GetRequestUrl();
            //log.RequestData = requestData;
            //if(respData != null )
            //{
            //    if(respData is String || respData is Boolean || respData is Int64)
            //    {
            //        log.ResponseData = respData.ToString();
            //    } else  
            //    {
            //        log.ResponseData = JsonConvert.SerializeObject(respData);
            //    }
            //}
                    
        }


        /// <summary>
        /// 分页请求查询日志
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageResult<RequestLogVO> GetPagedRequestLogs(LogPageReqDTO req)
        {
            PageResult<RequestLogVO> pageResult = new PageResult<RequestLogVO>
            {
                PageIndex = req.PageIndex,
                PageSize = req.PageSize,
                List = new List<RequestLogVO>()
            };
            //using (var db = new LiteDatabase(BLiteDb.GetInstance().GetLogDbPath()))
            //{
            //    var col = db.GetCollection<RequestLog>(CollectionName);
            //    var query = col.Query();
            //    if (!string.IsNullOrEmpty(req.ClientIp))
            //    {
            //        query = query.Where(x => x.ClientIp == req.ClientIp);
            //    }
            //    if (!string.IsNullOrEmpty(req.RequestUrl))
            //    {
            //        query = query.Where(x => x.RequestUrl.Contains(req.RequestUrl.Trim()));
            //    }
            //    if (req.StartTime.HasValue)
            //    {
            //        query = query.Where(x => x.RequestTime >= req.StartTime.Value);
            //    }
            //    if (req.EndTime.HasValue)
            //    {
            //        query = query.Where(x => x.RequestTime <= req.EndTime.Value);
            //    }
            //    if (!string.IsNullOrEmpty(req.Keyword))
            //    {
            //        query = query.Where(x=>x.ResponseData.Contains(req.Keyword.Trim()) || x.ResponseData.Contains(req.Keyword.Trim()));
            //    }

            //    pageResult.RecordCount = query.LongCount();
            //    var results = query.OrderByDescending(x=>x.RequestTime).Offset((pageResult.PageIndex-1)* pageResult.PageSize).Limit(pageResult.PageSize).ToList();
            //    if(results.Count > 0)
            //    {
            //        foreach (var result in results)
            //        {
            //            pageResult.List.Add(ToVO(result));
            //        }
            //    }
            //}
            return pageResult;
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="log"></param>
        ///// <returns></returns>
        //private RequestLogVO ToVO(RequestLog log)
        //{
        //    RequestLogVO vo = new RequestLogVO();
        //    vo.Id = log.Id.ToString();
        //    vo.RequestTime = log.RequestTime;
        //    vo.ClientIp = log.ClientIp;
        //    vo.UserAgent = log.UserAgent;
        //    vo.RequestUrl = log.RequestUrl;
        //    vo.RequestData = log.RequestData;
        //    vo.ResponseData = log.ResponseData;
        //    return vo;
        //}
    }
}