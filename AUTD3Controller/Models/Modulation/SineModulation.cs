/*
 * File: SineModulation.cs
 * Project: Modulation
 * Created Date: 06/05/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 06/05/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

namespace AUTD3Controller.Models.Modulation
{
    public class SineModulation
    {
        public int Frequency { get; set; }
        public float Amp { get; set; }
        public float Offset { get; set; }

        public SineModulation() { }

        public SineModulation(int frequency, float amp, float offset)
        {
            Frequency = frequency;
            Amp = amp;
            Offset = offset;
        }

        public AUTD3Sharp.Modulation ToModulation() => AUTD3Sharp.Modulation.SineModulation(Frequency, Amp, Offset);
    }
}
