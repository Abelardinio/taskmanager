﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.DbConnection;

namespace TaskManager.DbConnection.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190324122327_F16_InitialCreate")]
    partial class F16_InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskManager.DbConnection.Entities.FeatureEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("TaskManager.DbConnection.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskManager.DbConnection.Entities.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Added");

                    b.Property<string>("Description");

                    b.Property<int?>("FeatureId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Priority");

                    b.Property<int>("Status");

                    b.Property<DateTime>("TimeToComplete");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskManager.DbConnection.Entities.FeatureEntity", b =>
                {
                    b.HasOne("TaskManager.DbConnection.Entities.ProjectEntity", "Project")
                        .WithMany("Features")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaskManager.DbConnection.Entities.TaskEntity", b =>
                {
                    b.HasOne("TaskManager.DbConnection.Entities.FeatureEntity", "Feature")
                        .WithMany("Tasks")
                        .HasForeignKey("FeatureId");
                });
#pragma warning restore 612, 618
        }
    }
}
