using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contract.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        
        public string Fullname { get;  set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get;  set; }

        public long RoleId { get;  set; }

        public IFormFile ProfilePhoto { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
