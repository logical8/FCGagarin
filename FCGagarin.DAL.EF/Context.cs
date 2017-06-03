using System;
using System.Data.Entity;

namespace FCGagarin.DAL.EF
{
    public sealed class Context : IContext
    {
        private DbContext _context;

        public Context(DbContext context)
        {
            _context = context;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;
            _context.Dispose();
            _context = null;
        }
    }
}