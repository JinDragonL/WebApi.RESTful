using System.Threading.Tasks;

namespace WebApiRestful.Infrastructure.CommonService
{
    public interface IEmailTemplateReader
    {
        Task<string> GetTemplate(string templateName);
    }
}