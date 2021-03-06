/*
 * File: ControlPointsReactive.cs
 * Project: Helpers
 * Created Date: 06/05/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using Reactive.Bindings;

namespace AUTD3Controller.Helpers;

public class ControlPointsReactive : ReactivePropertyBase
{
    public ReactivePropertySlim<int> No { get; }
    public ReactivePropertySlim<double> X { get; }
    public ReactivePropertySlim<double> Y { get; }
    public ReactivePropertySlim<double> Z { get; }
    public ReactivePropertySlim<byte> Duty { get; }

    public ControlPointsReactive(int no)
    {
        No = new ReactivePropertySlim<int>(no);
        X = new ReactivePropertySlim<double>();
        Y = new ReactivePropertySlim<double>();
        Z = new ReactivePropertySlim<double>();
        Duty = new ReactivePropertySlim<byte>(0xFF);
    }

    public ControlPointsReactive(int no, Vector3Class v, byte duty)
    {
        No = new ReactivePropertySlim<int>(no);
        X = new ReactivePropertySlim<double>(v.X);
        Y = new ReactivePropertySlim<double>(v.Y);
        Z = new ReactivePropertySlim<double>(v.Z);
        Duty = new ReactivePropertySlim<byte>(duty);
    }

    public Vector3Class ToVector3() => new(X.Value, Y.Value, Z.Value);
}
