using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public string state;
	public bool moving;
	public float jspeed = 0f;
	public float top = 2f;
	public float gravity = 5f;
	public float JumpPower = 5f;
	public float GrY = WorldManager.Instance.GroundY;
	// Use this for initialization
	void Start () {
		state = "n";
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		//basic move
		transform.Translate(Vector3.right * GameManager.Instance.ScrollSpeed * Time.deltaTime);
		//end basic move
		
		//input
		if(Input.GetKeyDown(KeyCode.Space) && !moving) {
			state = "j";
			jspeed = JumpPower;
			moving = true;
			transform.Translate(Vector3.up * (jspeed - gravity) * Time.deltaTime);
		}
		else if(Input.GetKeyDown(KeyCode.Z) && !moving) {
			state = "d";
			moving = true;
		}
		//end input

		//move execute
		// if(state == "ju") {
		// 	if(transform.position.y < top) {
		// 		transform.Translate(Vector3.up * jspeed * Time.deltaTime);
		// 	}
		// 	else if(transform.position.y >= top) state = "jd";
		// }
		// else if(state == "jd") {
		// 	if(transform.position.y >= WorldManager.Instance.GroundY) {
		// 		transform.Translate(Vector3.down * jspeed * Time.deltaTime);
		// 	}
		// 	else if(transform.position.y < WorldManager.Instance.GroundY) {
		// 		transform.position = new Vector2(transform.position.x,WorldManager.Instance.GroundY);
		// 		state = "n";
		// 		moving = false;
		// 	}
		// }
		//if(state == "d") down();
		//end move execute
	}

	void FixedUpdate() {
		if(transform.position.y > GrY) {
			transform.Translate(Vector3.up * (jspeed - gravity) * Time.deltaTime);
		}
		if(state == "j") {
			jspeed -= 0.1f;
			if(this.transform.position.y <= GrY) {
				state = "n";
				moving = false;
				jspeed = 0;
				transform.position = new Vector2(transform.position.x,GrY);
			}
		}
	}

	void OnTriggerEnter(Collider other) {

    }

	void jump() {

	}

	void down() {

	}

}
