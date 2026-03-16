using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.Mdsd.Emergency
{
    /// <summary>
    /// 急诊腕带上下文
    /// </summary>
    public class EmergencyTagDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="EmergencyDbContext"/>类型的新实例
        /// </summary>
        public EmergencyTagDbContext(DbContextOptions<EmergencyTagDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}
