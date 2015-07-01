using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BbqDbContext>());

            var context = new BbqDbContext();

            var food = new Food
            {
                MeatType = new MeatType { Name = "Beef" },
                MeatCut = new MeatCut { Name = "Brisket" },
                Recipe = new Recipe
                {
                    Title = "Aaron Franklin's Brisket Recipe",
                    Body = "equal parts salt and pepper and NOTHING ELSE"
                }
            };

            context.Set<Food>().Add(food);
            context.SaveChanges();

            Console.WriteLine(context.Set<MeatType>().Count());

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }

    public class BbqDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }


    public class MeatTypeConfiguration : EntityTypeConfiguration<MeatType>
    {
        public MeatTypeConfiguration()
        {
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Name).HasColumnType("char(50)");
        }
    }


    public class MeatCutConfiguration : EntityTypeConfiguration<MeatCut>
    {
        public MeatCutConfiguration()
        {
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }


    public class RecipeConfiguration : EntityTypeConfiguration<Recipe>
    {
        public RecipeConfiguration()
        {
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
    public class FoodConfiguration : EntityTypeConfiguration<Food>
    {
        public FoodConfiguration()
        {
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class MeatType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class MeatCut
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class Recipe
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

    }

    public class Food
    {
        public Guid Id { get; set; }
        public MeatType MeatType { get; set; }
        public MeatCut MeatCut { get; set; }
        public Recipe Recipe { get; set; }
    }
}
