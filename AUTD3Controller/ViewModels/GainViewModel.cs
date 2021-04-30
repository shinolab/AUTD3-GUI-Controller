/*
 * File: GainViewModel.cs
 * Project: ViewModels
 * Created Date: 31/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 30/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.ComponentModel;
using System.Reactive.Linq;
using AUTD3Controller.Models;
using AUTD3Controller.Models.Gain;
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

        public ReactiveProperty<FocalPoint> Focus { get; }
        public ReactiveProperty<BesselBeam> Bessel { get; }

        public GainViewModel()
        {
            GainSelect = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.GainSelect);
            AppendGainCommand = AUTDHandler.Instance.IsOpen.Select(b => b).ToReactiveCommand();
            AppendGainCommand.Subscribe(_ =>
            {
                AUTDHandler.Instance.AppendGain();
            });

            Focus = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Focus);
            Bessel = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Bessel);
        }
    }
}
