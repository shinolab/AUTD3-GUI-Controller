/*
 * File: ModulationViewModel.cs
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
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using AUTD3Controller.Models;
using AUTD3Controller.Models.Modulation;
using AUTD3Controller.Views.Modulation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels
{
    internal class ModulationViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveCommand AppendModulationCommand { get; }

        public ReactiveProperty<Page> Page { get; }
        public ReactiveCommand<string> TransitPage { get; }


        public ReactiveProperty<StaticModulation> Static { get; }
        public ReactiveProperty<SineModulation> Sine { get; }

        public ModulationViewModel()
        {
            AppendModulationCommand = AUTDHandler.Instance.IsOpen.Select(b => b).ToReactiveCommand();
            AppendModulationCommand.Subscribe(_ => AUTDHandler.Instance.SendModulation());

            Static = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Static);
            Sine = AUTDSettings.Instance.ToReactivePropertyAsSynchronized(i => i.Sine);

            Dictionary<string, Page> pageCache = new Dictionary<string, Page>();
            Page = new ReactiveProperty<Page>(new SineView());

            TransitPage = new ReactiveCommand<string>();
            TransitPage.Subscribe(page =>
            {
                if (!pageCache.ContainsKey(page)) pageCache.Add(page, (Page)Activator.CreateInstance(null!, page)?.Unwrap()!);
                Page.Value = pageCache[page]!;
                AUTDSettings.Instance.ModulationSelect = (ModulationSelect)Enum.Parse(typeof(ModulationSelect), page.Split('.').LastOrDefault()?[..^4] ?? string.Empty);
            });
        }
    }
}
