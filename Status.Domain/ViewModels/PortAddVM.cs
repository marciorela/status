using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Domain.ViewModels
{
    public class PortAddVM
    {
        public Guid ServerId { get; set; }

        public int Port { get; set; }

        public int CheckInterval { get; set; }

    }
}
