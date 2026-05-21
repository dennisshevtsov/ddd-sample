using DddSample.Domain;
using DddSample.Domain.DeliveryPoints;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace DddSample.Resources.DeliveryPoints;

[ApiController]
[Route("api/v1/warehouses/{warehouseId}/delivery-points")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public sealed class DeliveryPointController : ControllerBase
{
  private readonly IDeliveryPointRepository _deliveryPointRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeliveryPointController(IDeliveryPointRepository deliveryPointRepository, IUnitOfWork unitOfWork)
  {
    _deliveryPointRepository = deliveryPointRepository;
    _unitOfWork = unitOfWork;
  }

  /// <summary>
  /// Gets a delivery point by its ID.
  /// </summary>
  /// <param name="id">The ID of the delivery point.</param>
  [HttpGet("{id}", Name = "GetDeliveryPoint")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeliveryPointResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Get(
    [FromRoute][Required] string? id,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(id);
    DeliveryPointId deliveryPointId = DeliveryPointId.Parce(id);

    DeliveryPoint? deliveryPoint = await _deliveryPointRepository.GetAsync(deliveryPointId, cancellationToken);
    if (deliveryPoint is null)
    {
      return NotFound();
    }

    DeliveryPointResource deliveryPointResource = deliveryPoint.ToResource();
    return Ok(deliveryPointResource);
  }

  /// <summary>
  /// Create a new delivery point.
  /// </summary>
  /// <param name="resource">The delivery point.</param>
  [HttpPost(Name = "CreateDeliveryPoint")]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DeliveryPointResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Create(
    [FromRoute][Required] string? warehouseId,
    [FromBody][Required] DeliveryPointResource? resource,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(warehouseId);
    ArgumentNullException.ThrowIfNull(resource);

    DeliveryPoint deliveryPoint = resource.ToEntity(WarehouseId.Parce(warehouseId));
    _deliveryPointRepository.Add(deliveryPoint);
    await _unitOfWork.CommitAsync(cancellationToken);

    DeliveryPointResource created = deliveryPoint.ToResource();
    return CreatedAtAction
    (
      actionName: nameof(Get),
      routeValues: new { id = created.Id, },
      value: created
    );
  }
}
