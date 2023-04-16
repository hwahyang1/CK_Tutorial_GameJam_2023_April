using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

namespace CK_Tutorial_GameJam_April.SettingsScene.SettingElements
{
	/// <summary>
	/// 선택형 항목을 묶는 부모 Class 입니다.
	/// </summary>
	public abstract class DropdownElement : Elements
	{
		[Header("Values")]
		[SerializeField, ReadOnly]
		protected int currentSelection = 0;

		[Header("GameObjects")]
		[SerializeField]
		protected Dropdown dropdown;
		
		protected virtual void Awake()
		{
			dropdown.onValueChanged.AddListener(OnValueChanged);
		}

		/// <summary>
		/// 해당 Method는 currentSelection 변수를 기준으로 UI를 업데이트 시킵니다.
		/// 모든 작업이 완료된 후 base.Start()를 호출합니다.
		/// </summary>
		protected virtual void Start()
		{
			UpdateUI();
		}

		/// <summary>
		/// UI를 갱신합니다.
		/// </summary>
		protected virtual void UpdateUI()
		{
			dropdown.value = currentSelection;
		}

		/// <summary>
		/// 값이 변경되었을 때의 이벤트를 처리합니다.
		/// </summary>
		protected abstract void OnValueChanged(int index);
	}
}
