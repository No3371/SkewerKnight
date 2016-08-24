using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour {
	public bool attacking = false;
	public float angle;
	public GameObject character;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X) && !attacking) {
			attacking = true;
		}

		if(attacking) {
			attack();
		}

		//public float slope = mathf.atan2()
		character = GameObject.FindWithTag("character");
		transform.position = new Vector2(character.transform.position.x - 0.43f ,1f);
	}

	void FixedUpdate() {
		//angle = Mathf.Atan2(500 - transform.position.y, 500 - transform.position.x) * Mathf.Rad2Deg;
        //Debug.Log(mousePosition.x + " " + mousePosition.y);
        //if(angle > this.transform.rotation.z) transform.Rotate(0, 0, -1);
        //else if(angle < this.transform.rotation.z) transform.Rotate(0, 0, 1);
	}

	void attack() {

	}
}
