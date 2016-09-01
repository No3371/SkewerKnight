using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour {
	public MobManager.MobType Type;
	public bool ifCaught;
    public bool CountPlus = false;
	public int PositionOnSpear;
	public int Volume;
	public Spear Spear;
    public Vector2 Position;

    static float SlipDuration = 2f;

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
        if (ifCaught == true) //抓到
        {
            if (!CountPlus)
            {
                Caught();
                CountPlus = true;
            }
            else if (CountPlus && transform.parent == Spear.transform)
            {
                if ((Vector2)this.transform.localPosition == Position)
                {
                    this.transform.localPosition = Position;
                }
                else
                {
                    transform.localPosition = Vector2.MoveTowards(this.transform.localPosition, Position, SlipDuration * Time.deltaTime);
                }
            }
        }

    }

    void Caught()
    {
        MobManager.Instance.MobCount -= 1;
        this.transform.parent = Spear.transform;
        Spear.Count += Volume;
        PositionOnSpear = (Type == MobManager.MobType.Fat || Type == MobManager.MobType.BalloonFat) ? Spear.Count - 1 : Spear.Count;
        switch ((int)this.Type)
        {
            case 0:
            case 4:
                Position = Spear.PosList[PositionOnSpear];
                Position.y = -0.4f;
                break;
            case 1:
            case 2:
                Position = Spear.PosList[PositionOnSpear];
                Position.y = -0.3f;
                break;
            case 3:
                Position = Spear.PosList[PositionOnSpear];
                Position.y = -0.1f;
                break;
            case 5:
            case 6:
                if ((transform.childCount == 1))
                {
                    Destroy((transform.FindChild("BallonB")).gameObject);
                }
                Position = Spear.PosList[PositionOnSpear];
                Position.y = -0.5f;
                break;
            default:
                break;
        }
    }

    void UpdatePos()
    {
        this.transform.position = Spear.PosList[PositionOnSpear];
    }
}
