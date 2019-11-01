using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skynet.Cloud.Upms.Test.Service.Interface;
using UWay.Skynet.Cloud.Mvc;
//using UWay.Skynet.Cloud.Security.Filters;

namespace Skynet.Cloud.Cloud.CloudFoundryDemo.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IRemoteTest _remoteTest;

        public ValuesController(IRemoteTest remoteTest)
        {
            this._remoteTest = remoteTest;
        }

        //public ValuesController()
        //{

        //}

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        //[Permission]
        public ActionResult<string> Get(int id)
        {
            //var name = HttpContext.User.Identity.Name;
            //var authorities = HttpContext.User.Claims.Where(p => p.Type.Equals("authorities"));
            try
            {
                var result = _remoteTest.GetUser(HttpContext.User.UserId() ?? 0);
                result.Wait();
                return result.Result.Data.UserNo;
            } catch(Exception ex)
            {
                throw ex;
            }
            
            return HttpContext.User.UserName();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
