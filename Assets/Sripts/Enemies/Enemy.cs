using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int lives;
    [SerializeField] private int maxLives;
    [SerializeField] private int experienceToGive;
    protected SpriteRenderer sr;
    private FlashWhite flash;
    protected ObjectPooler desrtoyEffectPool;
    protected AudioSource hitSound;
    protected AudioSource deathSound;
    protected float speedX=0;
    protected float speedY=0; 
    public virtual void OnEnable()
    {
        lives = maxLives;
    }
    public virtual void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        flash= GetComponent<FlashWhite>();
    }

   
    public virtual void Update()
    {
       transform.position+= new Vector3(speedX*Time.deltaTime, speedY*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player=collision.gameObject.GetComponent<PlayerMovement>();
            if(player) player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        AudioManager.Instance.playModifiedSound(hitSound);
        lives -= damage;

        if (lives > 0)
        {
            flash.Flash();
        }
        else
        {
            AudioManager.Instance.playModifiedSound(deathSound);
            flash.Reset();
            GameObject deathEffect = desrtoyEffectPool.GetPoolGameObjects();
            deathEffect.transform.position = transform.position;
            deathEffect.transform.rotation = transform.rotation;
            deathEffect.transform.localScale = transform.localScale;
            deathEffect.SetActive(true);
            PlayerMovement.Instance.GetExperience(experienceToGive);
            gameObject.SetActive(false);
        }
    }
}
