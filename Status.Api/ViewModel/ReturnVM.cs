﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Api.ViewModel
{
    public class ReturnIdVM
    {
        public Guid Id { get; set; }
    }

    public class ReturnErrorVM
    {
        public string ErrorMessage { get; set; }
    }
}
