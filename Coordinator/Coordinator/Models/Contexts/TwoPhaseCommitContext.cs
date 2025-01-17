using Microsoft.EntityFrameworkCore;

namespace Coordinator.Models.Contexts
{
    public class TwoPhaseCommitContext:DbContext
    {
        public DbSet<NodeState> NodeStates { get; set; }
        public DbSet<Node> Nodes { get; set; }

        public TwoPhaseCommitContext(DbContextOptions options):base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //seed data
        //    modelBuilder.Entity<Node>().HasData(
        //        new Node("Order.API") { Id = Guid.NewGuid() },
        //        new Node("Stock.API") { Id = Guid.NewGuid() },
        //        new Node("Payment.API") { Id = Guid.NewGuid() }

        //        );
        //}

    }
}
