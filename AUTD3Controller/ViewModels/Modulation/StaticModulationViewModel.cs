/*
 * File: StaticModulationViewModel.cs
 * Project: Modulation
 * Created Date: 06/05/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using AUTD3Controller.Helpers;
using AUTD3Controller.Models;
using AUTD3Controller.Models.Modulation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels.Modulation;

public class StaticModulationViewModel : ReactivePropertyBase
{
    public ReactivePropertySlim<StaticModulation> Static { get; }

    public StaticModulationViewModel()
    {
        Static = AUTDSettings.Instance.ToReactivePropertySlimAsSynchronized(i => i.Static);
    }
}
