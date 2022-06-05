using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS6.Models
{
    public class Category
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public int id { get; set; }

        [Display(Name = "Skrót")]
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string shortName { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string longName { get; set; }
    }
}
