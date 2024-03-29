using Authentication.Models.DTOs.Authentications;
using Authentication.Models.DTOs.Users;
using Authentication.Models.Entities.Authorizations;
using Authentication.Models.Entities.Users;
using Authentication.Services.Foundations.Users;
using Authentication.Services.Orchestrations.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Authentication.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserOrchestrationService userOrchestrationService;

        public UsersController(IUserOrchestrationService userOrchestrationService)
        {
            this.userOrchestrationService = userOrchestrationService;
        }

        /// <summary>
        /// Register user and returns JWT token
        /// </summary>
        [HttpPost("register")]
        public async ValueTask<ActionResult<AuthenticatedResponse>> Register(CreateUserDto createUserDto) =>
            Ok(await this.userOrchestrationService.UserRegisterAsync(createUserDto));

        /// <summary>
        /// Authenticate user and returns JWT token
        /// </summary>
        [HttpPut("login")]
        public async ValueTask<ActionResult<AuthenticatedResponse>> Login(LoginRequestDto loginRequest) =>
            Ok(await this.userOrchestrationService.UserLoginAsync(loginRequest));

        /// <summary>
        /// Returns the list of all users
        /// </summary>
        /// <remarks>This method needs administrator privilege.</remarks>
        [Route("get-users-list")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<User>), (int)HttpStatusCode.OK)]
        [Authorization(AuthorizationType.All, "Admin")]
        public async ValueTask<ActionResult<IReadOnlyList<User>>> GetAllUsers() =>
            Ok(await this.userOrchestrationService.RetrieveAllUsersAsync());

        /// <summary>
        /// Returns the role of a user
        /// </summary>
        /// <remarks>This method needs administrator privilege.</remarks>
        [Route("get-role-of-user")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<User>), (int)HttpStatusCode.OK)]
        [Authorization(AuthorizationType.All, "Admin")]
        public async ValueTask<ActionResult<string>> GetRoleOfUser(string username) =>
            Ok(await this.userOrchestrationService.GetUserRole(username));
    }
}
