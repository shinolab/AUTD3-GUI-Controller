﻿/*
 * File: StaticModulation.cs
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
    public class StaticModulation : IModulation
    {
        public byte Duty { get; set; }

        public StaticModulation() { }

        public StaticModulation(byte duty = 0xFF)
        {
            Duty = duty;
        }

        public AUTD3Sharp.Modulation ToModulation() => AUTD3Sharp.Modulation.StaticModulation(Duty);
    }
}
