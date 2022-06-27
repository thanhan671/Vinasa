using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vinasa.Models
{
    public class Seminar
    {
        public Seminar()
        {
            SeminarParticipants = new List<SeminarParticipant>();
        }

        public int Id { get; set; }

        [Display(Name = "Tên Hội Nghị")]
        public string Title { get; set; }

        [Display(Name = "Thời Gian Diễn Ra")]
        public string OpenDate { get; set; }

        [Display(Name = "Địa Điểm Diễn Ra")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedUtc { get; set; }

        public List<SeminarParticipant> SeminarParticipants { get; set; }
    }
}