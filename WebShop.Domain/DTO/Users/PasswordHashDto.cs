using WebShop.Domain.Entities;

namespace WebShop.Domain.DTO.Users
{
    public class PasswordHashDto
    {
        public User User { get; set; }

        public string Hash { get; set; }

    }
}
