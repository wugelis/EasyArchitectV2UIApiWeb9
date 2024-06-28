using EasyArchitect.OutsideApiControllerBase;
using EasyArchitect.OutsideManaged.AuthExtensions.Attributes;
using EasyArchitect.OutsideManaged.AuthExtensions.Filters;
using EasyArchitect.OutsideManaged.AuthExtensions.Services;
using EasyArchitectCore.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyArchitectV2UIApiWeb9.Controllers
{
    /// <summary>
    /// 外部人員 ApiController 程式碼範本
    /// </summary>
    public class EasyArchitectV2ApiHostController : OutsideBaseApiController
    {
        private readonly IUriExtensions _uriExtensions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        /// <param name="uriExtensions"></param>
        /// <param name="httpContextAccessor"></param>
        public EasyArchitectV2ApiHostController(
            ILogger<OutsideBaseApiController> logger,
            IUserService userService,
            IUriExtensions uriExtensions,
            IHttpContextAccessor httpContextAccessor)
            : base(logger, userService, httpContextAccessor)
        {
            _userService = userService;
            _uriExtensions = uriExtensions;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 範例程式（需要驗證）
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [HttpGet]
        [APIName("GetPersons")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await Task.FromResult(new Person[]
            {
                new Person()
                {
                    ID = 1,
                    Name = "Gelis Wu"
                }
            });
        }

        /// <summary>
        /// 取得 Current Identity Id
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [APIName("GetIdentityId")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<decimal?> GetIdentityId()
        {
            return await Task.FromResult(_userService.IdentityId);
        }

        /// <summary>
        /// 取得 Current Identity Id
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [APIName("GetIdentityUser")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<string> GetIdentityUser()
        {
            return await Task.FromResult(_userService.IdentityUser);
        }
        /// <summary>
        /// Get Hello World Smaple Code.
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [APIName("GetHelloWorld")]
        [ApiLogException]
        [ApiLogonInfo]
        public async Task<string> GetHelloWorldAsync()
        {
            return await Task.FromResult("Hello World");
        }
    }

    /// <summary>
    /// 範例程式：請放置在你的 Models/Dto/VO 專案裡
    /// </summary>
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
