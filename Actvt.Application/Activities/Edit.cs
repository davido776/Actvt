using System.Threading;
using System.Threading.Tasks;
using Actvt.Domain;
using Actvt.Persistence;
using AutoMapper;
using MediatR;

namespace Actvt.Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public readonly IMapper _mapper;

            public Handler(DataContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.activity.Id);

                _mapper.Map(request.activity, activity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}