using System;

namespace FCGagarin.DAL.EF
{
    public interface IContext : IDisposable
    {
        int Save();
    }
}