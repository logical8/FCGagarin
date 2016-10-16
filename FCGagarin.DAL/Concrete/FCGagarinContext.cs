using FCGagarin.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCGagarin.DAL.Concrete
{
    public class FCGagarinContext : DbContext
    {
        public FCGagarinContext()
        {
            Database.SetInitializer<FCGagarinContext>(null);
        }
        
        public DbSet<News> News { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<VideoAlbum> VideoAlbums { get; set; }
    }
}
