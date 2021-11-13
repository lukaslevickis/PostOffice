using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostOfficeBackend.DAL.Entities;
using PostOfficeBackend.Dtos;
using PostOfficeBackend.Services;

namespace PostOfficeBackend.Controllers
{
    [Route("api/[controller]")]
    public class ParcelController : Controller
    {
        private readonly ParcelService _parcelService;

        public ParcelController(ParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _parcelService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            ParcelDto data = await _parcelService.GetByIdAsync(id);
            if (data == null)
            {
                return BadRequest();
            }

            return Ok(data);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Parcel parcel)
        {
            return Ok(await _parcelService.CreateAsync(parcel));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Parcel parcel)
        {
            if (id != parcel.Id)
            {
                return BadRequest();
            }

            return Ok(await _parcelService.UpdateAsync(parcel));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _parcelService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet]
        [Route("post/{id}")]
        public async Task<ActionResult> GetPostParcelsById(int id)
        {
            return Ok(await _parcelService.GetAllAsync(id));
        }
    }
}
