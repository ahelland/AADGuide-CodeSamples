using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ADFSTodoSPA.Controllers;

namespace ADFSTodoSPA.DAL
{
    public class TodoListServiceContext: DbContext
    {
        public TodoListServiceContext()
            : base("TodoListServiceContext")
        { }
        public DbSet<Todo> Todoes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}