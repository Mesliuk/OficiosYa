using Microsoft.AspNetCore.Mvc;
using OficiosYa.Api.Models;
using OficiosYa.Application.DTOs;
using OficiosYa.Application.Handlers.Usuarios;
using Microsoft.AspNetCore.Http;
using OficiosYa.Api.Services;
using AutoMapper;
using FluentValidation;

namespace OficiosYa.Api.Controllers
{
    [ApiController]
    [Route("api/registro/cliente")]
    public class RegistrarClienteController : ControllerBase
    {
        private readonly RegisterClienteHandler _registerClienteHandler;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterClienteRequest> _validator;

        public RegistrarClienteController(
            RegisterClienteHandler registerClienteHandler,
            IPhotoService photoService,
            IMapper mapper,
            IValidator<RegisterClienteRequest> validator)
        {
            _registerClienteHandler = registerClienteHandler;
            _photoService = photoService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Registrar([FromForm] RegisterClienteRequest request, IFormFile? FotoPerfil)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                foreach (var err in validationResult.Errors)
                {
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            string? relativePath = null;
            if (FotoPerfil != null)
            {
                relativePath = await _photoService.SaveAsync(FotoPerfil);
            }

            var dto = _mapper.Map<RegistroClienteDto>(request);
            
            dto.FotoPerfil = relativePath;

            var result = await _registerClienteHandler.HandleAsync(dto);
            return CreatedAtAction(nameof(Registrar), result);
        }
    }
}
