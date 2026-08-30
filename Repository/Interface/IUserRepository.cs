using MB_2.Models;
using MB_2.Models.User;

namespace MB_2.Repository.Interface
{
    public interface IUserRepository
    {
       
        Task<Commonresponse> CreateUserAsync(InputCreateUser input);

        Task<OutputUserDetails> GetUserData(string Email, string Password);
    }
}
