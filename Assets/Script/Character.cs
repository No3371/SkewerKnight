using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    public enum CharacterState {
        Normal,
        Jumping,
        Bending
    }

    public CharacterState State;
    
	public bool moving;
	public float jspeed = 0f;
	public float top = 2f;
	public float gravity = 7f;
	public float JumpPower = 10f;
	public float GrY;
	public Animator animator;
    public bool ToggleSwitch = true;
    AudioSource JumpSound, BendSound, BustSound;
    public AudioSource[] Audiolist;
    public GameObject Spear;
    GameObject HorseFace;
    SpriteRenderer HFS;
    bool Invincible = false;
    public float Busti;
    public int BustStatus = 0;

    // Use this for initialization
    void Start () {
        HorseFace = transform.Find("HorseFace").gameObject;
        HFS = HorseFace.GetComponent<SpriteRenderer>();
        State = CharacterState.Normal;
		moving = false;
		animator = GetComponent<Animator>();
        Audiolist = GetComponents<AudioSource>();
        JumpSound = Audiolist[0];
        BendSound = Audiolist[1];
        BustSound = Audiolist[2];
        GrY = WorldManager.Instance.GroundY;
    }
	
	// Update is called once per frame
	void Update () {
        bool BSwitch = true; //BendSound switch
        if (GameManager.Instance.IsPlayed)
        {
            //basic move
            transform.Translate(Vector3.right * GameManager.Instance.ScrollSpeed * Time.deltaTime);
            //end basic move

            //input
            if (InputManager.Instance.jump && State == CharacterState.Normal && !GameManager.Instance.Busted)
            {
                GameManager.Instance.MainCamera.GetComponent<CameraController>().LockY = true;
                JumpSound.Play();
                State = CharacterState.Jumping;
                jspeed = JumpPower;
                transform.Translate(Vector3.up * (jspeed - gravity) * Time.deltaTime);
                animator.SetBool("Jump", true);
            }
            else if (InputManager.Instance.bind && State != CharacterState.Jumping && !GameManager.Instance.Busted)
            {
                if (State == CharacterState.Bending && ToggleSwitch)
                {
                    ToggleSwitch = false;
                    Spear.GetComponent<Spear>().ToggleLock();
                }
                State = CharacterState.Bending;
                animator.SetBool("Bending", true);
            }
            else if (InputManager.Instance.bindup && !GameManager.Instance.Busted)
            {
                InputManager.Instance.bind = false;
                InputManager.Instance.bindup = false;
                ToggleSwitch = true;
                StartCoroutine(Uptime());
            }
            else
            {
                InputManager.Instance.bind = false;
                InputManager.Instance.bindup = false;
                if (State == CharacterState.Bending)
                {
                    if (State == CharacterState.Normal) Spear.GetComponent<Spear>().ToggleLock();
                    State = CharacterState.Normal;
                    animator.SetBool("Bending", false);
                    moving = false;
                }
            }
            if (!(State == CharacterState.Bending) && BSwitch)
            {
                BendSound.Play();
                BSwitch = false;
                HFS.enabled = true;
            }
            if ((State == CharacterState.Bending))
            {
                BSwitch = true;
                HFS.enabled = false;
            }
        }
    }

	void FixedUpdate() {
		if(transform.position.y > GrY) {
			transform.Translate(Vector3.up * (jspeed - gravity) * Time.deltaTime);
		}
		if(State == CharacterState.Jumping) {
			jspeed -= 0.14f;
            if (this.transform.position.y <= GrY) //著地
            {
                InputManager.Instance.jump = false;
                jspeed = 0;
                GameManager.Instance.MainCamera.GetComponent<CameraController>().LockY = false;
                State = CharacterState.Normal;
                animator.SetBool("JumpBack", false);
				transform.position = new Vector2(transform.position.x,GrY);
			}
            else if(jspeed <= gravity) //當腳色跳到最高時
            {
                animator.SetBool("JumpBack", true);
                animator.SetBool("Jump", false);
            }
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
        if (Invincible)
        {
            GameManager.Instance.Score += 150;
            Destroy(other.gameObject);
            MobManager.Instance.MobCount--;
        }
        else
        {
            if (other.gameObject.layer == 8)
            {
                if (other.tag == "Gate")
                {
                    if (State != CharacterState.Bending)
                    {
                        Destroy(other.gameObject);
                        GameManager.Instance.GameOver();
                    }
                }
                else
                {
                    Destroy(other.gameObject);
                    GameManager.Instance.GameOver();
                }
            }
        }
    }

    public IEnumerator Bust()
    {
        BustSound.Play();
        //GameManager.Instance.MainCamera.GetComponent<AudioSource>().volume = 0.8f;
        //GameManager.Instance.MainCamera.GetComponent<AudioSource>().pitch = 1.2f;
        if (State == CharacterState.Bending)
        {
            ToggleSwitch = true;
            Spear.GetComponent<Spear>().ToggleLock();
        }
        else if(State == CharacterState.Jumping)
        {
            animator.SetBool("JumpBack", false);
            animator.SetBool("Jump", false);
            animator.Play("Normal");
        }
        for (int i = 5; i < Spear.transform.childCount; i++) Destroy(Spear.transform.GetChild(i).gameObject);
        Spear.GetComponent<Spear>().Count = 0;
        Spear.GetComponent<Spear>().Caught.Clear();
        Invincible = true;
        for(Busti = 0; Busti < 6; Busti += Time.deltaTime)
        {
            if (Busti < 1f)
            {
                GameManager.Instance.MainCamera.GetComponent<AudioSource>().pitch += (Time.deltaTime * 0.2f);
                GameManager.Instance.MainCamera.GetComponent<AudioSource>().volume -= (Time.deltaTime * 0.2f);
                BustStatus = 1;
                transform.localScale += new Vector3(Time.deltaTime * 5, Time.deltaTime * 5, 0);
            }
            else if (Busti > 5f)
            {
                GameManager.Instance.MainCamera.GetComponent<AudioSource>().pitch -= (Time.deltaTime * 0.2f);
                GameManager.Instance.MainCamera.GetComponent<AudioSource>().volume += (Time.deltaTime * 0.2f);
                BustStatus = 2;
                transform.localScale -= new Vector3(Time.deltaTime * 5, Time.deltaTime * 5, 0);
            }
            else BustStatus = 0;
            yield return 0;
        }
        GameManager.Instance.MainCamera.GetComponent<AudioSource>().volume = 1f;
        GameManager.Instance.MainCamera.GetComponent<AudioSource>().pitch = 1f;
        BustStatus = 0;
        transform.localScale = new Vector3(1, 1, 1);
        Invincible = false;
        GameManager.Instance.Busted = false;
    }
    IEnumerator Uptime()
    {
        for(float i = 0; i < 0.1 ; i += Time.deltaTime)
        {
            yield return 0;
        }
        Spear.GetComponent<Spear>().ToggleLock();
    }
}
