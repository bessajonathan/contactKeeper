using AutoMapper;
using ContactKeeperApi.Application.Contact.ViewModel;
using ContactKeeperApi.Application.Interfaces;
using ContactKeeperApi.Application.ViewModels;
using ContactKeeperApi.Common.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace ContactKeeperApi.Application.Contact.Queries.GetContacts
{
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IListViewModel<ContactViewModel>>
    {
        private readonly IContactKeeperContext context;
        private readonly IMapper mapper;

        public GetContactsQueryHandler(IContactKeeperContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<IListViewModel<ContactViewModel>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var data = await context.Contacts
                .Include(y => y.User)
                .OrderBy(x => request.OrderBy)
                .Where(x => x.UserId == request.UserId)
                .ToPagedListAsync(request.Page, request.PageSize);

            if (data.Count == 0)
                throw new NotFoundException("Dados não econtrados.");

            return new ListViewModel<ContactViewModel>
            {
                Data = mapper.Map<IEnumerable<ContactViewModel>>(data),
                HasNext = data.HasNextPage,
                TotalItemCount = data.TotalItemCount
            };
        }
    }
}
