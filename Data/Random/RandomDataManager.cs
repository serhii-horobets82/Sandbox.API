using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Evoflare.API.Data
{
    public class RandomDataManager
    {
        public List<Person> Persons { get; set; }

        public RandomDataManager()
        {
            var resourceKey = "Evoflare.API.Data.Random.persons.json";
            Persons = JsonConvert.DeserializeObject<List<Person>>(DbInitializer.ReadEmbeddedResource(resourceKey));
        }
        public Person GetRandomPerson()
        {
            var rand = new Random(DateTime.Now.Second);
            return Persons.ElementAt(rand.Next(Persons.Count - 1));
        }
    }

    // https://uinames.com/
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Region { get; set; }
        public string Photo { get; set; }
        public Birthday Birthday { get; set; }
    }

    public class Birthday
    {
        public string Dmy { get; set; }
    }

}