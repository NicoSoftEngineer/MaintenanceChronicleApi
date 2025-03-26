using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaintenanceChronicle.Utilities.Constants;

namespace MaintenanceChronicle.Data.Entities.Business;
[Table(nameof(EmailMessage))]
public class EmailMessage
{
    public Guid Id { get; set; }
    [MaxLength(StringLengthConstants.MaxEmailLength)]
    public string RecipientEmail { get; set; } = null!;
    [MaxLength(StringLengthConstants.MaxNameLength)]
    public string? RecipientName { get; set; }
    [MaxLength(StringLengthConstants.MaxNameLength)]
    public string Subject { get; set; } = null!;
    [MaxLength(StringLengthConstants.MaxEmailBodyLength)]
    public string Body { get; set; } = null!;
    public bool Sent { get; set; }
    public Instant CreatedAt { get; set; }
    public Instant SendAt { get; set; }
    [MaxLength(StringLengthConstants.MaxEmailLength)]
    public string FromEmail { get; set; } = null!;
    [MaxLength(StringLengthConstants.MaxNameLength)]
    public string FromName { get; set; } = null!;
}
