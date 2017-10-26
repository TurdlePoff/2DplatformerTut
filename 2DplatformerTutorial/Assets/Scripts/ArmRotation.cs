using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotOffset = 0;

	// Update is called once per frame
	void Update () {
        //Subtract pos of player from mouse pos
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();     //Keep same proportions of xyz but when added together they will == 1

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;   //Find angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotOffset);
	}
}
