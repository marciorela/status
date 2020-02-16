using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.Entities
{
    public class BaseEntity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public DateTime DataInc { get; set; } = DateTime.Now;
        
        [Required]
        public DateTime DataAlt { get; set; } = DateTime.Now;
    }
}
