using System.Net.Mail;
using GrHw.Client.Domain;
using GrHw.Framework;

namespace GrHw.Client.Business.Implementation
{
    public class PersonLineProcessor : ILineProcessor<Person>
    {
        public Person ParseLine(string[] line)
        {
            return new Person
            {
                LastName = line[0].Trim(),
                FirstName = line[1].Trim(),
                Gender = line[2].Trim(),
                FavoriteColor = line[3].Trim(),
                DateOfBirth = line[4].AsDate().ToString("M/d/yyyy")
            };
        }

        public string ToLine(Person person, char delimiter)
        {
            
            return
                $"{person.LastName}{delimiter}{person.FirstName}{delimiter}{person.Gender}{delimiter}{person.FavoriteColor}{delimiter}{person.DateOfBirth.AsDate():d}";
        }
    }
}