using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 5f; //rate of fire. 0 = single burst, 1+ = multi
    public float Damage = 10f;

    public LayerMask notToHit; //Tells us what we want to hit //raycast is a layer of objects which we do not want to hit

    float timeToFire = 0f;
    Transform firePoint;

    // Use this for initialization
    void Awake ()
    {
        firePoint = transform.FindChild("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No fire point? WhaAAAAAAAT");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1")) //Input.GetKeyDown(KeyCode.
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) //Input.GetKeyDown(KeyCode.
            {
                timeToFire = Time.time + 1 / fireRate; //Time + delay (nxt time to fire) shoot.
                Shoot();
            }
        }
	}

    void Shoot()
    {
        //Debug.Log("Test");
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                                       Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPos, mousePos - firePointPos, 100, notToHit); //distance is the parameter of the shooting
        Debug.DrawLine(firePointPos, (mousePos - firePointPos) * 100, Color.cyan, 0.1f, true);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPos, hit.point, Color.red, 0.1f, true);
        }

    }
}
