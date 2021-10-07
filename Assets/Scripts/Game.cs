using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	#region SIngleton:Game

	public static Game Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

	[SerializeField] private Text[] _allCoinsUIText;

	public int Coins;

	void Start()
	{
		UpdateAllCoinsUIText();
	}
	public void UseCoins(int amount)
	{
		Coins -= amount;
	}
	public bool HasEnoughCoins(int amount)
	{
		return (Coins >= amount);
	}
	public void UpdateAllCoinsUIText()
	{
		for (int i = 0; i < _allCoinsUIText.Length; i++)
		{
			_allCoinsUIText[i].text = Coins.ToString();
		}
	}
}
