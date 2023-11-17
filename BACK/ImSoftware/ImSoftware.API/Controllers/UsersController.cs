using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ImSoftware.BLL.Services.Contract;
using ImSoftware.DTO;
using ImSoftware.Utility;
using ImSoftware.API.Utility;

namespace ImSoftware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List()
        {
            var rsp = new Response<List<UserDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.List();
            } catch(Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> add([FromBody] UserDTO user)
        {
            var rsp = new Response<UserDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.Add(user);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> update([FromBody] UserDTO user)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.Update(user);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> delete(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _userService.Delete(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}
