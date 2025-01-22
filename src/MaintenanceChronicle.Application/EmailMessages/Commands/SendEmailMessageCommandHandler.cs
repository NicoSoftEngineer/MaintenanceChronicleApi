using MaintenanceChronicle.Api.Options;
using MaintenanceChronicle.Application.Contracts.EmailMessages.Commands;
using MediatR;
using Microsoft.Extensions.Options;

namespace MaintenanceChronicle.Application.EmailMessages.Commands;

public class SendEmailMessageCommandHandler(IOptions<EnvironmentOptions> environmentOptions) : IRequestHandler<SendEmailMessageCommand>
{
    public async Task Handle(SendEmailMessageCommand request, CancellationToken cancellationToken)
    {

    }
}
