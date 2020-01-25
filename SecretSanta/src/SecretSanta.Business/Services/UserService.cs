using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public class UserService : EntityService<User>, IEntityService<User>
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }    
    }
}

