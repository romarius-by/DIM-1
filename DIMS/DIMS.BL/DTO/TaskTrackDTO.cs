﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.DTO
{
    public class TaskTrackDTO
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }
        public DateTime? TrackDate { get; set; }
        public string TrackNote { get; set; }
        public virtual UserTaskDTO UserTask { get; set; }

    }
}
