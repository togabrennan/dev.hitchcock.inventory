using core.Data;
using core.Domain;
using core.Queries;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Gets bins, currently from a mocked bin store but we can update the model
/// and this handler when we have the final specificaton from Smart Rack
/// </summary>
public class GetBinsHandler : IRequestHandler<GetBinsQuery, IEnumerable<Bin>>
{
    private readonly IPersistence<Bin> _binStore;

    public GetBinsHandler(IPersistence<Bin> jsonStore) => _binStore = jsonStore;

    public async Task<IEnumerable<Bin>> Handle(
        GetBinsQuery request,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() => _binStore.GetAll());
    }
}