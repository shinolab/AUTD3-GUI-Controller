/*
 * File: ErrorDialog.xaml.cs
 * Project: Domain
 * Created Date: 30/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 30/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.ComponentModel;
using Reactive.Bindings;

namespace AUTD3Controller.Domain
{
    public sealed partial class ErrorDialog
    {
        public ErrorDialog()
        {
            InitializeComponent();
        }
    }

    public class ErrorDialogViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<string> Message { get; set; }

        public ErrorDialogViewModel()
        {
            Message = new ReactiveProperty<string>();
        }
    }
}
