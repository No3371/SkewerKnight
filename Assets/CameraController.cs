using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public bool LockY = false;
    bool LockCamera = false;
    bool Check = false;
    public GameObject Character;
    Character Bust;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, -0.57f + Camera.main.orthographicSize, -6f);
        Bust = Character.GetComponent<Character>();
    }
	
	// Update is called once per frame
	void Update () {
        if (LockY) transform.position = new Vector3(GameManager.Instance.Character.transform.position.x, transform.position.y) + new Vector3(3, 0, -6);
        else if (!LockY) {
            if (GameManager.Instance.Busted)
            {
                if (!LockCamera) StartCoroutine(BustCamera());
            }
            else
            {
                transform.position = new Vector3(GameManager.Instance.Character.transform.position.x + 3, -2f + GetComponent<Camera>().orthographicSize, -6f);
                LockCamera = false;
            }
        }
    }
    IEnumerator BustCamera()
    {
        LockCamera = true;
        while (true)
        {
            if(Bust.BustStatus == 1) transform.position = new Vector3(GameManager.Instance.Character.transform.position.x + 3 + Bust.Busti * 5, -2f + GetComponent<Camera>().orthographicSize, -6f);
            else if(Bust.BustStatus == 2) transform.position = new Vector3(GameManager.Instance.Character.transform.position.x + 3 + ((6 - Bust.Busti) * 5), -2f + GetComponent<Camera>().orthographicSize, -6f);
            else transform.position = new Vector3(GameManager.Instance.Character.transform.position.x + 8, -2f + GetComponent<Camera>().orthographicSize, -6f);
            if (!GameManager.Instance.Busted) break;
            yield return 0;
        }
    }

}
