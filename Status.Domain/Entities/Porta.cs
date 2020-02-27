using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.Entities
{
    public class Porta : BaseEntity
    {
        [Required]
        public int Numero { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int CheckInterval { get; set; }

        [Required]  //FK -> SERVIDOR
        public Guid ServidorId { get; set; }
        public Servidor Servidor { get; set; }

    }
}
