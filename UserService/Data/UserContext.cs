using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data
{
    public class UserContext: DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated(); //create a database on the first call
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           // this.Database.EnsureCreated();
            //Map entities to table 
            modelBuilder.Entity<UserModel>().ToTable("Users");

            //Configure Primary Keys
            modelBuilder.Entity<UserModel>().HasKey(x => x.Id).HasName("PK_Users");      

            //Configure indexes
            modelBuilder.Entity<UserModel>().HasIndex(y => y.Name).IsUnique().HasDatabaseName("Idx_Name");

            //Configure columns
            modelBuilder.Entity<UserModel>().Property(x => x.Id).HasColumnType("int").UseMySqlIdentityColumn();//.UseMySqlComputedColumn().ValueGeneratedOnAdd(); 
            modelBuilder.Entity<UserModel>().Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<UserModel>().Property(x => x.Status).HasColumnType("nvarchar(50)").IsRequired();

            modelBuilder.Entity<UserModel>(entity => { entity.Property(e => e.Id).IsRequired(); });

            #region Users
            modelBuilder.Entity<UserModel>().HasData(new {Id = 1, Name = "Alex", Status = "Active"},
                                                     new {Id = 2, Name = "Maria", Status = "Blocked"});
            #endregion              

        }



    }
}
