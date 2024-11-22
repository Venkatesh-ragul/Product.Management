using Microsoft.AspNetCore.Mvc;
using Product.Management.Api.Contracts.Interfaces;
using AutoMapper;
using Product.Management.Api.Exceptions;
using Product.Management.Api.Validator;
using Product.Management.Api.Models.DTO;
using Product.Management.Api.Models.Domain;
using Product.Management.Api.UnitofWork;
using Product.Management.Api.Repositories.Interfaces;
using Product.Management.Api.Repositories.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product.Management.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    IProductsRepository _productsRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfwork _unitOfWork;

    public ProductsController(IMapper mapper, IUnitOfwork unitOfwork, IProductsRepository productsRepository)
    {
        this._mapper = mapper;
        _unitOfWork = unitOfwork;
        _productsRepository = productsRepository;
    }

    [HttpGet]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get()
    {
        var productDetails = await _productsRepository.GetAsync();
        var data = _mapper.Map<List<ProductsDetailsDto>>(productDetails.Value);

        return data == null ? NoContent() : Ok(data);
    }

    [HttpGet("GetProductsFilters")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetProductsFilters(string? query,
         string? sortBy, string? sortDirection,
         int? pageNumber, int? pageSize)
    {
        var filteredProducts = await _productsRepository.GetFilterProducts(query, sortBy, sortDirection, pageNumber, pageSize);
        var data = _mapper.Map<List<ProductsDetailsDto>>(filteredProducts);

        return filteredProducts == null ? NoContent() : Ok(filteredProducts);
    }

    [HttpGet("GetById")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Products>> GetById(int id)
    {
        var productDetail = await _productsRepository.GetByIdAsync(id);
        var data = _mapper.Map<ProductsDetailsDto>(productDetail);

        return productDetail == null ? NoContent() : Ok(productDetail);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Products>> Post(CreateProductRequest objProduct)
    {
        var validator = new CreateProductValidator(_productsRepository);
        var validationResult = await validator.ValidateAsync(objProduct);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid product data", validationResult);

        var productstoInsert = _mapper.Map<Products>(objProduct);
        var response = await _productsRepository.CreateAsync(productstoInsert);

        return CreatedAtAction(nameof(Post), new { response });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(400)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(int Id, UpdateProductRequest productObj)
    {
        var validator = new UpdateProductValidator(_productsRepository);
        var validationResult = await validator.ValidateAsync(productObj);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid product details.", validationResult);

        var productstoUpdate = _mapper.Map<Products>(productObj);

        return await _productsRepository.UpdateAsync(Id, productstoUpdate);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        return await _productsRepository.DeleteAsync(id);
    }
}
