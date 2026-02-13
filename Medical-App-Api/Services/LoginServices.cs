using Medical_App_Api.Data;
using Medical_App_Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Medical_App_Api.Services
{
    public class LoginServices
    {
        private readonly AppDataContext _context;
        private readonly PasswordHasher<LoginAccount> _passwordHasher;

        public LoginServices(AppDataContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<LoginAccount>();
        }

        public async Task AddLoginAccount(string email,string password)
        {
            var account = new LoginAccount
            {
                Email = email,
            };

            account.PasswordHash =
            _passwordHasher.HashPassword(account, password);

            _context.LoginAccounts.Add(account);
            await _context.SaveChangesAsync();
        }

        // i am currious if i should make it return a task of type class or tuple of bool and message or keep throwing exceptions
        public async Task ChangePassword(string email,string oldpassword,string newpassword)
        {
            var account = await _context.LoginAccounts.FirstOrDefaultAsync(a => a.Email == email);

            if(account is null)
            {
                throw new Exception($"no user found with email {email}");
            }

            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, oldpassword);
            if (result == PasswordVerificationResult.Success)
            {
                account.PasswordHash = _passwordHasher.HashPassword(account, newpassword);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("wrong old password try again");
            }

        }

        public async Task DeleteLoginAccount(string email,string password)
        {
            var account = await _context.LoginAccounts.FirstOrDefaultAsync(a => a.Email == email);

            if (account is null)
            {
                throw new Exception($"no user found with email {email}");
            }

            var result = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                _context.LoginAccounts.Remove(account);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("wrong old password try again");
            }
        }
    }
}
