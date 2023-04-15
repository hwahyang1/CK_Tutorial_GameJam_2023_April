using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.PreloadScene.Settings;

namespace CK_Tutorial_GameJam_April.SettingsScene.SettingElements.SubElements
{
	/// <summary>
	/// 전체화면 선택을 관리합니다.
	/// </summary>
	public class Fullscreen : SelectCategoryElement
	{
		protected override void Start()
		{
			currentSelection = (int)SettingsManager.Instance.GetSettings().displayMode;
			base.Start();
		}

		protected override void OnValueChanged()
		{
			DefineSettings oldData = SettingsManager.Instance.GetSettings();
			oldData.displayMode = (DisplayMode)currentSelection;
			SettingsManager.Instance.SetSettings(oldData);
			SettingsManager.Instance.ApplySettings();
			SettingsManager.Instance.SaveSettings();
			
			base.OnValueChanged();
		}
	}
}
