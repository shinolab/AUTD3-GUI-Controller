/*
 * File: AUTDSettings.cs
 * Project: Models
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 07/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using AUTD3Controller.Helpers;
using Reactive.Bindings;

namespace AUTD3Controller.Models
{
    [DataContract]
    public class GeometrySetting
    {
        [DataMember]
        public int No { get; set; }
        [DataMember]
        public float X { get; set; }
        [DataMember]
        public float Y { get; set; }
        [DataMember]
        public float Z { get; set; }
        [DataMember]
        public float RZ1 { get; set; }
        [DataMember]
        public float RY { get; set; }
        [DataMember]
        public float RZ2 { get; set; }

        public GeometrySetting() { }

        public GeometrySetting(GeometrySettingReactive obj)
        {
            No = obj.No.Value;
            X = obj.X.Value;
            Y = obj.Y.Value;
            Z = obj.Z.Value;
            RZ1 = obj.RZ1.Value;
            RY = obj.RY.Value;
            RZ2 = obj.RZ2.Value;
        }
    }

    public class GeometrySettingReactive : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<int> No { get; }
        public ReactiveProperty<float> X { get; }
        public ReactiveProperty<float> Y { get; }
        public ReactiveProperty<float> Z { get; }
        public ReactiveProperty<float> RZ1 { get; }
        public ReactiveProperty<float> RY { get; }
        public ReactiveProperty<float> RZ2 { get; }

        public GeometrySettingReactive(int id)
        {
            No = new ReactiveProperty<int>(id);
            X = new ReactiveProperty<float>();
            Y = new ReactiveProperty<float>();
            Z = new ReactiveProperty<float>();
            RZ1 = new ReactiveProperty<float>();
            RY = new ReactiveProperty<float>();
            RZ2 = new ReactiveProperty<float>();
        }

        public GeometrySettingReactive(GeometrySetting obj)
        {
            No = new ReactiveProperty<int>(obj.No);
            X = new ReactiveProperty<float>(obj.X);
            Y = new ReactiveProperty<float>(obj.Y);
            Z = new ReactiveProperty<float>(obj.Z);
            RZ1 = new ReactiveProperty<float>(obj.RZ1);
            RY = new ReactiveProperty<float>(obj.RY);
            RZ2 = new ReactiveProperty<float>(obj.RZ2);
        }
    }

    public enum LinkSelect
    {
        SOEM,
        LocalTwinCAT
    }

    public enum GainSelect
    {
        Focus,
        Bessel
    }

    public enum ModulationSelect
    {
        Sine,
        Static
    }

    [DataContract]
    public class AUTDSettings : INotifyPropertyChanged
    {
        private static Lazy<AUTDSettings> _lazy = new Lazy<AUTDSettings>(() => new AUTDSettings());
        public static AUTDSettings Instance { get => _lazy.Value; set => _lazy = new Lazy<AUTDSettings>(() => value); }

#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        [JsonIgnore]
        public ObservableCollectionWithItemNotify<GeometrySettingReactive> GeometriesReactive { get; internal set; }

        [DataMember]
        public GeometrySetting[]? Geometries { get; set; }

        [DataMember]
        public LinkSelect LinkSelected { get; set; }
        [DataMember]
        public string InterfaceName { get; set; }

        [DataMember]
        public GainSelect GainSelect { get; set; }
        [DataMember]
        public ModulationSelect ModulationSelect { get; set; }

        [DataMember]
        public float FocusX { get; set; } = 90;
        [DataMember]
        public float FocusY { get; set; } = 70;
        [DataMember]
        public float FocusZ { get; set; } = 150;
        [DataMember]
        public byte FocusDuty { get; set; } = 0xFF;

        [DataMember]
        public float BesselX { get; set; } = 90;
        [DataMember]
        public float BesselY { get; set; } = 70;
        [DataMember]
        public float BesselZ { get; set; } = 0;
        [DataMember]
        public float BesselDX { get; set; } = 0;
        [DataMember]
        public float BesselDY { get; set; } = 0;
        [DataMember]
        public float BesselDZ { get; set; } = 1.0f;
        [DataMember]
        public float BesselTheta { get; set; } = 0.1f * MathF.PI;
        [DataMember]
        public byte BesselDuty { get; set; } = 0xFF;

        [DataMember]
        public int SinFrequency { get; set; } = 150;
        [DataMember]
        public float SinAmp { get; set; } = 1.0f;
        [DataMember]
        public float SinOffset { get; set; } = 0.5f;
        [DataMember]
        public byte StaticDuty { get; set; } = 0xFF;

        private AUTDSettings()
        {
            InterfaceName = string.Empty;
            GeometriesReactive = new ObservableCollectionWithItemNotify<GeometrySettingReactive>();
            Geometries = null;
            LinkSelected = LinkSelect.SOEM;
        }
    }
}
