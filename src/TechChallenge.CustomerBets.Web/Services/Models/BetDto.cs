using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechChallenge.CustomerBets.Web.Services.Models
{
    public class BetDto
    {
        public int CustomerId { get; set; }
        public int RaceId { get; set; }
        public decimal ReturnStake { get; set; }
        public bool Won { get; set; }
    }
}