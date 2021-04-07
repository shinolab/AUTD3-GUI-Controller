﻿/*
 * File: ObservableCollectionWithItemNotify.cs
 * Project: Helpers
 * Created Date: 29/03/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 07/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AUTD3Controller.Helpers
{
    public sealed class ObservableCollectionWithItemNotify<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {

        public ObservableCollectionWithItemNotify()
        {
            CollectionChanged += ItemsCollectionChanged;
        }

        public ObservableCollectionWithItemNotify(IEnumerable<T> collection) : base(collection)
        {
            CollectionChanged += ItemsCollectionChanged;
            foreach (var item in collection)
                item.PropertyChanged += ItemPropertyChanged;
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged? item in e.OldItems)
                    if (item != null)
                        item.PropertyChanged -= ItemPropertyChanged;

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged? item in e.NewItems)
                    if (item != null)
                        item.PropertyChanged += ItemPropertyChanged;
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var reset = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(reset);
        }
    }
}