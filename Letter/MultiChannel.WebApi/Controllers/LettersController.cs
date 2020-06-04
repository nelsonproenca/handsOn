using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Multichannel.Application.Letters.Models;
using Multichannel.Core.Commands;
using MultiChannel.WebApi.Filters;

namespace MultiChannel.WebApi.Controllers
{
    /// <summary>
    /// Letters Controller class.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ErrorHandler]
    public class LettersController : ControllerBase
    {
        private readonly ICommonCommands<LetterModel> commoCommandsLetter;
        private readonly ICommonQueries<LetterModel> commonQueriesLetters;

        /// <summary>
        /// Initializes a new instance of the <see cref="LettersController"/> class.
        /// </summary>
        /// <param name="commoCommandsLetter">commoCommandsLetter.</param>
        /// <param name="commonQueriesLetters">commonQueriesLetters.</param>
        public LettersController(ICommonCommands<LetterModel> commoCommandsLetter, ICommonQueries<LetterModel> commonQueriesLetters)
        {
            this.commoCommandsLetter = commoCommandsLetter;
            this.commonQueriesLetters = commonQueriesLetters;
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="requestId">requestId.</param>
        /// <returns>ActionResult.</returns>
        // GET api/letters/byRequest/
        [HttpGet("byRequest/{requestId}", Name = "byRequest")]
        public async Task<IActionResult> GetAsync(Guid requestId)
        {
            var result = await commonQueriesLetters.GetByRequestAsync(requestId);

            if (result == null)
            {
                return NotFound("Request Id not found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="tenantId">id.</param>
        /// <returns>a object.</returns>
        // GET api/letters/byTenant/5
        [HttpGet("byTenant/{tenantId}", Name ="byTenant")]
        public async Task<IActionResult> GetByTenantAsync(Guid tenantId)
        {
            var result = await commonQueriesLetters.GetByTenantAsync(tenantId);

            if (result == null)
            {
                return NotFound("Tenant Id not found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>a object.</returns>
        // GET api/letters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await commonQueriesLetters.GetOneAsync(id);

            if (result == null)
            {
                return NotFound("Id not found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Post.
        /// </summary>
        /// <param name="value">value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        // POST api/letters
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] LetterModel value)
        {
            var result = await commoCommandsLetter.CreateAsync(value);

            if (result == 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Put.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="value">value.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        // PUT api/letters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] LetterModel value)
        {
            var result = await commoCommandsLetter.UpdateAsync(id, value);

            if (result <= 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        // DELETE api/letters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await commoCommandsLetter.DeleteAsync(id);

            if (result <= 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
