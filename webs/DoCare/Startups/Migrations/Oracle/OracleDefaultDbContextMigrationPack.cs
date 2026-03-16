using FastFrame.Core;
using FastFrame.EFCore;
using FastFrame.EFCore.Oracle;
using FastFrame.Entity;
using System;
using System.ComponentModel;

namespace DoCare.Startups.Oracle
{
    /// <summary>
    /// Oracle-DefaultDbContext迁移模块
    /// </summary>
    [DependsOnPacks(typeof(OracleEFCorePack))]
    [Description("Oracle-DefaultDbContext迁移模块")]
    public class OracleDefaultDbContextMigrationPack : MigrationPackBase<DefaultDbContext>
    {
        /// <summary>
        /// 获取 模块启动顺序，模块启动的顺序先按级别启动，级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public override int Order => 2;

        protected override DatabaseType DatabaseType => DatabaseType.Oracle;

        protected override DefaultDbContext CreateDbContext(IServiceProvider scopedProvider)
        {
            return new OracleDesignTimeDefaultDbContextFactory(scopedProvider).CreateDbContext(new string[0]);
        }
    }
}