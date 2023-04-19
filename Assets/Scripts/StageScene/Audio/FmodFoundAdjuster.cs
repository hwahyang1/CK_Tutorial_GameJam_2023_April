using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using FMODUnity;

namespace CK_Tutorial_GameJam_April.StageScene.Audio
{
	/// <summary>
	/// Fmod Parameter Trigger의 Found를 조정합니다.
	/// </summary>
	public class FmodFoundAdjuster : MonoBehaviour
	{
		[SerializeField]
		private int parameterIndex;

		private StudioParameterTrigger parameterTrigger;

		private void Start()
		{
			parameterTrigger = GetComponent<StudioParameterTrigger>();
		}

		public void ChangeValue(float value)
		{
			parameterTrigger.Emitters[0].Params[parameterIndex].Value = value;
			parameterTrigger.TriggerParameters();
		}
	}
}
