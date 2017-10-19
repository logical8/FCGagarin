using System.Collections.Generic;
using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Entities;

namespace FCGagarin.BLL.Services.Interfaces
{
    public interface IRoundService : IEntityService<Round>
    {
        Round GetById(int id);
        Round GetPreviousRound();
        Round GetNextRound();
        void SaveOrUpdate(IEnumerable<RoundDTO> rounds);
    }
}