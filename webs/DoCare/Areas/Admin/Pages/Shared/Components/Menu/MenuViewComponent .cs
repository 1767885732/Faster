using DoCare.Core.Administrator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoCare.Admin.Pages.Components.Menu
{
    /// <summary>
    /// 管理员菜单
    /// </summary>
    public class MenuViewComponent : ViewComponent
    {
        private ISidebarMenu menuService;

        public MenuViewComponent(ISidebarMenu menuService)
        {
            this.menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var menus = menuService.GetSidebarMenu();
            return View("menu", menus);
        }
    }
}
