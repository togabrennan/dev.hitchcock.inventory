using MediatR;
using core.Domain;

namespace core.Commands;

public record DeleteItemCommand(Guid id) : IRequest<bool>;