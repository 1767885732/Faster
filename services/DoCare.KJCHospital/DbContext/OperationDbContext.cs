using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.KJCHospital.DbContext
{
    /// <summary>
    /// 手术数据库上下文
    /// </summary>
    public class OperationDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="OperationDbContext"/>类型的新实例
        /// </summary>
        public OperationDbContext(DbContextOptions<OperationDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}
