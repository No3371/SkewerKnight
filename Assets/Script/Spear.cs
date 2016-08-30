using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spear : MonoBehaviour {
	public bool Attacking = false;
	public float BaseMouseY;
	public GameObject Character;
    Animator animator, HorseFace;
    AudioSource Sound;

    public int Count; //0~5

    List<AudioClip> SoundList = new List<AudioClip>();

    public List<GameObject> Caught = new List<GameObject>();

    public List<Vector2> PosList = new List<Vector2>();

    float LastAttackTime;

    public bool Lock = false;
	// Use this for initialization
	void Start () {
        BaseMouseY = Screen.height / 2;
        animator = GetComponent<Animator>();

        Animator[] temp = transform.parent.GetComponentsInChildren<Animator>();
        foreach(Animator a in temp)
        {
            if (a.gameObject.name == "HorseFace") HorseFace = a;
        }

        SoundList.AddRange(Resources.LoadAll<AudioClip>("Sounds"));
        for (int i = 0; i < 5; i++)
        {
            PosList.Add(GetComponentsInChildren<Transform>()[i].localPosition);
        } 
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - LastAttackTime > 0.5f) Attacking = false;
        if (!Lock)
        {
            UpdateAngle();
            if (Input.GetMouseButtonDown(0))
            {
                Attacking = true;
                animator.SetTrigger("Push");
                LastAttackTime = Time.time;
            }

        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {   if(Count >= 5)
            {
                Debug.Log("time to eat");
                //Eat();
            }
            else
            {
                Debug.Log(Count);
                other.GetComponent<Mob>().ifCaught = true;
                other.GetComponent<Mob>().Spear = this;
                Caught.Add(other.gameObject);
            }
            
            
        }
    }

    int[] Score = { 50, 80, 80, 100, 100, -1, -1 };

    void Eat()
    {
        int tempScore = 0;
        HorseFace.SetTrigger("Eat");
        Count = 0;
        foreach(GameObject C in Caught)
        {
            tempScore += Score[(int) C.GetComponent<Mob>().Type];
        }
        Caught.Clear();
        GameManager.Instance.Score += tempScore;
    }

    public void ToggleLock() {
        Lock = !Lock;
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }

	void UpdateAngle()
    {
        float angle = (Input.mousePosition.y - BaseMouseY) / 10;
        if (angle > 0) angle = (Input.mousePosition.y - BaseMouseY) / 5;
        if (angle < 0) angle += 360f;
        if (angle > 90 && angle < 180) angle = 90f;
        else if (angle > 180 && angle < 340) angle = 340f;
        this.transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
