using HIMS.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Interfaces
{
    public interface IDirectionService
    {
        void SaveDirection(DirectionDTO direction);
        DirectionDTO GetDirection(int? id);
        void UpdateDireciton(DirectionDTO direction);
        void DeleteDirection(int? id);

        IEnumerable<DirectionDTO> GetDirections();
        void Dispose();
    }
}
