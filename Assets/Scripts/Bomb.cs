using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {




    bool isBoom = false;
	// Use this for initialization
	void Start () {
		Invoke("Boom",2f);
	}


    void OnTriggerEnter(Collider collider) {
        if (collider.tag.Equals("AI")) {
            Destroy(collider);
			Boom();
        }
    }


    void Boom() {
      if(!isBoom)
          StartCoroutine(BombBoom());
    }


    IEnumerator BombBoom()
    {
        isBoom = true;
        
        float Maxtime = 1.5f;
		float Mintime = 0.5f;
		GetComponent<Rigidbody>().useGravity=false;
		GetComponent<Rigidbody>().velocity=Vector3.zero;
		for (float i = 0; i <= Maxtime; i += Time.deltaTime)
        {
            transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
