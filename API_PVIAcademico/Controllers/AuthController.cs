using API_PVIAcademico.DTO;
using API_PVIAcademico.Helpers;
using API_PVIAcademico.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace API_PVIAcademico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors ("All")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceAuth _serviceAuth;
        private readonly IServiceUser _serviceUser;
        public AuthController(IServiceAuth serviceAuth, IServiceUser serviceUser)
        {
            _serviceAuth = serviceAuth;
            _serviceUser = serviceUser;
        }
        [HttpPost]
        [Route("/auth")]
        public async Task<ActionResult> Login(AuthResponse login)
        {
            var user = await _serviceAuth.Login(login);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                token = _serviceAuth.generateJwtToken(user),
                userId = user.Id,
                userEmail = user.Email,
                userName = user.Name,
            });
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("renew")]
        public async Task<ActionResult> RenewToken()
        {
            var userId = AuthHelper.GetUserId(HttpContext);

            var user = await _serviceUser.GetById(userId);
            if (user != null)
            {
                return Ok(new
                {
                    token = _serviceAuth.generateJwtToken(user),
                    userId = user.Id,
                    userEmail = user.Email,
                    userName = user.Name,
                });
            }
            else
            {
                return Unauthorized();
            }

        }
    }
}
