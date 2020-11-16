using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Notice
    {
        public int NoticeID { get; set; }
        public DateTime PublishDate { get; set; }
        public string NoticeNo { get; set; }
        public string HeadLine { get; set; }
        public string NoticeBody { get; set; }
    }
}
