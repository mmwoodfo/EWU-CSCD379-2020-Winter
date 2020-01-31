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

namespace SecretSanta.Api.Tests.Controllers
{
    public abstract class EntityControllerTest<TEntity> : TestBase<TEntity> where TEntity : EntityBase
    {
        [TestMethod]
        public void Create_EntityController_Success()
        {
            //Arrange

            //Act & Assert
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_ThrowException()
        {
            //Arrange, Act & Assert
        }

        [TestMethod]
        public async Task GetAll_WithExistingEntitys_Success()
        {
            //Arrange


            //Act


            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingEntity_Success()
        {
            //Arrange

            //Act

            //Assert

        }

        [TestMethod]
        public async Task GetById_WithExistingEntity_404Error()
        {
            //Arrange

            //Act

            //Assert

        }

        [TestMethod]
        public void Create_Post_Success()
        {
            //Arrange


            //Act


            //Assert
        }

        [TestMethod]
        public void Update_Entity_Success()
        {
            //Arrange


            //Act


            //Assert
        }

        [TestMethod]
        public void Delete_EntityWithId_Success()
        {
            //Arrange


            //Act


            //Assert
        }

       
    }

    public abstract class EntityService<TEntity> where TEntity : EntityBase
    {
        private Dictionary<int, TEntity> Items { get; } = new Dictionary<int, TEntity>();
        protected abstract TEntity CreateWithId(TEntity entity, int id);

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FetchAllAsync()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
    

    //public class TestEntity : TEntity
    //{
    //    public TestEntity(TEntity Entity, int id)
    //        : base(TEntity.FirstName, Entity.LastName)
    //    {
    //        Id = id;
    //    }
    //}
}
