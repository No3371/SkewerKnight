using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
	public MobManager.MobType Type;
	public bool ifCaught;
    public bool CountPlus = false;
	public int PositionOnSpear;
	public int Volume;
	public Spear Spear;

    static float SlipDuration = 0.4f;

	// Use this for initialization
	void Start () {
		ifCaught = false;
		if (Type == MobManager.MobType.Fat || Type == MobManager.MobType.BalloonFat) {
			Volume = 3;
		} else
			Volume = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (ifCaught == true)
        {
            if (!CountPlus)
            {
                Caught();
                CountPlus = true;
            }
            else {
                transform.position = Spear.PosList[PositionOnSpear - 1];
            }
        }
    }

    void Caught()
    {
        Spear.Count += Volume;
        PositionOnSpear = (Type == MobManager.MobType.Fat || Type == MobManager.MobType.BalloonFat) ? Spear.Count - 1 : Spear.Count;
    }

    void UpdatePos()
    {
        this.transform.position = Spear.PosList[PositionOnSpear];
    }
}
