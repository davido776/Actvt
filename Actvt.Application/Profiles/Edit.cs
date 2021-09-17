using System.Threading;
using System.Threading.Tasks;
using Actvt.Application.Core;
using Actvt.Application.Interfaces;
using Actvt.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Actvt.Application.Profiles
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string DisplayName { get; set; }
            public string Bio { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor,IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(u=>u.Photos).FirstOrDefaultAsync(u => u.UserName ==_userAccessor.GetUsername());

                user.DisplayName = request.DisplayName ?? user.DisplayName;

                user.Bio = request.Bio ?? user.Bio;

                
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("failed to edit user");

                return Result<Unit>.Success(Unit.Value);
            }
        }


    }
}