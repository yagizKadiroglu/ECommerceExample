using Application.Features.OperationClaims.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries.GetByIdOperationClaim
{
    public class GetByIdOperationClaimQuery : IRequest<OperationClaimDto>
    {
        public int Id { get; set; }

        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;

            public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;

            }


            public async Task<OperationClaimDto> Handle(GetByIdOperationClaimQuery request,
                CancellationToken cancellationToken)
            {


                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(b => b.Id == request.Id);
                OperationClaimDto operationClaimDto = _mapper.Map<OperationClaimDto>(operationClaim);
                return operationClaimDto;
            }
        }
    }
}
