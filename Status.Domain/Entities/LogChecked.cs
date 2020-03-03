using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.Entities
{
    public class LogChecked
    {
        public Guid Id { get; set; } = new Guid();

        [Required]
        public Guid PortId { get; set; }

        [Required]
        public DateTime DateTimeChecked { get; set; }

        [Required]
        public bool Status { get; set; }

        public double TimeMS { get; set; }

        public string Obs { get; set; }

    }
}
