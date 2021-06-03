/*
 * File: General.cs
 * Project: Models
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 03/06/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.ComponentModel;

namespace AUTD3Controller.Models
{
    public enum AngleUnit
    {
        Radian,
        Degree
    }

    public class General : INotifyPropertyChanged
    {
        private static Lazy<General> _lazy = new Lazy<General>(() => new General());
        public static General Instance { get => _lazy.Value; set => _lazy = new Lazy<General>(() => value); }

#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public AngleUnit AngleUnit { get; set; }

        private General()
        {
            AngleUnit = AngleUnit.Radian;
        }

        public double ConvertAngle(double angle)
        {
            return AngleUnit == AngleUnit.Radian ? angle : angle / 180.0 * Math.PI;
        }
    }
}
