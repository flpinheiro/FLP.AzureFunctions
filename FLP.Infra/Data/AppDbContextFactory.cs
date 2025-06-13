//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace FLP.Infra.Data
//{
//    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//            // Use your actual connection string here or load from config
//            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FunctionDemo;Trusted_Connection=True;MultipleActiveResultSets=true");

//            return new AppDbContext(optionsBuilder.Options);
//        }
//    }
//}
