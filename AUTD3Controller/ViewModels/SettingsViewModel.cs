/*
 * File: SettingsViewModel.cs
 * Project: ViewModels
 * Created Date: 29/03/2021
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
using AUTD3Controller.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public AngleUnit[] AngleUnits { get; } = (AngleUnit[])Enum.GetValues(typeof(AngleUnit));
        public ReactiveProperty<AngleUnit> AngleUnit { get; }

        public SettingsViewModel()
        {
            AngleUnit = General.Instance.ToReactivePropertyAsSynchronized(g => g.AngleUnit);
        }
    }
}