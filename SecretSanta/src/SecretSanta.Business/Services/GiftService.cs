using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    class GiftService : EntityService<Gift>
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
        public override async Task<Gift> FetchByIdAsync(int id) =>
          await ApplicationDbContext.Set<Gift>().Include(nameof(Gift.User)).SingleAsync(item => item.Id == id);
    }
}
