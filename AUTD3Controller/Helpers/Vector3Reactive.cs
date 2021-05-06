/*
 * File: Vector3Reactive.cs
 * Project: Helpers
 * Created Date: 06/05/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 06/05/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.ComponentModel;
using Reactive.Bindings;

namespace AUTD3Controller.Helpers
{
    public class Vector3Reactive : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<int> No { get; }
        public ReactiveProperty<float> X { get; }
        public ReactiveProperty<float> Y { get; }
        public ReactiveProperty<float> Z { get; }

        public Vector3Reactive(int no)
        {
            No = new ReactiveProperty<int>(no);
            X = new ReactiveProperty<float>();
            Y = new ReactiveProperty<float>();
            Z = new ReactiveProperty<float>();
        }

        public Vector3Reactive(int no, Vector3Class v)
        {
            No = new ReactiveProperty<int>(no);
            X = new ReactiveProperty<float>(v.X);
            Y = new ReactiveProperty<float>(v.Y);
            Z = new ReactiveProperty<float>(v.Z);
        }

        public Vector3Class ToVector3() => new Vector3Class(X.Value, Y.Value, Z.Value);
    }
}
