using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private ObjectPooler effectDyingPool;
    private float speedX;
    private float speedY;
    private bool charging;

    private float switchInterval;

    private float switchTimer;
    Animator anim;
    int maxLives=100;
    int lives;
    int damage=20;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {

        EnterChargeState();
        lives = maxLives;
        AudioManager.Instance.playSound(AudioManager.Instance.spawn);
    }
    void Start()
    {
        effectDyingPool = GameObject.Find("Boom3Pool").
        GetComponent<ObjectPooler>();
        lives = maxLives;
       
        
        EnterChargeState();
        AudioManager.Instance.playSound(AudioManager.Instance.spawn);
    }

    
    void Update()
    {
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
        speedY = Random.Range(-2f, 2f);
        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;
        charging= false;
    }
    void EnterChargeState()
    {
        if (!charging)
            AudioManager.Instance.playModifiedSound(AudioManager.Instance.dive);

        anim.SetBool("charging", true);
        speedX = -8f;
        speedY = 0;
        switchInterval = Random.Range(0.6f, 1.3f); ;
        switchTimer = switchInterval;
        charging = true;
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))

        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
                asteroid.TakeDamage(damage);
            
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player)
                player.TakeDamage(damage);

        }

    }
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.playModifiedSound(AudioManager.Instance.hitBoss);
        lives -= damage;
        if(lives <= 0)
        {
            GameObject bossDying = effectDyingPool.GetPoolGameObjects();
            bossDying.transform.position = transform.position;
            bossDying.transform.rotation = transform.rotation;
            bossDying.SetActive(true);
            AudioManager.Instance.playSound(AudioManager.Instance.dead);

            gameObject.SetActive(false);
        }

    }

}
