using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

public class BasicModal : Modal
{
	[Header("Basic Settings")]
	[SerializeField] private string _parentContainerName = "MainModalContainer";
	[SerializeField] private bool _closeWhenClickedOutside;
	[SerializeField] private List<Button> _closeButtons = new List<Button>();

	private ModalContainer _parentContainer;

	public override IEnumerator Initialize()
	{
		_parentContainer = ModalContainer.Find(_parentContainerName);

		if (_closeWhenClickedOutside)
		{
			var btnClosePanel = transform.GetChild(0).gameObject;
			btnClosePanel.SetActive(true);
			_closeButtons.Add(btnClosePanel.GetComponent<Button>());
		}

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
