using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTimer2 : MonoBehaviour
{
    public WordManager2 wordManager;

	public float wordDelay = 1.5f;
	private float nextWordTime = 0f;

	private void Update()
	{
		if (Time.time >= nextWordTime)
		{
			wordManager.AddWord();
			nextWordTime = Time.time + wordDelay;
			wordDelay *= .99f;
		}
	}
}
