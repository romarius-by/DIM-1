using DIMS.BL.DTO;
using System;

namespace DIMS.Server.Models.Tasks
{
    public class TaskTrackViewModel
    {
        public int TaskTrackId { get; set; }
        public int UserTaskId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TrackDate { get; set; }
        public string TrackNote { get; set; }
        public string TaskName { get; set; }
        public int UserId { get; set; }
    }
}