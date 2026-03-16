using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoCare.NTSYHospital
{
    public class HisDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="HisDbContext"/>类型的新实例
        /// </summary>
        public HisDbContext(DbContextOptions<HisDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}
