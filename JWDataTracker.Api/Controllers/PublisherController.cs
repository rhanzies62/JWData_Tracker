using JWDataTracker.Application;
using JWDataTracker.Application.Publisher;
using JWDataTracker.Domain.Grid;
using JWDataTracker.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

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
    }
}
