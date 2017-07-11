using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
	public float BreakForce = 5f;
	public GameObject Loot;
	public float LootTime = Mathf.Infinity;

	void OnCollisionEnter(Collision collision)
  	{
		if(collision.relativeVelocity.magnitude >= BreakForce) {
			if(Loot != null){
				GameObject clone = Instantiate(Loot, transform.position, transform.rotation) as GameObject;
				Destroy(clone, LootTime);
			}

			Destroy(gameObject);
		}
   }
}
