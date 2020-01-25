using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    class UserService : EntityService<User>
    {
        public UserService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }    
    }
}

