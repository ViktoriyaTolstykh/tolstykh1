
using Microsoft.EntityFrameworkCore;

namespace tolstykh1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<human> humen { get; set; }// контекст данных
    }
}


 