using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PrzepisyDlaCiebie.Models
{
    public class Przepis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(100)")]
        [DisplayName("Nazwa")]
        public string NazwaPrzepisu { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(4000)")]
        [DisplayName("Tresc")]
        public string TrescPrzepisu { get; set; }

      
        [Column(TypeName = "nvarchar(1000)")]
        [DisplayName("NazwaZdjecia")]
        public string NazwaZdjecia { get; set; }

        [NotMapped]
        [Column(TypeName = "nvarchar(1000)")]
        [DisplayName("Image File")]
        public IFormFile Image { get; set; }

    }
}
