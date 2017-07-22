using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Models
{
    public class BetViewModel
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public decimal TotalReturnStake { get; set; }
        public bool IsWinning => TotalReturnStake > 0;
        public string TotalReturnStakeHighlight => IsWinning ? "success" : "danger";

        public int RaceId { get; set; }

        public decimal ReturnStake { get; set; }
        public bool IsLargeWin => Won && ReturnStake > 200;
        public string ReturnStakeHighlight => IsLargeWin ? "danger" : "";

        public bool Won { get; set; }
    }
}