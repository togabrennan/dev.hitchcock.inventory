using core.Data;
using core.Domain;
using core.Queries;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Gets bin details
/// </summary>
public class GetBinDetailHandler : IRequestHandler<GetBinDetailQuery, Bin>
{
    private readonly IPersistence<Bin> _binStore;

    public GetBinDetailHandler(IPersistence<Bin> jsonStore) => _binStore = jsonStore;

    public async Task<Bin> Handle(
        GetBinDetailQuery request,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() => _binStore.GetById(request.id));
    }
}