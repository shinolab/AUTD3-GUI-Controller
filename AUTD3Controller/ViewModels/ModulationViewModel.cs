/*
 * File: ModulationViewModel.cs
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
    internal class ModulationViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<ModulationSelect> ModulationSelect { get; }
        public ReactiveCommand AppendModulationCommand { get; }

        public ReactiveProperty<int> SinFrequency { get; }
        public ReactiveProperty<float> SinAmp { get; }
        public ReactiveProperty<float> SinOffset { get; }

        public ReactiveProperty<byte> StaticDuty { get; }

        public ModulationViewModel()
        {
            ModulationSelect = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.ModulationSelect);
            AppendModulationCommand = AUTDHandler.Instance.IsOpen.Select(b => b).ToReactiveCommand();
            AppendModulationCommand.Subscribe(_ => AUTDHandler.Instance.AppendModulation());

            SinFrequency = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.SinFrequency);
            SinAmp = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.SinAmp);
            SinOffset = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.SinOffset);

            StaticDuty = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.StaticDuty);
        }
    }
}
