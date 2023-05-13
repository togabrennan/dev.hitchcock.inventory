using MediatR;
using core.Domain;

namespace core.Commands;

public record ModifyBinItemCommand(Bin bin, Item item) : IRequest<Bin>;