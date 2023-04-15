using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Settings;

namespace CK_Tutorial_GameJam_April.SettingsScene.SettingElements.SubElements
{
	/// <summary>
	/// 배경음 볼륨 항목을 정의합니다.
	/// </summary>
	public class BGMVolume : ScrollElement
	{
		protected override void Start()
		{
			currentValue = SettingsManager.Instance.GetSettings().Background * 100f;
			base.Start();
		}

		protected override void OnValueChanged(float currentValue)
		{
			base.OnValueChanged(currentValue);
			DefineSettings oldData = SettingsManager.Instance.GetSettings();
			float adjustedValue = Mathf.Floor(currentValue) / 100f;
			oldData.Background = adjustedValue;
			SettingsManager.Instance.SetSettings(oldData);
			SettingsManager.Instance.ApplySettings();
			SettingsManager.Instance.SaveSettings();
		}
	}
}
