using JWDataTracker.Application;
using JWDataTracker.Application.Publisher;
using JWDataTracker.Domain.Grid;
using JWDataTracker.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace JWDataTracker.Api.Controllers
{
    
    [ApiController]
    public class PublisherController : BaseController
    {
        private readonly IPublisherService publisherService;
        public PublisherController(IAuthenticatedUser currentUser, IPublisherService publisherService) : base(currentUser)
        {
            this.publisherService = publisherService;
        }

        [HttpPost]
        public Response AddEdit(PublisherDto model)
        {
            if (model.PublisherId == 0) return publisherService.Add(model);
            else return publisherService.Edit(model);
        }

        [HttpPost]
        public GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter)
        {
            return publisherService.ListPublishers(filter);
        }

        [HttpGet]
        public PublisherDto Get(int publisherId)
        {
            PublisherDto model = publisherService.GetById(publisherId);
            return model;

        }

        [HttpGet]
        public Response Delete(long publisherId)
        {
            Response model = publisherService.Delete(publisherId);
            return model;
        }

        [HttpGet]
        public IEnumerable<PublisherDto> List()
        {
            return publisherService.List();
        }

        [HttpPost]
        public GridResultGeneric<PublisherRecentPartGrid> ListPublisherRecentParts(GridFilter filter, int publisherId)
        {
            return publisherService.ListPublisherRecentParts(filter,publisherId);
        }

        [HttpGet]
        public DataSourceResult ListPublisherGrid([DataSourceRequest] DataSourceRequest request)
        {
            return publisherService.List().ToDataSourceResult(request);
        }

        [HttpPost]
        public DataSourceResult ListPublisherRecentPartGrid([DataSourceRequest] DataSourceRequest request, long publisherId)
        {
            return publisherService.ListPublisherRecentPartsGrid(publisherId).ToDataSourceResult(request);
        }
    }
}
