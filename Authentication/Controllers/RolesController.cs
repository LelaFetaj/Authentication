﻿using Authentication.Models.Entities.Authorizations;
using Authentication.Models.Entities.Roles;
using Authentication.Services.Foundations.Roles;
using Authentication.Services.Processings.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Authentication.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase 
    {
        private readonly IRoleProcessingService roleProcessingService;

        public RolesController(IRoleProcessingService roleProcessingService) 
        {
            this.roleProcessingService = roleProcessingService;
        }

        /// <summary>
        /// Returns the list of all roles
        /// </summary>
        /// <remarks>This method needs administrator privilege.</remarks>
        [Route("get-roles-list")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Role>), (int)HttpStatusCode.OK)]
        [Authorization(AuthorizationType.All, "Admin")]
        public async ValueTask<ActionResult<IReadOnlyList<Role>>> GetAllRoles() =>
            Ok(await this.roleProcessingService.RetrieveAllRolesAsync());
    }
}
