/*
 * File: FocalPoint.cs
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


using AUTD3Sharp.Utils;

namespace AUTD3Controller.Models.Gain
{
    public class FocalPoint : IGain
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte Duty { get; set; }

        public FocalPoint(float x, float y, float z, byte duty = 0xFF)
        {
            X = x;
            Y = y;
            Z = z;
            Duty = duty;
        }

        public AUTD3Sharp.Gain ToGain() => AUTD3Sharp.Gain.FocalPointGain(new Vector3f(X, Y, Z), Duty);
    }
}
