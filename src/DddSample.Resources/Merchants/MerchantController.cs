using DddSample.Domain;
using DddSample.Domain.Merchants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace DddSample.Resources.Merchants;

[ApiController]
[Route("api/v1/merchant")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public sealed class MerchantController : ControllerBase
{
  private readonly IMerchantRepository _merchantRepository;
  private readonly IUnitOfWork _unitOfWork;

  public MerchantController(IMerchantRepository merchantRepository, IUnitOfWork unitOfWork)
  {
    _merchantRepository = merchantRepository;
    _unitOfWork = unitOfWork;
  }

  /// <summary>
  /// Gets a merchant by its ID.
  /// </summary>
  /// <param name="id">The ID of the merchant.</param>
  [HttpGet("{id}", Name = "GetMerchant")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Get(
    [FromRoute][Required] string? id,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(id);
    MerchantId merchantId = MerchantId.Parce(id);

    Merchant? merchant = await _merchantRepository.GetAsync(merchantId, cancellationToken);
    if (merchant is null)
    {
      return NotFound();
    }

    MerchantResource merchantResource = merchant.ToResource();
    return Ok(merchantResource);
  }

  /// <summary>
  /// Create a new merchant.
  /// </summary>
  /// <param name="resource">The merchant.</param>
  [HttpPost(Name = "CreateMerchant")]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MerchantResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Create(
    [FromBody][Required] MerchantResource? resource,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(resource);

    Merchant merchant = resource.ToEntity();
    _merchantRepository.Add(merchant);
    await _unitOfWork.CommitAsync(cancellationToken);

    MerchantResource createdResource = merchant.ToResource();
    return CreatedAtAction
    (
      actionName: nameof(Get),
      routeValues: new { id = createdResource.Id, },
      value: createdResource
    );
  }

  /// <summary>
  /// Replace a merchant by its ID. If there is no merchant with this ID, a new merchant will be created.
  /// </summary>
  /// <param name="id">The ID of a merchant.</param>
  /// <param name="resource">The merchant.</param>
  [HttpPut("{id}", Name = "ReplaceMerchant")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantResource))]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MerchantResource))]
  [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMetadata))]
  public async Task<IActionResult> Replace(
    [FromRoute][Required] string? id,
    [FromBody][Required] MerchantResource? resource,
    CancellationToken cancellationToken)
  {
    ArgumentNullException.ThrowIfNull(id);
    MerchantId merchantId = MerchantId.Parce(id);

    ArgumentNullException.ThrowIfNull(resource);

    Merchant? merchant = await _merchantRepository.GetAsync(merchantId, cancellationToken);
    if (merchant is null)
    {
      merchant = resource.ToEntity(merchantId);
      _merchantRepository.Add(merchant);
      await _unitOfWork.CommitAsync(cancellationToken);

      MerchantResource created = merchant.ToResource();
      return CreatedAtAction
      (
        actionName: nameof(Get),
        routeValues: new { id = created.Id, },
        value: created
      );
    }

    ArgumentNullException.ThrowIfNull(resource.Name);
    merchant.Replace(resource.Name);
    await _unitOfWork.CommitAsync(cancellationToken);

    MerchantResource replaced = merchant.ToResource();
    return Ok(replaced);
  }
}
