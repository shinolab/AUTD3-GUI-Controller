/*
 * File: STM.cs
 * Project: Models
 * Created Date: 06/05/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 06/05/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.Linq;
using System.Text.Json.Serialization;
using AUTD3Controller.Helpers;
using AUTD3Sharp;
using AUTD3Sharp.Utils;

namespace AUTD3Controller.Models
{
    public class STM
    {
        [JsonIgnore]
        public ObservableCollectionWithItemNotify<Vector3Reactive> PointsReactive { get; internal set; }

        public Vector3Class[]? Points { get; set; }

        public float Frequency { get; set; }

        public STM()
        {
            PointsReactive = new ObservableCollectionWithItemNotify<Vector3Reactive>();
            Points = null;
            Frequency = 1;
        }

        public PointSequence ToPointSequence()
        {
            var seq = PointSequence.Create();
            seq.AddPoints(PointsReactive.Select(s => new Vector3f(s.X.Value, s.Y.Value, s.Z.Value)).ToArray());
            seq.SetFrequency(Frequency);
            return seq;
        }
    }
}
