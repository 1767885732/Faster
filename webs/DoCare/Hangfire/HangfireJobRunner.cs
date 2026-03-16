using FastFrame.Core;
using FastFrame.Hangfire;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DoCare.Hangfire
{
    [Dependency(ServiceLifetime.Singleton)]
    public class HangfireJobRunner : IHangfireJobRunner
    {
        public void Start()
        {
            //BackgroundJob.Enqueue<UserManager<User>>(m => m.FindByIdAsync("1"));
            //string jobId = BackgroundJob.Schedule<UserManager<User>>(m => m.FindByIdAsync("2"), TimeSpan.FromMinutes(2));
            //BackgroundJob.ContinueWith<TestHangfireJob>(jobId, m => m.GetUserCount());
            //RecurringJob.AddOrUpdate<TestHangfireJob>(m => m.GetUserCount(), Cron.Minutely, TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate<TestHangfireJob>(m=>m.LockUser2(), Cron.Minutely, TimeZoneInfo.Local);
        }
    }
     

    //public class TestHangfireJob
    //{
    //    private readonly IIdentityContract _identityContract;
    //    private readonly IServiceProvider _provider;

    //    /// <summary>
    //    /// 初始化一个<see cref="TestHangfireJob"/>类型的新实例
    //    /// </summary>
    //    public TestHangfireJob(IIdentityContract identityContract, IServiceProvider provider)
    //    {
    //        _identityContract = identityContract;
    //        _provider = provider;
    //    }

    //    /// <summary>
    //    /// 获取用户数量
    //    /// </summary>
    //    public string GetUserCount()
    //    {
    //        List<string> list = new List<string>();
    //        list.Add(_identityContract.Users.Count().ToString());
    //        list.Add(_identityContract.GetHashCode().ToString());
    //        return list.ExpandAndToString();
    //    }

    //    public async Task<string> LockUser2()
    //    {
    //        List<string> list = new List<string>();
    //        UserManager<User> userManager = _provider.GetService<UserManager<User>>();
    //        User user2 = await userManager.FindByIdAsync("2");
    //        list.Add($"user2.IsLocked: {user2.IsLocked}");
    //        user2.IsLocked = !user2.IsLocked;
    //        await userManager.UpdateAsync(user2);
    //        IUnitOfWork unitOfWork = _provider.GetUnitOfWork<User, int>();
    //        unitOfWork.Commit();
    //        user2 = await userManager.FindByIdAsync("2");
    //        list.Add($"user2.IsLocked: {user2.IsLocked}");
    //        return list.ExpandAndToString();
    //    }
    //}
}