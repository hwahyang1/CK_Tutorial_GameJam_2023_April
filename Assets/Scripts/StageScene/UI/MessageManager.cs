using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Cysharp.Threading.Tasks;

namespace CK_Tutorial_GameJam_April.StageScene.UI
{
	/// <summary>
	/// 대사 출력을 관리합니다.
	/// </summary>
	public class MessageManager : MonoBehaviour
	{
		[Header("GameObjects")]
		[SerializeField]
		private GameObject parent;

		[SerializeField]
		private Text name;

		[SerializeField]
		private Text messageArea;

		[SerializeField]
		private Image image;

		[Header("Settings")]
		[SerializeField]
		private float messageDelay;

		private List<string> messages;
		private int currentIndex;
		private bool clicked = false;

		private void Start()
		{
			parent.SetActive(false);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0) && GameManager.Instance.status == GameStatus.MessageViewing)
			{
				clicked = true;
			}
		}

		public void Show(string name, List<string> messages, Sprite sprite = null, Action callback = null)
		{
			currentIndex = 0;
			this.name.text = name;
			this.messages = messages;
			messageArea.text = "";
			parent.SetActive(true);
			GameManager.Instance.status = GameStatus.MessageViewing;

			ShowMessage(sprite, callback).Forget();
		}

		private async UniTaskVoid ShowMessage(Sprite sprite, Action callback)
		{
			image.sprite = sprite;
			image.color = new Color(1f, 1f, 1f, sprite == null ? 0f : 1f);
			
			for (; currentIndex < messages.Count; currentIndex++)
			{
				messageArea.text = "";
				string currentMessage = messages[currentIndex].Replace("\\n", "\n");
				for (int i = 0; i < currentMessage.Length; i++)
				{
					if (clicked)
					{
						clicked = false;
						break;
					}

					if (currentMessage[i] == ' ') // 공백은 한번에 한해 제함
					{
						messageArea.text += currentMessage[i];
						continue;
					}

					messageArea.text += currentMessage[i];

					await UniTask.Delay(TimeSpan.FromSeconds(messageDelay));
				}

				messageArea.text = currentMessage;

				while (true)
				{
					if (clicked)
					{
						clicked = false;
						break;
					}

					await UniTask.DelayFrame(1);
				}
			}
			
			parent.SetActive(false);
			GameManager.Instance.status = GameStatus.Playing;
			callback?.Invoke();
		}
	}
}
