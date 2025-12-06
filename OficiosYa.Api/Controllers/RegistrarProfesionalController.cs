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
    [Route("api/registro/profesional")]
    public class RegistrarProfesionalController : ControllerBase
    {
        private readonly RegisterProfesionalHandler _registerProfesionalHandler;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterProfesionalRequest> _validator;

        public RegistrarProfesionalController(
            RegisterProfesionalHandler registerProfesionalHandler,
            IPhotoService photoService,
            IMapper mapper,
            IValidator<RegisterProfesionalRequest> validator)
        {
            _registerProfesionalHandler = registerProfesionalHandler;
            _photoService = photoService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Registrar([FromForm] RegisterProfesionalRequest request, IFormFile? FotoPerfil)
        {
            // perform manual async validation to allow use of async rules
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

            var dto = _mapper.Map<RegistroProfesionalDto>(request);
            dto.FotoPerfil = relativePath;

            var result = await _registerProfesionalHandler.HandleAsync(dto);
            return CreatedAtAction(nameof(Registrar), result);
        }
    }
}
