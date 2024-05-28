using Application.MediatR.Accounts.Commands;
using Application.MediatR.Accounts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public async Task<IActionResult> Index(GetAccountsQuery query)
        {
            ViewData["Accounts"] = await Mediator.Send(query);
            return View(query);
        }

        public IActionResult Create()
            => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountCommand command)
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

        public async Task<IActionResult> Edit(GetAccountByIdQuery query)
        {
            var entity = await Mediator.Send(query);
            var command = new EditAccountCommand()
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName,
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountCommand command)
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

        public async Task<IActionResult> Delete(DeleteAccountCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Succeed)
                foreach (var message in result.Messages) Notyf.Success(message);
            else
                foreach (var message in result.Messages) Notyf.Error(message);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Login()
           => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginCommand command)
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

        [HttpPost]
        public async Task<IActionResult> Logout(LogoutCommand command)
        {
            var result = await Mediator.Send(command);
            return RedirectToAction(nameof(Login));
        }
    }
}