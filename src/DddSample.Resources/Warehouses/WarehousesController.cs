using DddSample.Domain;
using DddSample.Domain.Warehouses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace DddSample.Resources.Warehouses;

[ApiController]
[Route("api/v1/warehouses")]
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
  [HttpGet("{id}", Name = "GetWarehouse")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WarehouseResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Get(
    [FromRoute][Required] string? id,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(id);
    WarehouseId warehouseId = WarehouseId.Parce(id);

    Warehouse? warehouse = await _warehouseRepository.GetAsync(warehouseId, cancellationToken);
    if (warehouse is null)
    {
      return NotFound();
    }

    WarehouseResource warehouseResource = warehouse.ToResource();
    return Ok(warehouseResource);
  }

  /// <summary>
  /// Create a new warehause.
  /// </summary>
  /// <param name="resource">The warehouse.</param>
  [HttpPost(Name = "CreateWarehouse")]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WarehouseResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Create(
    [FromBody][Required] WarehouseResource? resource,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(resource);

    Warehouse warehouse = resource.ToEntity();
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
}
