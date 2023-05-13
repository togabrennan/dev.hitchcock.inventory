using core.Data;
using core.Domain;
using core.Queries;
using core.Commands;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Add item event handler
/// </summary>
public class AddItemHandler : IRequestHandler<AddItemCommand, Item>
{ 
    private readonly IPersistence<Item> _itemStore;

    public AddItemHandler(IPersistence<Item> jsonStore) => _itemStore = jsonStore;

    public Task<Item> Handle(
        AddItemCommand request,
        CancellationToken cancellationToken)
    {
        var existingItem = _itemStore.GetAll().FirstOrDefault(i => i.Id == request.item.Id);
        if (existingItem != null)
        {
            _itemStore.Delete(existingItem.Id);
        }
        _itemStore.Add(request.item);

        return Task.Run(() => request.item);
    }
}