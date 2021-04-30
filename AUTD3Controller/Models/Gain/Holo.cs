/*
 * File: Holo.cs
 * Project: Gain
 * Created Date: 30/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 30/04/2021
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
        SDP = 0,
        EVD = 1,
        GS = 2,
        GSPAT = 3,
        NAIVE = 4,
        LM = 5
    }

    public class SDPParams
    {
        public float Regularization { get; set; } = -1;
        public int Repeat { get; set; } = -1;
        public float Lambda { get; set; } = -1;
        public bool NormalizeAmp { get; set; } = true;

        internal AUTD3Sharp.Gain.SDPParams ToParam()
        {
            return new AUTD3Sharp.Gain.SDPParams()
            {
                Regularization = Regularization,
                Repeat = Repeat,
                Lambda = Lambda,
                NormalizeAmp = NormalizeAmp
            };
        }
    }

    public class EVDParams
    {
        public float Regularization { get; set; } = -1;
        public bool NormalizeAmp { get; set; } = true;

        internal AUTD3Sharp.Gain.EVDParams ToParam()
        {
            return new AUTD3Sharp.Gain.EVDParams()
            {
                Regularization = Regularization,
                NormalizeAmp = NormalizeAmp
            };
        }
    }

    public class NLSParams
    {
        public float Eps1 { get; set; } = -1;
        public float Eps2 { get; set; } = -1;
        public int KMax { get; set; } = -1;
        public float Tau { get; set; } = -1;

        internal AUTD3Sharp.Gain.NLSParams ToParam()
        {
            return new AUTD3Sharp.Gain.NLSParams()
            {
                Eps1 = Eps1,
                Eps2 = Eps2,
                KMax = KMax,
                Tau = Tau,
                Initial = null
            };
        }
    }

    public class HoloSettingReactive : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<int> No { get; }
        public ReactiveProperty<float> X { get; }
        public ReactiveProperty<float> Y { get; }
        public ReactiveProperty<float> Z { get; }
        public ReactiveProperty<float> Amp { get; }

        public HoloSettingReactive(int no)
        {
            No = new ReactiveProperty<int>(no);
            X = new ReactiveProperty<float>();
            Y = new ReactiveProperty<float>();
            Z = new ReactiveProperty<float>();
            Amp = new ReactiveProperty<float>(1.0f);
        }

        public HoloSettingReactive(HoloSetting s)
        {
            No = new ReactiveProperty<int>(s.No);
            X = new ReactiveProperty<float>(s.X);
            Y = new ReactiveProperty<float>(s.Y);
            Z = new ReactiveProperty<float>(s.Z);
            Amp = new ReactiveProperty<float>(s.Amp);
        }
    }

    public class HoloSetting
    {
        public int No { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Amp { get; set; }

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

        public Holo()
        {
            HoloSettingsReactive = new ObservableCollectionWithItemNotify<HoloSettingReactive>();
            HoloSettings = null;
        }

        public AUTD3Sharp.Gain ToGain() => OptMethod switch
        {
            OptMethod.SDP => AUTD3Sharp.Gain.HoloGainSDP(Foci, Amps, SDPParams.ToParam()),
            OptMethod.EVD => AUTD3Sharp.Gain.HoloGainEVD(Foci, Amps, EVDParams.ToParam()),
            OptMethod.GS => AUTD3Sharp.Gain.HoloGainGS(Foci, Amps, GSRepeat),
            OptMethod.GSPAT => AUTD3Sharp.Gain.HoloGainGSPAT(Foci, Amps, GSPATRepeat),
            OptMethod.NAIVE => AUTD3Sharp.Gain.HoloGainNaive(Foci, Amps),
            OptMethod.LM => AUTD3Sharp.Gain.HoloGainLM(Foci, Amps, NLSParams.ToParam()),
            _ => throw new ArgumentOutOfRangeException()
        };

        private Vector3f[] Foci => HoloSettingsReactive.Select(s => new Vector3f(s.X.Value, s.Y.Value, s.Z.Value)).ToArray();
        private float[] Amps => HoloSettingsReactive.Select(s => s.Amp.Value).ToArray();
    }
}
