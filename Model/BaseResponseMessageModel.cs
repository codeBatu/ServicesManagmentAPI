﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseResponseMessageModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}
