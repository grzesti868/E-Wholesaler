﻿using System;
using E_hurtownia.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;

namespace E_hurtownia.Models
{
    public partial class EhurtowniaContext : DbContext
    {
        public EhurtowniaContext()
        {
        }

        public EhurtowniaContext(DbContextOptions<EhurtowniaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Objects> Objects { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<OrderStatuses> OrderStatuses { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<RightsAssignments> RightsAssignments { get; set; }
        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<Storehouses> Storehouses { get; set; }
        public virtual DbSet<Storekeepers> Storekeepers { get; set; }
        public virtual DbSet<Units> Units { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => { builder.AddDebug(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigUtil.GetDbConnectionString()).UseLoggerFactory(LoggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.IdAddress);

                entity.ToTable("ADDRESSES");

                entity.Property(e => e.IdAddress)
                    .HasColumnName("ID_ADDRESS")
                    .ValueGeneratedNever();

                entity.Property(e => e.ApartmentNum).HasColumnName("APARTMENT_NUM");

                entity.Property(e => e.BuildingNum).HasColumnName("BUILDING_NUM");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("CITY")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("COUNTRY")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("POSTAL_CODE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("STREET")
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.IdComapny);

                entity.ToTable("COMPANIES");

                entity.HasIndex(e => e.FkAddress);

                entity.Property(e => e.IdComapny)
                    .HasColumnName("ID_COMAPNY")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkAddress).HasColumnName("FK_ADDRESS");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nip)
                    .HasColumnName("NIP")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Regon)
                    .HasColumnName("REGON")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkAddressNavigation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.FkAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPANIES_ADDRESSES");
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);

                entity.ToTable("CUSTOMERS");

                entity.HasIndex(e => e.FkCompany);

                entity.HasIndex(e => e.FkPerson);

                entity.HasIndex(e => e.FkUser);

                entity.Property(e => e.IdCustomer)
                    .HasColumnName("ID_CUSTOMER")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkCompany).HasColumnName("FK_COMPANY");

                entity.Property(e => e.FkPerson).HasColumnName("FK_PERSON");

                entity.Property(e => e.FkUser).HasColumnName("FK_USER");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.FkCompanyNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.FkCompany)
                    .HasConstraintName("FK_CUSTOMERS_COMPANIES");

                entity.HasOne(d => d.FkPersonNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.FkPerson)
                    .HasConstraintName("FK_CUSTOMERS_PERSONS");

