﻿/*
 * File: AUTDHandler.cs
 * Project: Models
 * Created Date: 31/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 06/05/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.ComponentModel;
using System.Linq;
using AUTD3Sharp;
using AUTD3Sharp.Utils;
using Reactive.Bindings;

namespace AUTD3Controller.Models
{
    internal class AUTDHandler : INotifyPropertyChanged, IDisposable
    {
        private static readonly Lazy<AUTDHandler> Lazy = new Lazy<AUTDHandler>(() => new AUTDHandler());
        public static AUTDHandler Instance => Lazy.Value;

#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        private readonly AUTD _autd;
        public ReactiveProperty<bool> IsOpen { get; }
        public ReactiveProperty<bool> IsRunning { get; }

        private AUTDHandler()
        {
            _autd = new AUTD();
            IsOpen = new ReactiveProperty<bool>(false);
            IsRunning = new ReactiveProperty<bool>(false);
        }

        private void AddDevices()
        {
            _autd.ClearDevices();
            foreach (var item in AUTDSettings.Instance.GeometriesReactive)
            {
                _autd.AddDevice(new Vector3f(item.X.Value, item.Y.Value, item.Z.Value), new Vector3f(item.RotateZ1.Value, item.RotateY.Value, item.RotateZ2.Value));
            }
        }

        public string? Open()
        {
            try
            {
                AddDevices();

                var link = AUTDSettings.Instance.LinkSelected switch
                {
                    LinkSelect.SOEM =>
                        Link.SOEMLink(
                            AUTDSettings.Instance.InterfaceName.Split(',').LastOrDefault()?.Trim() ?? string.Empty,
                            _autd.NumDevices),
                    LinkSelect.LocalTwinCAT =>
                        Link.LocalEtherCATLink(),
                    _ => throw new NotImplementedException()
                };

                if (!_autd.OpenWith(link)) return AUTD.LastError;

                IsOpen.Value = true;
                _autd.Clear();
                _autd.Synchronize();
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void Close()
        {
            _autd.Close();
            IsOpen.Value = false;
        }

        public void AppendGain()
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
            _autd.AppendGainSync(gain);
            IsRunning.Value = true;
        }

        public void AppendModulation()
        {
            var instance = AUTDSettings.Instance;
            var gain = instance.ModulationSelect switch
            {
                ModulationSelect.Static => instance.Static.ToModulation(),
                ModulationSelect.Sine => instance.Sine.ToModulation(),
                _ => throw new ArgumentOutOfRangeException()
            };
            _autd.AppendModulationSync(gain);
        }

        public void AppendSTM()
        {
            var instance = AUTDSettings.Instance;
            var seq = instance.STM.ToPointSequence();
            _autd.AppendSequence(seq);
            IsRunning.Value = true;
        }

        public void Stop()
        {
            _autd.Stop();
            IsRunning.Value = false;
        }

        public void Dispose()
        {
            _autd.Clear();
            _autd.Close();
            _autd.Dispose();
        }
    }
}
