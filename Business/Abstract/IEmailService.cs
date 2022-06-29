﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract;

public interface IEmailService
{
    void Send(string to, string subject, string content, string from = null);
}
