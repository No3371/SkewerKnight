using UnityEngine;
using System.Collections;

public class AutoDispose : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void onDestroy()
    {
        if(this.gameObject.layer == 9)
        {
            MobManager.Instance.MobCount -= 1;
        }
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x - GameManager.Instance.MainCamera.transform.position.x < -15) Destroy(this);
	}
}
