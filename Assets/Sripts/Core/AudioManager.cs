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
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);  // صححها إلى gameObject (بـ g صغيرة)
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ⬅ اختياري: إذا أردت بقاءه بين المشاهد
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
