/*
 * File: SettingManager.cs
 * Project: Models
 * Created Date: 07/04/2021
 * Author: Shun Suzuki
 * -----
 * Last Modified: 19/11/2021
 * Modified By: Shun Suzuki (suzuki@hapis.k.u-tokyo.ac.jp)
 * -----
 * Copyright (c) 2021 Hapis Lab. All rights reserved.
 * 
 */

using System.IO;
using System.Linq;
using System.Text.Json;
using AUTD3Controller.Helpers;
using AUTD3Controller.Models.Gain;

namespace AUTD3Controller.Models;

internal static class SettingManager
{
    public class TotalSetting
    {
        public General General { get; set; }
        public AUTDSettings AUTDSettings { get; set; }

        public TotalSetting()
        {
            General = General.Instance;
            AUTDSettings = AUTDSettings.Instance;
        }
    }

    internal static void CopyGeometry() => AUTDSettings.Instance.Geometries = AUTDSettings.Instance.GeometriesReactive.Select(g => new GeometrySetting(g)).ToArray();
    internal static void CopyHoloSetting() => AUTDSettings.Instance.Holo.HoloSettings = AUTDSettings.Instance.Holo.HoloSettingsReactive.Select(s => new HoloSetting(s)).ToArray();

    internal static void CopySeq()
    {
        AUTDSettings.Instance.Seq.Points =
            AUTDSettings.Instance.Seq.PointsReactive.Select(s => s.ToVector3()).ToArray();
        AUTDSettings.Instance.Seq.Duties =
            AUTDSettings.Instance.Seq.PointsReactive.Select(s => s.Duty.Value).ToArray();
    }

    internal static void LoadGeometry()
    {
        AUTDSettings.Instance.GeometriesReactive = new ObservableCollectionWithItemNotify<GeometrySettingReactive>();
        if (AUTDSettings.Instance.Geometries == null) return;
        foreach (var geometry in AUTDSettings.Instance.Geometries) AUTDSettings.Instance.GeometriesReactive.Add(new GeometrySettingReactive(geometry));
    }
    internal static void LoadHoloSetting()
    {
        AUTDSettings.Instance.Holo.HoloSettingsReactive = new ObservableCollectionWithItemNotify<HoloSettingReactive>();
        if (AUTDSettings.Instance.Holo.HoloSettings == null) return;
        foreach (var s in AUTDSettings.Instance.Holo.HoloSettings) AUTDSettings.Instance.Holo.HoloSettingsReactive.Add(new HoloSettingReactive(s));
    }
    internal static void LoadSeq()
    {
        AUTDSettings.Instance.Seq.PointsReactive = new ObservableCollectionWithItemNotify<ControlPointsReactive>();
        if (AUTDSettings.Instance.Seq.Points is null || AUTDSettings.Instance.Seq.Duties is null) return;
        foreach (var ((p, d), i) in AUTDSettings.Instance.Seq.Points.Zip(AUTDSettings.Instance.Seq.Duties).Select((s, i) => (s, i))) AUTDSettings.Instance.Seq.PointsReactive.Add(new ControlPointsReactive(i, p, d));
    }

    internal static void SaveSetting(string path)
    {
        CopyGeometry();
        CopyHoloSetting();
        CopySeq();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(new TotalSetting(), options);
        File.WriteAllText(path, jsonString);
    }

    internal static void LoadSetting(string path)
    {
        var jsonString = File.ReadAllText(path);
        var obj = JsonSerializer.Deserialize<TotalSetting>(jsonString);
        if (obj is null) return;
        AUTDSettings.Instance = obj.AUTDSettings;
        General.Instance = obj.General;
        LoadGeometry();
        LoadHoloSetting();
        LoadSeq();
    }
}
