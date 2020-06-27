using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logyca.Models
{
    public class Code
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Owner { get; set; }
        [Required, MaxLength(100), StringLength(110)]
        public string Name { get; set; }

        [ForeignKey("Owner")]
        public Enterprise Enterprise { get; set; }
    }
}
