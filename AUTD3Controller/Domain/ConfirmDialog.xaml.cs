﻿/*
 * File: ConfirmDialog.xaml.cs
 * Project: Domain
 * Created Date: 07/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 03/06/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using AUTD3Controller.Helpers;
using Reactive.Bindings;

namespace AUTD3Controller.Domain
{
    public sealed partial class ConfirmDialog
    {
        public ConfirmDialog()
        {
            InitializeComponent();
        }
    }

    public class ConfirmDialogViewModel : ReactivePropertyBase
    {


        public ReactiveProperty<string> Message { get; set; }

        public ConfirmDialogViewModel()
        {
            Message = new ReactiveProperty<string>();
        }
    }
}
