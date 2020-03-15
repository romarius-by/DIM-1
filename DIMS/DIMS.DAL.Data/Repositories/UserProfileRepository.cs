using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private readonly DIMSDBContext _dimsDbContext;

        public UserProfileRepository(DIMSDBContext dIMSDBContext)
        {
            _dimsDbContext = dIMSDBContext;
        }

        public void Create(UserProfile userProfile)
        {
            _dimsDbContext.UserProfiles.Add(userProfile);
        }

        public void Delete(int id)
        {
            var entity = _dimsDbContext.UserProfiles.Find(id);
            
            if (entity != null)
            {
                _dimsDbContext.UserProfiles.Remove(entity);
            }
        }

        public void DeleteByEmail(string email)
        {
            var entity = _dimsDbContext.UserProfiles.Where(x => x.Email == email).FirstOrDefault();

            if (entity != null)
            {
                _dimsDbContext.UserProfiles.Remove(entity);
                _dimsDbContext.SaveChanges();
            }
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _dimsDbContext.UserProfiles.Where(predicate).ToList();
        }

        public UserProfile Get(int id)
        {
            return _dimsDbContext.UserProfiles.Find(id);
        }

        public UserProfile GetByEmail(string email)
        {
            return _dimsDbContext.UserProfiles.Find(email);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _dimsDbContext.UserProfiles;
        }

        public void Update (UserProfile userProfile)
        {
            _dimsDbContext.Entry(userProfile).State = System.Data.Entity.EntityState.Modified;
        }

    }
}
