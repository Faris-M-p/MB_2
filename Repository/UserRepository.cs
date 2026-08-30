using MB_2.Models;
using MB_2.Models.Entity;
using MB_2.Models.User;
using MB_2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
namespace MB_2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Commonresponse> CreateUserAsync(InputCreateUser input)
        {
            var query = _context.Users.Where(u => u.Name == input.Name || u.Email == input.Email);
            var  user= await query.FirstOrDefaultAsync();

            if (user != null)
            {
                return 
                    new Commonresponse
                    {
                        Status = false,
                        Message = "User already exists."
                    };
            }

          user = new User
                {
                    Name = input.Name,
                    Password = input.Password,
                    Email = input.Email,
                    IsActive = true,
                    IsDeleted = false
                };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return 
                new Commonresponse
                {
                    Status = true,
                    Message = "User created successfully."
                };
        }

        public async Task<OutputUserDetails> GetUserData(string Email,string password)
        {
            var quiry = _context.Users.Where(u => u.Email == Email && u.Password == password && u.IsDeleted == false);


            var users = await quiry
                .Select(u => new OutputUserDetails
                {
                    FK_User = u.ID_User,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive
                })
                .FirstOrDefaultAsync();
            return users;
        }

    }
}
