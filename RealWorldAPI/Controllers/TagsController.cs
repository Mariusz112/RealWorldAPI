using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealWorldApp.Commons.Interfaces;

namespace RealWorldAPI.Controllers
{
    [Authorize]
    [Route("api")]
    //  [ApiController]
    public class TagsController : Controller
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }
    }
}
