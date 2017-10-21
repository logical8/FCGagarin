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
        private readonly IArenaRepository _arenaRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public RoundService(IRoundRepository roundRepository, DbContext context, IArenaRepository arenaRepository, ITeamRepository teamRepository, ITournamentRepository tournamentRepository) : base(roundRepository, context)
        {
            _roundRepository = roundRepository;
            _arenaRepository = arenaRepository;
            _teamRepository = teamRepository;
            _tournamentRepository = tournamentRepository;
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
                    r.Tournament.Name == round.Tournament.Name &&
                    r.Guest.Name == round.Guest.Name && 
                    r.Home.Name == round.Home.Name).Include(x=>x.Score).FirstOrDefault();
                if (matchedRound == null)
                {
                    _roundRepository.Add(round);
                }
                else
                {
                    matchedRound.Date = round.Date;
                    matchedRound.Arena = round.Arena;
                    if (matchedRound.Score != null)
                    {
                        if (round.Score != null)
                        {
                            matchedRound.Score.GuestResult = round.Score.GuestResult;
                            matchedRound.Score.HomeResult = round.Score.HomeResult;
                        }
                    }
                    else
                    {
                        if (round.Score != null)
                        {
                            matchedRound.Score = new Score
                            {
                                GuestResult = round.Score.GuestResult,
                                HomeResult = round.Score.HomeResult
                            };
                        }
                    }
                    _roundRepository.Edit(matchedRound);
                }
                _roundRepository.Save();
            }
        }

        private Round ConvertToRound(RoundDTO roundDTO)
        {
            if (string.IsNullOrWhiteSpace(roundDTO.Guest) || string.IsNullOrWhiteSpace(roundDTO.Home) || string.IsNullOrWhiteSpace(roundDTO.Number))
            {
                return null;
            }
            return new Round
            {
                Date = ConvertToDate(roundDTO.Date, roundDTO.Time),
                Score = ConvertToScore(roundDTO.Score),
                Number = roundDTO.Number,
                Arena = ConvertToArena(roundDTO.Arena),
                Guest = ConvertToTeam(roundDTO.Guest),
                Home = ConvertToTeam(roundDTO.Home),
                Tournament = ConvertToTournament(roundDTO.Tournament)
            };
        }

        private Tournament ConvertToTournament(string tournamentName)
        {
            var result = _tournamentRepository.FindBy(x => x.Name == tournamentName).FirstOrDefault();
            if (result == null)
            {
                return new Tournament
                {
                    Name = tournamentName
                };
            }
            return result;
        }

        private Team ConvertToTeam(string teamName)
        {
            var result = _teamRepository.FindBy(x => x.Name == teamName).FirstOrDefault();
            if (result == null)
            {
                return new Team
                {
                    Name = teamName
                };
            }
            return result;
        }

        private Arena ConvertToArena(string arenaName)
        {
            var result = _arenaRepository.FindBy(x=>x.Name == arenaName).FirstOrDefault();
            if (result == null)
            {
                return new Arena
                {
                    Name = arenaName
                };
            }
            return result;
        }

        private Score ConvertToScore(string roundDTOScore)
        {
            if (!roundDTOScore.Contains(":"))
            {
                return null;
            }
            return new Score
            {
                HomeResult = int.Parse(roundDTOScore.Trim().Split(':')[0]),
                GuestResult = int.Parse(roundDTOScore.Trim().Split(':')[1])
            };
        }

        private static DateTime? ConvertToDate(string roundDTODate, string roundDTOTime)
        {
            if (roundDTODate.Length > 8 && DateTime.TryParse(roundDTODate.Substring(0, 10), out DateTime date))
            {
                if (roundDTOTime.Length >=4 && TimeSpan.TryParse(roundDTOTime, out TimeSpan dateTime))
                {
                    return date + dateTime;
                }
                return date.Date;
            }

            return null;
        }
    }
}