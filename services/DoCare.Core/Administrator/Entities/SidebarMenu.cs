using System.Collections.Generic;

namespace DoCare.Core.Administrator.Entities
{
    public class SidebarMenu
    {
        public SidebarMenu()
        {
            Guid = Pid = Name = Icon = Url = string.Empty;
            Children = new List<SidebarMenu>();
        }

        public string Guid { get; set; }

        public string Pid { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public int OrderNo { get; set; }

        public List<SidebarMenu> Children { get; set; }
    }
}
