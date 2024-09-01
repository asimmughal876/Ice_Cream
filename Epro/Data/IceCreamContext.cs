using System;
using System.Collections.Generic;
using Epro.Models;
using Microsoft.EntityFrameworkCore;

namespace Epro.Data;

public partial class IceCreamContext : DbContext
{
    public IceCreamContext()
    {
    }

    public IceCreamContext(DbContextOptions<IceCreamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserRecipe> UserRecipes { get; set; }

    public virtual DbSet<UserRecord> UserRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-1LCAB9T\\SQLEXPRESS;Initial Catalog=ICE_CREAM;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.CId).HasName("PK__Contact__A9FDEC3236824D07");

            entity.ToTable("Contact");

            entity.Property(e => e.CId).HasColumnName("C_Id");
            entity.Property(e => e.CEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("C_Email");
            entity.Property(e => e.CMessage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("C_Message");
            entity.Property(e => e.CName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("C_Name");
            entity.Property(e => e.CPhone).HasColumnName("C_Phone");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FId).HasName("PK__feedback__2910CFC58E042D6F");

            entity.ToTable("feedback");

            entity.Property(e => e.FId).HasColumnName("f_Id");
            entity.Property(e => e.FMessage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("f_Message");
            entity.Property(e => e.FUserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("f_user_Name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__C3905BCF43289C05");

            entity.ToTable("orders");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.OrderUser).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderUserId)
                .HasConstraintName("FK__orders__OrderUse__5DCAEF64");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemsId).HasName("PK__orderIte__D5BB2555BB71DAC5");

            entity.ToTable("orderItems");

            entity.Property(e => e.OrderItemsOrderId).HasColumnName("OrderItems_OrderId");
            entity.Property(e => e.OrderItemsProdId).HasColumnName("OrderItems_ProdId");

            entity.HasOne(d => d.OrderItemsOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemsOrderId)
                .HasConstraintName("FK__orderItem__Order__60A75C0F");

            entity.HasOne(d => d.OrderItemsProd).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemsProdId)
                .HasConstraintName("FK__orderItem__Order__619B8048");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__RECIPES__84399C183925F17C");

            entity.ToTable("RECIPES");

            entity.Property(e => e.RecipeId).HasColumnName("RECIPE_ID");
            entity.Property(e => e.RecipeImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RECIPE_IMAGE");
            entity.Property(e => e.RecipeIngredients)
                .HasColumnType("text")
                .HasColumnName("RECIPE_INGREDIENTS");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RECIPE_NAME");
            entity.Property(e => e.RecipePrice).HasColumnName("RECIPE_PRICE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ROLE__8AFACE1ADD18591E");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRecipe>(entity =>
        {
            entity.HasKey(e => e.UserRecipeId).HasName("PK__USER_REC__4E277DAE4BC39828");

            entity.ToTable("USER_RECIPES");

            entity.Property(e => e.UserRecipeId).HasColumnName("USER_RECIPE_ID");
            entity.Property(e => e.UserRecipeIngredients)
                .HasColumnType("text")
                .HasColumnName("USER_RECIPE_INGREDIENTS");
            entity.Property(e => e.UserRecipeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_RECIPE_NAME");
            entity.Property(e => e.UserRecipePrize)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_RECIPE_PRIZE");
            entity.Property(e => e.UserRecipeStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_RECIPE_STATUS");
            entity.Property(e => e.UserRecipeUserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_RECIPE_USER_NAME");
        });

        modelBuilder.Entity<UserRecord>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserReco__1788CC4C29A04B35");

            entity.ToTable("UserRecord");

            entity.Property(e => e.PaymentAmount)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRole).WithMany(p => p.UserRecords)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("FK__UserRecor__UserR__4F7CD00D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
