using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject bulletPrefab;
	// Use this for initialization
	void Start () {
		InstantiateBullet();
	}
	
	void InstantiateBullet(){
		GameObject bullet=Instantiate(bulletPrefab,Vector3.zero,Quaternion.identity) as GameObject;
		bullet.transform.SetParent(gameObject.transform);
		bullet.transform.localPosition=Vector3.zero;
		bullet.GetComponent<Rigidbody>().velocity = transform.up * 60f;
	}
}
