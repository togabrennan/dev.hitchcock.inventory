using MediatR;
using core.Domain;

namespace core.Commands;

public record AddItemToBinCommand(Item item, Bin bin) : IRequest<Bin>;