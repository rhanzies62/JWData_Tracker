using JWDataTracker.Domain.Grid;
using JWDataTracker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.Publisher
{
    public interface IPublisherService
    {
        Response Add(PublisherDto model);
        Response Edit(PublisherDto model);
        IEnumerable<PublisherDto> List();
        Response Delete(long publisherId);
        PublisherDto GetById(int publisherId);
        GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter);
    }
}
