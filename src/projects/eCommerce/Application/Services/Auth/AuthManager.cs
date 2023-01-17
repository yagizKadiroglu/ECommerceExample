using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Auth
{
    public class AuthManager : IAuthService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly ITokenHelper _tokenHelper;


        public AuthManager(IUserOperationClaimRepository userOperationClaimRepository, ITokenHelper tokenHelper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IList<OperationClaim> operationClaims = await _userOperationClaimRepository
                .Query()
                .AsNoTracking()
                .Where(p => p.UserId == user.Id)
                .Select(p => new OperationClaim
                {
                    Id = p.OperationClaimId,
                    Name = p.OperationClaim.Name
                })
                .ToListAsync();

            AccessToken accessToken = _tokenHelper.CreateToken(user, operationClaims);
            return accessToken;
        }
    }
}
