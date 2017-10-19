using System.Collections.Generic;
using FCGagarin.BLL.Presenters.Interfaces;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.BLL.Presenters
{
    public class RoundPresenter : IRoundPresenter
    {
        private readonly IRoundService _roundService;

        public RoundPresenter(IRoundService roundService)
        {
            _roundService = roundService;
        }

        public NearestRoundsViewModel GetNearestRoundsViewModel()
        {
            var prevRound = _roundService.GetPreviousRound();
            var nextRound = _roundService.GetNextRound();

            var viewModel = new NearestRoundsViewModel();
            if (prevRound != null)
            {
                viewModel.PreviousRounds.Add(new RoundViewModel
                {
                    Number = prevRound.Number,
                    Date = prevRound.Date?.ToShortDateString(),
                    Guest = prevRound.Guest.Name,
                    Home = prevRound.Home.Name,
                    Arena = prevRound.Arena.Name,
                    Tournament = prevRound.Tournament.Name,
                    DayOfWeek = prevRound.Date?.DayOfWeek.ToString(),
                    Score = prevRound.Score.ToString()
                });
            }
            if (nextRound != null)
            {
                viewModel.NextRounds.Add(new RoundViewModel
                {
                    Number = nextRound.Number,
                    Date = nextRound.Date?.ToShortDateString(),
                    Guest = nextRound.Guest.Name,
                    Home = nextRound.Home.Name,
                    Arena = nextRound.Arena.Name,
                    Tournament = nextRound.Tournament.Name,
                    DayOfWeek = nextRound.Date?.DayOfWeek.ToString(),
                    Score = nextRound.Score.ToString()
                });
            }

            return viewModel;
        }
    }
}