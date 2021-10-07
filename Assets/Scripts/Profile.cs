using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
	#region Singlton:Profile
	public static Profile Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}
	#endregion

	public class Avatar
	{
		public Sprite Image;
	}


	[SerializeField] private GameObject _avatarUITemplate;
	[SerializeField] private Transform _avatarsScrollView;


	[SerializeField] private Color _activeAvatarColor;
	[SerializeField] private Color _defaultAvatarColor;

	[SerializeField] private Image _currentAvatar;

	private GameObject _gameObjectAvatar;
	private int _newSelectedIndex;
	private int _previousSelectedIndex;

	public List<Avatar> AvatarsList;
	void Start()
	{
		GetAvailableAvatars();
		_newSelectedIndex = _previousSelectedIndex = 0;
	}
	void GetAvailableAvatars()
	{
		for (int i = 0; i < Shop.Instance.ShopItemsList.Count; i++)
		{
			if (Shop.Instance.ShopItemsList[i].IsPurchased)
			{				
				AddAvatar(Shop.Instance.ShopItemsList[i].Image);
			}
		}
		SelectAvatar(_newSelectedIndex);
	}
	public void AddAvatar(Sprite img)
	{
		if (AvatarsList == null)
			AvatarsList = new List<Avatar>();

		Avatar avatar = new Avatar() { Image = img };		
		AvatarsList.Add(avatar);		
		_gameObjectAvatar = Instantiate(_avatarUITemplate, _avatarsScrollView);
		_gameObjectAvatar.transform.GetChild(0).GetComponent<Image>().sprite = avatar.Image;
		_gameObjectAvatar.transform.GetComponent<Button>().AddEventListener(AvatarsList.Count - 1, OnAvatarClick);
	}
	void OnAvatarClick(int AvatarIndex)
	{
		SelectAvatar(AvatarIndex);
	}
	void SelectAvatar(int AvatarIndex)
	{
		_previousSelectedIndex = _newSelectedIndex;
		_newSelectedIndex = AvatarIndex;
		_avatarsScrollView.GetChild(_previousSelectedIndex).GetComponent<Image>().color = _defaultAvatarColor;
		_avatarsScrollView.GetChild(_newSelectedIndex).GetComponent<Image>().color = _activeAvatarColor;

		_currentAvatar.sprite = AvatarsList[_newSelectedIndex].Image;
	}
}
