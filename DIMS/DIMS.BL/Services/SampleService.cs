using AutoMapper;
using HIMS.BL.Infrastructure;
using HIMS.BL.Interfaces;
using HIMS.BL.Models;
using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Services
{
    public class SampleService : ISampleService
    {
        private IUnitOfWork Database { get; }
        private IProcedureManager Pm { get; }

        public SampleService(IUnitOfWork uow, IProcedureManager pm)
        {
            Database = uow;
            Pm = pm;
        }
        public void SaveSample(SampleDTO sampleTM)
        {
            // Validation
            if (sampleTM.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(sampleTM.Name)} must be less then 25"
                    , nameof(sampleTM.Name));
            if (sampleTM.Description.Length > 255)
                throw new ValidationException($"The length of {nameof(sampleTM.Description)} must be less then 25"
                    , nameof(sampleTM.Description));

            var sample = new Sample
            {
                Name = sampleTM.Name,
                Description = sampleTM.Description
            };

            Database.Samples.Create(sample);
            Database.Save();
        }
        public void UpdateSample(SampleDTO sampleDTO)
        {
            // Validation
            if (sampleDTO.Name.Length > 25)
                throw new ValidationException($"The length of {nameof(sampleDTO.Name)} must be less then 25"
                    , nameof(sampleDTO.Name));
            if (sampleDTO.Description.Length > 255)
                throw new ValidationException($"The length of {nameof(sampleDTO.Description)} must be less then 25"
                    , nameof(sampleDTO.Description));

            var sample = Database.Samples.GetById(sampleDTO.SampleId);

            if (sample != null)
            {
                Mapper.Map(sampleDTO, sample);
                Database.Save();
            }
        }

        public IEnumerable<SampleDTO> GetSamples()
        {
            return Mapper.Map<IEnumerable<Sample>, List<SampleDTO>>(Database.Samples.GetAll());
        }

        public SampleDTO GetSample(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Sample's id value is not set", String.Empty);

            var sample = Database.Samples.GetById(id.Value);

            if (sample == null)
                throw new ValidationException($"The Sample with id = {id} was not found", String.Empty);

            return Mapper.Map<Sample, SampleDTO>(sample);
        }

        public void DeleteSample(int? id)
        {
            if (!id.HasValue)
                throw new ValidationException("The Sample's id value is not set", String.Empty);

            Database.Samples.DeleteById(id.Value);
            Database.Save();
        }

        public void SaveChanges()
        {
            Database.Save();
        }

        public int GetSampleEntriesAmout(bool isAdmin = false)
        {
            return Pm.GetSampleEntriesAmount(isAdmin);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
