using System.Collections.ObjectModel;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Collection<Gift> Gifts { get; set; }

        public User(int id, string firstName, string lastName, Collection<Gift> gifts)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gifts = gifts;
        }
    }
}
