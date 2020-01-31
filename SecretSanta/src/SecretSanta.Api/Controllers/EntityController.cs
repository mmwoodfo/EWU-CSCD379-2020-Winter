using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController<TEntity> : ControllerBase where TEntity : class
    {

        private IEntityService<TEntity> EntityService { get; }

        public EntityController(IEntityService<TEntity> entityService)
        {
            EntityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
        }

        // GET: api/Entity
        [HttpGet]
        public async Task<IEnumerable<TEntity>> Get()
        {
            List<TEntity> entities = await EntityService.FetchAllAsync();
            return entities;
        }

        // GET: api/Entity/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            if (await EntityService.FetchByIdAsync(id) is { } entity)
            {
                return Ok(entity);
            }
            return NotFound();
        }

        // POST: api/Entity
        [HttpPost]
        public async Task<TEntity> Post(TEntity value)
        {
            
            return await EntityService.InsertAsync(value);
        }

        // PUT: api/Entity/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> Put(int id,TEntity value)
        {
            if(await EntityService.UpdateAsync(id,value) is TEntity entity)
            {
                return entity;
            }
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            if(await EntityService.DeleteAsync(id) is TEntity entity)
            {
                return entity;
            }
            return NotFound();
        }
    }
}
