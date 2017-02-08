using System;

namespace FCGagarin.BLL.DTO
{

    public class TrainingDTO
    {
        public int Id { get; set; }
        public virtual DateTime Date { get; set; }
        

        public virtual UserProfileDTO Coach { get; set; }

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

