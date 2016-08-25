using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour {
	public bool Attacking = false;
	public float BaseMouseY;
	public GameObject Character;
	// Use this for initialization
	void Start () {
        BaseMouseY = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update () {
        float angle = (Input.mousePosition.y - BaseMouseY) / 10;
        if(angle > 0) angle = (Input.mousePosition.y - BaseMouseY) / 5;
        if (angle < 0) angle += 360f;
        if (angle > 90 && angle < 180) angle = 90f;
        else if (angle > 180 && angle < 340) angle = 340f;
        this.transform.localEulerAngles = new Vector3(0, 0, angle) ;
        Debug.Log(Input.mousePosition.y + "/" + angle);
	}

	void FixedUpdate() {
	}

	void attack() {

	}
}
