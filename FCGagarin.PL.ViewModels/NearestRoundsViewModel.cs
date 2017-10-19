using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FCGagarin.PL.ViewModels
{
    public class NearestRoundsViewModel
    {
        public NearestRoundsViewModel()
        {
            PreviousRounds = new List<RoundViewModel>();
            NextRounds = new List<RoundViewModel>();
        }

        public List<RoundViewModel> PreviousRounds { get; set; }
        public List<RoundViewModel> NextRounds { get; set; }
    }
}