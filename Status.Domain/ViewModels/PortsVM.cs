using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.ViewModels
{
    public class PortsVM
    {
        [Required]
        public int PortNumber { get; set; }
    
        [Required]
        public int CheckInterval { get; set; }
    }

    public class PortCheckedVM
    {
        [Required]
        public Guid PortId { get; set; }

        [Required]
        public DateTime DataChecked { get; set; } = DateTime.Now;

        [Required]
        public bool Status { get; set; } = true;

        public double TimeMS { get; set; }

        public string Obs { get; set; }
    }

    public class PortStatusVM
    {
        public Guid PortId { get; set; }

        public int PortNumber { get; set; }

        public string Host { get; set; }

        public bool Active { get; set; }

        public int CheckInterval { get; set; }

        public bool Status { get; set; }

        public DateTime LastChecked { get; set; }

    }

}