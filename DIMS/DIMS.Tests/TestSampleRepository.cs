using System;
using HIMS.BL.Services;
using HIMS.EF.DAL.Data;
using HIMS.Server.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HIMS.Tests
{
    [TestClass]
    public class TestSampleRepository
    {
        private StubSampleRepository stubSampleRepository;

        public TestSampleRepository()
        {
            stubSampleRepository = new StubSampleRepository();
        }

        [TestMethod]
        public void CreateSample() 
        {
            var sample = new Sample { Description = "4", Name = "name4", SampleId = 4 };
            stubSampleRepository.Create(sample);

            Assert.AreEqual("name4", stubSampleRepository.Get(sample.SampleId).Name);
        }
    }
}
