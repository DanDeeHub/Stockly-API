using System.Windows.Input;
using MediatR;

namespace Stockly.Api.Commands.Base;

public interface IBaseCommand<out TResponse> : IRequest<TResponse>;