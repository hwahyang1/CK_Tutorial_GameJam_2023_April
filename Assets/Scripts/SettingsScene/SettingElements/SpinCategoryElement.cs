using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CK_Tutorial_GameJam_April.SettingsScene.SettingElements
{
	/// <summary>
	/// Spin 항목을 묶는 부모 Class 입니다.
	/// </summary>
	public abstract class SelectCategoryElement : Elements
	{
		[SerializeField]
		protected List<string> selections = new List<string>(); // 사용 가능한 선택지들
		protected int currentSelection = 0;

		[Header("GameObjects")]
		[SerializeField]
		protected GameObject elementLeftArrow;
		[SerializeField]
		protected GameObject elementSelected;
		[SerializeField]
		protected GameObject elementRightArrow;

		protected virtual void Awake()
		{
			elementLeftArrow.GetComponent<Button>().onClick.AddListener(OnLeftArrowClicked);
			elementRightArrow.GetComponent<Button>().onClick.AddListener(OnRightArrowClicked);
		}

		/// <summary>
		/// 해당 Method는 currentSelection 변수를 기준으로 UI를 업데이트 시킵니다.
		/// 모든 작업이 완료된 후 base.Start()를 호출합니다.
		/// </summary>
		protected virtual void Start()
		{
			OnValueChanged();
		}

		/// <summary>
		/// 선택 값이 바뀌었을 때 이벤트를 처리합니다.
		/// </summary>
		protected virtual void OnValueChanged()
		{
			elementSelected.GetComponent<Text>().text = selections[currentSelection];
		}

		/* Button Events */
		protected virtual void OnLeftArrowClicked()
		{
			if (currentSelection == 0)
			{
				currentSelection = selections.Count - 1;
			}
			else
			{
				currentSelection -= 1;
			}
			OnValueChanged();
		}
		protected virtual void OnRightArrowClicked()
		{
			if (currentSelection == selections.Count - 1)
			{
				currentSelection = 0;
			}
			else
			{
				currentSelection += 1;
			}
			OnValueChanged();
		}
	}
}
