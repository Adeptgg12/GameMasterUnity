using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner2 : MonoBehaviour
{
    public List<GameObject> boxList;
    public GameObject boxPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Test Button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObject();
        }
    }

    public void SpawnObject()
    {
        GameObject lastBox = boxList[boxList.Count - 1];

        Vector3 newPosition = new Vector3(lastBox.transform.position.x, lastBox.transform.position.y + 0.95f, 0);
        GameObject newObject = Instantiate(boxPrefab, newPosition, Quaternion.identity);

        boxList.Add(newObject);  // เพิ่มไปยัง List
        Debug.Log("Object added to list. Total objects: " + boxList.Count);
    }
}
