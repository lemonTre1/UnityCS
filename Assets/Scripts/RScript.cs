using UnityEngine;
using System.Collections;

public class RScript : MonoBehaviour {

	public GameObject lightEffect;
    float DownSpeed;
	// Use this for initialization
	void Start () {
		StartCoroutine(LightEffects());
		Destroy(gameObject,5f);
	}


    void Update() {
		
    }


	IEnumerator LightEffects(){
		while(true)
		{
			yield return new  WaitForSeconds(0.01f);
			Vector3 pos=new Vector3(Random.Range(-20,20),6,Random.Range(-10,10));
			GameObject light=Instantiate(lightEffect,Vector3.zero,Quaternion.identity) as GameObject;

			light.transform.SetParent(gameObject.transform);
			light.transform.localPosition=pos;
			light.transform.position=new Vector3(light.transform.position.x,6,light.transform.position.z);
		}
	
	}
		
}
