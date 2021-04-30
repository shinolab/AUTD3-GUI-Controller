/*
 * File: BesselBeam.cs
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
    public class BesselBeam : IGain
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float DirX { get; set; }
        public float DirY { get; set; }
        public float DirZ { get; set; }

        public float Theta { get; set; }
        public byte Duty { get; set; }

        public BesselBeam(float x, float y, float z, float dx, float dy, float dz, float theta, byte duty = 0xFF)
        {
            X = x;
            Y = y;
            Z = z;
            DirX = dx;
            DirY = dy;
            DirZ = dz;
            Theta = theta;
            Duty = duty;
        }

        public AUTD3Sharp.Gain ToGain() => AUTD3Sharp.Gain.BesselBeamGain(new Vector3f(X, Y, Z), new Vector3f(DirX, DirY, DirZ), Theta, Duty);
    }
}
