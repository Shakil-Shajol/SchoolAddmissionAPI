using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        [Required, StringLength(50), Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required, StringLength(50), Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Required, StringLength(50), Display(Name = "Mother Name")]
        public string MotherName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Date Of Birth")]
        public DateTime BirthDate { get; set; }
        [Required,StringLength(17,MinimumLength =17)]
        public string BirthRegistrationNo { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required, StringLength(20), Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required, StringLength(100), Display(Name = "Email"),EmailAddress]
        public string Email { get; set; }
        public string PreviousSchool { get; set; }
        public string TCPath { get; set; }      //File
        public string ImagePath { get; set; }   //Image
        public double LastPublicExamGPA { get; set; }
        [Required, StringLength(200), Display(Name = "Address")]
        public string Address { get; set; }
        public int StanderdClassId { get; set; }
        public StanderdClass DesiredClass { get; set; }
        public virtual ICollection<RoomCandidate> Exams { get; set; }
        public virtual ICollection<SelectedCandidate> SelectedExams { get; set; }
        public virtual ICollection<SubjectWiseResult> SubjectWiseResults { get; set; }
    }
}
