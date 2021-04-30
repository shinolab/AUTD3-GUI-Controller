/*
 * File: PlaneWave.cs
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
    public class PlaneWave : IGain
    {
        public float DirX { get; set; }
        public float DirY { get; set; }
        public float DirZ { get; set; }

        public byte Duty { get; set; }

        public PlaneWave(float dx, float dy, float dz, byte duty = 0xFF)
        {
            DirX = dx;
            DirY = dy;
            DirZ = dz;
            Duty = duty;
        }

        public AUTD3Sharp.Gain ToGain() => AUTD3Sharp.Gain.PlaneWaveGain(new Vector3f(DirX, DirY, DirZ), Duty);
    }
}
