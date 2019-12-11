using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skynet.Cloud.Upms.Test.Entity;
using Skynet.Cloud.Upms.Test.Service.Interface;
using UWay.Skynet.Cloud.Linq;
using UWay.Skynet.Cloud.Request;

namespace UWay.Skynet.Cloud.ApiDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUserService userService;
        public ValuesController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<string>> GetFilter()


        //    return new string[] { "value1", "value2" };
        //}

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<DataSourceTableResult> Get(int id)
        {
            var ddt = userService.Page();
            return ddt;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
