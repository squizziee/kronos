using Kronos.Machina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Kronos.Machina.Infrastructure.Data
{
    public class VideoDatabaseContext : DbContext
    {
        public DbSet<VideoData> VideoData { get; set; }
        public DbSet<VideoUploadStrategy> UploadStrategies { get; set; }

        public VideoDatabaseContext(DbContextOptions<VideoDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
