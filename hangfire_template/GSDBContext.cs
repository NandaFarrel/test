using hangfire_template.Models;
using System;
using System.Web;
using System.Data.Entity;

namespace hangfire_template
{
    public partial class GSDbContext : DbContext
    {
        // --- Model untuk OpenProject ---
        public virtual DbSet<TWorkPackage> TWorkPackages { get; set; }
        public virtual DbSet<TlkpUserMapping> TlkpUserMappings { get; set; }
        // ------------------------------------------

        public DbSet<TempListDataRejectPortal> TempListDataRejectPortal { get; set; }
        public DbSet<Master_ttfbgc4008888> Master_ttfbgc4008888 { get; set; }
        public DbSet<Master_ttfbgc1608888> Master_ttfbgc1608888 { get; set; }
        public DbSet<Master_ttfbgc1208888> Master_ttfbgc1208888 { get; set; }
        public DbSet<Master_ttdpur4028888> Master_ttdpur4028888 { get; set; }
        public DbSet<Master_ttdpur4008888> Master_ttdpur4008888 { get; set; }
        public DbSet<Master_ttdpur2028888> Master_ttdpur2028888 { get; set; }
        public DbSet<Master_ttdpur2008888> Master_ttdpur2008888 { get; set; }
        public DbSet<TempDashboardLineChartSalesOrder> TempDashboardLineChartSalesOrder { get; set; }
        public DbSet<ListSPCTPAD_ttxmsl4298888> ListSPCTPAD_ttxmsl4298888 { get; set; }
        public DbSet<ListSPCTPAD_ttxmsl4288888> ListSPCTPAD_ttxmsl4288888 { get; set; }
        public DbSet<ListSPCTPAD_twhwmd2158888> ListSPCTPAD_twhwmd2158888 { get; set; }
        public DbSet<ListSPCTPAD_twhinh5218888> ListSPCTPAD_twhinh5218888 { get; set; }
        public DbSet<ListSPCTPAD_twhinr1108888> ListSPCTPAD_twhinr1108888 { get; set; }
        public DbSet<ListSPCTPAD_ttcibd0018888> ListSPCTPAD_ttcibd0018888 { get; set; }
        public DbSet<ListSPCTPAD_ttdpur2018888> ListSPCTPAD_ttdpur2018888 { get; set; }

        // DIUBAH: Hanya ada satu constructor yang menunjuk ke Web.config
        public GSDbContext() : base("name=HangfireDb") { }

        // Constructor kedua yang menerima parameter manual sudah dihapus.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GSDbContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TempDashboardLineChartSalesOrder>()
           .Property(p => p.delivered_uninvoice)
           .HasPrecision(18, 3);

            modelBuilder.Entity<TempDashboardLineChartSalesOrder>()
               .Property(p => p.delivered_invoiced)
               .HasPrecision(18, 3);

            modelBuilder.Entity<TempDashboardLineChartSalesOrder>()
               .Property(p => p.undelivered)
               .HasPrecision(18, 3);

            modelBuilder.Entity<TempDashboardLineChartSalesOrder>()
               .Property(p => p.cancel_so)
               .HasPrecision(18, 3);

            modelBuilder.Entity<TempDashboardLineChartSalesOrder>()
               .Property(p => p.avg_value)
               .HasPrecision(18, 3);
        }
    }

    public partial class TargetDbContext : DbContext
    {
        public DbSet<TempListDataRejectPortalDBSQL> TempListDataRejectPortalDBSQL { get; set; }
        public DbSet<TempListBudget> TempListBudget { get; set; }
        public DbSet<TempListCustomer> TempListCustomer { get; set; }
        public DbSet<Table_SPT_STOCK> Table_SPT_STOCK { get; set; }

        public TargetDbContext() : base("name=TargetDbContext") { } // Pastikan ada connection string "TargetDbContext" di Web.config

        public TargetDbContext(string dbSource, string dbName, string dbUsers, string dbPass)
            : base($"Data Source=" + dbSource + ";initial catalog=" + dbName + ";User Id=" + dbUsers + ";Password=" + dbPass + "; ") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TargetDbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TempListBudget>()
                 .Property(p => p.budget_amount)
                 .HasPrecision(30, 7);
            modelBuilder.Entity<TempListBudget>()
                 .Property(p => p.order_amount)
                 .HasPrecision(30, 7);
            modelBuilder.Entity<TempListDataRejectPortalDBSQL>()
                 .Property(p => p.qoor)
                 .HasPrecision(18, 3);
        }
    }
}