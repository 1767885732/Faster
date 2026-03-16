using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoCare.Mdsd.Emergency
{
    /// <summary>
    /// 急诊数据库上下文
    /// </summary>
    public class EmergencyDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="EmergencyDbContext"/>类型的新实例
        /// </summary>
        public EmergencyDbContext(DbContextOptions<EmergencyDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}
