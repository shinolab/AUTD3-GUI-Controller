/*
 * File: ReactivePropertyBase.cs
 * Project: Helpers
 * Created Date: 03/06/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 10/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.ComponentModel;

namespace AUTD3Controller.Helpers
{
    public class ReactivePropertyBase : INotifyPropertyChanged
    {
#pragma warning disable 414
#pragma warning disable CS8612
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore CS8612
#pragma warning restore 414
    }
}
