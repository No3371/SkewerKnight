using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public string state;
	public bool moving;
	public int speed;
	public int jspeed;
	public float top = 1.24f;
	public float Basicy = -0.24f;
	// Use this for initialization
	void Start () {
		state = "n";
		moving = false;
		speed = 10;
		jspeed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//basic move
		transform.Translate(Vector3.right * speed * Time.deltaTime);
		//end basic move
		
		//input
		if(Input.GetKeyDown(KeyCode.Space) && !moving) {
			state = "ju";
			moving = true;

		}
		else if(Input.GetKeyDown(KeyCode.Z) && !moving) {
			state = "d";
			moving = true;
		}
		//end input

		//move execute
		if(state == "ju") {
			if(transform.position.y < top) {
				transform.Translate(Vector3.up * jspeed * Time.deltaTime);
			}
			else if(transform.position.y >= top) state = "jd";
		}
		else if(state == "jd") {
			if(transform.position.y >= Basicy) {
				transform.Translate(Vector3.down * jspeed * Time.deltaTime);
			}
			else if(transform.position.y < Basicy) {
				transform.position = new Vector2(transform.position.x,Basicy);
				state = "n";
				moving = false;
			}
		}
		if(state == "d") down();
		//end move execute
	}

	void OnTriggerEnter(Collider other) {
    }

	void jump() {

	}

	void down() {

	}

}
