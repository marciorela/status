using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.ViewModels
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

    public class ServersAllVM
    {
        public Guid UserId { get; set; }

        public Guid ServerId { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public int CheckInterval { get; set; }

        public DateTime LastChecked { get; set; }
    }
}
