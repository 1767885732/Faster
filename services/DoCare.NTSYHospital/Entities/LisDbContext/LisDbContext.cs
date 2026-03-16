using FastFrame.EFCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoCare.NTSYHospital
{
    public class LisDbContext : DbContextBase
    {
        /// <summary>
        /// 初始化一个<see cref="LisDbContext"/>类型的新实例
        /// </summary>
        public LisDbContext(DbContextOptions<LisDbContext> options, IEntityManager entityManager, IServiceProvider serviceProvider)
            : base(options, entityManager, serviceProvider)
        { }
    }
}
