using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class UtapiaOptimissaContext : DbContext
{
    public UtapiaOptimissaContext()
    {
    }

    public UtapiaOptimissaContext(DbContextOptions<UtapiaOptimissaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Transacction> Transacctions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= UTapiaOptimissa; Trusted_Connection=True; User ID=sa; Password=pass@word1;TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Account1).HasName("PK__Account__EA162E106ABC8C89");

            entity.ToTable("Account");

            entity.Property(e => e.Account1)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("account");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Owner)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("owner");
        });

        modelBuilder.Entity<Transacction>(entity =>
        {
            entity.HasKey(e => e.Idtransaction).HasName("PK__Transacc__5D70FB171CF6924C");

            entity.ToTable("Transacction");

            entity.Property(e => e.Idtransaction).HasColumnName("idtransaction");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.Fromaccount)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("fromaccount");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("sentAt");
            entity.Property(e => e.Toaccount)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("toaccount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
