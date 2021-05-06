/*
 * File: AUTDSettings.cs
 * Project: Models
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 06/05/2021
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
using AUTD3Controller.Models.Gain;
using AUTD3Controller.Models.Modulation;
using AUTD3Sharp;
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
        public float RotateZ1 { get; set; }
        [DataMember]
        public float RotateY { get; set; }
        [DataMember]
        public float RotateZ2 { get; set; }

        public GeometrySetting() { }

        public GeometrySetting(GeometrySettingReactive obj)
        {
            No = obj.No.Value;
            X = obj.X.Value;
            Y = obj.Y.Value;
            Z = obj.Z.Value;
            RotateZ1 = obj.RotateZ1.Value;
            RotateY = obj.RotateY.Value;
            RotateZ2 = obj.RotateZ2.Value;
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
        public ReactiveProperty<float> RotateZ1 { get; }
        public ReactiveProperty<float> RotateY { get; }
        public ReactiveProperty<float> RotateZ2 { get; }

        public GeometrySettingReactive(int id)
        {
            No = new ReactiveProperty<int>(id);
            X = new ReactiveProperty<float>();
            Y = new ReactiveProperty<float>();
            Z = new ReactiveProperty<float>();
            RotateZ1 = new ReactiveProperty<float>();
            RotateY = new ReactiveProperty<float>();
            RotateZ2 = new ReactiveProperty<float>();
        }

        public GeometrySettingReactive(GeometrySetting obj)
        {
            No = new ReactiveProperty<int>(obj.No);
            X = new ReactiveProperty<float>(obj.X);
            Y = new ReactiveProperty<float>(obj.Y);
            Z = new ReactiveProperty<float>(obj.Z);
            RotateZ1 = new ReactiveProperty<float>(obj.RotateZ1);
            RotateY = new ReactiveProperty<float>(obj.RotateY);
            RotateZ2 = new ReactiveProperty<float>(obj.RotateZ2);
        }
    }

    public enum LinkSelect
    {
        SOEM,
        LocalTwinCAT
    }

    public enum GainSelect
    {
        FocalPoint,
        BesselBeam,
        Holo,
        PlaneWave,
        TransducerTest
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

        [DataMember] public FocalPoint Focus { get; set; } = new FocalPoint(90, 70, 150);
        [DataMember] public BesselBeam Bessel { get; set; } = new BesselBeam(90, 70, 0, 0, 0, 1, AUTD.Pi / 10);
        [DataMember] public Holo Holo { get; set; } = new Holo();
        [DataMember] public PlaneWave PlaneWave { get; set; } = new PlaneWave(0, 0, 1);
        [DataMember] public TransducerTest TransducerTest { get; set; } = new TransducerTest(0, 0xFF, 0);

        [DataMember] public SineModulation Sine { get; set; } = new SineModulation(150, 1.0f, 0.5f);
        [DataMember] public StaticModulation Static { get; set; } = new StaticModulation(0xFF);

        [DataMember] public STM STM { get; set; } = new STM();

        private AUTDSettings()
        {
            InterfaceName = string.Empty;
            GeometriesReactive = new ObservableCollectionWithItemNotify<GeometrySettingReactive>();
            Geometries = null;
            LinkSelected = LinkSelect.SOEM;
        }
    }
}
