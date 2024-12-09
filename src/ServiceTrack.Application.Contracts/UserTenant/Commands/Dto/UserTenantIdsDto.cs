using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTrack.Application.Contracts.UserTenant.Commands.Dto;
public class UserTenantIdsDto
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
}
