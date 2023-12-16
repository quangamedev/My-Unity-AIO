using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

public class BasicModal : Modal
{
	[SerializeField] private string _parentContainerName = "MainModalContainer";
	[SerializeField] private Button[] _closeButtons;

	private ModalContainer _parentContainer;

	public override IEnumerator Initialize()
	{
		_parentContainer = ModalContainer.Find(_parentContainerName);
		foreach (var button in _closeButtons)
		{
			button.onClick.AddListener(OnBtnCloseClick);
		}
		yield break;
	}

	public virtual void Close()
	{
		foreach (var button in _closeButtons)
		{
			button.interactable = false;
		}
		_parentContainer.Pop(true);
	}

	private void OnBtnCloseClick()
	{
		Close();
	}
}
