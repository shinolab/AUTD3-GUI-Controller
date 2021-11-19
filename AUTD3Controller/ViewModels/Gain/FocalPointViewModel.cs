/*
 * File: FocalPointViewModel.cs
 * Project: Gain
 * Created Date: 30/04/2021
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
using AUTD3Controller.Models.Gain;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels.Gain;

public class FocalPointViewModel : ReactivePropertyBase
{
    public ReactivePropertySlim<FocalPoint> Focus { get; }

    public FocalPointViewModel()
    {
        Focus = AUTDSettings.Instance.ToReactivePropertySlimAsSynchronized(i => i.Focus);
    }
}
