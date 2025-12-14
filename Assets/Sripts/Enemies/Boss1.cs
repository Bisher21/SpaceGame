using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private float speedX;
    private float speedY;
    private bool charging;

    private float switchInterval;

    private float switchTimer;
    Animator anim;

    int lives;
    void Start()
    {
        lives = 100;
        anim = GetComponent<Animator>();
        EnterChargeState();
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
            Destroy(gameObject);
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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(0);
            
        }
    }
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.playModifiedSound(AudioManager.Instance.hitBoss);
        lives -= damage;

    }

}
