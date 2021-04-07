/*
 * File: SettingManager.cs
 * Project: Models
 * Created Date: 07/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 07/04/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.IO;
using System.Text.Json;
using AUTD3Controller.Helpers;

namespace AUTD3Controller.Models
{
    internal static class SettingManager
    {
        public class TotalSetting
        {
            public General General { get; set; }
            public AUTDSettings AUTDSettings { get; set; }

            public TotalSetting()
            {
                General = General.Instance;
                AUTDSettings = AUTDSettings.Instance; ;
            }
        }

        internal static void CopyGeometry()
        {
            AUTDSettings.Instance.Geometries = new GeometrySetting[AUTDSettings.Instance.GeometriesReactive.Count];
            for (var i = 0; i < AUTDSettings.Instance.GeometriesReactive.Count; i++) {
                AUTDSettings.Instance.Geometries[i] = new GeometrySetting(AUTDSettings.Instance.GeometriesReactive[i]);
            }
        }

        internal static void LoadGeometry()
        {
            AUTDSettings.Instance.GeometriesReactive =
                new ObservableCollectionWithItemNotify<GeometrySettingReactive>();

            if (AUTDSettings.Instance.Geometries == null) return;

            foreach (var geometry in AUTDSettings.Instance.Geometries)
                AUTDSettings.Instance.GeometriesReactive.Add(new GeometrySettingReactive(geometry));
        }

        internal static void SaveSetting(string path)
        {
            CopyGeometry();
            var options = new JsonSerializerOptions {
                WriteIndented = true,
            };
            var jsonString = JsonSerializer.Serialize(new TotalSetting(), options);
            File.WriteAllText(path, jsonString);
        }

        internal static void LoadSetting(string path)
        {
            var jsonString = File.ReadAllText(path);
            var obj = JsonSerializer.Deserialize<TotalSetting>(jsonString);
            AUTDSettings.Instance = obj.AUTDSettings;
            General.Instance = obj.General;
            LoadGeometry();
        }
    }
}
