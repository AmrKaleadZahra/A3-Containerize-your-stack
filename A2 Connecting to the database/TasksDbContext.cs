using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace A2_Connecting_to_the_database;

public partial class TasksDbContext : DbContext
{
    public TasksDbContext()
    {
    }

    public TasksDbContext(DbContextOptions<TasksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbTask> Tasks { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tasks__3214EC07E4A33FC8");

            entity.ToTable("tasks");

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
