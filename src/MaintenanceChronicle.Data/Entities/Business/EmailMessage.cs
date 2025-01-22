using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintenanceChronicle.Data.Entities.Business;
[Table(nameof(EmailMessage))]
public class EmailMessage
{
    public Guid Id { get; set; }
    public string RecipientEmail { get; set; } = null!;
    public string? RecipientName { get; set; }
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public bool Sent { get; set; }
    public Instant CreatedAt { get; set; }
    public string FromEmail { get; set; } = null!;
    public string FromName { get; set; } = null!;
}
