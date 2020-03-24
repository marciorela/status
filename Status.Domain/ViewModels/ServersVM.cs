using Status.Domain.Entities;
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
        public string Nome { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        public string Host { get; set; }
    }

    public class ServerPortStatusVM
    {
        public Guid Id { get; set; }

        public int Numero { get; set; }

        public bool Status { get; set; }
    }

    public class ServerPortsVM
    {
        public Guid ServerId { get; set; }

        public string Host { get; set; }

        public string Nome { get; set; }

        public List<ServerPortStatusVM> Portas { get; set; } = new List<ServerPortStatusVM>();

        public int PortsOk { get; set; } = 0;

        public int PortsError { get; set; } = 0;

        public bool Status { get; set; }
    }

    public class ServerEditVM
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Host deve ser informado")]
        public string Host { get; set; }

        [Required(ErrorMessage = "Nome do servidor deve ser informado")]
        public string Nome { get; set; }

        public string Portas { get; set; }
    }
}

