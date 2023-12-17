using Entities.Dtos;

namespace UI.Models
{
    public class RegisterModel
    {
        public UserForRegisterDto UserForRegister { get; set; }
        public string? Message { get; set; }
    }
}
