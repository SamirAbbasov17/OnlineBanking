using Application.BlogItems.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BlogItems.Queries.Request
{
    public class GetByIdBlogQueryRequest : IRequest<GetByIdBlogQueryResponse>
    {
        public int Id { get; set; }
    }
}
