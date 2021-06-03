/*
 * File: SawModulation.cs
 * Project: Modulation
 * Created Date: 03/06/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 03/06/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

namespace AUTD3Controller.Models.Modulation
{
    public class SawModulation
    {
        public int Frequency { get; set; }

        public SawModulation() { }

        public SawModulation(int frequency)
        {
            Frequency = frequency;
        }

        public AUTD3Sharp.Modulation ToModulation() => AUTD3Sharp.Modulation.Saw(Frequency);
    }
}
