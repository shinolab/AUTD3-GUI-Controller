/*
 * File: General.cs
 * Project: Models
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 10/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using AUTD3Controller.Helpers;

namespace AUTD3Controller.Models
{
    public enum AngleUnit
    {
        Radian,
        Degree
    }

    public class General : ReactivePropertyBase
    {
        private static Lazy<General> _lazy = new Lazy<General>(() => new General());
        public static General Instance { get => _lazy.Value; set => _lazy = new Lazy<General>(() => value); }

        public AngleUnit AngleUnit { get; set; }

        public General()
        {
            AngleUnit = AngleUnit.Radian;
        }

        public double ConvertAngle(double angle)
        {
            return AngleUnit == AngleUnit.Radian ? angle : angle / 180.0 * Math.PI;
        }
    }
}
