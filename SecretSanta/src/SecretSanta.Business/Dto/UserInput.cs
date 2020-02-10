using System.Collections.Generic;

namespace SecretSanta.Business.Dto
{
    public class UserInput
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IList<Gift>? Gifts { get; }
    }
}
