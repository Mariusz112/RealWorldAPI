using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RealWorldApp.Commons.Entities;
using RealWorldApp.Commons.Interfaces;
using RealWorldApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldApp.BAL.Services
{
    public class TagsService : ITagsService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IArticleRepositorie articleRepositorie;
        private readonly UserManager<User> _userManager;
        private readonly ITagsRepositorie _tagsRepositorie;

        public TagsService(ILogger<UserService> logger, IMapper mapper, AuthenticationSettings authenticationSettings, IArticleRepositorie articleRepositorie, UserManager<User> userManager, ITagsRepositorie tagsRepositorie)
        {
            _logger = logger;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
            this.articleRepositorie = articleRepositorie;
            _userManager = userManager;
            _tagsRepositorie = tagsRepositorie;
        }
    }
}
