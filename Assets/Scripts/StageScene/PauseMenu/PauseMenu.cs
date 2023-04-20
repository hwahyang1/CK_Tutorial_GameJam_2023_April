using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

using CK_Tutorial_GameJam_April.StageScene.Save;
using CK_Tutorial_GameJam_April.PreloadScene.Scene;
using CK_Tutorial_GameJam_April.PreloadScene.Alert;

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
			if (GameManager.Instance.status == GameStatus.ExitSaving) return;
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
			if (GameManager.Instance.status == GameStatus.ExitSaving) return;
			ToggleStatus();
		}

		public void OnSettingsButtonClicked()
		{
			if (GameManager.Instance.status == GameStatus.ExitSaving) return;
			if (SceneManager.sceneCount != 1) return;
			SceneChange.Instance.Add("SettingsScene");
		}

		public void OnMenuButtonClicked()
		{
			if (GameManager.Instance.status == GameStatus.ExitSaving) return;
			AlertManager.Instance.Show(AlertType.Double, "확인", "지금까지의 모든 진행상황이 저장됩니다.\n계속하시겠습니까?",
			                           new Dictionary<string, Action>()
			                           {
				                           {
					                           "예", () =>
					                                {
						                                GameManager.Instance.status =
							                                GameStatus.ExitSaving;

						                                //AlertManager.Instance.Show(AlertType.None, "알림", "잠시만 기다려 주세요. 저장이 끝난 후 메인 화면으로 돌아갑니다.", new Dictionary<string, Action>());
						                                
						                                GameSaveData.Instance
						                                            .ExportData(() =>
						                                                        {
							                                                        //AlertManager.Instance.Pop();
							                                                        SceneChange.Instance.ChangeScene("MenuScene",
								                                                         true,
								                                                         true,
								                                                         () =>
								                                                         {
									                                                         GameSaveData
										                                                         .Instance
										                                                         .Exit();
								                                                         });
						                                                        });
					                                }
				                           },
				                           { "아니요", null }
			                           });
		}

		public void OnDesktopButtonClicked()
		{
			if (GameManager.Instance.status == GameStatus.ExitSaving) return;
			AlertManager.Instance.Show(AlertType.Double, "확인", "지금까지의 모든 진행상황이 저장됩니다.\n계속하시겠습니까?",
			                           new Dictionary<string, Action>()
			                           {
				                           {
					                           "예", () =>
					                                {
						                                GameManager.Instance.status =
							                                GameStatus.ExitSaving;

						                                //AlertManager.Instance.Show(AlertType.None, "알림", "잠시만 기다려 주세요. 저장이 끝난 후 게임이 종료됩니다.", new Dictionary<string, Action>());
						                                
						                                GameSaveData.Instance
						                                            .ExportData(() =>
						                                                        {
							                                                        #if UNITY_EDITOR
							                                                        EditorApplication
								                                                        .ExecuteMenuItem("Edit/Play");
							                                                        #else
																						Application.Quit();
							                                                        #endif
						                                                        });
					                                }
				                           },
				                           { "아니요", null }
			                           });
		}
	}
}
