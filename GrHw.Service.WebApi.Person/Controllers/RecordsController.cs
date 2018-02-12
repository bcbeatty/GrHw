using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrHw.Server.Business;
using Microsoft.AspNetCore.Mvc;

namespace GrHw.Service.WebApi.Person.Controllers
{
    [Route("api/[controller]")]
    public class RecordsController : Controller
    {
        private readonly IPersonFactory _personFactory;

        public RecordsController(IPersonFactory personFactory)
        {
            _personFactory = personFactory;
        }

        [HttpGet]
        [Route("Gender")]
        public IEnumerable<Server.Domain.Person> GetByGender()
        {
            return _personFactory.GetList().OrderBy(s=>s.Gender).ThenByDescending(s=>s.LastName);
        }


        [HttpGet]
        [Route("Birthdate")]
        public IEnumerable<Server.Domain.Person> GetByBirthdate()
        {
            return _personFactory.GetList().OrderBy(s => s.DateOfBirth);
        }

        [HttpGet]
        [Route("Name")]
        public IEnumerable<Server.Domain.Person> GetByName()
        {
            return _personFactory.GetList().OrderBy(s => s.LastName);
        }

        // POST api/values
        [HttpPost]
        public Server.Domain.Person Post([FromBody]string value)
        {
            return _personFactory.PostString(value);
        }

        
    }
}
