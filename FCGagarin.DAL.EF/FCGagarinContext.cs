using System.Data.Entity;
using FCGagarin.DAL.Entities;

namespace FCGagarin.DAL.EF
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
