﻿/*
 * File: PlaneWaveViewModel.cs
 * Project: Gain
 * Created Date: 30/04/2021
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
using AUTD3Controller.Models.Gain;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels.Gain
{
    public class PlaneWaveViewModel : ReactivePropertyBase
    {


        public ReactiveProperty<PlaneWave> PlaneWave { get; }

        public PlaneWaveViewModel()
        {
            PlaneWave = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.PlaneWave);
        }
    }
}
