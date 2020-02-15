using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Linq;

namespace HIMS.Tests
{
    [TestClass]
    public class TestSampleRepository
    {

        private DIMSDBContext context;
        private EntityConnection connection;
/*
        [TestInitialize]
        public void Initialize() 
        {
            connection = CreateConnection();
            context = CreateContext(connection);

        }*/


        private EntityConnection CreateConnection()
        {
            return Effort.EntityConnectionFactory.CreateTransient("name=FakeDIMSDBConnection");
        }

        private DIMSDBContext CreateContext(EntityConnection connectionString)
        {
            return new DIMSDBContext(connectionString);
        }

        private SampleRepository CreateRepository(DIMSDBContext context)
        {

            return new SampleRepository(context);
        }

        [TestMethod]
        public void ShouldCreateSample() 
        {
            connection = CreateConnection();
            context = CreateContext(connection);

            var sampleRepository = CreateRepository(context);

            var sample = new Sample { Description = "4", Name = "name4", SampleId = 4 };
            sampleRepository.Create(sample);

            Assert.AreEqual("name4", sampleRepository.Get(sample.SampleId).Name);
        }

        [TestMethod]
        public void ShouldDeleteSample()
        {
            var sampleRepository = SeedSampleRepository();

            sampleRepository.Delete(2);

            context.SaveChanges();
            Assert.AreEqual(2, sampleRepository.GetAll().ToList().Count);
        } 

        private SampleRepository SeedSampleRepository()
        {
            context.Samples.AddRange(
                new List<Sample>
                {
                new Sample { Description = "1", Name = "name1", SampleId = 1 },
                new Sample { Description = "2", Name = "name2", SampleId = 2 },
                new Sample { Description = "3", Name = "name3", SampleId = 3 }

                }
                );
            context.SaveChanges();

            var sampleRepository = CreateRepository(context);

            return sampleRepository;
        }
    }
}
