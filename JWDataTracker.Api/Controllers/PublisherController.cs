using JWDataTracker.Application;
using JWDataTracker.Application.Publisher;
using JWDataTracker.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWDataTracker.Api.Controllers
{
    [Route("[controller]")]
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

    }
}
