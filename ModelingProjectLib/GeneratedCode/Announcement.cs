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

/// <remarks>Новость или анонс</remarks>
public class Announcement : IDiscussable
{
	public virtual DateTime CreateDate
	{
		get;
		set;
	}

	public virtual string Text
	{
		get;
		set;
	}

	public virtual string Title
	{
		get;
		set;
	}

	public virtual int Id
	{
		get;
		set;
	}

	public virtual News News
	{
		get;
		set;
	}

	public virtual User AnnouncementCreator
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

