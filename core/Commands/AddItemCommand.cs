using MediatR;
using core.Domain;

namespace core.Commands;

public record AddItemCommand(Item item) : IRequest<Item>;