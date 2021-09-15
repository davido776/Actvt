using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Actvt.Application.Core;
using Actvt.Application.Interfaces;
using Actvt.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Actvt.Application.Photos
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id {get;set;}
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            public DataContext _context { get; }
            public IPhotoAccessor _photoAccessor { get; }
            public IUserAccessor _userAccessor { get; }
            public Handler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _photoAccessor = photoAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user  = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

                if(photo == null) return null;

                if(photo.IsMain) return Result<Unit>.Failure("You cannot delete your main photo");

                var result = await _photoAccessor.DeletePhoto(photo.Id);

                if(result == null) return Result<Unit>.Failure("problem deleting photo from cloudinary");

                user.Photos.Remove(photo);

                var success = await _context.SaveChangesAsync() > 0;

                if(success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("problem deleting photo from cloudinary");
            }
        }
    }
}