using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaintenanceChronicle.Infrastructure;

public interface IServiceInstaller
{
    int Order { get; }
    void Install(IServiceCollection services, IConfiguration configuration);
}