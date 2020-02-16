using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.Entities
{
    public class Servidor : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Host { get; set; }

        [Required]
        public int CheckInterval { get; set; }

        [Required] // FK -> USUARIO
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public virtual ICollection<Porta> Portas { get; set; }
    }
}
