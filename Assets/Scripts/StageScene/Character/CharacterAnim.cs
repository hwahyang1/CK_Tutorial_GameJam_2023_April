using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// 캐릭터의 애니메이션을 결정
	/// </summary>
	public class CharacterAnim : MonoBehaviour
	{
		private Rigidbody2D rb;
		private Animator animator;
		private bool isMove = false;
		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (rb.velocity.x > 0)
			{
				isMove = true;
			}
			else
			{
				isMove = false;
			}
		}
		
	}
}

