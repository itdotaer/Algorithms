using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class PersonController : ApiController
    {
        private readonly Person[] _people = new Person[]
        {
            new Person{Id=1,Name="Harry1",Age = 24},
            new Person{Id=2,Name="Harry2",Age = 24},
            new Person{Id=3,Name="Harry3",Age = 24}
        };

        // GET api/values
        public IEnumerable<Person> Get()
        {
            return _people;
        }

        // GET api/values/5
        public Person Get(int id)
        {
            try
            {
                var person = _people.First(x => x.Id == id);
                return person;
            }
            catch (Exception)
            {
                return null;
            }
        }

        // POST api/values
        public void Post([FromBody]Person value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Person value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
