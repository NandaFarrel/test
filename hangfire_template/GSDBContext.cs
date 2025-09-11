using hangfire_template.Models;
using System.Data.Entity;

namespace hangfire_template
{
    // Konteks ini berisi tabel-tabel lama dan tabel sinkronisasi baru
    public partial class GSDbContext : DbContext
    {
        // --- Model-model untuk OpenProject & Trello Integration ---
        public virtual DbSet<TWorkPackage> TWorkPackages { get; set; }
        public virtual DbSet<TlkpUserMapping> TlkpUserMappings { get; set; }
        public virtual DbSet<TSyncLog> TSyncLogs { get; set; }

        // --- PENAMBAHAN DbSet UNTUK TABEL SINKRONISASI BARU ---
        public virtual DbSet<TProject> TProjects { get; set; }
        public virtual DbSet<TStatus> TStatuses { get; set; }
        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<TComment> TComments { get; set; }
        public virtual DbSet<TChecklist> TChecklists { get; set; }
        public virtual DbSet<TChecklistItem> TChecklistItems { get; set; }
        // --------------------------------------------------------

        // --- Model-model Anda yang sudah ada ---
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

        // Menggunakan connection string "HangfireDb" dari Web.config
        public GSDbContext() : base("name=HangfireDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<GSDbContext>(null);
            base.OnModelCreating(modelBuilder);

            // Konfigurasi presisi untuk model-model yang sudah ada
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

    // Konteks kedua Anda dibiarkan tidak berubah
    public partial class TargetDbContext : DbContext
    {
        public DbSet<TempListDataRejectPortalDBSQL> TempListDataRejectPortalDBSQL { get; set; }
        public DbSet<TempListBudget> TempListBudget { get; set; }
        public DbSet<TempListCustomer> TempListCustomer { get; set; }
        public DbSet<Table_SPT_STOCK> Table_SPT_STOCK { get; set; }

        public TargetDbContext() : base("name=TargetDbContext") { }

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