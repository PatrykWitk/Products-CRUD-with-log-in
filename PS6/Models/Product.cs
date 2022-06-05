using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PS6.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public int id { get; set; }
        //
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string name { get; set; }
        //
        [Display(Name = "Cena")]
        [Required]
        [Range(0, int.MaxValue)]
        public decimal price { get; set; }
        //
        [Display(Name = "Kategoria")]
        public string desc { get; set; }
        //
        [Display(Name = "Id category")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public int categoryId { get; set; }

    }
}
