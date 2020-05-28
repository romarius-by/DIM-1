using DIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DIMS.EF.DAL.Data.Repositories
{
    public class vTaskRepository : IViewRepository<VTask>
    {
        private readonly DIMSDBContext _dIMSDBContext;

        public vTaskRepository(DIMSDBContext dIMSDBContext)
        {
            _dIMSDBContext = dIMSDBContext;
        }

        public IEnumerable<VTask> Find(Func<VTask, bool> predicate)
        {
            return _dIMSDBContext.vTasks.Where(predicate).ToList();
        }

        public VTask GetById(int id)
        {
            return _dIMSDBContext.vTasks.Find(id);
        }

        public IEnumerable<VTask> GetAll()
        {
            return _dIMSDBContext.vTasks;
        }
    }
}
