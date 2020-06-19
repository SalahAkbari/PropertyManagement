using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Property.Domain.DTOs;
using Property.Provider;

namespace Property.Controllers
{
    [Route("api/landlords")]
    public class PropertyController : Controller
    {
        private readonly IPropertyProvider _provider;

        public PropertyController(IPropertyProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Get all Properties.
        /// </summary>
        
        /// <returns code="200">A list of Properties</returns>

        [HttpGet("{landlordId}/property")]
        public async Task<IActionResult> Get(int landlordId)
        {
            var dtOs = await _provider.GetAllProperties(landlordId);
            return Ok(dtOs);
        }

        /// <summary>
        /// Get a specific property.
        /// </summary>

        /// <returns code="200">Get Successfull (Success Status Code)</returns>
        /// <response code="400">If the PropertyDTO based on the landlordId and propertyId could not be found</response> 
        /// 
        [HttpGet("{landlordId}/property/{id}", Name = "GetProperty")]
        public async Task<IActionResult> Get(int landlordId, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _provider.GetProperty(landlordId, id);
            if (item == null) return NotFound();//404 Not Found (Client Error Status Code)
            return Ok(item);//Get Successfull (Success Status Code)
        }

        /// <summary>
        /// Creates a new Property.
        /// </summary>
        
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">If the dto is null or the ModelState is invalid</response> 
        /// 

        //You might wonder why the ids are sent in as separate parameters as opposed to 
        //sending them with the request body and receive it in the Property object. 
        //The reason is, ids should be passed into the action with the URL to follow the
        //REST standard. If you do decide to send in the ids with the Property object as well,
        //you should check that they are the same as the ones in the URL before taking any action.

        [HttpPost("{landlordId}/property")]
        public IActionResult Post(int landlordId, [FromBody]PropertyBaseDto dto)
        {
            if (dto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _provider.AddProperty(landlordId, dto);
            return result == null ? StatusCode(500, "A problem occurred while handling your request.")
                : CreatedAtRoute("GetProperty", new { landlordId = result.LandlordId, id = result.PropertyId }, result);
        }

        /// <summary>
        /// Delete a Property.
        /// </summary>
       
        /// <response code="204">No Content, for delete usually, successfull request that shouldn't return anything</response>
        /// <response code="400">If the PropertyDTO based on the propertyId could not be found</response> 
        [HttpDelete("{landlordId}/property/{id}")]
        public async Task<IActionResult> Delete(int landlordId, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _provider.DeleteProperty(id);
            if (!result.Value) return NotFound();
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return NoContent();
        }

        /// <summary>
        /// Update a Property.
        /// </summary>

        /// <response code="204">No Content, for update usually, successfull request that shouldn't return anything</response>
        /// <response code="400">If the PropertyDTO based on the propertyId could not be found</response> 

        [HttpPut("{landlordId}/property")]
        public IActionResult Put([FromBody]PropertyDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _provider.EditProperty(dto);
            if (!result.Value) return NotFound();
            if (result == null) return StatusCode(500, "A problem occurred while handling your request.");
            return NoContent();
        }
    }
}