using UnityEngine;

public class BeetleMorph : Enemy
{
    [SerializeField]
    private Sprite[] sprites;
    float timer;
    float frequency;
    float amplitude;
    float centerY;
    public override void OnEnable()
    {
        base.OnEnable();
        timer = transform.position.y;
        frequency = Random.Range(0.3f, 1f);
        
        amplitude = Random.Range(0.8f, 1.5f); 
        centerY=transform.position.y;   
    }
    public override void Start()
    {
        base.Start();
        sr.sprite= sprites[Random.Range(0,sprites.Length)];
        desrtoyEffectPool =GameObject.Find("BeetlePobPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.beetleHit;
        deathSound = AudioManager.Instance.beetleDeath;
        speedX = Random.Range(-0.8f, -1.6f);

    }
    public override void Update()
    {
        
        base.Update();
        timer-= Time.deltaTime;
        float sine = Mathf.Sin(timer*frequency)*amplitude;
        transform.position = new Vector3(transform.position.x, centerY+sine);
    }


}
