using FCGagarin.Domain.Model;
using System;

public class Training
{
    public int Id { get; set; }
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

