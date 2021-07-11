/*
 * File: LinkViewModel.cs
 * Project: ViewModels
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 11/07/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AUTD3Controller.Domain;
using AUTD3Controller.Helpers;
using AUTD3Controller.Models;
using AUTD3Sharp;
using MaterialDesignThemes.Wpf;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller.ViewModels
{
    internal class LinkViewModel : ReactivePropertyBase
    {
        public ObservableCollection<string> Interfaces { get; }
        public ReactivePropertySlim<string> InterfaceName { get; }
        public ReactivePropertySlim<LinkSelect> LinkSelected { get; }

        public ReactivePropertySlim<uint> CycleTicks { get; }
        public ReactivePropertySlim<ushort> EmulatorPort { get; }

        public AsyncReactiveCommand UpdateInterfaces { get; }
        public AsyncReactiveCommand Open { get; }
        public ReactiveCommand Close { get; }

        public LinkViewModel()
        {
            Interfaces = new ObservableCollection<string>();

            LinkSelected = AUTDSettings.Instance.ToReactivePropertySlimAsSynchronized(i => i.LinkSelected);
            CycleTicks = AUTDSettings.Instance.ToReactivePropertySlimAsSynchronized(i => i.CycleTicks);
            EmulatorPort = AUTDSettings.Instance.ToReactivePropertySlimAsSynchronized(i => i.EmulatorPort);

            try
            {
                UpdateInterfacesList();
            }
            catch (DllNotFoundException)
            {
                LinkSelected.Value = LinkSelect.TwinCAT;
            }

            InterfaceName = AUTDSettings.Instance.ToReactivePropertySlimAsSynchronized(i => i.InterfaceName);

            UpdateInterfaces = LinkSelected.Select(s => s == LinkSelect.SOEM).ToAsyncReactiveCommand();
            UpdateInterfaces.Subscribe(async _ =>
            {
                try
                {
                    UpdateInterfacesList();
                }
                catch (DllNotFoundException e)
                {
                    var vm = new ErrorDialogViewModel { Message = { Value = $"{e.Message}.\nDid you install npcap or winpcap?" } };
                    var dialog = new ErrorDialog
                    {
                        DataContext = vm
                    };
                    await DialogHost.Show(dialog, "MessageDialogHost");
                }
            });

            Close = AUTDHandler.Instance.IsOpen.Select(i => i).ToReactiveCommand();
            Close.Subscribe(_ =>
            {
                AUTDHandler.Instance.Close();
            });
            Open = AUTDHandler.Instance.IsOpen.Select(i => !i).ToAsyncReactiveCommand();
            Open.Subscribe(async _ =>
            {
                var res = await Task.Run(() => AUTDHandler.Instance.Open());

                if (res == null) return;

                var vm = new ErrorDialogViewModel { Message = { Value = $"Failed to Open AUTD. {res}" } };
                var dialog = new ErrorDialog
                {
                    DataContext = vm
                };
                await DialogHost.Show(dialog, "MessageDialogHost");
            });
        }

        private void UpdateInterfacesList()
        {
            Interfaces.Clear();
            var adapters = AUTD.EnumerateAdapters();
            foreach (var adapter in adapters) Interfaces.Add($"{adapter}");
        }
    }
}
