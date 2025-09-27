using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingApp.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public bool Gender { get; set; }
        public int DivisiId { get; set; }
        public string Note { get; set; }
        public DateTime Tanggal { get; set; }
    }
}