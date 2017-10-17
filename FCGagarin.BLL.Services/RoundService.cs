using System;
using System.Data.Entity;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.Entities;
using FCGagarin.DAL.Repositories.Interfaces;
using MoreLinq;

namespace FCGagarin.BLL.Services
{
    public class RoundService : EntityService<Round>, IRoundService
    {
        private readonly IRoundRepository _roundRepository;

        public RoundService(IRoundRepository roundRepository, DbContext context) : base(roundRepository, context)
        {
            _roundRepository = roundRepository;
        }

        public Round GetById(int id)
        {
            return _roundRepository.GetById(id);
        }

        public Round GetPreviousRound()
        {
            return _roundRepository.FindBy(r=>r.Date < DateTime.Now).MinBy(r => Math.Abs((r.Date - DateTime.Now).Ticks));
        }

        public Round GetNextRound()
        {
            return _roundRepository.FindBy(r => r.Date > DateTime.Now).MinBy(r => Math.Abs((r.Date - DateTime.Now).Ticks));
        }
    }
}