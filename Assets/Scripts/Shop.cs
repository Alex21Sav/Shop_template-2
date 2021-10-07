using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	#region Singlton:Shop

	public static Shop Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	#endregion

	[System.Serializable] public class ShopItem
	{
		public Sprite Image;
		public int Price;
		public bool IsPurchased = false;
	}


	[SerializeField] private Animator _noCoinsAnim;
	[SerializeField] private GameObject _itemTemplate;
	[SerializeField] private Transform _shopScrollView;
	[SerializeField] private GameObject _shopPanel;

	private Button _buyBtn;
	private GameObject _gameObjectAvatar;

	public List<ShopItem> ShopItemsList;

	void Start()
	{
		int len = ShopItemsList.Count;
		for (int i = 0; i < len; i++)
		{
			_gameObjectAvatar = Instantiate(_itemTemplate, _shopScrollView);
			_gameObjectAvatar.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
			_gameObjectAvatar.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();
			_buyBtn = _gameObjectAvatar.transform.GetChild(2).GetComponent<Button>();
			if (ShopItemsList[i].IsPurchased)
			{
				DisableBuyButton();
			}
			_buyBtn.AddEventListener(i, OnShopItemBtnClicked);
		}
	}

	void OnShopItemBtnClicked(int itemIndex)
	{
		if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
		{
			Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);			
			ShopItemsList[itemIndex].IsPurchased = true;			
			_buyBtn = _shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
			DisableBuyButton();			
			Game.Instance.UpdateAllCoinsUIText();			
			Profile.Instance.AddAvatar(ShopItemsList[itemIndex].Image);
		}
		else
		{
			_noCoinsAnim.SetTrigger("NoCoins");			
		}
	}
	void DisableBuyButton()
	{
		_buyBtn.interactable = false;
		_buyBtn.transform.GetChild(0).GetComponent<Text>().text = "PURCHASED";
	}	
	public void OpenShop()
	{
		_shopPanel.SetActive(true);
	}
	public void CloseShop()
	{
		_shopPanel.SetActive(false);
	}
}
