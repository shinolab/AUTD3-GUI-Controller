/*
 * File: IGain.cs
 * Project: Gain
 * Created Date: 30/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

namespace AUTD3Controller.Models.Gain;

public interface IGain
{
    public AUTD3Sharp.Gain ToGain();
}
