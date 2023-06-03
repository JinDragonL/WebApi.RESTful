using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiRestful.Domain.Entities;
using WebApiRestful.ViewModel;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Update()
        {
            UserModel userModel = new()
            {
                Id = 0,
                Fullname = "Robot",
                Password = "123",
                Username = "robot.123"
            };

            User user = _mapper.Map<User>(userModel);


            UserModel modelUser = _mapper.Map<UserModel>(user);

            List<User> users = new List<User>() { user };

            var ls = _mapper.Map<List<UserModel>>(users);


            return Ok(1);
        }
    }
}
