using FastFrame.Core;
using Microsoft.Extensions.DependencyInjection;
using ECGConversion;
using ECGConversion.ECGSignals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DoCare.EcgConversion.Common
{
    [Dependency(ServiceLifetime.Scoped)]
    public class EcgImgConversion
    {
        private int _width = 1152;
        private int _height = 580;
        private float _Gain = 10f;
        private int _Zoom = 1;
        private int _OffsetX = 0;
        private int _OffsetY = 0;
        private Signals _CurrentSignal = null;
        private Bitmap _DrawBuffer = null;
        private ECGDraw.ECGDrawType _DrawType = ECGDraw.ECGDrawType.Regular;
        private IECGFormat _CurrentECG = null;
        private float Gain
        {
            get
            {
                lock (this)
                {
                    return _Gain;
                }
            }
            set
            {
                lock (this)
                {
                    if (value == 40f)
                    {
                        _Gain = value;
                    }
                    else if (value == 20f)
                    {
                        _Gain = value;

                    }
                    else if (value == 10f)
                    {
                        _Gain = value;
                    }
                    else if (value == 5f)
                    {
                        _Gain = value;
                    }
                }
            }
        }
        private IECGFormat CurrentECG
        {
            get
            {
                lock (this)
                {
                    return _CurrentECG;
                }
            }
            set
            {
                lock (this)
                {
                    _Zoom = 1;
                    _OffsetX = 0;
                    _OffsetY = 0;

                    if ((_CurrentECG != null)
                    && (_CurrentECG != value))
                        _CurrentECG.Dispose();

                    if (value == null)
                    {
                        if (_CurrentECG != null)
                            _CurrentECG.Dispose();

                        _CurrentECG = null;
                        _CurrentSignal = null;
                    }
                    else
                    {
                        Gain = 10f;
                        _CurrentECG = value;

                        if (_CurrentECG.Signals.getSignals(out _CurrentSignal) != 0)
                        {
                            _CurrentECG.Dispose();
                            _CurrentECG = null;
                        }
                        else
                        {
                            if (_CurrentSignal != null)
                            {
                                for (int i = 0, e = _CurrentSignal.NrLeads; i < e; i++)
                                {
                                    ECGTool.NormalizeSignal(_CurrentSignal[i].Rhythm, _CurrentSignal.RhythmSamplesPerSecond);
                                }
                            }

                            Signals sig = _CurrentSignal.CalculateTwelveLeads();

                            if (sig != null)
                                _CurrentSignal = sig;

                            if (_CurrentSignal.IsBuffered)
                            {
                                BufferedSignals bs = _CurrentSignal.AsBufferedSignals;

                                bs.LoadSignal(bs.RealRhythmStart, bs.RealRhythmStart + 60 * bs.RhythmSamplesPerSecond);
                            }
                            else
                            {
                                int start, end;

                                _CurrentSignal.CalculateStartAndEnd(out start, out end);

                            }
                        }

                        ECGDraw.ECGDrawType dt = ECGDraw.PossibleDrawTypes(_CurrentSignal);

                    }

                    _DrawBuffer = null;
                }
            }
        }
        private Bitmap DrawBuffer
        {
            get
            {
                lock (this)
                {
                    return _DrawBuffer;
                }
            }
            set
            {
                lock (this)
                {
                    if (_DrawBuffer != null)
                        _DrawBuffer.Dispose();

                    _DrawBuffer = value;
                }
            }
        }

        public Bitmap ConvertToBitmap(IECGFormat ecgFormat)
        {
            lock (this)
            {
                CurrentECG = ecgFormat;

                if (_CurrentECG == null)
                {
                    return null;
                }

                if (_DrawBuffer == null)
                {
                    int w = _width,
                        h = _height;


                    int n = 0;
                    int[,] s = { { 782, 492 }, { 1042, 657 }, { 1302, 822 } };

                    for (; n < s.GetLength(0); n++)
                        if ((s[n, 0] > w)
                        || (s[n, 1] > h))
                            break;

                    n += 2;

                    // zoom mode on
                    if (_Zoom > 1)
                    {
                        n *= _Zoom;
                        w *= _Zoom;
                        h *= _Zoom;

                        int start, end;

                        if (_DrawType != ECGConversion.ECGDraw.ECGDrawType.Regular)
                        {
                            start = n * 5 * 33 + 1;
                            end = n * 5 * 52 + 1;
                        }
                        else
                        {
                            _CurrentSignal.CalculateStartAndEnd(out start, out end);

                            end = (((end - start) * 25 * n) / _CurrentSignal.RhythmSamplesPerSecond) + 1 + (n * 5);
                            start = int.MaxValue;
                        }

                        if (w > end)
                            w = end;

                        if (w < _width)
                            w = _width;

                        if (h > start)
                            h = start;

                        if (h < _height)
                            h = _height;
                    }

                    _DrawBuffer = new Bitmap(w, h);

                    ECGConversion.ECGDraw.DpiX = ECGConversion.ECGDraw.DpiY = 25.4f * n;

                    int
                        oldSPS = _CurrentSignal.RhythmSamplesPerSecond,
                        ret = ECGDraw.DrawECG(Graphics.FromImage(_DrawBuffer), _CurrentSignal, _DrawType, 0, 25.0f, _Gain, true);

                    if (ret < 0)
                    {
                        Graphics g = Graphics.FromImage(_DrawBuffer);
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        ret = ECGDraw.DrawECG(g, _CurrentSignal, ECGDraw.ECGDrawType.Regular, 0, 25.0f, _Gain, false);
                    }
                }

                if (_OffsetX < 0)
                    _OffsetX = 0;
                else if (_OffsetX > (_DrawBuffer.Width - _width))
                    _OffsetX = _DrawBuffer.Width - _width;

                if (_OffsetY < 0)
                    _OffsetY = 0;
                else if (_OffsetY > (_DrawBuffer.Height - _height))
                    _OffsetY = _DrawBuffer.Height - _height;

                return _DrawBuffer;
            }
        }

    }
}
