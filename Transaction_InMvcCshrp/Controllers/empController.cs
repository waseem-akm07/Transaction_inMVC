using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessAccessLayer.Repo;
using DataTransferLayer.DTO;

namespace Transaction_InMvcCshrp.Controllers
{
    public class empController : ApiController
    {
        private IBusinessLayer businessLayer = new BusinessLayer();


        // GET: api/emp
        [HttpGet]
        [Route("api/emp/getemployees")]
        public IEnumerable<HelperModel> GetEmployees()
        {
            return businessLayer.GetEmployees();
        }

        // GET: api/emp/5
        [HttpGet]
        [Route("api/emp/getemployee/{id}")]
        public IEnumerable<HelperModel> Getemployee(int id)
        {
            return businessLayer.Getemployee(id);
        }

        // POST: api/emp
        [HttpPost]
        [Route("api/emp/postemployee")]
        public void Post(List<HelperModel> model)
        {
            businessLayer.Create(model);
        }

        // PUT: api/emp/5
        [HttpPut]
        [Route("api/emp/putemployee/{id}")]
        public void Put(int id, List<HelperModel> model)
        {
            businessLayer.Update(id, model);
        }

        // DELETE: api/emp/5
        [HttpDelete]
        [Route("api/emp/delete/{id}")]
        public void Delete(int id)
        {
            businessLayer.Delete(id);
        }
    }
}
