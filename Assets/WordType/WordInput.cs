using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordInput : MonoBehaviour
{

	public WordManager wordManager;

	// Update is called once per frame
	void Update()
	{
		foreach (char letter in Input.inputString)
		{
			wordManager.TypeLetter(letter);
		}
	}

}
