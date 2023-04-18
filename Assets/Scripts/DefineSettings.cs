using System.Collections;
using System.Collections.Generic;
using Serializable = System.SerializableAttribute;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 디스플레이 모드를 지정합니다.
	/// </summary>
	[Serializable]
	public enum DisplayMode
	{
		Fullscreen,
		BorderLessFullscreen,
		Windowed
	}

	/// <summary>
	/// 해상도의 높이를 지정합니다.
	/// Reference: https://ko.wikipedia.org/wiki/16:9
	/// </summary>
	[Serializable]
	public enum ResolutionHeight
	{
		_720, // HD
		_900, // HD+
		_1080, // FHD
		_1152, // QWXGA
		_1440, // QHD
		_2160, // 4K UHD
	}

	/// <summary>
	/// 사용 가능한 설정값을 정의합니다.
	/// </summary>
	[Serializable]
	public class DefineSettings
	{
		public ResolutionHeight resolutionHeight;
		public DisplayMode displayMode;

		[SerializeField]
		private float master;

		public float Master
		{
			get => master;
			set
			{
				if (value <= 0f) master = 0f;
				else if (value >= 1f) master = 1f;
				else master = value;
			}
		}

		[SerializeField]
		private float background;

		public float Background
		{
			get => background;
			set
			{
				if (value <= 0f) background = 0f;
				else if (value >= 1f) background = 1f;
				else background = value;
			}
		}

		[SerializeField]
		private float effect;

		public float Effect
		{
			get => effect;
			set
			{
				if (value <= 0f) effect = 0f;
				else if (value >= 1f) effect = 1f;
				else effect = value;
			}
		}

		public bool isFirstRun;

		public DefineSettings()
		{
			resolutionHeight = ResolutionHeight._720;
			displayMode = DisplayMode.Windowed;

			Master = 1f;
			Background = 1f;
			Effect = 1f;

			isFirstRun = true;
		}

		public DefineSettings(
			ResolutionHeight resolutionHeight,
			DisplayMode displayMode,
			float master,
			float background,
			float effect,
			bool isFirstRun
		)
		{
			this.resolutionHeight = resolutionHeight;
			this.displayMode = displayMode;

			this.Master = master;
			this.Background = background;
			this.Effect = effect;

			this.isFirstRun = isFirstRun;
		}
	}
}
