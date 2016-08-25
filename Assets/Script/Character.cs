using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public enum CharacterState {
        Normal,
        Jumping,
        Bending
    }

    CharacterState State;
    
	public bool moving;
	public float jspeed = 0f;
	public float top = 2f;
	public float gravity = 7f;
	public float JumpPower = 10f;
	public float GrY;
	public Animator animator;

	// Use this for initialization
	void Start () {
		State = CharacterState.Normal;
		moving = false;
		animator = GetComponent<Animator>();

        GrY = WorldManager.Instance.GroundY;

    }
	
	// Update is called once per frame
	void Update () {
		//basic move
		transform.Translate(Vector3.right * GameManager.Instance.ScrollSpeed * Time.deltaTime);
        //end basic move
        
        //input
        if (Input.GetKeyDown(KeyCode.Space) && !moving) {
            GameManager.Instance.MainCamera.GetComponent<CameraController>().LockY = true;
            State = CharacterState.Jumping;
            jspeed = JumpPower;
			moving = true;
			transform.Translate(Vector3.up * (jspeed - gravity) * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S) && !moving)
        {
            Debug.Log("detected.");
            State = CharacterState.Bending;
            animator.SetBool("Bending", true);
            moving = true;
        }
        //else
        //{
           // State = CharacterState.Normal;
           // animator.SetBool("Bending", false);
           // moving = false;
        //}
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
		if(State == CharacterState.Jumping) {
			jspeed -= 0.14f;
			if(this.transform.position.y <= GrY)
            {
                GameManager.Instance.MainCamera.GetComponent<CameraController>().LockY = false;
                State = CharacterState.Normal;
				moving = false;
				jspeed = 0;
				transform.position = new Vector2(transform.position.x,GrY);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 8) GameManager.Instance.GameOver();
    }
    

}
