using System;
using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.DAL.Entities
{
    public class Round : EntityInt
    {
        public Arena Arena { get; set; }
        public Team Home { get; set; }
        public Team Guest { get; set; }
        public int Number { get; set; }
        public Score Score { get; set; }
        public DateTime Date { get; set; }
        public Tournament Tournament { get; set; }
    }
}