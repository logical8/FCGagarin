﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Training : IDiscussable
{
	public virtual DateTime Date
	{
		get;
		set;
	}

	public virtual User Coach
	{
		get;
		set;
	}

	public virtual TrainingGroup Group
	{
		get;
		set;
	}

	public virtual TrainingType Type
	{
		get;
		set;
	}

	public virtual TrainingLocation TrainingLocation
	{
		get;
		set;
	}

	public virtual Discussion Discussion
	{
		get;
		set;
	}

	public virtual void ShowDiscussion()
	{
		throw new System.NotImplementedException();
	}

}
