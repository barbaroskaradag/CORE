using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JwtProje.Business.Interfaces;
using JwtProje.Business.StringInfos;
using JwtProje.Entities.Concrete;
using JwtProje.Entities.Dtos.AppUserDtos;
using JwtProje.Entities.Token;
using JwtProje.WebApi.CustomFilters;

namespace JwtProje.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtServices _jwtService;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public AuthController(IJwtServices jwtService, IAppUserService appUserService, IMapper mapper)
        {
            _mapper = mapper;
            _jwtService = jwtService;
            _appUserService = appUserService;
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserLoginDto appUserLoginDto)
        {
            //?
            // userName =>  varmı 
            // password => eşleşiyor mu?
            //_jwtService.GenerateJwt()

            var appUser = await _appUserService.FindByUserName(appUserLoginDto.UserName);
            if (appUser == null)
            {
                return BadRequest("kullanıcı adı veya şifre hatalı");
            }
            else
            {
                if (await _appUserService.CheckPassword(appUserLoginDto))
                {
                    var roles = await _appUserService.GetRolesByUserName(appUserLoginDto.UserName);
                    var token = _jwtService.GenerateJwtToken(appUser, roles);
                    JwtAccessToken jwtAccessToken = new JwtAccessToken();
                    jwtAccessToken.Token = token;
                    return Created("", jwtAccessToken);
                }
                return BadRequest("kullanıcı adı veya şifre hatalı");
            }
        }

        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignUp(AppUserAddDto appUserAddDto, [FromServices] IAppUserRoleService appUserRoleService, [FromServices] IAppRoleService appRoleService)
        {
            var appUser = await _appUserService.FindByUserName(appUserAddDto.UserName);
            if (appUser != null)
                return BadRequest($"{appUserAddDto.UserName} zaten alınmış");

            await _appUserService.Add(_mapper.Map<AppUser>(appUserAddDto));

            var user = await _appUserService.FindByUserName(appUserAddDto.UserName);
            var role = await appRoleService.FindByName(RoleInfo.Member);

            await appUserRoleService.Add(new AppUserRole
            {
                AppRoleId = role.Id,
                AppUserId = user.Id
            });
            return Created("", appUserAddDto);
        }



        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> ActiveUser()
        {
            var user = await _appUserService.FindByUserName(User.Identity.Name);
            var roles = await _appUserService.GetRolesByUserName(User.Identity.Name);

            AppUserDto appUserDto = new AppUserDto
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Roles = roles.Select(I => I.Name).ToList()
            };

            return Ok(appUserDto);
        }

    }
}