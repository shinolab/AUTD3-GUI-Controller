/*
 * File: Vector3Class.cs
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

namespace AUTD3Controller.Helpers
{
    public class Vector3Class
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3Class()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public Vector3Class(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
