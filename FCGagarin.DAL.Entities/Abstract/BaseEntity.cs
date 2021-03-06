﻿namespace FCGagarin.DAL.Entities.Abstract
{
    public abstract class BaseEntity
    {

    }

    public abstract class EntityInt : Entity<int>
    {

    }

    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }


}