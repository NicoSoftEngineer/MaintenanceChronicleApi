using MaintenanceChronicle.Application.Contracts.EmailMessages.Queries.Dto;
using MaintenanceChronicle.Application.Contracts.Utils.Queries;
using MaintenanceChronicle.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceChronicle.Application.EmailMessages.Queries;

public class GetListOfEmailMessagesQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListOfEntityQuery<EmailMessageInListDto>, List<EmailMessageInListDto>>
{
    public async Task<List<EmailMessageInListDto>> Handle(GetListOfEntityQuery<EmailMessageInListDto> request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.EmailMessages
            .Select(e => e.ToListDto())
            .ToListAsync(cancellationToken);

        return list;
    }
}
