using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    TextMesh originalNumber;
    List<TextMesh> listOfNumbers = new List<TextMesh>();


    // Start is called before the first frame update
    void Start()
    {
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos(Mathf.PI / 3) * 2.3f, Mathf.Sin(Mathf.PI / 3) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos(Mathf.PI / 6) * 2.3f, Mathf.Sin(Mathf.PI / 6) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos(Mathf.PI * 2) * 2.3f, Mathf.Sin(Mathf.PI * 2) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 11) / 6) * 2.3f, Mathf.Sin((Mathf.PI * 11) / 6) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 5) / 3) * 2.3f, Mathf.Sin((Mathf.PI * 5) / 3) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 3) / 2) * 2.3f, Mathf.Sin((Mathf.PI * 3) / 2) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 4) / 3) * 2.3f, Mathf.Sin((Mathf.PI * 4) / 3) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 7) / 6) * 2.3f, Mathf.Sin((Mathf.PI * 7) / 6) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos(Mathf.PI) * 2.3f, Mathf.Sin(Mathf.PI) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 5) / 6) * 2.3f, Mathf.Sin((Mathf.PI * 5) / 6) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos((Mathf.PI * 2) / 3) * 2.3f, Mathf.Sin((Mathf.PI * 2) / 3) * 2.3f, 1), Quaternion.identity));
        listOfNumbers.Add(TextMesh.Instantiate(originalNumber, new Vector3(Mathf.Cos(Mathf.PI / 2) * 2.3f, Mathf.Sin(Mathf.PI / 2) * 2.3f, 1), Quaternion.identity));

        for (int i = 0; i < listOfNumbers.Count; i++)
        {
            listOfNumbers[i].text = (i + 1).ToString(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Figure out x and y using angle and radius, along with trig functions
        //Then place the numbers down
    }
}
