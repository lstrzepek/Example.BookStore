using Example.BookStore.Catalog.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Example.BookStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<BookListItem>> GetBooks()
    {
        return await _mediator.Send(new GetBooks());
    }

    [HttpPost]
    public async Task<IActionResult> RegisterBook(RegisterBook registerBook)
    {
        await _mediator.Send(registerBook);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBook(UpdateBook updateBook)
    {
        await _mediator.Send(updateBook);
        return Ok();
    }
}
