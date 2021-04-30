/*
 * File: Holo.cs
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

using System.Collections.Generic;
using AUTD3Sharp.Utils;

namespace AUTD3Controller.Models.Gain
{
    public enum OptMethod
    {
        SDP = 0,
        EVD = 1,
        GS = 2,
        GSPAT = 3,
        NAIVE = 4,
        LM = 5
    }

    public class Holo : IGain
    {
        public List<Vector3f> Foci { get; set; }
        public List<float> Amps { get; set; }

        public OptMethod OptMethod { get; set; }

        public Holo()
        {
            Foci = new List<Vector3f>();
            Amps = new List<float>();
        }

        public AUTD3Sharp.Gain ToGain() => OptMethod switch
        {
            OptMethod.SDP => AUTD3Sharp.Gain.HoloGainSDP(Foci.ToArray(), Amps.ToArray(), null)
        };
    }
}