                entity.HasOne(d => d.FkUserNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.FkUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUSTOMERS_USERS");
            });

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.HasKey(e => e.IdGroup);

                entity.ToTable("GROUPS");

                entity.Property(e => e.IdGroup)
                    .HasColumnName("ID_GROUP")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<Objects>(entity =>
            {
                entity.HasKey(e => e.IdObject);

                entity.ToTable("OBJECTS");

                entity.Property(e => e.IdObject)
                    .HasColumnName("ID_OBJECT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => e.IdOrderItem);

                entity.ToTable("ORDER_ITEMS");

                entity.Property(e => e.IdOrderItem)
                    .HasColumnName("ID_ORDER_ITEM")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FkOrder).HasColumnName("FK_ORDER");

                entity.Property(e => e.FkProduct).HasColumnName("FK_PRODUCT");

                entity.HasOne(d => d.FkOrderNavigation)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.FkOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_ITEMS_ORDERS");

                entity.HasOne(d => d.FkProductNavigation)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.FkProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_ITEMS_PRODUCTS");
            });

            modelBuilder.Entity<OrderStatuses>(entity =>
            {
                entity.HasKey(e => e.IdOrderStatus);

                entity.ToTable("ORDER_STATUSES");

                entity.Property(e => e.IdOrderStatus)
                    .HasColumnName("ID_ORDER_STATUS")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.IdOrder);

                entity.ToTable("ORDERS");

                entity.HasIndex(e => e.FkCustomer);

                entity.HasIndex(e => e.FkOrderStatus);

                entity.Property(e => e.IdOrder)
                    .HasColumnName("ID_ORDER")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOrdered)
                    .HasColumnName("DATE_ORDERED")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatePaid)
                    .HasColumnName("DATE_PAID")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateSent)
                    .HasColumnName("DATE_SENT")
                    .HasColumnType("datetime");

                entity.Property(e => e.FkCustomer).HasColumnName("FK_CUSTOMER");

                entity.Property(e => e.FkOrderStatus).HasColumnName("FK_ORDER_STATUS");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.FkCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FkCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_CUSTOMERS");

                entity.HasOne(d => d.FkOrderStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.FkOrderStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERS_ORDER_STATUSES");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.HasKey(e => e.IdPerson);

                entity.ToTable("PERSONS");

                entity.HasIndex(e => e.FkAddress);

                entity.Property(e => e.IdPerson)
                    .HasColumnName("ID_PERSON")
                    .ValueGeneratedNever();

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.FkAddress).HasColumnName("FK_ADDRESS");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("SEX")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkAddressNavigation)
                    .WithMany(p => p.Persons)
                    .HasForeignKey(d => d.FkAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERSONS_ADDRESSES");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("PRODUCTS");

                entity.HasIndex(e => e.FkUnit);

                entity.Property(e => e.IdProduct)
                    .HasColumnName("ID_PRODUCT")
                    .ValueGeneratedNever();

                entity.Property(e => e.BasePricePerUnit)
                    .HasColumnName("BASE_PRICE_PER_UNIT")
                    .HasColumnType("money");

                entity.Property(e => e.FkUnit).HasColumnName("FK_UNIT");

                entity.Property(e => e.ImgFile)
                    .HasColumnName("IMG_FILE")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.PdfFile)
                    .HasColumnName("PDF_FILE")
                    .HasColumnType("text");

                entity.HasOne(d => d.FkUnitNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTS_UNITS");
            });

            modelBuilder.Entity<Rights>(entity =>
            {
                entity.HasKey(e => e.IdRight);

                entity.ToTable("RIGHTS");

                entity.HasIndex(e => e.FkObject);

                entity.Property(e => e.IdRight)
                    .HasColumnName("ID_RIGHT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FkObject).HasColumnName("FK_OBJECT");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.FkObjectNavigation)
                    .WithMany(p => p.Rights)
                    .HasForeignKey(d => d.FkObject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RIGHTS_OBJECTS");
            });

            modelBuilder.Entity<RightsAssignments>(entity =>
            {
                entity.HasKey(e => e.IdRightAssignment);

                entity.ToTable("RIGHTS_ASSIGNMENTS");

                entity.HasIndex(e => e.FkGroup);

                entity.HasIndex(e => e.FkRight);

                entity.Property(e => e.IdRightAssignment)
                    .HasColumnName("ID_RIGHT_ASSIGNMENT")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkGroup).HasColumnName("FK_GROUP");

                entity.Property(e => e.FkRight).HasColumnName("FK_RIGHT");

                entity.HasOne(d => d.FkGroupNavigation)
                    .WithMany(p => p.RightsAssignments)
                    .HasForeignKey(d => d.FkGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RIGHTS_ASSIGNMENTS_GROUPS");

                entity.HasOne(d => d.FkRightNavigation)
                    .WithMany(p => p.RightsAssignments)
                    .HasForeignKey(d => d.FkRight)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RIGHTS_ASSIGNMENTS_RIGHTS");
            });

            modelBuilder.Entity<Stocks>(entity =>
            {
                entity.HasKey(e => e.IdStock);

                entity.ToTable("STOCKS");

                entity.HasIndex(e => e.FkProduct);

                entity.HasIndex(e => e.FkStorehouse);

                entity.Property(e => e.IdStock)
                    .HasColumnName("ID_STOCK")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.Property(e => e.FkProduct).HasColumnName("FK_PRODUCT");

                entity.Property(e => e.FkStorehouse).HasColumnName("FK_STOREHOUSE");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.FkProductNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.FkProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STOCKS_PRODUCTS");

                entity.HasOne(d => d.FkStorehouseNavigation)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.FkStorehouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STOCKS_STOREHOUSES");
            });

            modelBuilder.Entity<Storehouses>(entity =>
            {
                entity.HasKey(e => e.IdStorehouse);

                entity.ToTable("STOREHOUSES");

                entity.HasIndex(e => e.FkAddress);

                entity.Property(e => e.IdStorehouse)
                    .HasColumnName("ID_STOREHOUSE")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkAddress).HasColumnName("FK_ADDRESS");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.FkAddressNavigation)
                    .WithMany(p => p.Storehouses)
                    .HasForeignKey(d => d.FkAddress)
                    .HasConstraintName("FK_STOREHOUSES_ADDRESSES");
            });

            modelBuilder.Entity<Storekeepers>(entity =>
            {
                entity.HasKey(e => e.IdStorekeeper);

                entity.ToTable("STOREKEEPERS");

                entity.HasIndex(e => e.FkStorehouse);

                entity.HasIndex(e => e.FkUser);

                entity.Property(e => e.IdStorekeeper)
                    .HasColumnName("ID_STOREKEEPER")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkStorehouse).HasColumnName("FK_STOREHOUSE");

                entity.Property(e => e.FkUser).HasColumnName("FK_USER");

                entity.HasOne(d => d.FkStorehouseNavigation)
                    .WithMany(p => p.Storekeepers)
                    .HasForeignKey(d => d.FkStorehouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STOREKEEPERS_STOREHOUSES");

                entity.HasOne(d => d.FkUserNavigation)
                    .WithMany(p => p.Storekeepers)
                    .HasForeignKey(d => d.FkUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_STOREKEEPERS_USERS");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.HasKey(e => e.IdUnit);

                entity.ToTable("UNITS");

                entity.Property(e => e.IdUnit)
                    .HasColumnName("ID_UNIT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ShortName)
                    .HasColumnName("SHORT_NAME")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("USERS");

                entity.HasIndex(e => e.FkGroup);

                entity.Property(e => e.IdUser)
                    .HasColumnName("ID_USER")
                    .ValueGeneratedNever();

                entity.Property(e => e.FkGroup).HasColumnName("FK_GROUP");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("LOGIN")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.FkGroupNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FkGroup)
                    .HasConstraintName("FK_GROUP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
