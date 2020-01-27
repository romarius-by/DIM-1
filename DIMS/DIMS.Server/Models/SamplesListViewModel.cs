using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIMS.Server.Models
{
    public class SamplesListViewModel
    {
        public IEnumerable<SampleViewModel> Samples { get; set; }
        public int? SamplesAmount { get; set; }
    }
}