using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

	public GameObject bombPrefab;
	Transform endPos;
	// Use this for initialization
	void Start () {
		endPos=gameObject.transform.FindChild("endPos");
		InstantiateBomb();
	}
	
	void InstantiateBomb(){
		for(int i=0;i<5;i++)
		{
			Vector3 pos=new Vector3(20f,0,Random.Range(-20f,20f));
			float power=Random.Range(20f,40f);
			GameObject bomb=Instantiate(bombPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			bomb.transform.SetParent(gameObject.transform);
			bomb.transform.localPosition=pos;
			bomb.GetComponent<Rigidbody>().velocity = Vector3.forward * power;
		}
	
	}
}
