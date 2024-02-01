using AutoMapper;
using Api.Dtos.Users;
using Api.Infra;
using Api.Views.Users;
using Domain.Interfaces.Services;
using Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Create([FromBody] UserDto dto)
        {
            try
            {
                var user = _mapper.Map<User>(dto);

                var result = _userService.Create(user);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<UserView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("Login")]
        [HttpPost]
        [ProducesResponseType(typeof(LoginView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Login([FromBody] UserDto dto)
        {
            try
            {
                var user = _userService.Get(dto.EmailAddress, dto.Password);

                if (user == null)
                    return BadRequest("Usuário ou senha inválidos");

                var token = TokenService.GenerateToken(user);

                return Ok(new LoginView() { EmailAddress = dto.EmailAddress, AccessToken = token });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [Route("{id}/ChangePassword")]
        [HttpPut]
        [ProducesResponseType(typeof(UserView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordDto dto)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null) return BadRequest("Usuário não encontrado.");

                user.ChangeUserPassword(dto.Password);

                var result = _userService.Update(id, user);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<UserView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _userService.Delete(id);

                if (!result.Success) return BadRequest(result.Errors);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Authorize]
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(UserView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _userService.Get(id);

                var view = _mapper.Map<UserView>(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<UserView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            try
            {
                var entitys = _userService.Get();

                var view = _mapper.Map<List<UserView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
