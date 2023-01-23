using Entity;
using Entity.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AuthenticationRepository : GenericRepository<Account>, IAuthenticationRepository
    {
        private readonly SoleAuthenticity_DBContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(SoleAuthenticity_DBContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<Account> Authentication(IdToken idToken)
        {
            FirebaseToken decodedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                                            .VerifyIdTokenAsync(idToken.Token);
            Console.WriteLine("Decoded token" + decodedToken);
            string uid = decodedToken.Uid;
            var authUser = new FirebaseAuthProvider(new FirebaseConfig(_configuration.GetSection("API_key").Value));
            var auth = authUser.GetUserAsync(idToken.Token);

            var checkExist = await _dbContext.Accounts
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email.Equals(auth.Result.Email));
            if (checkExist == null)
            {
                Account account = new Account();
                account.Email = auth.Result.Email;
                account.Password = null;
                account.Name = auth.Result.DisplayName;
                account.Avatar = auth.Result.PhotoUrl;
                account.Phone = auth.Result.PhoneNumber;
                account.DateOfBirth = null;
                account.IsActive = true;
                account.RoleId = 3;
                _dbContext.Accounts.Add(account);
                await _dbContext.SaveChangesAsync();
                var checkExistWhenAddingSuccess = await _dbContext.Accounts
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email.Equals(account.Email));
                return checkExistWhenAddingSuccess;
            }
            return checkExist;
        }
    }
}
