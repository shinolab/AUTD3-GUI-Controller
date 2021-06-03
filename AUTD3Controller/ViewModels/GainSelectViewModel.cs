/*
 * File: GainSelectViewModel.cs
 * Project: ViewModels
 * Created Date: 31/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 03/06/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using AUTD3Controller.Helpers;
using AUTD3Controller.Models;
using AUTD3Controller.Models.Gain;
using AUTD3Controller.Views.Gain;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels
{
    internal class GainSelectViewModel : ReactivePropertyBase
    {
        public ReactiveCommand SendGainCommand { get; }

        public ReactiveProperty<Page> Page { get; }
        public ReactiveCommand<string> TransitPage { get; }

        public ReactiveProperty<BesselBeam> Bessel { get; }
        public ReactiveProperty<Holo> Holo { get; }
        public ReactiveProperty<PlaneWave> PlaneWave { get; }
        public ReactiveProperty<TransducerTest> TransducerTest { get; }

        public GainSelectViewModel()
        {
            SendGainCommand = AUTDHandler.Instance.IsOpen.Select(b => b).ToReactiveCommand();
            SendGainCommand.Subscribe(_ =>
            {
                AUTDHandler.Instance.SendGain();
            });

            Bessel = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Bessel);
            Holo = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Holo);
            PlaneWave = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.PlaneWave);
            TransducerTest = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.TransducerTest);

            Dictionary<string, Page> pageCache = new Dictionary<string, Page>();
            Page = new ReactiveProperty<Page>(new FocalPointView());

            TransitPage = new ReactiveCommand<string>();
            TransitPage.Subscribe(page =>
            {
                if (!pageCache.ContainsKey(page)) pageCache.Add(page, (Page)Activator.CreateInstance(null!, page)?.Unwrap()!);
                Page.Value = pageCache[page]!;
                AUTDSettings.Instance.GainSelect = (GainSelect)Enum.Parse(typeof(GainSelect), page.Split('.').LastOrDefault()?[..^4] ?? string.Empty);
            });
        }
    }
}
