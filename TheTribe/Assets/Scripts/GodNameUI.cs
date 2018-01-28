using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodNameUI : MonoBehaviour {
	Text nameText;
	GodNameGenerator generator;

	void Start () {
		nameText = GetComponent<Text>();
		generator = GetComponentInChildren<GodNameGenerator>();
		Color c = nameText.color;
		c.a = 0;
		nameText.color = c;
		nameText.text = GetGodName();
		Invoke("Display", 1);
	}

	string GetGodName()
	{
		TotemManager totem = Object.FindObjectOfType<TotemManager>();
		string head = totem.GetGodHeadGeneratedAspect().aspectName;
		string torso = totem.GetGodUpperbodyGeneratedAspect().aspectName;
		string leg = totem.GetGodLowerbodyGeneratedAspect().aspectName;
		string stick = totem.GetGodAccessoryGeneratedAspect().aspectName;
		return generator.GenerateGodName(head, torso, leg, stick);
	}

	public void Display()
	{
		StartCoroutine(MakeTextAppear());
	}

	public void Hide()
	{
		StartCoroutine(MakeTextDisappear());
	}
	
	IEnumerator MakeTextAppear()
	{
		Color c = nameText.color;
		while (nameText.color.a < 1)
		{
			c.a += Time.deltaTime * 0.5f;
			nameText.color = c;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator MakeTextDisappear()
	{
		Color c = nameText.color;
		while (nameText.color.a > 0)
		{
			c.a -= Time.deltaTime * 0.5f;
			nameText.color = c;
			yield return new WaitForEndOfFrame();
		}
	}
}
