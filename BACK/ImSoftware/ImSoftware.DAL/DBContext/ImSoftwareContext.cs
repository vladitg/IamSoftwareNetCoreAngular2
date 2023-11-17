using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ImSoftware.Model;

namespace ImSoftware.DAL.DBContext;

public partial class ImSoftwareContext : DbContext
{
    public ImSoftwareContext()
    {
    }

    public ImSoftwareContext(DbContextOptions<ImSoftwareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
