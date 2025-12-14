using UnityEngine;
using System.Collections;
public class Asteroid : MonoBehaviour
{
    
    [SerializeField] private GameObject ExplosionEffect;
    [SerializeField] private Sprite[] sprites;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private FlashWhite flash;
     private int lives = 5;
    void Start()
    {
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
    }

    
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
        }
        
    }
    public void TakeDamage(int damage)
    {
        AudioManager.Instance.playModifiedSound(AudioManager.Instance.hit);
        flash.Flash();
        lives-= damage;
        if (lives <= 0)
        {
            Instantiate(ExplosionEffect, transform.position, transform.rotation);
            AudioManager.Instance.playSound(AudioManager.Instance.dead);
            Destroy(gameObject);
        }
    }
   
}
