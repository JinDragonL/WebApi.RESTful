using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace WebApiRestful.Infrastructure.CommonService
{
    public class EmailTemplateReader : IEmailTemplateReader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailTemplateReader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> GetTemplate(string templateName)
        {
            string templateEmail = Path.Combine(_webHostEnvironment.ContentRootPath, templateName);

            string content = await File.ReadAllTextAsync(templateEmail);

            return content;
        }
    }
}
