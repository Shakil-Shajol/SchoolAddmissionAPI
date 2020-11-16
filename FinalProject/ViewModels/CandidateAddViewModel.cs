using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class CandidateAddViewModel
    {
        //public int Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthRegistrationNo { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PreviousSchool { get; set; }
        public IFormFile TC { get; set; }      //File
        public IFormFile Image { get; set; }   //Image
        public double LastPublicExamGPA { get; set; }
        public string Address { get; set; }
        public int StanderdClassId { get; set; }
        public int ExamId { get; set; }
    }
}
