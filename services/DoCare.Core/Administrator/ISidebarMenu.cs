using DoCare.Core.Administrator.Entities;
using System.Collections.Generic;

namespace DoCare.Core.Administrator
{
    public interface ISidebarMenu
    {
        List<SidebarMenu> GetSidebarMenu();
    }
}
