using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Api.ViewModel
{
    public class ServersVM
    {
        public Guid Id { get; set; } = new Guid();

        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public int CheckInterval { get; set; }
    }
}
