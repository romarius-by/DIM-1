using HIMS.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface ISampleService
    {
        void SaveSample(SampleDTO sampleTM);
        SampleDTO GetSample(int? id);
        void UpdateSample(SampleDTO sampleDTO);
        void DeleteSample(int? id);

        IEnumerable<SampleDTO> GetSamples();
        int GetSampleEntriesAmout(bool isAdmin);
        void Dispose();
    }
}
