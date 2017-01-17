using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour{



    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 2f);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("AI"))
            Destroy(gameObject);

        if (collider.tag.Equals("Terrain"))
            Destroy(gameObject);
    }

}
