using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DoCare.EcgConversion.Common
{
    [Dependency(ServiceLifetime.Singleton, AddSelf = true)]
    public class FileSystemWatcherHelper
    {
        private readonly ILogger<FileSystemWatcherHelper> logger;
        private Timer _timer;//定时器对象
        private int _timerInterval; //定时器触发间隔
        private readonly TimeSpan _disposalTimeout = new TimeSpan(0, 1, 0);//停止定时器超时时间
        private FileSystemWatcher _watcher;//文件监视器对象
        private FileSystemEventHandler _watchHandler;//监视事件回调函数
        private Dictionary<String, FileSystemEventArgs> _changedFiles = new Dictionary<string, FileSystemEventArgs>();//记录待处理文件的队列

        public FileSystemWatcherHelper(ILogger<FileSystemWatcherHelper> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 开启文件监视(已开启时,开启操作无效)
        /// </summary>
        /// <param name="path">监视目录的绝对路径</param>
        /// <param name="filter">监视文件过滤规则,如:*.xml</param>
        /// <param name="watchHandler">监视事件回调函数</param>
        /// <param name="timerInterval">事件延迟处理间隔,用来避免多次处理及保证文件写入完成,单位:毫秒</param>
        /// <returns></returns>
        public bool TurnOnWatcher(string path, string filter, FileSystemEventHandler watchHandler, int timerInterval = 2000)
        {
            try
            {
                if (IsOn())
                {
                    //避免重复开启产生问题
                    return true;

                    //如果参数易变,请使用下面代码,释放原有变量,重新赋值开启
                    //TurnOffWatcher();
                }
                _timer = new Timer(OnTimer, null, Timeout.Infinite, Timeout.Infinite);
                _watcher = new FileSystemWatcher(path, filter);
                _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                _watcher.Created += fileSystemWatcher_Changed;
                _watcher.Changed += fileSystemWatcher_Changed;
                //心电图不需要监控删除和重命名
                //_watcher.Deleted += fileSystemWatcher_Changed;
                //_watcher.Renamed += fileSystemWatcher_Changed;
                _watcher.EnableRaisingEvents = true;
                _watchHandler = watchHandler;
                _timerInterval = timerInterval;
                logger.LogInformation($"开启文件监视成功,监视目录path={path},监视文件类型filter={filter},回调延迟timerInterval={timerInterval}");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"文件监视开启失败,监视目录path={path},监视文件类型filter={filter},回调延迟timerInterval={timerInterval},错误信息:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 停止文件监控
        /// </summary>
        /// <returns></returns>
        public bool TurnOffWatcher()
        {
            try
            {
                if (_watcher != null)
                {
                    _watcher.EnableRaisingEvents = false;
                    _watcher = null;

                    DisposeTimer();
                }
                logger.LogInformation($"停止文件监视成功");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError($"停止文件监视失败,错误信息:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查文件监控是否开启
        /// </summary>
        /// <returns>true:已开启,false:未开启</returns>
        public bool IsOn()
        {
            if (_watcher == null)
                return false;
            return true;
        }

        /// <summary>
        /// 文件系统监视事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            lock (_changedFiles)
            {
                if (!_changedFiles.ContainsKey(e.Name))
                {
                    _changedFiles.Add(e.Name, e);
                }
            }
            try
            {
                _timer.Change(_timerInterval, Timeout.Infinite);
            }
            catch (ObjectDisposedException ex)
            {
                //当Timer.Change方法抛出ObjectDisposedException异常后的处理逻辑
                logger.LogError($"文件监控-定时器更改报错,错误信息:{ex.Message}");
            }
        }

        /// <summary>
        /// 定时器回调函数
        /// </summary>
        /// <param name="state"></param>
        private void OnTimer(object state)
        {
            Dictionary<String, FileSystemEventArgs> tempChangedFiles = new Dictionary<String, FileSystemEventArgs>();

            lock (_changedFiles)
            {
                foreach (KeyValuePair<string, FileSystemEventArgs> changedFile in _changedFiles)
                {
                    tempChangedFiles.Add(changedFile.Key, changedFile.Value);
                }
                _changedFiles.Clear();
            }

            foreach (KeyValuePair<string, FileSystemEventArgs> changedFile in tempChangedFiles)
            {
                _watchHandler(this, changedFile.Value);
            }
        }

        /// <summary>
        /// 释放定时器
        /// </summary>
        private void DisposeTimer()
        {
            using (var waitHandle = new ManualResetEvent(false))
            {
                //调用Timer的bool Dispose(WaitHandle notifyObject)重载方法，来结束Timer的触发，当线程池中的所有TimerCallBack方法都执行完毕后，Timer会发一个信号给timerDisposed
                // Dispose被调用过,返回false
                if (_timer.Dispose(waitHandle))
                {
                    //WaitHandle.WaitOne()方法会等待收到一个信号，否则一直被阻塞
                    if (!waitHandle.WaitOne(_disposalTimeout))
                    {
                        logger.LogError($"文件监控-等待定时器停止超时.");
                        throw new TimeoutException("等待定时器停止超时. ");
                    }
                }
            }
        }
    }
}
