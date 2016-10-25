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
    Vector2 Pos;
    float Ran;
    static float SlipDuration = 2f;
    Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        Pos = transform.position;
        Ran = Random.Range(0, 30);
        ifCaught = false;
		if (Type == MobManager.MobType.Fat || Type == MobManager.MobType.BalloonFat) {
			Volume = 3;
		} else
			Volume = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if(!ifCaught && (Type == MobManager.MobType.BalloonFat || Type == MobManager.MobType.BalloonKid))
        {
            Pos.y += (Mathf.Cos(Time.time + Ran) / 300);
            transform.position = Pos;
        }

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
                    transform.localPosition = Position;
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
        GetComponent<AudioSource>().Play();
        if(animator != null) animator.SetBool("die",true);
        MobManager.Instance.MobCount -= 1;
        this.transform.parent = Spear.transform;
        Spear.Count += Volume;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        PositionOnSpear = (Type == MobManager.MobType.Fat || Type == MobManager.MobType.BalloonFat) ? Spear.Count - 1 : Spear.Count;

		GameObject Blood = MobManager.Instance.Blood;
		GameObject temp = (GameObject)Instantiate(Blood,new Vector2(0,0) , new Quaternion());
        temp.GetComponent<Blood>().Mob = this;
        temp.GetComponent<Blood>().MobNumber = (int)this.Type;
		switch ((int)this.Type)
        {

		case 0:
			Position = Spear.PosList [PositionOnSpear];
			Position.y = -0.4f;
			break;
		case 4:
			Position = Spear.PosList [PositionOnSpear];
			Position.y = -0.4f;
            break;
		case 1:
		case 2:
			Position = Spear.PosList [PositionOnSpear];
			Position.y = -0.3f;
            break;
		case 3:
			Position = Spear.PosList [PositionOnSpear];
			Position.y = 0.09f;
            break;
		case 5:
		case 6:
			if ((transform.childCount == 1)) {
				Destroy ((transform.FindChild ("BallonB")).gameObject);
			}
			if (PositionOnSpear > 4)
				Position = Spear.PosList [4];
			else
				Position = Spear.PosList [PositionOnSpear];
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
