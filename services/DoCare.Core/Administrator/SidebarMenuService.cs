using DoCare.Core.Administrator.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DoCare.Core.Administrator
{
    public class SidebarMenuService : ISidebarMenu
    {
        private readonly IConfiguration configuration;
        public SidebarMenuService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<SidebarMenu> GetSidebarMenu()
        {
            IConfigurationSection section = configuration.GetSection("SidebarMenus");
            var lstSidebar = section?.Get<List<SidebarMenu>>();
            lstSidebar = GetFilterOrderMenu(lstSidebar);
            return lstSidebar;
        }

        private List<SidebarMenu> GetFilterOrderMenu(List<SidebarMenu> lstMenus)
        {
            List<SidebarMenu> firstSidebar =
                lstMenus.Where(a => string.IsNullOrEmpty(a.Pid))
                .OrderBy(a => a.OrderNo).ToList();

            firstSidebar.ForEach(a => a.Children = GetChildren(a.Guid, lstMenus));
            return firstSidebar;
        }

        private List<SidebarMenu> GetChildren(string id, List<SidebarMenu> lstMenuItem)
        {
            var lstChildren = new List<SidebarMenu>();
            if (!string.IsNullOrEmpty(id) && lstMenuItem != null && lstMenuItem.Count > 0)
            {
                lstChildren = lstMenuItem.Where(a => a.Pid == id).OrderBy(a => a.OrderNo).ToList();
            }

            if (lstChildren != null & lstChildren.Count > 0)
            {
                lstChildren.ForEach(a => a.Children = GetChildren(a.Guid, lstMenuItem));
            }
            return lstChildren;
        }
    }
}
