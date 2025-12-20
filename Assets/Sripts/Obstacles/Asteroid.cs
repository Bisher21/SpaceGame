using UnityEngine;
using System.Collections;
public class Asteroid : MonoBehaviour
{
    
    private ObjectPooler ExplosionEffectPool;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private FlashWhite flash;
    private int maxLives;
    private int lives ;
    private int damage ;

    private void OnEnable()
    {
        lives = maxLives;
        transform.rotation= Quaternion.identity;
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
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);
        rb.linearVelocity=new Vector2 (pushX, pushY);
        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale=new Vector2 (randomScale, randomScale);
        ExplosionEffectPool.transform.localScale=transform.localScale;
        maxLives = 5;
        lives = maxLives;
        damage = 1;
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
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.playModifiedSound(AudioManager.Instance.hit);
        flash.Flash();
        lives-= damage;
        if (lives <= 0)
        {
            
            GameObject explosionEffect = ExplosionEffectPool.GetPoolGameObjects();
            explosionEffect.transform.position = transform.position;
            explosionEffect.transform.rotation = transform.rotation;
            explosionEffect.transform.localScale = transform.localScale;
            explosionEffect.SetActive(true);
            AudioManager.Instance.playSound(AudioManager.Instance.asteroidExplosion);
            flash.Reset();
            gameObject.SetActive(false);
        }
    }
   
}
