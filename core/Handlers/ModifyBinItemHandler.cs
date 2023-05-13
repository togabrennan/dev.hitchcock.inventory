using core.Data;
using core.Domain;
using core.Queries;
using core.Commands;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Modify bin item event handler
/// </summary>
public class ModifyBinItemHandler : IRequestHandler<ModifyBinItemCommand, Bin>
{
    private readonly IPersistence<Bin> _binStore;

    public ModifyBinItemHandler(IPersistence<Bin> jsonStore) => _binStore = jsonStore;

    public Task<Bin> Handle(
        ModifyBinItemCommand request,
        CancellationToken cancellationToken)
    {
        if (HasEnoughItemsToModify(request.bin, request.item, request.item.Quantity))
        {
            request.bin.RemoveItem(request.item);
            request.bin.AddItem(request.item);
            _binStore.Update(request.bin);
        }

        return Task.Run(() => request.bin);
    }

    private bool HasEnoughItemsToModify(Bin bin, Item item, int quantityToRemove)
    {
        return true;
    }
}