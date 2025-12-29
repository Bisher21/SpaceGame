using UnityEngine;
using System.Collections;
public class Asteroid : MonoBehaviour
{
    
    private ObjectPooler ExplosionEffectPool;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private FlashWhite flash;
    private int maxLives=5;
    private int lives ;
    private int damage=1 ;
    private int experienceToGive=1;
    float pushX;
    float pushY;

    private void OnEnable()
    {
        lives = maxLives;
        transform.rotation= Quaternion.identity;
        pushX = Random.Range(-1f, 0);
        pushY = Random.Range(-1f, 1f);
        if(rb)
            rb.linearVelocity = new Vector2(pushX, pushY);
    }
    void Start()
    {
        ExplosionEffectPool = GameObject.Find("Boom2Pool").
        GetComponent<ObjectPooler>();
        sr = GetComponent<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
        flash= GetComponent<FlashWhite>();
        sr.sprite= sprites[Random.Range(0,
            sprites.Length)];
        pushX = Random.Range(-1f, 0);
        pushY = Random.Range(-1f, 1f);
        rb.linearVelocity=new Vector2 (pushX, pushY);
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale=new Vector2 (randomScale, randomScale);
        ExplosionEffectPool.transform.localScale=transform.localScale;
       
        lives = maxLives;
        
    }

    
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player)
                player.TakeDamage(damage);
        }
       
        
    }
    public void TakeDamage(int damage,bool isGetingExp)
    {
        AudioManager.Instance.playModifiedSound(AudioManager.Instance.hit);
        
        lives-= damage;
        if (lives > 0) { 
            flash.Flash();
        }
        else 
        {
            
            GameObject explosionEffect = ExplosionEffectPool.GetPoolGameObjects();
            explosionEffect.transform.position = transform.position;
            explosionEffect.transform.rotation = transform.rotation;
            explosionEffect.transform.localScale = transform.localScale;
            explosionEffect.SetActive(true);
            AudioManager.Instance.playSound(AudioManager.Instance.asteroidExplosion);
            flash.Reset();
            gameObject.SetActive(false);
            if(isGetingExp)
                PlayerMovement.Instance.GetExperience(experienceToGive);

        }
    }
   
}
