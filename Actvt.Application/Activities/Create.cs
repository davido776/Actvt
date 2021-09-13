using System.Threading;
using System.Threading.Tasks;
using Actvt.Domain;
using Actvt.Persistence;
using MediatR;
using FluentValidation;
using Actvt.Application.Core;
using Actvt.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Actvt.Application.Activities
{
    public class Create
    {
        
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity{get;set;}
        }

        public class CommandValidator : AbstractValidator<Command>
        {
             public CommandValidator()
             {
                  RuleFor(x=>x.Activity).SetValidator(new ActivityValidator());
                  
             }
        }

        public class Handler : IRequestHandler<Command,Result<Unit>>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly DataContext _context;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());
                
                var attendee = new ActivityAttendee
                {
                    AppUser = user,
                    Activity = request.Activity,
                    IsHost = true
                };

                request.Activity.Attendees.Add(attendee);

                _context.Activities.Add(request.Activity);
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed to create activity");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}