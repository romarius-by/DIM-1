using HIMS.EF.DAL.Data;
using HIMS.Tests.DAL.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Tests.DAL.Data
{
    [TestClass]
    public class TestUserRepository 
    {

        private readonly StubUserProfileRepository stubUserProfileRepository;

        public TestUserRepository()
        {
            stubUserProfileRepository = new StubUserProfileRepository();
        }

        [TestMethod]
        public void ShouldCreateUserProfile()
        {
            var user = new UserProfile {
                UserId = 4,
                Name = "up4",
                LastName = "up4",
                Sex = "male",
                DirectionId = 2,
                Education = "BSU",
                Email = "email4",
                UniversityAverageScore = 10.0,
                MathScore = 6.2,
                MobilePhone = "444",
                Skype = "sss",
                StartDate = System.DateTime.Now,
                BirthDate = System.DateTime.Now,
                Address = "ddd"
            };

            stubUserProfileRepository.Create(user);

            Assert.AreEqual("up4", stubUserProfileRepository.Get(user.UserId).Name);
        }

        [TestMethod]
        public void ShouldDeleteUserProfile()
        {
            int id = 4;

            var mock = new Mock<IRepository<UserProfile>>();

            stubUserProfileRepository.Delete(id);

            mock.Verify(v => v.Delete(4), Times.Once);

        }

    }
}
