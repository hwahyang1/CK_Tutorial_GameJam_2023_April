using System.Collections;
using System.Collections.Generic;
using Enum = System.Enum;

using UnityEngine;
using UnityEngine.UI;

using CK_Tutorial_GameJam_April.PreloadScene.Settings;

namespace CK_Tutorial_GameJam_April.SettingsScene.SettingElements.SubElements
{
	/// <summary>
	/// 해상도 선택을 관리합니다.
	/// </summary>
	public class Resolution : DropdownElement
	{
		protected override void Awake()
		{
			dropdown.options.Clear();

			foreach (ResolutionHeight current in Enum.GetValues(typeof(ResolutionHeight)))
			{
				int resolutionHeight = 0;
				switch (current)
				{
					case ResolutionHeight._720:
						resolutionHeight = 720;
						break;
					case ResolutionHeight._900:
						resolutionHeight = 900;
						break;
					case ResolutionHeight._1080:
						resolutionHeight = 1080;
						break;
					case ResolutionHeight._1152:
						resolutionHeight = 1152;
						break;
					case ResolutionHeight._1440:
						resolutionHeight = 1440;
						break;
					case ResolutionHeight._2160:
						resolutionHeight = 2160;
						break;
				}

				dropdown.options.Add(new Dropdown.OptionData($"{resolutionHeight / 9 * 16} x {resolutionHeight}px"));
			}

			base.Awake();
		}

		protected override void Start()
		{
			currentSelection = (int)SettingsManager.Instance.GetSettings().resolutionHeight;
			base.Start();
		}

		protected override void OnValueChanged(int index)
		{
			currentSelection = index;

			DefineSettings oldData = SettingsManager.Instance.GetSettings();
			oldData.resolutionHeight = (ResolutionHeight)currentSelection;
			SettingsManager.Instance.SetSettings(oldData);
			SettingsManager.Instance.ApplySettings();
			SettingsManager.Instance.SaveSettings();

			UpdateUI();
		}
	}
}
