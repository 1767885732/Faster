using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.KJCHospital.DbContext
{
    /// <summary>
    /// 心电图数据库上下文
    /// </summary>
    public class EcgDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="EcgDbContext"/>类型的新实例
        /// </summary>
        public EcgDbContext(DbContextOptions<EcgDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}
