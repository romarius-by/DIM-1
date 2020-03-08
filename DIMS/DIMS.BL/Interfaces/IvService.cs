﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IvService<T>
    {
        T GetItem(int? id);
        IEnumerable<T> GetItems();
        
        void Dispose();
    }
}
