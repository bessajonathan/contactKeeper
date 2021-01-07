using System;

namespace ContactKeeperApi.Application.Auth.ViewModel
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Expires { get; set; }
    }
}
