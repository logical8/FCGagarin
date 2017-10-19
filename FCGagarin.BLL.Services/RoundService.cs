using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.DTO;
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
            var rounds = _roundRepository.FindBy(r => r.Date != null).Where(r => r.Date < DateTime.Now).ToArray();
            return !rounds.Any() ? null : rounds.MinBy(r => Math.Abs((r.Date.GetValueOrDefault() - DateTime.Now).Ticks));
        }

        public Round GetNextRound()
        {
            var rounds = _roundRepository.FindBy(r => r.Date != null).Where(r => r.Date > DateTime.Now).ToArray();
            return !rounds.Any() ? null : rounds.MinBy(r => Math.Abs((r.Date.GetValueOrDefault() - DateTime.Now).Ticks));
        }

        public void SaveOrUpdate(IEnumerable<RoundDTO> rounds)
        {
            foreach (var roundDTO in rounds)
            {
                var round = ConvertToRound(roundDTO);
                if (round == null) continue;

                var matchedRound = _roundRepository.FindBy(
                    r => r.Number == round.Number && 
                    r.Tournament == round.Tournament &&
                    r.Guest.Name == round.Guest.Name && 
                    r.Home.Name == round.Home.Name).FirstOrDefault();
                if (matchedRound == null)
                {
                    _roundRepository.Add(round);
                }
                else
                {
                    matchedRound.Date = round.Date;
                    matchedRound.Arena = round.Arena;
                    matchedRound.Score = round.Score;
                    _roundRepository.Edit(matchedRound);
                }
            }
        }

        private Round ConvertToRound(RoundDTO roundDTO)
        {
            if (string.IsNullOrWhiteSpace(roundDTO.Guest) || string.IsNullOrWhiteSpace(roundDTO.Home) || string.IsNullOrWhiteSpace(roundDTO.Number))
            {
                return null;
            }
            var round = new Round
            {
                Date = roundDTO.Date,
                Score = roundDTO.Score,
                Number = roundDTO.Number,
                Arena = roundDTO.Arena,
                Guest = roundDTO.Guest,
                Home = roundDTO.Home,
                Tournament = roundDTO.Tournament
            };
        }
    }
}