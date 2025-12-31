namespace Domyra.Application.Features.Widgets.Queries;

using Contracts.Repositories;
using Models;
using MediatR;

public sealed class GetWidgetsQueryHandler :
    IRequestHandler<GetWidgetsQuery, IEnumerable<WidgetModel>>
{
    private readonly IWidgetRepository _widgetRepository;
    
    public GetWidgetsQueryHandler(IWidgetRepository widgetRepository)
    {
        _widgetRepository = widgetRepository;
    }

    public async Task<IEnumerable<WidgetModel>> Handle(GetWidgetsQuery request, CancellationToken cancellationToken)
    { 
        var devices = await _widgetRepository.FetchAllAsync(request.Parameters, cancellationToken);
        
        return devices.Select(WidgetModel.From);
    }
}