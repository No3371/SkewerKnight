using UnityEngine;
using System.Collections;

public class AutoDispose : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x - GameManager.Instance.MainCamera.transform.position.x < -8) Destroy(this.gameObject);
	}
}
