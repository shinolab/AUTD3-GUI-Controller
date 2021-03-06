/*
 * File: EnumToVisibilityConverter.cs
 * Project: Converter
 * Created Date: 30/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AUTD3Controller.Converter;

public class EnumToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is not string parameterString) return DependencyProperty.UnsetValue;

        if (Enum.IsDefined(value.GetType(), value) == false) return DependencyProperty.UnsetValue;

        return (int)Enum.Parse(value.GetType(), parameterString) == (int)value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
