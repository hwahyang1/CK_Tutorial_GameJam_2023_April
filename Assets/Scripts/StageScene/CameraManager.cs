using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CK_Tutorial_GameJam_April
{
	/// <summary>
	/// Description
	/// </summary>
	public class CameraManager : MonoBehaviour
	{
		private CharacterManager characterManager;

		private Vector3 position;
		private void Start()
		{
			characterManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
		}

		private void Update()
		{
			position = characterManager.transform.position;
			transform.position = new Vector3(position.x, position.y, -10f);
		}
	}
}
