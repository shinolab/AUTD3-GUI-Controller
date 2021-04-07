/*
 * File: GainViewModel.cs
 * Project: ViewModels
 * Created Date: 31/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 07/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.ComponentModel;
using System.Reactive.Linq;
using AUTD3Controller.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels
{
    internal class GainViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<GainSelect> GainSelect { get; }
        public ReactiveCommand AppendGainCommand { get; }

        public ReactiveProperty<float> FocusX { get; }
        public ReactiveProperty<float> FocusY { get; }
        public ReactiveProperty<float> FocusZ { get; }
        public ReactiveProperty<byte> FocusDuty { get; }

        public ReactiveProperty<float> BesselX { get; }
        public ReactiveProperty<float> BesselY { get; }
        public ReactiveProperty<float> BesselZ { get; }
        public ReactiveProperty<float> BesselDX { get; }
        public ReactiveProperty<float> BesselDY { get; }
        public ReactiveProperty<float> BesselDZ { get; }
        public ReactiveProperty<float> BesselTheta { get; }
        public ReactiveProperty<byte> BesselDuty { get; }

        public GainViewModel()
        {
            GainSelect = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.GainSelect);
            AppendGainCommand = AUTDHandler.Instance.IsOpen.Select(b => b).ToReactiveCommand();
            AppendGainCommand.Subscribe(_ => {
                AUTDHandler.Instance.AppendGain();
            });

            FocusX = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.FocusX);
            FocusY = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.FocusY);
            FocusZ = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.FocusZ);
            FocusDuty = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.FocusDuty);

            BesselX = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselX);
            BesselY = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselY);
            BesselZ = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselZ);
            BesselDX = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselDX);
            BesselDY = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselDY);
            BesselDZ = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselDZ);
            BesselTheta = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselTheta);
            BesselDuty = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.BesselDuty);
        }
    }
}
