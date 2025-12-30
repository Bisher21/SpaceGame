using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;


    public AudioSource dead;
    public AudioSource hit;
    public AudioSource pauseAndUnpause;
    public AudioSource turbo;
    public AudioSource shoot;
    public AudioSource asteroidExplosion;
    public AudioSource hitBoss;
    public AudioSource dive;
    public AudioSource spawn;
    public AudioSource beetleHit;
    public AudioSource beetleDeath;
    public AudioSource locusHit;
    public AudioSource locusDeath;
    public AudioSource locusCharge;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);  
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void playSound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
    public void playModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
