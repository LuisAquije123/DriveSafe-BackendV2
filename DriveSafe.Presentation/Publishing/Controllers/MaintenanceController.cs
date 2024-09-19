using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Queries;
using DriveSafe.Domain.Publishing.Models.Response;
using DriveSafe.Domain.Publishing.Services;
using DriveSafe.Presentation.Publishing.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveSafe.Presentation.Publishing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMaintenanceCommandService _maintenanceCommandService;
        private readonly IMaintenanceQueryService _maintenanceQueryService;
        
        public MaintenanceController(IMaintenanceQueryService maintenanceQueryService, IMaintenanceCommandService maintenanceCommandService, IMapper mapper)
        {
            _maintenanceQueryService = maintenanceQueryService;
            _maintenanceCommandService = maintenanceCommandService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(List<MaintenanceResponse>), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        [AuthorizeCustom("admin", "owner", "tenant")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _maintenanceQueryService.Handle(new GetAllMaintenancesQuery());
            
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetMaintenanceById")]
        [ProducesResponseType(typeof(List<MaintenanceResponse>), 200)]
        [ProducesResponseType(typeof(void),statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        [Produces(MediaTypeNames.Application.Json)]
        [AuthorizeCustom("admin", "owner", "tenant")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _maintenanceQueryService.Handle(new GetMaintenanceByIdQuery(id));
            
            if (result==null) StatusCode(StatusCodes.Status404NotFound);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(void),StatusCodes.Status500InternalServerError)]
        [AuthorizeCustom("admin","owner", "tenant")]
        public async Task<IActionResult> PostAsync([FromBody] CreateMaintenanceCommand command)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var result = await _maintenanceCommandService.Handle(command);
            
            return CreatedAtRoute("Get", new { id = result }, result);
        }
        
    }
}
