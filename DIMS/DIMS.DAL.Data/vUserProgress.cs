//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HIMS.EF.DAL.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class vUserProgress
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int TaskTrackId { get; set; }
        public string UserName { get; set; }
        public string TaskName { get; set; }
        public string TrackNote { get; set; }
        public System.DateTime TrackDate { get; set; }
    }
}
