using core.Data;
using core.Domain;
using core.Queries;
using core.Commands;
using MediatR;

namespace core.Handlers;

/// <summary>
/// Remove Item from Bin Event handler
/// </summary>
public class RemoveItemFromBinHandler : IRequestHandler<RemoveItemFromBinCommand, Bin>
{
    private readonly IPersistence<Bin> _binStore;

    public RemoveItemFromBinHandler(IPersistence<Bin> jsonStore) => _binStore = jsonStore;

    public async Task<Bin> Handle(RemoveItemFromBinCommand request, CancellationToken cancellationToken)
    {
        request.bin.RemoveItem(request.item);
        _binStore.Update(request.bin);

        return await Task.Run(() => request.bin);
    }
}