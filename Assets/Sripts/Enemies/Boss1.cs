using UnityEngine;

public class Boss1 : Enemy
{
    
  
    private bool charging;

    private float switchInterval;

    private float switchTimer;
    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public override void OnEnable()
    {
        base.OnEnable();

        EnterChargeState();
        
        AudioManager.Instance.playSound(AudioManager.Instance.spawn);
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
        if (switchTimer > 0)
        {
            switchTimer-= Time.deltaTime;

        }
        else
        {
            if (charging && transform.position.x> playerPos)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }
        }
        if (transform.position.y > 3 || transform.position.y < -3)
        {
            speedY *= -1;

        }else if(transform.position.x < playerPos)
        {
            EnterChargeState();
        }
        bool boost = PlayerMovement.Instance.isBoosting;
        float moveX;
        if (boost && !charging)
        {
            moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f;
        }
        else
        {
            moveX = speedX * Time.deltaTime;

        }
            
        float moveY= speedY*Time.deltaTime;
        transform.position += new Vector3(moveX, moveY);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
    void EnterPatrolState()
    {
        anim.SetBool("charging", false);
        
        speedX = 0;
        speedY = Random.Range(-1f, 1f);
        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;
        charging= false;
    }
    void EnterChargeState()
    {
        if (!charging)
            AudioManager.Instance.playModifiedSound(AudioManager.Instance.dive);

        anim.SetBool("charging", true);
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(0.6f, 1.3f); ;
        switchTimer = switchInterval;
        charging = true;
        

    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Obstacle"))

        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
                asteroid.TakeDamage(damage,false);
            
        }
        

    }
   

}
