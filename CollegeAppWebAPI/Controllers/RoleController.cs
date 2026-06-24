using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Role> _roleRepository;
        private APIResponse _apiResponse;

        public RoleController(IMapper mapper, ICollegeRepository<Role> roleRepository)
        {
            this._mapper = mapper;
            this._roleRepository = roleRepository;
            this._apiResponse = new();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<APIResponse>> CreateRoleAsync(RoleDTO roleDTO)
        {
            try
            {
                if (roleDTO == null)
                {
                    return BadRequest();
                }

                Role role = _mapper.Map<Role>(roleDTO);
                role.IsDeleted = false;
                role.CreatedDate = DateTime.Now;
                role.ModifiedDate = DateTime.Now;

                var result = await _roleRepository.CreateAsync(role);
                roleDTO.Id = result.Id;
                _apiResponse.Data = roleDTO;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
                //return CreatedAtRoute("GetRoleById", new { id = roleDTO.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("All", Name = "GetAllRoles")]
        public async Task<ActionResult<APIResponse>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                _apiResponse.Data = _mapper.Map<List<RoleDTO>>(roles);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch(Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetRoleById")]
        public async Task<ActionResult<APIResponse>> GetRoleByIdAsync(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest();
                }
                var role = await _roleRepository.GetByIdAsync(role => role.Id == id);
                if(role == null)
                {
                    return NotFound($"The Role not found with id {id}");
                }
                _apiResponse.Data = _mapper.Map<RoleDTO>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpPut("updaterecord")]
        public async Task<APIResponse> UpdateRoleAsync([FromBody] RoleDTO roleDTO)
        {
            try
            {
                if (roleDTO == null || roleDTO.Id <= 0)
                {
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.Errors.Add("Invalid payload or missing Id");
                    return _apiResponse;
                }

                var existingRecord = await _roleRepository.GetByIdAsync(role => role.Id == roleDTO.Id, true);
                if (existingRecord == null)
                {
                    _apiResponse.Status = false;
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.Errors.Add($"Role with id {roleDTO.Id} not found");
                    return _apiResponse;
                }

                var role = _mapper.Map<Role>(roleDTO);
                await _roleRepository.UpdateAsync(role);

                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                _apiResponse.Data = null;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpPatch("updaterecordpartial/{id:int}")]
        public async Task<APIResponse> UpdateRolePartialAsync(int id, [FromBody] JsonPatchDocument<RoleDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.Errors.Add("Invalid request");
                return _apiResponse;
            }

            var existingRecord = await _roleRepository.GetByIdAsync(role => role.Id == id, true);
            if (existingRecord == null)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                _apiResponse.Errors.Add($"Role with id {id} not found");
                return _apiResponse;
            }

            var roleDTO = _mapper.Map<RoleDTO>(existingRecord);
            patchDocument.ApplyTo(roleDTO, ModelState);

            if (!ModelState.IsValid)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.Errors.Add("Invalid model state");
                return _apiResponse;
            }

            existingRecord = _mapper.Map<Role>(roleDTO);
            await _roleRepository.UpdateAsync(existingRecord);

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            return _apiResponse;
        }

        [HttpDelete("deleterole/{id:int}", Name = "DeleteRoleById")]
        public async Task<APIResponse> DeleteRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(role => role.Id == id);
            if (role == null)
            {
                _apiResponse.Status = false;
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                _apiResponse.Errors.Add($"No Role found with id {id}");
                return _apiResponse;
            }

            var deleted = await _roleRepository.DeleteAsync(role);
            _apiResponse.Status = deleted;
            _apiResponse.StatusCode = deleted ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            _apiResponse.Data = deleted;
            return _apiResponse;
        }
    }
}
