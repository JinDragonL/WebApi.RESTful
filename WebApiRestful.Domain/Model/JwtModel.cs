using System;

namespace WebApiRestful.Domain.Model
{
    public class JwtModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
