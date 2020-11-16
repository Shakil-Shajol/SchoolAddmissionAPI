using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.ViewModels;
using FinalProject.Models;

namespace FinalProject.Profiles
{
    public class CandidatesProfile:Profile
    {
        public CandidatesProfile()
        {
            CreateMap<CandidateAddViewModel, Candidate>();
        }
    }

    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<ExamAddViewModel, Exam>();
        }
    }
}
