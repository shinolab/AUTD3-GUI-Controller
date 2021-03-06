/*
 * File: AUTDHandler.cs
 * Project: Models
 * Created Date: 31/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.Linq;
using AUTD3Controller.Helpers;
using AUTD3Sharp;
using AUTD3Sharp.Utils;
using Reactive.Bindings;

namespace AUTD3Controller.Models;

internal class AUTDHandler : ReactivePropertyBase, IDisposable
{
    private static readonly Lazy<AUTDHandler> Lazy = new(() => new AUTDHandler());
    public static AUTDHandler Instance => Lazy.Value;

    private AUTD? _autd;
    public ReactivePropertySlim<bool> IsOpen { get; }
    public ReactivePropertySlim<bool> IsRunning { get; }

    private AUTDHandler()
    {
        _autd = null;
        IsOpen = new ReactivePropertySlim<bool>();
        IsRunning = new ReactivePropertySlim<bool>();
    }

    private void AddDevices()
    {
        foreach (var item in AUTDSettings.Instance.GeometriesReactive)
        {
            _autd?.AddDevice(new Vector3d(item.X.Value, item.Y.Value, item.Z.Value), new Vector3d(item.RotateZ1.Value, item.RotateY.Value, item.RotateZ2.Value));
        }
    }

    public string? Open()
    {
        try
        {
            _autd = new AUTD();

            AddDevices();

            var link = AUTDSettings.Instance.LinkSelected switch
            {
                LinkSelect.SOEM => Link.SOEM(AUTDSettings.Instance.InterfaceName.Split(',').LastOrDefault()?.Trim() ?? string.Empty,
                    _autd.NumDevices, AUTDSettings.Instance.CycleTicks),
                LinkSelect.TwinCAT => Link.TwinCAT(),
                LinkSelect.Emulator => Link.Emulator(AUTDSettings.Instance.EmulatorPort, _autd),
                _ => throw new NotImplementedException()
            };

            if (!_autd.Open(link)) return AUTD.LastError;

            IsOpen.Value = true;
            _autd.Clear();
            return null;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public void Close()
    {
        _autd?.Close();
        _autd?.Dispose();
        _autd = null;
        IsOpen.Value = false;
    }

    public bool SendGain()
    {
        var instance = AUTDSettings.Instance;
        var gain = instance.GainSelect switch
        {
            GainSelect.FocalPoint => instance.Focus.ToGain(),
            GainSelect.BesselBeam => instance.Bessel.ToGain(),
            GainSelect.Holo => instance.Holo.ToGain(),
            GainSelect.PlaneWave => instance.PlaneWave.ToGain(),
            GainSelect.TransducerTest => instance.TransducerTest.ToGain(),
            _ => throw new ArgumentOutOfRangeException()
        };
        var res = _autd?.Send(gain) >= 0;
        IsRunning.Value = res;
        return res;
    }

    public bool SendModulation()
    {
        var instance = AUTDSettings.Instance;
        var gain = instance.ModulationSelect switch
        {
            ModulationSelect.Static => instance.Static.ToModulation(),
            ModulationSelect.Sine => instance.Sine.ToModulation(),
            ModulationSelect.SinePressure => instance.SinePressure.ToModulation(),
            ModulationSelect.Square => instance.Square.ToModulation(),
            _ => throw new ArgumentOutOfRangeException()
        };
        return _autd?.Send(gain) >= 0;
    }

    public bool SendSeq()
    {
        var instance = AUTDSettings.Instance;
        var seq = instance.Seq.ToPointSequence();
        var res = _autd?.Send(seq) >= 0;
        IsRunning.Value = res;
        return res;
    }

    public void Stop()
    {
        _autd?.Stop();
        IsRunning.Value = false;
    }

    public void Dispose()
    {
        if (_autd is { IsOpen: true })
        {
            _autd.Clear();
            _autd.Close();
        }
        _autd?.Dispose();
        _autd = null;
    }
}
