using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Data
{
    class GiftUser
    {
        public int GiftId { get; set; }
        public Gift Gift { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
