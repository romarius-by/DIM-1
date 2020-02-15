using System;
using System.Collections.Generic;
using HIMS.EF.DAL.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Core.EntityClient;

namespace HIMS.Tests
{
    public class StubSampleRepository : IRepository<Sample>
    {
        

        

        
        public StubSampleRepository()
        {
            var samples = new List<Sample>
            {
                new Sample { Description = "1", Name = "name1", SampleId = 1 },
                new Sample { Description = "2", Name = "name2", SampleId = 2 },
                new Sample { Description = "3", Name = "name3", SampleId = 3 }
            };

            

        }

        public void Create(Sample item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(Sample item)
        {
            throw new NotImplementedException();
        }


        /*
                public void Create(Sample item)
                {
                    context.Samples.Add(item);
                }

                public void Delete(int id)
                {

                    Sample sample = context.Samples.Find(id);

                    if (sample != null)
                    {
                        context.Samples.Remove(sample);
                    }

                }

                public IEnumerable<Sample> Find(Func<Sample, bool> predicate)
                {
                    return context.Samples.Where(predicate).ToList();
                }

                public Sample Get(int id)
                {
                    return context.Samples.Find(id);
                }

                public IEnumerable<Sample> GetAll()
                {
                    return samples;
                }

                public void Update(Sample item)
                {
                    context.Entry(item).State = EntityState.Modified;
                }*/
    }
}