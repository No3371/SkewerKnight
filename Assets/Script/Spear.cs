using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spear : MonoBehaviour {
	public bool Attacking = false;
	public float BaseMouseY;
    public float EatDelay = 0.1f;
	public GameObject Character;
    Animator animator, HorseFace;
    AudioSource Sound,SpearSound,CaughtSound;
    float EatTime = 0;
    public int Count; //0~5

    List<AudioClip> SoundList = new List<AudioClip>();

    public List<GameObject> Caught = new List<GameObject>();

    public List<Vector2> PosList = new List<Vector2>();
    float LastAttackTime;
    public AudioSource[] Audiolist;
    public bool Lock = false;
    Component[] ChildRenderer;
    // Use this for initialization
    void Start () {
        BaseMouseY = Screen.height / 2;
        animator = GetComponent<Animator>();
        HorseFace = GameObject.Find("HorseFace").GetComponent<Animator>();
        Audiolist = GetComponents<AudioSource>();
        SpearSound = Audiolist[0];
        CaughtSound = Audiolist[1];
        SoundList.AddRange(Resources.LoadAll<AudioClip>("Sounds"));
        for (int i = 0; i < 6; i++)
        {
            PosList.Add(GetComponentsInChildren<Transform>()[i].localPosition);
        } 
    }
	
	// Update is called once per frame
	void Update () {
        if(Count >= 5) Eat();

        if ((Time.time - LastAttackTime > 0.43f))
        {
            if (!Lock)
            {
                if (Input.GetMouseButtonDown(0) )
                {
                        SpearSound.Play();
                        Attacking = true;
                        animator.SetTrigger("Push");
                        LastAttackTime = Time.time;
                }
                else Attacking = false;
            }
        }

        if(!Lock) UpdateAngle();

        if (EatTime != 0)
        {
            if ((Time.time - EatTime) > EatDelay)
            {
                for (int i = 5; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
                EatTime = 0;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (Attacking)
        {
            if (other.gameObject.layer == 9)
            {
                CaughtSound.Play();
                other.GetComponent<Mob>().ifCaught = true;
                other.GetComponent<Mob>().Spear = this;
                Caught.Add(other.gameObject);
            }
        }
    }

    int[] Score = { 50, 80, 80, 100, 100, -1, -1 };

    void Eat()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        EatTime = Time.time;
        HorseFace.SetTrigger("Eat");
        int tempScore = 0;
        foreach(GameObject C in Caught)
        {
            tempScore += Score[(int) C.GetComponent<Mob>().Type];
        }
        GameManager.Instance.Score += tempScore;
        Count = 0;
        Caught.Clear();
    }

    public void ToggleLock() {
        Lock = !Lock;
        ChildRenderer = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer child in ChildRenderer)
            child.enabled = !child.enabled;
    }

	void UpdateAngle()
    {
        float angle = (Input.mousePosition.y - BaseMouseY) / 10;
        if (angle > 0) angle = (Input.mousePosition.y - BaseMouseY) / 5;
        if (angle < 0) angle += 360f;
        if (angle > 90 && angle < 180) angle = 90f;
        else if (angle > 180 && angle < 340) angle = 340f;
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
