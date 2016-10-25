using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public bool LockY = false;

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(0, -0.57f + Camera.main.orthographicSize, -6f);
	}
	
	// Update is called once per frame
	void Update () {
        if(LockY)
            this.transform.position = new Vector3(GameManager.Instance.Character.transform.position.x, this.transform.position.y) + new Vector3(3, 0, -6);
        else if(!LockY) this.transform.position = new Vector3(GameManager.Instance.Character.transform.position.x + 3, -2f + this.GetComponent<Camera>().orthographicSize, -6f);
    }


}
