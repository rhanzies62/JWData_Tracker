using JWDataTracker.Application;
using JWDataTracker.Application.Publisher;
using JWDataTracker.Domain.Grid;
using JWDataTracker.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWDataTracker.Api.Controllers
{
    
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;
        public PublisherController(IAuthenticatedUser currentUser, IPublisherService publisherService) //: base(currentUser)
        {
            this.publisherService = publisherService;
        }

        [HttpPost]
        [Route("AddEdit")]
        public Response AddEdit(PublisherDto model)
        {
            if (model.PublisherId == 0) return publisherService.Add(model);
            else return publisherService.Edit(model);
        }

        [HttpPost]
        [Route("ListPublishers")]
        public GridResultGeneric<PublisherGridDto> ListPublishers(GridFilter filter)
        {
            return publisherService.ListPublishers(filter);
        }

    }
}
