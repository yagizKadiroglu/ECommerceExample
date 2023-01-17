using Application.Services.Auth;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<AccessToken>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessToken>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
            if (!HashingHelper.VerifyPasswordHash(request.UserForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException("HATA");

            AccessToken accessToken = await _authService.CreateAccessToken(user);
            return accessToken;
        }
    }
}
