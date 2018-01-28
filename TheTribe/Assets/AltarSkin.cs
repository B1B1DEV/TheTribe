using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarSkin : MonoBehaviour {
	public GameObject[] skins;
	int currentIndex = 0;

	void Start () {
		TribeManager.OnNewAge += AgeChanged;
		ToNextSkin(currentIndex);
	}

	private void OnDestroy()
	{
		TribeManager.OnNewAge -= AgeChanged;
	}

	void AgeChanged()
	{
		ToNextSkin(currentIndex + 1);
	}

	void ToNextSkin(int newIndex)
	{
		if (newIndex >= 0 && skins.Length > newIndex)
		{
			skins[currentIndex].SetActive(false);
			currentIndex = newIndex;
			skins[currentIndex].SetActive(true);
		}
	}
}
