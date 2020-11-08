using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
	// !! Testing class for shake method
    // Start is called before the first frame update
    void Start()
    {
	//	ShakeNow();
		//StartCoroutine(WTF());
    }

	IEnumerator WTF() {
		yield return new WaitForSeconds(1.0f);
		ShakeNow();
	}

    // Update is called once per frame
	void ShakeNow() {
		float startAmount = 0.1f;
		float amount = startAmount;
		float posX = transform.localPosition.x;
		float time = 0.05f;
		int times = 5;
		OneShakeX(posX, amount, startAmount, times, time);
	}
	void OneShakeX(float startPosX, float amount, float startAmount, int times, float time) {
		LeanTween.moveLocalX(gameObject, startPosX + amount / 2, time).setOnComplete(() => {
			LeanTween.moveLocalX(gameObject, startPosX - amount / 2, time).setOnComplete(() => {
				amount -= startAmount / times;
				if (amount > 0) {
					OneShakeX(startPosX, amount, startAmount, times, time);
				} else {
					LeanTween.moveLocalX(gameObject, startPosX, time / 2);
				}
			});
		});
	
	}
}
