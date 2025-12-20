using UnityEngine;
using System.Collections;
public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer sr;


    private Material defaultMaterial;
    private Material whiteMaterial;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
        defaultMaterial = sr.material;

        whiteMaterial = Resources.Load<Material>("Material/Mwhite");
    }

    
    public void Flash()
    {
        sr.material = whiteMaterial;
        StartCoroutine(SwitchTodefault());
    }
    IEnumerator SwitchTodefault()
    {
        yield return new WaitForSeconds(0.2f);
        sr.material = defaultMaterial;
    }
    public void Reset()
    {
        sr.material = defaultMaterial;
    }
}
