using UnityEngine;

public class PhaserWeapon : MonoBehaviour
{
    public static PhaserWeapon Instance;
    [SerializeField] private ObjectPooler bulletpooler;

    public float speed;
    public int damage;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

    }

    public void Shoot()
    {
        
       GameObject bullet= bulletpooler.GetPoolGameObjects();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        AudioManager.Instance.playSound(AudioManager.Instance.shoot);
    }

}