using DoCare.EcgConversion.Common;
using FastFrame.Core;
using FastFrame.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoCare.EcgConversion
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class EcgMonitorService
    {
        private readonly ILogger<EcgMonitorService> logger;
        private readonly FileSystemWatcherHelper fileSystemWatcherHelper;
        private readonly IUnitOfWorkManager unitOfWorkManager;
        private readonly EcgConversionService ecgConversionService;
        private string rootPath;//心电图文件夹目录
        private string fileFilter;//心电图文件过滤规则
        private readonly string errorPath = @"\EcgError";
        private readonly string backupPath = @"\EcgBackUp";

        public EcgMonitorService(ILogger<EcgMonitorService> logger,
            FileSystemWatcherHelper fileSystemWatcherHelper,
            EcgConversionService ecgConversionService,
            IUnitOfWorkManager unitOfWorkManager)
        {
            this.logger = logger;
            this.ecgConversionService = ecgConversionService;
            this.unitOfWorkManager = unitOfWorkManager;
            this.fileSystemWatcherHelper = fileSystemWatcherHelper;
        }

        /// <summary>
        /// 开启心电图监视器
        /// </summary>
        /// <param name="path">监视目录</param>
        /// <param name="fileFilter">
        /// 文件过滤规则,如*.xml
        /// 只支持一种文件类型
        /// </param>
        /// <param name="delayInterval">事件延迟处理间隔,用来避免多次处理及保证文件写入完成,单位:毫秒</param>
        /// <returns></returns>
        public async Task<bool> TurnOnMonitor(string path, string fileFilter, int delayInterval)
        {
            rootPath = path;
            this.fileFilter = fileFilter;
            try
            {
                //解析已经存在的心电图文件
                if (!fileSystemWatcherHelper.IsOn())
                {
                    //监视未开启的情况下才解析,避免重复解析
                    await ConvertExsitingEcg();
                }
                //开启文件夹监视
                if (fileSystemWatcherHelper.TurnOnWatcher(path, fileFilter, FileSystemWatcher_Changed, delayInterval))
                {
                    return true;
                }
                else
                {
                    logger.LogError($"EcgMonitorService.TurnOnMonitor:文件夹监视开启失败");
                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"EcgMonitorService.TurnOnMonitor:文件夹监视开启错误,错误信息:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 关闭心电图监视器
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TurnOffMonitor()
        {
            try
            {
                return fileSystemWatcherHelper.TurnOffWatcher();
            }
            catch (Exception ex)
            {
                logger.LogError($"EcgMonitorService.TurnOffMonitor:文件夹监视停止发生错误,错误信息:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查心电图目录
        /// </summary>
        /// <param name="rootPath">心电图目录绝对路径</param>
        /// <returns>true:检查通过,false:目录不存在</returns>
        public bool CheckDirectory(string rootPath)
        {
            if (Directory.Exists(rootPath))
            {
                if (!Directory.Exists(rootPath + backupPath))
                {
                    Directory.CreateDirectory(rootPath + backupPath);
                }
                if (!Directory.Exists(rootPath + errorPath))
                {
                    Directory.CreateDirectory(rootPath + errorPath);
                }
                return true;

            }
            else
            {
                return false;
            }
        }

        #region 私有函数
        /// <summary>
        /// 监视事件回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            await SyncEcgFile(e.FullPath);
        }

        /// <summary>
        /// 转换目录下已存在的心电图文件
        /// </summary>
        private async Task ConvertExsitingEcg()
        {
            foreach (string filePath in Directory.GetFiles(rootPath, fileFilter))
            {
                await SyncEcgFile(filePath);
            }
        }

        /// <summary>
        /// 同步心电图文件
        /// </summary>
        /// <param name="fullPath">文件完整路径</param>
        /// <returns></returns>
        private async Task SyncEcgFile(string fullPath)
        {
            logger.LogDebug($"EcgMonitorService.SyncEcgFile:心电图文件解析开始,fullPath:{fullPath}");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullPath);
            try
            {
                if (ecgConversionService.SyncEcgFile(fullPath))
                {
                    //源文件移动到备份文件夹
                    MoveFile(fileNameWithoutExtension, rootPath + backupPath);
                    logger.LogDebug($"EcgMonitorService.SyncEcgFile:心电图解析成功");
                }
                else
                {
                    MoveFile(fileNameWithoutExtension, rootPath + errorPath);
                    logger.LogDebug($"EcgMonitorService.SyncEcgFile:心电图解析失败");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"EcgMonitorService.SyncEcgFile:解析失败,错误信息:{ex.Message}");
                MoveFile(fileNameWithoutExtension, rootPath + errorPath);
            }
        }



        /// <summary>
        /// 多个同名文件移动
        /// </summary>
        /// <param name="fileNameWithoutExtension">没有扩展名的文件名</param>
        /// <param name="moveToPath">移动到的路径</param>
        private void MoveFile(string fileNameWithoutExtension, string moveToPath)
        {
            //多个同名文件移动
            DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
            foreach (var fileItem in directoryInfo.GetFiles().Where(o => o.Name.Substring(0, fileNameWithoutExtension.Length) == fileNameWithoutExtension))
            {
                fileItem.MoveTo(moveToPath + @"\" + fileItem.Name);
            }
        }

        /// <summary>
        /// 单个文件移动
        /// </summary>
        /// <param name="fullPath">文件的完整绝对路径</param>
        /// <param name="fileName">文件名,包含扩展名</param>
        /// <param name="moveToPath">移动到的路径</param>
        private void MoveFile(string fullPath, string fileName, string moveToPath)
        {
            //单个文件移动
            System.IO.File.Move(fullPath, moveToPath + @"\" + fileName);
        }

        #endregion
    }
}
