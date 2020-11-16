using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class AddmissionContext:DbContext
    {
        public AddmissionContext(DbContextOptions<AddmissionContext> options):base(options)
        {

        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamCenter> ExamCenters { get; set; }
        public DbSet<ExamSubject> ExamSubjects { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomCandidate> RoomCandidates { get; set; }
        public DbSet<SelectedCandidate> SelectedCandidates { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<StanderdClass> StanderdClasses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectWiseResult> SubjectWiseResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomCandidate>()
                .HasKey(x => new { x.ExamId, x.CandidateId });
            modelBuilder.Entity<RoomCandidate>()
                .HasOne(e => e.Exam)
                .WithMany(e => e.Candidates)
                .HasForeignKey(e => e.ExamId);
            modelBuilder.Entity<RoomCandidate>()
                .HasOne(e => e.Candidate)
                .WithMany(e => e.Exams)
                .HasForeignKey(e => e.CandidateId);
            modelBuilder.Entity<ExamSubject>()
                .HasKey(x => new { x.ExamId, x.SubjectId });
            modelBuilder.Entity<ExamSubject>()
                .HasOne(e => e.Exam)
                .WithMany(e => e.Subjects)
                .HasForeignKey(e => e.ExamId);
            modelBuilder.Entity<ExamSubject>()
                .HasOne(e => e.Subject)
                .WithMany(e => e.Exams)
                .HasForeignKey(e => e.SubjectId);
            modelBuilder.Entity<SelectedCandidate>()
                .HasKey(x => new { x.ExamId, x.CandidateId });
            modelBuilder.Entity<SelectedCandidate>()
                .HasOne(e => e.Exam)
                .WithMany(e => e.SelectedCandidates)
                .HasForeignKey(e => e.ExamId);
            modelBuilder.Entity<SelectedCandidate>()
                .HasOne(e => e.Candidate)
                .WithMany(e => e.SelectedExams)
                .HasForeignKey(e => e.CandidateId);
            modelBuilder.Entity<SubjectWiseResult>()
                .HasKey(x => new { x.ExamId, x.CandidateId,x.SubjectId });
            modelBuilder.Entity<SubjectWiseResult>()
                .HasOne(e => e.Exam)
                .WithMany(e => e.SubjectWiseResults)
                .HasForeignKey(e => e.ExamId);
            modelBuilder.Entity<SubjectWiseResult>()
                .HasOne(e => e.Candidate)
                .WithMany(e => e.SubjectWiseResults)
                .HasForeignKey(e => e.CandidateId);
            modelBuilder.Entity<SubjectWiseResult>()
                .HasOne(e => e.Subject)
                .WithMany(e => e.SubjectWiseResults)
                .HasForeignKey(e => e.SubjectId);
        }

    }
}
