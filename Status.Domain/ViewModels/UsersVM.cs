using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.ViewModels
{
    public class UsersVM
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

    }

    public class UserNewVM
    {
        [Required(ErrorMessage = "Nome deve ser informado")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email deve ser informado")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha deve ser informada")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        public string ReSenha { get; set; }
    }
}
