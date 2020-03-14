using HIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class vUserTaskRepository : IViewRepository<vUserTask>
    {

        private readonly DIMSDBContext _dIMSDBContext;

        public vUserTaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vUserTask> Find(Func<vUserTask, bool> predicate)
        {
            return _dIMSDBContext.vUserTasks.Where(predicate).ToList();
        }

        public vUserTask Get(int id)
        {
            return _dIMSDBContext.vUserTasks.Find(id);
        }

        public IEnumerable<vUserTask> GetByUserId(int id)
        {
            return _dIMSDBContext.vUserTasks.Where(t => t.UserId == id).ToList();
        }

        public IEnumerable<vUserTask> GetAll()
        {
            return _dIMSDBContext.vUserTasks;
        }
    }
}
