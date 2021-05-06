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
using AUTD3Sharp.Utils;
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

        public Vector3Reactive(int no, Vector3f v)
        {
            No = new ReactiveProperty<int>(no);
            X = new ReactiveProperty<float>(v.x);
            Y = new ReactiveProperty<float>(v.y);
            Z = new ReactiveProperty<float>(v.z);
        }

        public Vector3f ToVector3f() => new Vector3f(X.Value, Y.Value, Z.Value);
    }
}
