using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FMODUnity;

using CK_Tutorial_GameJam_April.PreloadScene.Settings;

namespace CK_Tutorial_GameJam_April.StageScene.Audio
{
	/// <summary>
	/// Fmod Parameter Trigger의 Master Volume을 조정합니다.
	/// </summary>
	public class FmodMasterAudioAdjuster : MonoBehaviour
	{
		[SerializeField]
		private int parameterIndex;

		private StudioParameterTrigger parameterTrigger;

		private void Start()
		{
			parameterTrigger = GetComponent<StudioParameterTrigger>();
		}

		private void Update()
		{
			parameterTrigger.Emitters[0].Params[parameterIndex].Value = SettingsManager.Instance.BackgroundVolume;
			parameterTrigger.TriggerParameters();
		}
	}
}
