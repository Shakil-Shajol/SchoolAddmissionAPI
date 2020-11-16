﻿// <auto-generated />
using System;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinalProject.Migrations
{
    [DbContext(typeof(AddmissionContext))]
    partial class AddmissionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalProject.Models.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("BirthRegistrationNo")
                        .IsRequired()
                        .HasMaxLength(17);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<string>("ImagePath");

                    b.Property<double>("LastPublicExamGPA");

                    b.Property<string>("MotherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("PreviousSchool");

                    b.Property<int>("StanderdClassId");

                    b.Property<string>("TCPath");

                    b.HasKey("CandidateId");

                    b.HasIndex("StanderdClassId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("FinalProject.Models.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExamCode");

                    b.Property<DateTime>("ExamDate");

                    b.Property<int>("FullMark");

                    b.Property<int>("PassMark");

                    b.Property<int>("SessionId");

                    b.HasKey("ExamId");

                    b.HasIndex("SessionId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("FinalProject.Models.ExamCenter", b =>
                {
                    b.Property<int>("CenterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CenterName");

                    b.HasKey("CenterId");

                    b.ToTable("ExamCenters");
                });

            modelBuilder.Entity("FinalProject.Models.ExamSubject", b =>
                {
                    b.Property<int>("ExamId");

                    b.Property<int>("SubjectId");

                    b.Property<int>("Mark");

                    b.Property<int>("PassMark");

                    b.HasKey("ExamId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ExamSubjects");
                });

            modelBuilder.Entity("FinalProject.Models.Notice", b =>
                {
                    b.Property<int>("NoticeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HeadLine");

                    b.Property<string>("NoticeBody");

                    b.Property<string>("NoticeNo");

                    b.Property<DateTime>("PublishDate");

                    b.HasKey("NoticeID");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("FinalProject.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity");

                    b.Property<int>("CenterId");

                    b.Property<string>("RoomNo");

                    b.HasKey("RoomId");

                    b.HasIndex("CenterId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("FinalProject.Models.RoomCandidate", b =>
                {
                    b.Property<int>("ExamId");

                    b.Property<int>("CandidateId");

                    b.Property<int>("RoomId");

                    b.HasKey("ExamId", "CandidateId");

                    b.HasAlternateKey("CandidateId", "ExamId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomCandidates");
                });

            modelBuilder.Entity("FinalProject.Models.SelectedCandidate", b =>
                {
                    b.Property<int>("ExamId");

                    b.Property<int>("CandidateId");

                    b.Property<bool>("IsAdmitted");

                    b.Property<bool>("IsInWatingList");

                    b.HasKey("ExamId", "CandidateId");

                    b.HasAlternateKey("CandidateId", "ExamId");

                    b.ToTable("SelectedCandidates");
                });

            modelBuilder.Entity("FinalProject.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SessionYear");

                    b.HasKey("SessionId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("FinalProject.Models.StanderdClass", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassName");

                    b.HasKey("ClassId");

                    b.ToTable("StanderdClasses");
                });

            modelBuilder.Entity("FinalProject.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SubjectName");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("FinalProject.Models.SubjectWiseResult", b =>
                {
                    b.Property<int>("ExamId");

                    b.Property<int>("CandidateId");

                    b.Property<int>("SubjectId");

                    b.Property<bool>("IsPass");

                    b.Property<double>("ObtainMarks");

                    b.HasKey("ExamId", "CandidateId", "SubjectId");

                    b.HasAlternateKey("CandidateId", "ExamId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SubjectWiseResults");
                });

            modelBuilder.Entity("FinalProject.Models.Candidate", b =>
                {
                    b.HasOne("FinalProject.Models.StanderdClass", "DesiredClass")
                        .WithMany("Candidates")
                        .HasForeignKey("StanderdClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.Exam", b =>
                {
                    b.HasOne("FinalProject.Models.Session", "Session")
                        .WithMany("Exams")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.ExamSubject", b =>
                {
                    b.HasOne("FinalProject.Models.Exam", "Exam")
                        .WithMany("Subjects")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.Subject", "Subject")
                        .WithMany("Exams")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.Room", b =>
                {
                    b.HasOne("FinalProject.Models.ExamCenter", "ExamCenter")
                        .WithMany("Rooms")
                        .HasForeignKey("CenterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.RoomCandidate", b =>
                {
                    b.HasOne("FinalProject.Models.Candidate", "Candidate")
                        .WithMany("Exams")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.Exam", "Exam")
                        .WithMany("Candidates")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.Room", "Room")
                        .WithMany("RoomCandidates")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.SelectedCandidate", b =>
                {
                    b.HasOne("FinalProject.Models.Candidate", "Candidate")
                        .WithMany("SelectedExams")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.Exam", "Exam")
                        .WithMany("SelectedCandidates")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProject.Models.SubjectWiseResult", b =>
                {
                    b.HasOne("FinalProject.Models.Candidate", "Candidate")
                        .WithMany("SubjectWiseResults")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.Exam", "Exam")
                        .WithMany("SubjectWiseResults")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProject.Models.Subject", "Subject")
                        .WithMany("SubjectWiseResults")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
