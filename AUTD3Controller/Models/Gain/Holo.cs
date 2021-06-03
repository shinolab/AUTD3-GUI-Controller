﻿/*
 * File: Holo.cs
 * Project: Gain
 * Created Date: 30/04/2021
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
using System.Linq;
using System.Text.Json.Serialization;
using AUTD3Controller.Helpers;
using AUTD3Sharp.Utils;
using Reactive.Bindings;

namespace AUTD3Controller.Models.Gain
{
    public enum OptMethod
    {
        SDP,
        EVD,
        Naive,
        GS,
        GSPAT,
        LM,
        Greedy
    }

    public class SDPParams
    {
        public double Alpha { get; set; } = 1e-3f;
        public ulong Repeat { get; set; } = 100;
        public double Lambda { get; set; } = 0.9f;
        public bool NormalizeAmp { get; set; } = true;
    }

    public class EVDParams
    {
        public double Gamma { get; set; } = 1;
        public bool NormalizeAmp { get; set; } = true;
    }

    public class NLSParams
    {
        public double Eps1 { get; set; } = 1e-8f;
        public double Eps2 { get; set; } = 1e-8f;
        public ulong KMax { get; set; } = 5;
        public double Tau { get; set; } = 1e-3f;
    }

    public class HoloSettingReactive : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<int> No { get; }
        public ReactiveProperty<double> X { get; }
        public ReactiveProperty<double> Y { get; }
        public ReactiveProperty<double> Z { get; }
        public ReactiveProperty<double> Amp { get; }

        public HoloSettingReactive(int no)
        {
            No = new ReactiveProperty<int>(no);
            X = new ReactiveProperty<double>();
            Y = new ReactiveProperty<double>();
            Z = new ReactiveProperty<double>();
            Amp = new ReactiveProperty<double>(1.0f);
        }

        public HoloSettingReactive(HoloSetting s)
        {
            No = new ReactiveProperty<int>(s.No);
            X = new ReactiveProperty<double>(s.X);
            Y = new ReactiveProperty<double>(s.Y);
            Z = new ReactiveProperty<double>(s.Z);
            Amp = new ReactiveProperty<double>(s.Amp);
        }
    }

    public class HoloSetting
    {
        public int No { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Amp { get; set; }

        public HoloSetting() { }

        public HoloSetting(HoloSettingReactive s)
        {
            No = s.No.Value;
            X = s.X.Value;
            Y = s.Y.Value;
            Z = s.Z.Value;
            Amp = s.Amp.Value;
        }
    }

    public class Holo : IGain
    {
        [JsonIgnore]
        public ObservableCollectionWithItemNotify<HoloSettingReactive> HoloSettingsReactive { get; internal set; }

        public HoloSetting[]? HoloSettings { get; set; }

        public OptMethod OptMethod { get; set; }

        public SDPParams SDPParams { get; set; } = new SDPParams();
        public EVDParams EVDParams { get; set; } = new EVDParams();
        public NLSParams NLSParams { get; set; } = new NLSParams();
        public uint GSRepeat { get; set; } = 100;
        public uint GSPATRepeat { get; set; } = 100;
        public int GreedyPhaseDiv { get; set; } = 16;

        public Holo()
        {
            HoloSettingsReactive = new ObservableCollectionWithItemNotify<HoloSettingReactive>();
            HoloSettings = null;
        }

        public AUTD3Sharp.Gain ToGain() => OptMethod switch
        {
            OptMethod.SDP => AUTD3Sharp.Gain.HoloSDP(Foci, Amps, SDPParams.Alpha, SDPParams.Lambda, SDPParams.Repeat, SDPParams.NormalizeAmp),
            OptMethod.EVD => AUTD3Sharp.Gain.HoloEVD(Foci, Amps, EVDParams.Gamma, EVDParams.NormalizeAmp),
            OptMethod.GS => AUTD3Sharp.Gain.HoloGS(Foci, Amps, GSRepeat),
            OptMethod.GSPAT => AUTD3Sharp.Gain.HoloGSPAT(Foci, Amps, GSPATRepeat),
            OptMethod.Naive => AUTD3Sharp.Gain.HoloNaive(Foci, Amps),
            OptMethod.LM => AUTD3Sharp.Gain.HoloLM(Foci, Amps, NLSParams.Eps1, NLSParams.Eps2, NLSParams.Tau, NLSParams.KMax),
            OptMethod.Greedy => AUTD3Sharp.Gain.HoloGreedy(Foci, Amps, GreedyPhaseDiv),
            _ => throw new ArgumentOutOfRangeException()
        };

        private Vector3d[] Foci => HoloSettingsReactive.Select(s => new Vector3d(s.X.Value, s.Y.Value, s.Z.Value)).ToArray();
        private double[] Amps => HoloSettingsReactive.Select(s => s.Amp.Value).ToArray();
    }
}
