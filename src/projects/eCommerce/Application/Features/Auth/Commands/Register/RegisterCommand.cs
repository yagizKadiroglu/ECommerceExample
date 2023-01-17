using Application.Services.Auth;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<AccessToken>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }
        public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetAsync(u => u.Email == request.UserForRegisterDto.Email) != null)
                throw new BusinessException("Böyle bir kullanıcı mevcut");

            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out byte[] passwordHash,
                out byte[] passwordSalt);

            User createdUser = await _userRepository.AddAsync(new()
            {
                Email = request.UserForRegisterDto.Email,
                FirstName = request.UserForRegisterDto.FirstName,
                LastName = request.UserForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            });

            AccessToken accessToken = await _authService.CreateAccessToken(createdUser);

            return accessToken;

        }
    }
}
