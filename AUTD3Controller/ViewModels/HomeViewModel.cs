/*
 * File: HomeViewModel.cs
 * Project: ViewModels
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 03/06/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using AUTD3Controller.Helpers;
using AUTD3Controller.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels
{
    public class HomeViewModel : ReactivePropertyBase
    {
        public ReactivePropertySlim<AngleUnit> AngleUnit { get; }

        public HomeViewModel()
        {
            AngleUnit = General.Instance.ToReactivePropertySlimAsSynchronized(g => g.AngleUnit);
        }
    }
}
