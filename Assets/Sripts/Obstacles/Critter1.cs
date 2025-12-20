using TMPro;
using UnityEngine;

public class Critter1 : MonoBehaviour
{
    
    [SerializeField] Sprite[] sprites;
    ObjectPooler getZappedEffectPool;
    ObjectPooler burnEffectPool;
    SpriteRenderer sr;
    float moveSpeed;
    Vector3 targetPosition;
    Quaternion targetRotation;


    private float moveTimer;
    private float moveInterval;
    void Start()
    {
        burnEffectPool=GameObject.Find("CritterBurnPool").GetComponent<ObjectPooler>();
        getZappedEffectPool = GameObject.Find("CritterZapPool").GetComponent<ObjectPooler>();
        sr =GetComponent<SpriteRenderer>();
        sr.sprite=sprites[Random.Range(0,sprites.Length)];
        moveSpeed=Random.Range(0.5f,3.0f);
        GenerateRandomPosition();
        moveInterval= Random.Range(0.1f, 3.0f);
        moveTimer = moveInterval;


    }

    
    void Update()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(0.3f, 2.0f);
            moveTimer = moveInterval;
        }
        targetPosition -= new Vector3(GameManager.Instance.worldSpeed*Time.deltaTime, 0);
        transform.position = Vector3.MoveTowards(transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime);
        Vector3 relativepos = targetPosition - transform.position;
        if(relativepos != Vector3.zero) {
            targetRotation= Quaternion.LookRotation(Vector3.forward,relativepos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 *Time.deltaTime);
        }
       

    }

    private void GenerateRandomPosition()
    {
        float randomX=Random.Range(-5f,5f);
        float randomY = Random.Range(-5f, 5f);

        targetPosition=new Vector2(randomX,randomY);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            
            GameObject zapEffect = getZappedEffectPool.GetPoolGameObjects();
            zapEffect.transform.position = transform.position;
            zapEffect.transform.rotation = transform.rotation;
            zapEffect.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.Instance.playModifiedSound(AudioManager.Instance.hit);
            GameManager.Instance.crittierCount++;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {

            
            GameObject burnEffect = burnEffectPool.GetPoolGameObjects();
            burnEffect.transform.position = transform.position;
            burnEffect.transform.rotation = transform.rotation;
            burnEffect.SetActive(true);
            gameObject.SetActive(false);
            AudioManager.Instance.playModifiedSound(AudioManager.Instance.hit);
            GameManager.Instance.crittierCount++;
        }
    }
}
