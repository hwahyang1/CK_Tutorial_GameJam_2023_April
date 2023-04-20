using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April.StageScene.Character
{
	/// <summary>
	/// 캐릭터 애니매이션 관리
	/// </summary>
	public class CharacterAnim : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer spriteRenderer;

		[SerializeField]
		private Sprite[] Walk = new Sprite[3];

		[SerializeField]
		private Sprite[] Eat = new Sprite[3];

		[SerializeField]
		private Sprite pickUp;

		[SerializeField]
		private Sprite suprized;

		private float time = 0f;

		public bool onWalk = false;
		public bool onRun = false;
		public bool onEat = false;
		public bool onPickUp = false;
		public bool onSuprized = false;

		private void Update()
		{
			if (onEat)
			{
				time += Time.deltaTime;
				if (time > 0f && time <= 0.25f)
				{
					spriteRenderer.sprite = Eat[0];
				}
				else if (time > 0.25f && time <= 0.5f)
				{
					spriteRenderer.sprite = Eat[1];
				}
				else if (time > 0.5f && time <= 0.75f)
				{
					spriteRenderer.sprite = Eat[2];
				}
				else
				{
					time = 0f; // 루프되야하니까
					//onEat = false;
				}
			}
			else if (onPickUp)
			{
				time += Time.deltaTime;
				spriteRenderer.sprite = pickUp;
				if (time > 0.5f)
				{
					time = 0f; // 루프되야하니까
					onPickUp = false;
				}
			}
			else if (onSuprized)
			{
				time += Time.deltaTime;
				spriteRenderer.sprite = suprized;
				if (time > 0.5f)
				{
					time = 0f; // 루프되야하니까
					onSuprized = false;
				}
			}
			else if (onWalk)
			{
				if (onRun) time += Time.deltaTime * 2f;
				time += Time.deltaTime;
				if (time > 0f && time <= 0.5f)
				{
					spriteRenderer.sprite = Walk[0];
				}
				else if (time > 0.5f && time <= 1f)
				{
					spriteRenderer.sprite = Walk[1];
				}
				else if (time > 1f && time <= 1.5f)
				{
					spriteRenderer.sprite = Walk[2];
				}
				else
				{
					time = 0f; // 루프되야하니까
				}
			}
			else
			{
				spriteRenderer.sprite = Walk[0];
				time = 0f;
			}
		}
	}
}
