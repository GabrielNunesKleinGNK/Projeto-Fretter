
using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Repository.Contexts
{
    public class CommandContext : DbContext, IUnitOfWork<CommandContext>
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options) { }
        public int Commit() => this.SaveChanges();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Query<MensagemProvedorDto>();
        }
    }
}
