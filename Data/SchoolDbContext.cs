using System;
using System.Collections.Generic;
using KetNoiDB.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace KetNoiDB.Data;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC07352E1FBA");

            entity.ToTable("Student");

            entity.Property(e => e.ImgUrl).IsUnicode(false);
            entity.Property(e => e.Mssv)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
