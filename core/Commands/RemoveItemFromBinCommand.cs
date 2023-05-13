using MediatR;
using core.Domain;

namespace core.Commands;

public record RemoveItemFromBinCommand(Bin bin, Item item) : IRequest<Bin>;