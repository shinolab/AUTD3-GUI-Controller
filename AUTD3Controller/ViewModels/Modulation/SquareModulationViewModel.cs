/*
 * File: SquareModulationViewModel.cs
 * Project: Modulation
 * Created Date: 03/06/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 03/06/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.ComponentModel;
using AUTD3Controller.Models;
using AUTD3Controller.Models.Modulation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels.Modulation
{
    public class SquareModulationViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<SquareModulation> Square { get; }

        public SquareModulationViewModel()
        {
            Square = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Square);
        }
    }
}
