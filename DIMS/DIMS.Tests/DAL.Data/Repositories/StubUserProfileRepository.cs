using HIMS.EF.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Tests.DAL.Data.Repositories
{
    public class StubUserProfileRepository : IRepository<UserProfile>
    {

        private List<UserProfile> userProfiles;

        private static Effort.Provider.EffortConnection connection = Effort.DbConnectionFactory.CreateTransient();
        private DIMSDBContext context = new DIMSDBContext(connection.ConnectionString);

        public StubUserProfileRepository()
        {
            userProfiles = new List<UserProfile>
            {
                new UserProfile { UserId = 1, Name = "up1", LastName = "up1", Sex = "male", DirectionId = 1, Education = "BSU", Email = "email1", UniversityAverageScore = 10.0, MathScore = 6.2, MobilePhone = "111", Skype = "xxx", StartDate = System.DateTime.Now, BirthDate = System.DateTime.Now, Address = "aaa" },
                new UserProfile { UserId = 2, Name = "up2", LastName = "up2", Sex = "male", DirectionId = 2, Education = "BSU", Email = "email2", UniversityAverageScore = 10.0, MathScore = 6.2, MobilePhone = "222", Skype = "yyy", StartDate = System.DateTime.Now, BirthDate = System.DateTime.Now, Address = "bbb" },
                new UserProfile { UserId = 3, Name = "up3", LastName = "up3", Sex = "female", DirectionId = 3, Education = "BSU", Email = "email3", UniversityAverageScore = 10.0, MathScore = 6.2, MobilePhone = "333", Skype = "zzz", StartDate = System.DateTime.Now, BirthDate = System.DateTime.Now, Address = "ccc" }
            };

            context.UserProfiles.AddRange(userProfiles);

        }

        public UserProfile Get(int id)
        {
            return context.UserProfiles.Find(id);
        }

        public void Create(UserProfile item)
        {
            context.UserProfiles.Add(item);
        }

        public void Update(UserProfile item)
        {
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = context.UserProfiles.Find(id);

            if (user != null)
            {
                context.UserProfiles.Remove(user);
            }
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return context.UserProfiles;
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return context.UserProfiles.Where(predicate).ToList();
        }


    }
   
}
