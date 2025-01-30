using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour {

	public GameObject wordPrefab;
	public Transform wordCanvas;

	public Transform panel;

	public WordDisplay SpawnWord ()
	{
		//Vector3 randomPosition = new Vector3(Random.Range(-2.5f, 2.5f), 7f);

		//GameObject wordObj = Instantiate(wordPrefab, randomPosition, Quaternion.identity, wordCanvas);

		GameObject newObject = Instantiate(wordPrefab);

        newObject.transform.SetParent(panel, false);

		newObject.transform.SetAsFirstSibling();

        //AddItemAnimation addItemAnimation = newObject.GetComponent<AddItemAnimation>();
        //addItemAnimation.itemImage.sprite = sprite;

        RectTransform rectTransform = newObject.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }

		WordDisplay wordDisplay = newObject.GetComponent<WordDisplay>();
		
		return wordDisplay;
	}

	public void InstantiateObject()
    {
        GameObject newObject = Instantiate(wordPrefab);

        newObject.transform.SetParent(panel, false);

        //AddItemAnimation addItemAnimation = newObject.GetComponent<AddItemAnimation>();
        //addItemAnimation.itemImage.sprite = sprite;

        RectTransform rectTransform = newObject.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

}
