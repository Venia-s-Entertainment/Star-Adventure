using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunShadowCaster : MonoBehaviour {
	[SerializeField] Transform track;


	void LateUpdate () {
		if (track) {
			transform.LookAt (track.position);
		}
	}
}