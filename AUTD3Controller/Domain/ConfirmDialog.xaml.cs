/*
 * File: ConfirmDialog.xaml.cs
 * Project: Domain
 * Created Date: 07/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 07/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.ComponentModel;
using System.Windows.Controls;
using Reactive.Bindings;

namespace AUTD3Controller.Domain
{
    public sealed partial class ConfirmDialog : UserControl
    {
        public ConfirmDialog()
        {
            this.InitializeComponent();
        }
    }

    public class ConfirmDialogViewModel : INotifyPropertyChanged
    {
#pragma warning disable 414
        public event PropertyChangedEventHandler PropertyChanged = null!;
#pragma warning restore 414

        public ReactiveProperty<string> Message { get; set; }

        public ConfirmDialogViewModel()
        {
            Message = new ReactiveProperty<string>();
        }
    }
}
