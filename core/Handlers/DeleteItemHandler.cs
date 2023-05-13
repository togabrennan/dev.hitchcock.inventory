using core.Data;
using core.Domain;
using core.Queries;
using core.Commands;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Remove Item event handler
/// </summary>
public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, bool>
{
    private readonly IPersistence<Item> _itemStore;

    public DeleteItemHandler(IPersistence<Item> jsonStore) => _itemStore = jsonStore;

    public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        _itemStore.Delete(request.id);

        return await Task.Run(() => true);
    }
}