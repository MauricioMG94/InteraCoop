using Microsoft.EntityFrameworkCore;
namespace InteraCoop.Backend.Data
{
    public class DataContext : DbContext 
    {        

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
