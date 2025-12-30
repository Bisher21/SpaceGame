using UnityEngine;
using System.Collections.Generic;
public class LocusMorph : Enemy
{
    [SerializeField] private List<Frames> locusFrames;
    private int enemyVarient;
    private bool charging;

    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprites;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        enemyVarient = Random.Range(0, locusFrames.Count);
        EnterIdle();
    }
    public override void Start()
    {
        base.Start();

        desrtoyEffectPool = GameObject.Find("LocusPopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.locusHit;
        deathSound = AudioManager.Instance.locusDeath;
        speedX = Random.Range(0.1f, 0.3f);
        speedY = Random.Range(-0.9f, 0.9f);
    }


    public override void Update()
    {
        base.Update();
        if (transform.position.y > 5 || transform.position.y < -5)
        {
            speedY *= -1;
        }
    }
    private void EnterIdle()
    {
        charging = false;
        sr.sprite = locusFrames[enemyVarient].sprites[0];
        speedX = Random.Range(0.1f, 0.3f);
        speedY = Random.Range(-0.9f, 0.9f);

    }

    private void EnterCharge()
    {
        if (!charging)
        {
            charging = true;
            sr.sprite = locusFrames[enemyVarient].sprites[1];
            AudioManager.Instance.playSound(AudioManager.Instance.locusCharge);
            speedX = Random.Range(-4f, -6f);
            speedY = 0;
        }
       
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(lives<= maxLives * 0.5f)
        {
            EnterCharge();
        }
    }
}

 
