using HIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.EF.DAL.Data.Repositories
{
    public class vTaskRepository : IViewRepository<vTask>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public vTaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<vTask> Find(Func<vTask, bool> predicate)
        {
            return _dIMSDBContext.vTasks.Where(predicate).ToList();
        }

        public vTask Get(int id)
        {
            return _dIMSDBContext.vTasks.Find(id);
        }

        public IEnumerable<vTask> GetAll()
        {
            return _dIMSDBContext.vTasks;
        }
    }
}
