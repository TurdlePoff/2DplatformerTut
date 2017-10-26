using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float fireRate = 5f; //rate of fire. 0 = single burst, 1+ = multi
    public float Damage = 10f;

    public LayerMask whatToHit; //Tells us what we want to hit //raycast is a layer of objects which we want to hit
    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;

    float timeToSpawnEffect = 0f;
    public float effectSpawnRate = 10f;

    float timeToFire = 0f;
    Transform firePoint;

    // Use this for initialization
    void Awake ()
    {
        firePoint = transform.Find("FirePoint");
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
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
    }
    //IEnumerator Effect()
    void Effect()
    {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}
