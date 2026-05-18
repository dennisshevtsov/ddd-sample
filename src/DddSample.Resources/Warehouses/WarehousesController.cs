using DddSample.Domain;
using DddSample.Domain.Warehouses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace DddSample.Resources.Warehouses;

[ApiController]
[Route("api/v1/merchant/{merchantId}/warehouses")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public sealed class WarehousesController : ControllerBase
{
  private readonly IWarehouseRepository _warehouseRepository;
  private readonly IUnitOfWork _unitOfWork;

  public WarehousesController(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork)
  {
    _warehouseRepository = warehouseRepository;
    _unitOfWork = unitOfWork;
  }

  /// <summary>
  /// Gets a warehouse by its ID.
  /// </summary>
  /// <param name="id">The ID of the warehouse.</param>
  /// <param name="fieldMask">The list of fields that should be included to the response. Example: name,locations.*</param>
  [HttpGet("{id}", Name = "GetWarehouse")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Get(
    [FromRoute(Name = "merchantId")][Required] string? merchantIdString,
    [FromRoute][Required] string? id,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(merchantIdString);
    MerchantId merchantId = MerchantId.Parce(merchantIdString);

    ArgumentNullException.ThrowIfNull(id);
    WarehouseId warehouseId = WarehouseId.Parce(id);

    Warehouse? warehouse = await _warehouseRepository.GetAsync(warehouseId, cancellationToken);
    if (warehouse is null)
    {
      return NotFound();
    }
    if (warehouse.MerchantId != merchantId)
    {
      return Forbid();
    }

    WarehouseResource warehouseResource = warehouse.ToResource();
    return Ok(warehouseResource);
  }

  /// <summary>
  /// Gets a list of warehouses that satisfy to conditions in the filter.
  /// </summary>
  /// <param name="filter">Use this parameter to filter the result. Available operators: eq, ne, gt, ge, lt, le, and, or, not, contains. Excample: GET api/v1/warehouses?filter=not(contains(name, 'abc'))</param>
  /// <param name="nextPageToken">The next page token.</param>
  /// <param name="maxPageSize">The max size of the page. If page contains less than this value, it does not mean the there is no more records. Only nextPageToken indicates if there are records still.</param>
  /// <param name="fieldMask">The list of fields that should be included to the response. Example: name,locations.*</param>
  [HttpGet(Name = "ListWarehouses")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponse<WarehouseResource>))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public IActionResult List(
    [FromQuery] string filter,
    [FromQuery] string nextPageToken,
    [FromQuery] int maxPageSize) => Ok(new ListResponse<WarehouseResource>
    {
      Results = [new WarehouseResource { Id = "test" }],
    });

  /// <summary>
  /// Create a new warehause.
  /// </summary>
  /// <param name="resource">The warehouse.</param>
  [HttpPost(Name = "CreateWarehouse")]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WarehouseResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Create(
    [FromRoute(Name = "merchantId")][Required] string? merchantIdString,
    [FromBody][Required] WarehouseResource? resource,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(merchantIdString);
    MerchantId merchantId = MerchantId.Parce(merchantIdString);

    ArgumentNullException.ThrowIfNull(resource);

    Warehouse warehouse = resource.ToEntity(merchantId);
    _warehouseRepository.Add(warehouse);
    await _unitOfWork.CommitAsync(cancellationToken);

    WarehouseResource createdResource = warehouse.ToResource();
    return CreatedAtAction
    (
      actionName: nameof(Get),
      routeValues: new { id = warehouse.Id, },
      value: createdResource
    );
  }

  /// <summary>
  /// Replace a warehouse by its ID. If there is no warehouse with this ID, a new warehouse will be created.
  /// </summary>
  /// <param name="id">The ID of a warehouse to undelete.</param>
  /// <param name="resource">The warehouse.</param>
  /// <param name="resourceId">The optional ID of a request to deduplicate requests. Use a random generated value.</param>
  /// <param name="validateOnly">If this field is true, no chages will be applied, the method will only validate the request.</param>
  [HttpPut("{id}", Name = "ReplaceWarehouse")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public IActionResult Replace([FromRoute] string id, [FromBody] WarehouseResource resource) => Ok(resource);

  /// <summary>
  /// Soft delete a warehouse.
  /// </summary>
  /// <param name="id">The ID of a warehouse to undelete.</param>
  /// <param name="resourceId">The optional ID of a request to deduplicate requests. Use a random generated value.</param>
  [HttpDelete("{id}", Name = "DeleteWarehouse")]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public IActionResult Delete([FromRoute] string id) => NoContent();
}
