using System.Threading.Tasks;
using DCMS.Application.Features.DocumentTypes.Commands.AddEdit;
using DCMS.Application.Features.DocumentTypes.Commands.Delete;
using DCMS.Application.Features.DocumentTypes.Queries.Export;
using DCMS.Application.Features.DocumentTypes.Queries.GetAll;
using DCMS.Application.Features.DocumentTypes.Queries.GetById;
using DCMS.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCMS.Server.Controllers.Utilities.Misc
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : BaseApiController<DocumentTypesController>
    {
        /// <summary>
        /// Get All Document Types
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.DocumentTypes.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documentTypes = await _mediator.Send(new GetAllDocumentTypesQuery());
            return Ok(documentTypes);
        }

        /// <summary>
        /// Get Document Type By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.DocumentTypes.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var documentType = await _mediator.Send(new GetDocumentTypeByIdQuery { Id = id });
            return Ok(documentType);
        }

        /// <summary>
        /// Create/Update a Document Type
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.DocumentTypes.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditDocumentTypeCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Document Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.DocumentTypes.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteDocumentTypeCommand { Id = id }));
        }

        /// <summary>
        /// Search Document Types and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.DocumentTypes.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportDocumentTypesQuery(searchString)));
        }
    }
}