using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Logyca.Models
{
    public class Enterprise
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100), StringLength(110)]
        public string Name { get; set; }
        [Required]
        public int Nit { get; set; }
        [Required]
        public int Gln { get; set; }
        public ICollection<Code> Code { get; set; }

    }
}
