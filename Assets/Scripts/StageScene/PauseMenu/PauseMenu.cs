using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.StageScene.PauseMenu
{
	/// <summary>
	/// Description
	/// </summary>
	public class PauseMenu : MonoBehaviour
	{
		[SerializeField]
		private GameObject canvas;

		private bool active = false;
		
		private void Start()
		{
			canvas.SetActive(false);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ToggleStatus();
			}
		}

		public void SetStatus(bool active)
		{
			this.active = active;
			canvas.SetActive(active);
		}

		private void ToggleStatus()
		{
			if (active)
			{
				if (SceneManager.sceneCount != 1) return;
				if (GameManager.Instance.status == GameStatus.Paused)
				{
					GameManager.Instance.status = GameStatus.Playing;
					SetStatus(false);
				}
			}
			else
			{
				if (GameManager.Instance.status == GameStatus.Playing)
				{
					GameManager.Instance.status = GameStatus.Paused;
					SetStatus(true);
				}
			}
		}
		
		/* =================== Events =================== */

		public void OnContinueButtonClicked()
		{
			ToggleStatus();
		}

		public void OnSettingsButtonClicked()
		{
			if (SceneManager.sceneCount != 1) return;
			SceneChange.Instance.Add("SettingsScene");
		}

		public void OnMenuButtonClicked()
		{
			
		}

		public void OnDesktopButtonClicked()
		{
			
		}
	}
}
