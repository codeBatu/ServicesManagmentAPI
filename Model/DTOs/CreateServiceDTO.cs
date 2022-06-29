using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs;

public class CreateServiceDTO
{
    public string? ServiceName { get; set; }
    public ServiceStatusEnum? ServiceStatus { get; set; }
    public string? Version { get; set; }
}
