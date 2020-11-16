using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ExamCenter
    {
        [Key]
        public int CenterId { get; set; }
        public string CenterName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

    }
}
