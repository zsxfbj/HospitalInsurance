using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using HospitalInsurance.WebApi.Model;
using HospitalInsurance.WebApi.Model.Common;
using HospitalInsurance.WebApi.Model.DTO;
using Newtonsoft.Json;

namespace HospitalInsurance.WebApi.Controllers
{
    /// <summary>
    /// 测试demo例子
    /// </summary>
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 获取指定id的记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="req"></param>
        [Route("")]
        [HttpPost]
        public ApiResult<RequestLogVO> Post([FromBody] LogPageReqDTO req)
        {
            return new ApiResult<RequestLogVO> { Code = Enums.ResultCodeEnum.Success, Data = new RequestLogVO { ClientIp = "127.0.0.1", Id = "111", RequestData =JsonConvert.SerializeObject(req), RequestTime = System.DateTime.Now, RequestUrl = "/api/values", UserAgent = "aaaa", ResponseData = "{\r\n  \"code\": 400,\r\n  \"errorMessage\": \"页码必须从1开始\"\r\n}" } };
        }

        // PUT api/values/5
        /// <summary>
        /// 设置keyvalue值
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Route("")]
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// 删除值
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [Route("")]
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
