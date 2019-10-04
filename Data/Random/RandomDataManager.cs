using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Evoflare.API.Data
{
    public class RandomDataManager
    {
        public List<Person> Persons { get; set; }

        private HashSet<int> usedIndexes = new HashSet<int>();
        public RandomDataManager()
        {
            var resourceKey = "Evoflare.API.Data.Random.persons.json";
            Persons = JsonConvert.DeserializeObject<List<Person>>(DbInitializer.ReadEmbeddedResource(resourceKey));
        }
        public Person GetRandomPerson()
        {
            var rand = new Random(DateTime.Now.Second);
            var index = rand.Next(Persons.Count - 1);
            while (usedIndexes.Contains(index))
            {
                index = rand.Next(Persons.Count - 1);
                if (usedIndexes.Count >= Persons.Count)
                    usedIndexes.Clear();
            }
            usedIndexes.Add(index);
            return Persons.ElementAt(index);
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