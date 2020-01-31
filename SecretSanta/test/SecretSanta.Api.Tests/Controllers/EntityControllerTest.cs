using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Business.Services;
using System.Linq;

namespace SecretSanta.Api.Tests.Controllers
{
    public abstract class EntityControllerTest<TEntity> where TEntity : EntityBase
    {
        protected abstract TEntity CreateInstance();

        [TestMethod]
        public void Create_EntityController_Success()
        {
            //Arrange
            var service = new EntityService<TEntity>();

            //Act & Assert
            _ = new EntityController<TEntity>(service);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_ThrowException()
        {
            //Arrange, Act & Assert
            _ = new EntityController<TEntity>(null!);
        }

        [TestMethod]
        public async Task GetAll_WithExistingEntitys_Success()
        {
            //Arrange
            var service = new EntityService<TEntity>();
            TEntity entity = CreateInstance();
            await service.InsertAsync(entity);

            var controller = new EntityController<TEntity>(service);

            //Act
            IEnumerable<TEntity> entities = await controller.Get();

            //Assert
            Assert.AreEqual(1, entities.ToList().Count);
        }

        [TestMethod]
        public async Task GetById_WithExistingEntity_Success()
        {
            //Arrange
            var service = new EntityService<TEntity>();
            TEntity entity = CreateInstance();
            entity = await service.InsertAsync(entity);

            var controller = new EntityController<TEntity>(service);

            //Act
            ActionResult<TEntity> rv = await controller.Get(entity.Id);

            //Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

        [TestMethod]
        public async Task GetById_WithExistingEntity_404Error()
        {
            //Arrange
            var service = new EntityService<TEntity>();

            var controller = new EntityController<TEntity>(service);

            //Act
            ActionResult<TEntity> rv = await controller.Get(0);

            //Assert
            Assert.IsTrue(rv.Result is NotFoundResult);
        }

        [TestMethod]
        public async Task Create_Post_Success()
        {
            //Arrange
            var service = new EntityService<TEntity>();
            TEntity entity = CreateInstance();

            var controller = new EntityController<TEntity>(service);

            //Act
            ActionResult<TEntity> rv = await controller.Post(entity);

            //Assert
            Assert.IsTrue(rv.Value != null);
        }

        [TestMethod]
        public async Task Update_Entity_Success()
        {
            //Arrange


            //Act


            //Assert
        }

        [TestMethod]
        public async Task Delete_EntityWithId_Success()
        {
            //Arrange


            //Act


            //Assert
        }

       
    }

    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        private Dictionary<int, TEntity> Items { get; } = new Dictionary<int, TEntity>();
        protected virtual TEntity CreateWithId(TEntity entity, int id)
        {
            return entity;//should never run
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FetchAllAsync()
        {
            var items = Items.Values.ToList();
            return Task.FromResult(items);
        }

        public Task<TEntity?> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out TEntity? entity))
            {
                Task<TEntity?> task1 = Task.FromResult<TEntity?>(entity);
                return task1;
            }
            Task<TEntity?> task2 = Task.FromResult<TEntity?>(null!);
            return task2;
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            
            int id = Items.Count + 1;
            Items[id] = CreateWithId(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<TEntity[]> InsertAsync(params TEntity[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> UpdateAsync(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }

    }
}
