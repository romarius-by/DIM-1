using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    class TaskTrackRepository : IRepository<TaskTrack>
    {
        private DIMSDBContext _dIMSDBContext;

        public TaskTrackRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public void Create(TaskTrack item)
        {
            _dIMSDBContext.TaskTracks.Add(item);
        }

        public void Delete(int id)
        {
            TaskTrack taskTrack = _dIMSDBContext.TaskTracks.Find(id);

            if (taskTrack != null)
            {
                _dIMSDBContext.TaskTracks.Remove(taskTrack);
            }
        }

        public IEnumerable<TaskTrack> Find(Func<TaskTrack, bool> predicate)
        {
            return _dIMSDBContext.TaskTracks.Where(predicate).ToList();
        }

        public TaskTrack Get(int id)
        {
            return _dIMSDBContext.TaskTracks.Find(id);
        }

        public IEnumerable<TaskTrack> GetAll()
        {
            return _dIMSDBContext.TaskTracks;
        }

        public void Update(TaskTrack item)
        {
            _dIMSDBContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
