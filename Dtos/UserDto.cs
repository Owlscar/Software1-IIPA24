using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Software1_IIPA24.Dtos
{
    public class UserDto
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public int IdState { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Response { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}