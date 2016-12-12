using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {
	public Mob Mob;
    public int MobNumber;
    // Use this for initialization
    void Start () {
        transform.parent = Mob.transform;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        switch (MobNumber)
        {
            case 0:
                transform.localPosition = new Vector2(-0.3f,0.35f);
                break;
            case 1:
                transform.localPosition = new Vector2(-0.203f,0.23f);
                break;
            case 2:
                transform.localPosition = new Vector2(-0.203f,0.248f);
                break;
            case 3:
                transform.localPosition = new Vector2(-0.203f,-0.15f);
                break;
            case 4:
                transform.localPosition = new Vector2(-0.245f,0.348f);
                break;
            case 5:
            case 6:
                transform.localPosition = new Vector2(-0.355f,0.44f);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        float ChangTime;
        float zindex;
        zindex = GameManager.Instance.Spear.transform.localEulerAngles.z;
    }
}
