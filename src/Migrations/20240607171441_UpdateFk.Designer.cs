﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RulesEnginePOC;

#nullable disable

namespace RulesEnginePOC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240607171441_UpdateFk")]
    partial class UpdateFk
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RulesEnginePOC.Models.Entitlement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Entitlement");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "BaseVehicleId"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ContentSilo"
                        },
                        new
                        {
                            Id = 3,
                            Name = "PartsForEstimating"
                        },
                        new
                        {
                            Id = 99,
                            Name = "NotRealEntitlement"
                        });
                });

            modelBuilder.Entity("RulesEnginePOC.Models.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Actions")
                        .HasColumnType("jsonb")
                        .HasColumnName("actions");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Criteria")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("criteria");

                    b.Property<int>("EntitlementId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("ParentRuleId")
                        .HasColumnType("integer");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EntitlementId");

                    b.HasIndex("ParentRuleId");

                    b.ToTable("Rule");
                });

            modelBuilder.Entity("RulesEnginePOC.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RulesEnginePOC.Models.UserRule", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RuleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "RuleId");

                    b.HasIndex("RuleId");

                    b.ToTable("UserRule");
                });

            modelBuilder.Entity("RulesEnginePOC.Models.Rule", b =>
                {
                    b.HasOne("RulesEnginePOC.Models.Entitlement", "Entitlement")
                        .WithMany()
                        .HasForeignKey("EntitlementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RulesEnginePOC.Models.Rule", null)
                        .WithMany("ChildRules")
                        .HasForeignKey("ParentRuleId");

                    b.Navigation("Entitlement");
                });

            modelBuilder.Entity("RulesEnginePOC.Models.UserRule", b =>
                {
                    b.HasOne("RulesEnginePOC.Models.Rule", "Rule")
                        .WithMany()
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RulesEnginePOC.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rule");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RulesEnginePOC.Models.Rule", b =>
                {
                    b.Navigation("ChildRules");
                });
#pragma warning restore 612, 618
        }
    }
}