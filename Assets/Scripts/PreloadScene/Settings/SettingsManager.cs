using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

using Newtonsoft.Json;

namespace CK_Tutorial_GameJam_April.PreloadScene.Settings
{
	/// <summary>
	/// 설정을 관리합니다.
	/// </summary>
	public class SettingsManager : SingleTon<SettingsManager>
	{
		private DefineSettings settings;
		private string configPath;

		private FullScreenMode fullScreenMode;

		private float backgroundVolume;
		public float BackgroundVolume => backgroundVolume;
		private float effectVolume;
		public float EffectVolume => effectVolume;

		protected override void Awake()
		{
			base.Awake();

			configPath = Application.persistentDataPath + @"\Data.cfg";

			LoadSettings();
			ApplySettings();
		}

		/// <summary>
		/// 파일에서 설정을 가져옵니다.
		/// 파일이 존재하지 않을 경우, 기본값을 불러오고 파일로 저장합니다.
		/// </summary>
		public void LoadSettings()
		{
			if (!File.Exists(configPath))
			{
				settings = new DefineSettings();
				SaveSettings();
			}
			else
			{
				string data = File.ReadAllText(configPath);
				settings = JsonConvert.DeserializeObject<DefineSettings>(data);
			}
		}

		/// <summary>
		/// 현재 설정값을 반환합니다.
		/// </summary>
		/// <returns>현재 저장된 설정값입니다.</returns>
		public DefineSettings GetSettings() => settings;

		/// <summary>
		/// 현재 설정을 변경합니다.
		/// </summary>
		/// <param name="changeTo">변경할 설정값을 지정합니다.</param>
		public void SetSettings(DefineSettings changeTo) => settings = changeTo;

		/// <summary>
		/// 현재 저장된 설정값을 적용합니다.
		/// </summary>
		public void ApplySettings()
		{
			switch (settings.displayMode)
			{
				case DisplayMode.Fullscreen:
					fullScreenMode = FullScreenMode.ExclusiveFullScreen;
					break;
				case DisplayMode.BorderLessFullscreen:
					fullScreenMode = FullScreenMode.FullScreenWindow;
					break;
				case DisplayMode.Windowed:
					fullScreenMode = FullScreenMode.Windowed;
					break;
			}
			
			int resolutionHeight = 0;
			switch (settings.resolutionHeight)
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
			Screen.SetResolution(resolutionHeight / 9 * 16, resolutionHeight, fullScreenMode);

			QualitySettings.vSyncCount = 1;
			Application.targetFrameRate = 60; // 사실 의미 없음
			
			backgroundVolume = settings.Master * settings.Background;
			effectVolume = settings.Master * settings.Effect;
		}

		/// <summary>
		/// 현재 저장된 설정값을 파일로 저장합니다.
		/// </summary>
		public void SaveSettings()
		{
			string data = JsonConvert.SerializeObject(settings);
			File.WriteAllText(configPath, data);
		}
	}
}
