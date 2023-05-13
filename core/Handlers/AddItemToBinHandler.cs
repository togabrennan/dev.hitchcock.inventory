using core.Data;
using core.Domain;
using core.Queries;
using core.Commands;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Add item to bin event handler
/// </summary>
public class AddItemToBinHandler : IRequestHandler<AddItemToBinCommand, Bin>
{ 
    private readonly IPersistence<Bin> _binStore;

    public AddItemToBinHandler(IPersistence<Bin> jsonStore) => _binStore = jsonStore;

    public Task<Bin> Handle(
        AddItemToBinCommand request,
        CancellationToken cancellationToken)
    {
        var existingItem = request.bin.Items.Where(p => p.Id == request.item.Id).FirstOrDefault();
        if (existingItem != null)
        {
            request.bin.RemoveItem(existingItem);
        }
        request.bin.AddItem(request.item);
        _binStore.Update(request.bin);

        return Task.Run(() => request.bin);
    }
}