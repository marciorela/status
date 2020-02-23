using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Api.ViewModel
{
    public class PortAddVM
    {
        public Guid ServerId { get; set; }

        public int Port { get; set; }
    }
}
