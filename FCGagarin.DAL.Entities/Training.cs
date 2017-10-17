using System;
using FCGagarin.DAL.Entities.Abstract;

namespace FCGagarin.DAL.Entities
{

    public class Training : EntityInt
    {
        public virtual DateTime Date
        {
            get;
            set;
        }

        public virtual UserProfile Coach
        {
            get;
            set;
        }

        //public virtual TrainingGroup Group
        //{
        //    get;
        //    set;
        //}

        //public virtual TrainingType Type
        //{
        //    get;
        //    set;
        //}

        //public virtual TrainingLocation TrainingLocation
        //{
        //    get;
        //    set;
        //}
    }
}

