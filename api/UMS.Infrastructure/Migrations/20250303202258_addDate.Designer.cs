﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UMS.Infrastructure.Persistence;

#nullable disable

namespace UMS.Infrastructure.Migrations
{
    [DbContext(typeof(UmsDbContext))]
    [Migration("20250303202258_addDate")]
    partial class addDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("UMS.Core.Entities.ApplicationType", b =>
                {
                    b.Property<int>("type_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("type")
                        .HasColumnType("longtext");

                    b.HasKey("type_id");

                    b.ToTable("ApplicationTypes");
                });

            modelBuilder.Entity("UMS.Core.Entities.Applications", b =>
                {
                    b.Property<int?>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("applicant_id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("approved_at")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("approver_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("observations")
                        .HasColumnType("longtext");

                    b.Property<int?>("type_id")
                        .HasColumnType("int");

                    b.Property<int?>("zone_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("applicant_id");

                    b.HasIndex("approver_id");

                    b.HasIndex("type_id");

                    b.HasIndex("zone_id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("UMS.Core.Entities.Role", b =>
                {
                    b.Property<int?>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("role")
                        .HasColumnType("longtext");

                    b.HasKey("role_id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("UMS.Core.Entities.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("birthDate")
                        .HasColumnType("date");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("gender")
                        .HasColumnType("longtext");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<string>("last_name")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<string>("otpSecret")
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .HasColumnType("longtext");

                    b.Property<int?>("role_id")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.Property<int?>("zone_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("role_id");

                    b.HasIndex("zone_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UMS.Core.Entities.Zone", b =>
                {
                    b.Property<int?>("zone_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("zone")
                        .HasColumnType("longtext");

                    b.HasKey("zone_id");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("UMS.Core.Entities.Applications", b =>
                {
                    b.HasOne("UMS.Core.Entities.User", "applicant")
                        .WithMany("ApplicationsApplicant")
                        .HasForeignKey("applicant_id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("UMS.Core.Entities.User", "approver")
                        .WithMany("ApplicationsApproved")
                        .HasForeignKey("approver_id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("UMS.Core.Entities.ApplicationType", "type")
                        .WithMany("Applications")
                        .HasForeignKey("type_id");

                    b.HasOne("UMS.Core.Entities.Zone", "zone")
                        .WithMany("Applications")
                        .HasForeignKey("zone_id");

                    b.Navigation("applicant");

                    b.Navigation("approver");

                    b.Navigation("type");

                    b.Navigation("zone");
                });

            modelBuilder.Entity("UMS.Core.Entities.User", b =>
                {
                    b.HasOne("UMS.Core.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("role_id");

                    b.HasOne("UMS.Core.Entities.Zone", "Zone")
                        .WithMany("Users")
                        .HasForeignKey("zone_id");

                    b.Navigation("Role");

                    b.Navigation("Zone");
                });

            modelBuilder.Entity("UMS.Core.Entities.ApplicationType", b =>
                {
                    b.Navigation("Applications");
                });

            modelBuilder.Entity("UMS.Core.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("UMS.Core.Entities.User", b =>
                {
                    b.Navigation("ApplicationsApplicant");

                    b.Navigation("ApplicationsApproved");
                });

            modelBuilder.Entity("UMS.Core.Entities.Zone", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
