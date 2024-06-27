using EasyArchitect.OutsideControllerBase;
using EasyArchitect.OutsideManaged.AuthExtensions.Models;
using EasyArchitectCore.Core;
using EasyArchitectV2UIApiWeb9.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace EasyArchitectV2UIApiWeb9.Controllers
{
    /// <summary>
    /// EasyArchitect V2 WebApp Home Controller 範例程式
    /// </summary>
    public class HomeController : OutsideBaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 示範首頁（需要登入）
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext!.Request.Cookies.TryGetValue(UserInfo.LOGIN_USER_INFO, out string? accountJSON);
            _httpContextAccessor.HttpContext.Session.SetString(UserInfo.LOGIN_USER_INFO, accountJSON!);

            return View();
        }

        /// <summary>
        /// 隱私權畫面
        /// </summary>
        /// <returns></returns>
        [NeedLogin]
        public IActionResult Privacy()
        {
            ViewBag.Account = JsonSerializer.Deserialize<AuthenticateRequest>(_httpContextAccessor!.HttpContext!.Session.GetString(UserInfo.LOGIN_USER_INFO)!);

            return View();
        }

        /// <summary>
        /// 登入系統畫面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 執行登入作業
        /// </summary>
        /// <param name="authenticateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(AuthenticateRequest authenticateRequest)
        {
            if (ModelState.IsValid)
#pragma warning disable S125 // Sections of code should not be commented out
            {
                // 請在這進行 UserName & Password 的判別，如果身分證卻才進行 ProcessLogin() 的作業以取得 Token
                // 下面的 CheckUser() 只是範例程式，請自行修改
                /* if (CheckUser(authenticateRequest.Username, authenticateRequest.Password))
                { */
                if (ProcessLogin(authenticateRequest))
                {
                    return RedirectToAction("Index");
                }
                /* } */
            }
#pragma warning restore S125 // Sections of code should not be commented out

            return View();
        }

        /// <summary>
        /// 登出系統
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Logout()
        {
            return View();
        }

        /// <summary>
        /// 登出系統
        /// </summary>
        /// <param name="forms"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout(IFormCollection forms)
        {
            return await base.LogoutProcess();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}