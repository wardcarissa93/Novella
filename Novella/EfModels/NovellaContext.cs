using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Novella.EfModels;

public partial class NovellaContext : DbContext
{
    public NovellaContext()
    {
    }

    public NovellaContext(DbContextOptions<NovellaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ImageStore> ImageStores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:freeazuresqlservercarissa.database.windows.net,1433;Initial Catalog=Novella;Persist Security Info=False;User ID=CarissaWard;Password=P@ssw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImageStore>(entity =>
        {
            entity.HasKey(e => e.PkImageId);

            entity.ToTable("ImageStore");

            entity.HasIndex(e => e.FileName, "UQ__ImageSto__431DED434E59CA40").IsUnique();

            entity.Property(e => e.PkImageId).HasColumnName("pkImageId");
            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fileName");
            entity.Property(e => e.FkProductId).HasColumnName("fkProductId");
            entity.Property(e => e.Image).HasColumnName("image");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
