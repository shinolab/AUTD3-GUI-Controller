/*
 * File: MainWindow.xaml.cs
 * Project: AUTD3Controller
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AUTD3Controller.Domain;
using AUTD3Controller.Helpers;
using AUTD3Controller.Models;
using AUTD3Controller.Views;
using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace AUTD3Controller;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void Window_Initialized(object sender, EventArgs e)
    {
        try
        {
            SettingManager.LoadSetting("settings.json");
        }
        catch (Exception)
        {
            // ignore
        }
    }
}

public class MainWindowModel : ReactivePropertyBase
{


    public Page Page { get; set; } = new Home();
}

public class MainWindowViewModel : ReactivePropertyBase
{
    public ReactivePropertySlim<Page> Page { get; }

    public ReactiveCommand<string> TransitPage { get; }
    public AsyncReactiveCommand ButtonPower { get; }
    public ReactiveCommand<string> OpenUrl { get; }

    public AsyncReactiveCommand Save { get; }
    public AsyncReactiveCommand Load { get; }

    public AsyncReactiveCommand Start { get; }
    public ReactiveCommand Stop { get; }

    private MainWindowModel Model { get; }

    public MainWindowViewModel()
    {
        var pageCache = new Dictionary<string, Page>();

        Model = new MainWindowModel();
        Page = Model.ToReactivePropertySlimAsSynchronized(m => m.Page);

        TransitPage = new ReactiveCommand<string>();
        TransitPage.Subscribe(page =>
        {
            if (!pageCache.ContainsKey(page)) pageCache.Add(page, (Page)Activator.CreateInstance(null!, page)?.Unwrap()!);
            Page.Value = pageCache[page];
        });

        ButtonPower = new AsyncReactiveCommand();
        ButtonPower.Subscribe(async _ =>
        {
            var vm = new ConfirmDialogViewModel { Message = { Value = "Are you sure to quit the application?" } };
            var dialog = new ConfirmDialog
            {
                DataContext = vm
            };
            var res = await DialogHost.Show(dialog, "MessageDialogHost");
            if (res is true)
            {
                AUTDHandler.Instance.Dispose();
                SettingManager.SaveSetting("settings.json");
                Application.Current.Shutdown();
            }
        });

        OpenUrl = new ReactiveCommand<string>();
        OpenUrl.Subscribe(url =>
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        });

        Save = new AsyncReactiveCommand();
        Save.Subscribe(async _ =>
        {
            var dialogArgs = new SaveFileDialogArguments
            {
                Width = 600,
                Height = 800,
                Filters = "json files|*.json",
                ForceFileExtensionOfFileFilter = true,
                CreateNewDirectoryEnabled = true
            };
            var result = await SaveFileDialog.ShowDialogAsync("MessageDialogHost", dialogArgs);
            if (result.Canceled) return;
            try
            {
                SettingManager.SaveSetting(result.File);
            }
            catch
            {
                var vm = new ErrorDialogViewModel { Message = { Value = "Failed to save settings." } };
                var dialog = new ErrorDialog
                {
                    DataContext = vm
                };
                await DialogHost.Show(dialog, "MessageDialogHost");
            }
        });

        Load = new AsyncReactiveCommand();
        Load.Subscribe(async _ =>
        {
            var dialogArgs = new OpenFileDialogArguments
            {
                Width = 600,
                Height = 800,
                Filters = "json files|*.json"
            };
            var result = await OpenFileDialog.ShowDialogAsync("MessageDialogHost", dialogArgs);
            if (result.Canceled) return;
            try
            {
                SettingManager.LoadSetting(result.File);
            }
            catch
            {
                var vm = new ErrorDialogViewModel { Message = { Value = "Failed to load settings." } };
                var dialog = new ErrorDialog
                {
                    DataContext = vm
                };
                await DialogHost.Show(dialog, "MessageDialogHost");
            }
        });

        Start = AUTDHandler.Instance.IsRunning.Select(x => !x).ToAsyncReactiveCommand();
        Start.Subscribe(async _ =>
        {
            if (!AUTDHandler.Instance.IsOpen.Value)
            {
                var res = await Task.Run(() => AUTDHandler.Instance.Open());
                if (res != null)
                {
                    var vm = new ErrorDialogViewModel { Message = { Value = $"Failed to open AUTD: {res}.\nSee Link options." } };
                    var dialog = new ErrorDialog
                    {
                        DataContext = vm
                    };
                    await DialogHost.Show(dialog, "MessageDialogHost");
                    return;
                }
            }

            AUTDHandler.Instance.SendGain();
            AUTDHandler.Instance.SendModulation();

        });
        Stop = AUTDHandler.Instance.IsRunning.Select(x => x).ToReactiveCommand();
        Stop.Subscribe(_ =>
        {
            AUTDHandler.Instance.Stop();
        });
    }
}
