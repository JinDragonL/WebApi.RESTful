using System.Threading;
using System.Threading.Tasks;
using WebApiRestful.Domain.Model;

namespace WebApi.Restful.Core.Abstract
{
    public interface IEmailHelper
    {
        Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}