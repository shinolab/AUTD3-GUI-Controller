/*
 * File: FocalPointViewModel.cs
 * Project: Gain
 * Created Date: 30/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 30/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.ComponentModel;
using AUTD3Controller.Models;
using AUTD3Controller.Models.Gain;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels.Gain
{
    public class FocalPointViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<FocalPoint> Focus { get; }

        public FocalPointViewModel()
        {
            Focus = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Focus);
        }
    }
}
