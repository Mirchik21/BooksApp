using Application.MediatR.Accounts.Commands;
using Application.MediatR.Accounts.Queries;
using Application.MediatR.Books.Commands;
using Application.MediatR.Books.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class BookController : BaseController
    {
        public async Task<IActionResult> Index(GetBooksQuery query)
        {
            ViewData["Books"] = await Mediator.Send(query);
            return View(query);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (result.Succeed)
                {
                    foreach (var message in result.Messages) Notyf.Success(message);
                    return RedirectToAction(nameof(Index));
                }
                foreach (var message in result.Messages) Notyf.Error(message);
            }
            return View(command);
        }

        public async Task<IActionResult> Edit(GetBookByIdQuery query)
        {
            var entity = await Mediator.Send(query);
            var command = new EditBookCommand()
            {
                Id = entity.Id,
                Name = entity.Name,
                AuthorName = entity.AuthorName,
                PublishYear = entity.PublishYear,
                Genre = entity.Genre,
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (result.Succeed)
                {
                    foreach (var message in result.Messages) Notyf.Success(message);
                    return RedirectToAction(nameof(Index));
                }
                foreach (var message in result.Messages) Notyf.Error(message);
            }
            return View(command);
        }

        public async Task<IActionResult> Delete(DeleteBookCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Succeed)
                foreach (var message in result.Messages) Notyf.Success(message);
            else
                foreach (var message in result.Messages) Notyf.Error(message);

            return RedirectToAction(nameof(Index));
        }
    }
}