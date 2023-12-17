using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UI.Models;

namespace UI.ViewComponents
{
    public class MenuViewComponent: ViewComponent
    {
        private IUserService _userService;

        public MenuViewComponent(IUserService userService)
        {
            _userService=userService;
        }

        public IViewComponentResult Invoke()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            MenuViewModel menuModel = new MenuViewModel();
            if (claim != null)
            {
                int userId = int.Parse(claim.Value);
                var result = _userService.GetById(userId);
                menuModel = new MenuViewModel()
                {
                    Name = result.Name,
                    Surname = result.Surname
                };
            }
            return View(menuModel);
        }
    }
}
