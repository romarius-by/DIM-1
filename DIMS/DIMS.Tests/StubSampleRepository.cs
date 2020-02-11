using System;
using System.Collections.Generic;
using HIMS.EF.DAL.Data;

namespace HIMS.Tests
{
    public class StubSampleRepository : IRepository<Sample>
    {
        private readonly List<Sample> samples;
        public StubSampleRepository()
        {
            samples = new List<Sample>
            {
                new Sample { Description = "1", Name = "name1", SampleId = 1 },
                new Sample { Description = "2", Name = "name2", SampleId = 2 },
                new Sample { Description = "3", Name = "name3", SampleId = 3 }
            };
        }

        public void Create(Sample item)
        {
            samples.Add(item);
        }

        public void Delete(int id)
        {
            Sample sample = new Sample();

            foreach(Sample s in samples)
            {
                if (s.SampleId == id)
                {
                    sample = s;
                }
            }
            
            if (sample != null)
            {
                samples.Remove(sample);
            }

        }

        public IEnumerable<Sample> Find(Func<Sample, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Sample Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Sample> GetAll()
        {
            return samples;
        }

        public void Update(Sample item)
        {
            throw new NotImplementedException();
        }
    }
}