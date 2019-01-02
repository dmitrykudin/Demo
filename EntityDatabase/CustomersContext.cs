using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDatabase
{
    public class CustomersContext : DbContext
    {
        public CustomersContext() : base("CustomersContext") { }

        public virtual DbSet<AssociationRule> AssociationRules { get; set; }
        public virtual DbSet<ClassTag> ClassTags { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductClass> ProductClasses { get; set; }
        public virtual DbSet<ProductItem> ProductItems { get; set; }
        public virtual DbSet<ProductRating> ProductRatings { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<RuleCondition> RuleConditions { get; set; }
        public virtual DbSet<RuleResult> RuleResults { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssociationRule>()
                .Property(e => e.Confidence)
                .HasPrecision(18, 4);

            modelBuilder.Entity<AssociationRule>()
                .HasMany(e => e.RuleConditions)
                .WithRequired(e => e.AssociationRule)
                .HasForeignKey(e => e.RuleId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AssociationRule>()
                .HasMany(e => e.RuleResults)
                .WithRequired(e => e.AssociationRule)
                .HasForeignKey(e => e.RuleId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ProductRatings)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductClass>()
                .HasMany(e => e.ClassTags)
                .WithRequired(e => e.ProductClass)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductClass>()
                .HasMany(e => e.ChildenClasses)
                .WithOptional(e => e.ParentClass)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<ProductClass>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductClass)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductItems)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductRatings)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.RuleConditions)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.RuleResults)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ProductRating>()
                .Property(e => e.Rating)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Purchase>()
                .HasMany(e => e.ProductItems)
                .WithRequired(e => e.Purchase)
                .HasForeignKey(e => e.PurchaseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shop>()
                .HasMany(e => e.Purchases)
                .WithRequired(e => e.Shop)
                .HasForeignKey(e => e.ShopId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.ClassTags)
                .WithRequired(e => e.Tag)
                .HasForeignKey(e => e.TagId)
                .WillCascadeOnDelete(false);
        }
    }
}
