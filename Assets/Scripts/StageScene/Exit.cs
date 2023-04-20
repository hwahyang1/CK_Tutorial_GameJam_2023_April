using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using CK_Tutorial_GameJam_April.CreditScene;
using CK_Tutorial_GameJam_April.PreloadScene.Alert;
using CK_Tutorial_GameJam_April.PreloadScene.Scene;

namespace CK_Tutorial_GameJam_April.StageScene
{
	/// <summary>
	/// 탈출구를 관리합니다.
	/// </summary>
	public class Exit : MonoBehaviour
	{
		[SerializeField]
		private Sprite[] doors = new Sprite[2];

		private bool active = false;

		private SpriteRenderer spriteRenderer;

		private void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void SetDoorActive(bool active)
		{
			this.active = active;
			spriteRenderer.sprite = doors[active ? 1 : 0];
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (active)
			{
				GameManager.Instance.status = GameStatus.Dead;
				CreditParams.Instance.isControllable = false;
				SceneChange.Instance.ChangeScene("CreditScene");
			}
			else
			{
				AlertManager.Instance.Show(AlertType.Single, "알림", "열쇠를 3개 이상 가지고 있어야 탈출 할 수 있습니다.",
				                           new Dictionary<string, Action>() { { "확인", null } });
			}
		}
	}
}
