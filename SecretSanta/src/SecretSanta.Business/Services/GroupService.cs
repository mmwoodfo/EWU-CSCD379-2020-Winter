using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecretSanta.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Business.Services
{
    public class GroupService : EntityService<Group>, IEntityService<Group>
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper) :
            base(dbContext, mapper)
        { }
    }
}
