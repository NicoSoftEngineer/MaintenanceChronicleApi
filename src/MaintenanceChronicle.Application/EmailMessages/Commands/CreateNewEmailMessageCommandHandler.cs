using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands.Dto;
using MaintenanceChronicle.Data;
using MaintenanceChronicle.Utilities.Options;
using MediatR;
using Microsoft.Extensions.Options;
using NodaTime;

namespace MaintenanceChronicle.Application.EmailMessages.Commands;
/// <summary>
/// Handler for <see cref="CreateNewEmailMessageCommand"/>
/// </summary>
public class CreateNewEmailMessageCommandHandler(AppDbContext dbContext, IClock clock, IOptions<EnvironmentOptions> environmentOptions) : IRequestHandler<CreateNewEmailMessageCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewEmailMessageCommand request, CancellationToken cancellationToken)
    {
        request.NewEmailMessage.FromEmail ??= environmentOptions.Value.SenderEmail;
        request.NewEmailMessage.FromName ??= environmentOptions.Value.SenderName;

        var entity = request.NewEmailMessage.ToEntity(clock.GetCurrentInstant());

        await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
