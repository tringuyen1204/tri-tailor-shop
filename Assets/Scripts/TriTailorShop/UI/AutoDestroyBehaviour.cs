using System.Collections;
using UnityEngine;

public class AutoDestroyBehaviour : MonoBehaviour
{
    [SerializeField] protected float lifeTime;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}