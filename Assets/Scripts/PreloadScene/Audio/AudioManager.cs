using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NaughtyAttributes;

using CK_Tutorial_GameJam_April.PreloadScene.Settings;

namespace CK_Tutorial_GameJam_April.PreloadScene.Audio
{
	/// <summary>
	/// 소리의 재생을 관리합니다.
	/// </summary>
	public class AudioManager : SingleTon<AudioManager>
	{
		[SerializeField]
		private AudioSource backgroundAudio;

		[SerializeField, ReadOnly]
		private float backgroundAudioMax;

		[SerializeField]
		private AudioSource effectAudio;

		[SerializeField, ReadOnly]
		private float effectAudioMax;

		private void FixedUpdate()
		{
			backgroundAudioMax = SettingsManager.Instance.BackgroundVolume;
			effectAudioMax = SettingsManager.Instance.EffectVolume;
		}

		/// <summary>
		/// 효과음을 재생합니다.
		/// </summary>
		public void PlayEffectAudio(AudioClip clip)
		{
			effectAudio.volume = effectAudioMax;
			effectAudio.PlayOneShot(clip, effectAudioMax);
		}

		/// <summary>
		/// 배경음을 재생합니다.
		/// </summary>
		public void PlayBackgroundAudio(AudioClip clip)
		{
			backgroundAudio.volume = effectAudioMax;
			backgroundAudio.clip = clip;
			backgroundAudio.Play();
		}

		/// <summary>
		/// 배경음의 재생을 중지합니다.
		/// </summary>
		public void StopBackgroundAudio()
		{
			backgroundAudio.Stop();
			//StartCoroutine(nameof(StopBackgroundAudioCoroutine));
		}

		private IEnumerator StopBackgroundAudioCoroutine()
		{
			float time = 0f;
			while (time <= 0.75f)
			{
				time += 0.01f;
				backgroundAudio.volume -= 0.03f;
				yield return new WaitForSeconds(0.01f);
			}

			backgroundAudio.Stop();
		}
	}
}
