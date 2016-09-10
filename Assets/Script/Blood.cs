using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour {
    public Spear Spear;
    public Vector2 Position;
    public int PositionOnSpear;
    public Sprite[] Sprite;
    float SlipDuration = 2f;
    // Use this for initialization
    void Start () {
        Position.y = -0.06f;
        transform.parent = Spear.transform;
        transform.localPosition = new Vector2(2.61f, -0.09f);
        transform.rotation = new Quaternion(0,0,0,0);
    }
	
	// Update is called once per frame
	void Update () {
        if ((Vector2)transform.localPosition != Position)
        {
            transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, Position, SlipDuration * Time.deltaTime);
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = Sprite[PositionOnSpear];
        }
    }
}
