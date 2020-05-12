using HIMS.EF.DAL.Data;
using Moq;
using System.Collections.Generic;
using System.Linq;
using HIMS.EF.DAL.Data.Repositories;
using System.Data.EntityClient;
using NUnit.Framework;

namespace HIMS.Tests.DAL.Data
{
    [TestFixture]
    public class UserProfileTests
    {
        private DIMSDBContext context;
        private EntityConnection connection;

        private EntityConnection CreateConnection()
        {
            return Effort.EntityConnectionFactory.CreateTransient("name=FakeDIMSDBConnection");
        }

        private DIMSDBContext CreateContext(EntityConnection connectionString)
        {
            return new DIMSDBContext(connectionString);
        }

        private UserProfileRepository CreateRepository(DIMSDBContext context)
        {
            return new UserProfileRepository(context);
        }

        private UserProfileRepository SeedUserProfileRepository()
        {
            context.UserProfiles.AddRange(
               new List<UserProfile>
               {
               new UserProfile {
                    UserId = 1, Name = "username", LastName = "userLname", Sex = "male", DirectionId = 2,
                    Education = "Harvard", Email = "email", UniversityAverageScore = 6.5d, MathScore = 9.0d,
                    MobilePhone = "+375(33)1922", Skype = "skype", StartDate = System.DateTime.Now,
                    BirthDate = System.DateTime.Now, Address = "dom 5" },
               new UserProfile {
                    UserId = 2, Name = "username", LastName = "userLname", Sex = "male", DirectionId = 2,
                    Education = "Harvard", Email = "email", UniversityAverageScore = 6.5d, MathScore = 9.0d,
                    MobilePhone = "+375(33)1922", Skype = "skype", StartDate = System.DateTime.Now,
                    BirthDate = System.DateTime.Now, Address = "dom 5" },
               new UserProfile {
                    UserId = 3, Name = "username", LastName = "userLname", Sex = "male", DirectionId = 2,
                    Education = "Harvard", Email = "email", UniversityAverageScore = 6.5d, MathScore = 9.0d,
                    MobilePhone = "+375(33)1922", Skype = "skype", StartDate = System.DateTime.Now,
                    BirthDate = System.DateTime.Now, Address = "dom 5" }
               }
               );
            context.SaveChanges();

            var repo = CreateRepository(context);

            return repo;
        }

        [SetUp]
        public void Initialize()
        {
            connection = CreateConnection();
            context = CreateContext(connection);
        }

        [Test]
        public void ShouldCreateUserProfile()
        {
            //Arrange
            var repo = CreateRepository(context);
            var user = new UserProfile
            {
                UserId = 5,
                Name = "username",
                LastName = "userLname",
                Sex = "male",
                DirectionId = 2,
                Education = "Harvard",
                Email = "email",
                UniversityAverageScore = 6.5d,
                MathScore = 9.0d,
                MobilePhone = "+375(33)1922",
                Skype = "skype",
                StartDate = System.DateTime.Now,
                BirthDate = System.DateTime.Now,
                Address = "dom 5"
            };

            //Act
            repo.Create(user);

            //Assert
            Assert.IsNotNull(repo.Get(user.UserId));
        }

        [Test]
        public void ShouldDeleteUserProfileById()
        {
            //Arrange
            int id = 2;
            var repo = SeedUserProfileRepository();
            var mock = new Mock<UserProfileRepository>();

            //Act
            mock.Setup(u => u.Delete(It.IsAny<int>()));
            repo.Delete(id);

            //Assert
            mock.Verify(u => u.Delete(id), Times.Once);
        }

        [Test]
        public void ShoulGetUserProfileById()
        {
            //Arrange
            int id = 3;
            var repo = SeedUserProfileRepository();
            var mock = new Mock<IRepository<UserProfile>>();

            //Act
            mock.Setup(u => u.Get(It.IsAny<int>()));
            repo.Get(id);

            //Assert
            mock.Verify(u => u.Get(id), Times.Once);
        }
    }
}
