/*
 * File: ErrorDialog.xaml.cs
 * Project: Domain
 * Created Date: 30/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using AUTD3Controller.Helpers;
using Reactive.Bindings;

namespace AUTD3Controller.Domain;

public sealed partial class ErrorDialog
{
    public ErrorDialog()
    {
        InitializeComponent();
    }
}

public class ErrorDialogViewModel : ReactivePropertyBase
{
    public ReactivePropertySlim<string> Message { get; set; }

    public ErrorDialogViewModel()
    {
        Message = new ReactivePropertySlim<string>();
    }
}
