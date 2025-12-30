using UnityEngine;

public class Boss2 : Enemy
{
    private bool charging = true;
    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        
    }
    public override void OnEnable()
    {
        base.OnEnable();

        EnterIdleState();

        
    }
    public override void Start()
    {
        base.Start();
      
        desrtoyEffectPool = GameObject.Find("Boom3Pool").
        GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.hitBoss;
        deathSound = AudioManager.Instance.dead;
        

    }

    public override void Update()
    {
        base.Update();
        float playerPos = PlayerMovement.Instance.transform.position.x;
        if (transform.position.y > 4 || transform.position.y < -4)
        {
            speedY *= -1;
        }
        if (transform.position.x > 7.5)
        {
            EnterIdleState();
        } else if(transform.position.x < -7 || transform.position.x<playerPos)
        {
            EnterChargeState();
        }
    }



    private void EnterIdleState()
    {
        if (charging)
        {
            speedX = 0.4f;
            speedY = Random.Range(-1.2f, 1.2f);
            charging = false;
            anim.SetBool("charging", false);
        }
    }

    private void EnterChargeState()
    {
        if (!charging)
        {
            speedX = Random.Range(3.5f, 4f);
            speedY = 0;
            charging = true;
            anim.SetBool("charging", true);
        }
    }
   
}
