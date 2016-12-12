using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDebug : MonoBehaviour {
    public float rs;
	// Use this for initialization
	void Start () {
        rs = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.touchCount == 1) GetComponent<Text>().text = "count" + Input.touchCount + " " + Input.touches[0].position.x;
        //else if (Input.touchCount > 1) GetComponent<Text>().text = "count" + Input.touchCount + " " + Input.touches[0].position.x + " " + Input.touches[1].position.x;
        //else GetComponent<Text>().text = "no input" + rs + "!!";
    }
}
