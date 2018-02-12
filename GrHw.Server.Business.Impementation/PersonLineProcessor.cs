using GrHw.Framework;
using GrHw.Server.Domain;

namespace GrHw.Server.Business.Impementation
{
    public class PersonLineProcessor : ILineProcessor<Person>
    {
        public PersonLineProcessor(char delimiter)
        {
            Delimiter = delimiter;
        }

        public char Delimiter { get; }

        public Person ParseLine(string line)
        {
            var str = line.Split(Delimiter);
            return new Person
            {
                LastName = str[0],
                FirstName = str[1],
                Gender = str[2],
                FavoriteColor = str[3],
                DateOfBirth = str[4].AsDate()
            };
        }
        

        
    }
}