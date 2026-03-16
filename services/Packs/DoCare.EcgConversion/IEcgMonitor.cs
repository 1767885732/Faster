using DoCare.Core.Ecg.Entities;
using FastFrame.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoCare.EcgConversion
{
    [IgnoreDependency]
    public interface IEcgMonitor
    {
        bool SyncEcgFile(string fullPath);
    }
}
