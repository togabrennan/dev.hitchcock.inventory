using core.Data;
using core.Domain;
using core.Queries;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Gets item details
/// </summary>
public class GetItemDetailHandler : IRequestHandler<GetItemDetailQuery, Item>
{
    private readonly IPersistence<Item> _itemStore;

    public GetItemDetailHandler(IPersistence<Item> jsonStore) => _itemStore = jsonStore;

    public async Task<Item> Handle(
        GetItemDetailQuery request,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() => _itemStore.GetById(request.id));
    }
}