using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Models
{
    public class BetViewModel
    {
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public int RaceId { get; set; }
        public decimal ReturnStake { get; set; }
        public bool Won { get; set; }
    }
}