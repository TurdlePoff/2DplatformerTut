using System.Collections;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int moveSpeed = 230;

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed); //Use instead of rigidbody //Local space = from local orientation of objectGlobal space = 0, 0, 0
        Destroy(this.gameObject, 1);
        //Set object pooling so that large prefabs spawned will be limited

    }
}
