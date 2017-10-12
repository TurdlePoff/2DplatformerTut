using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

    public int offsetX = 2;             //Offset so we dont get slow buddy inst. errors

    public bool hasARightBuddy = false; //Used to check if instantiating is needed
    public bool hasALeftBuddy = false;

    public bool reverseScale = false;   //Used if object is not tilable

    private float spriteWidth = 0f;     //width of element
    private Camera cam;
    private Transform myTransform;

    void Awake() //Called before Start() //Assigns all pre-logic. Great for references.
    {
        //Set up camera reference
        cam = Camera.main;
        myTransform = transform;
    }

    // Use this for initialization
    void Start ()
    {

        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!hasALeftBuddy || !hasARightBuddy) //Checking if elements still need buddies, else do nothing
        {
            //Calc the camera's extent (half the width of what the camera can see [world coords])
            float camHorExtend = cam.orthographicSize * Screen.width / Screen.height;

            //Calc the x position where the camera can see the edge of the sprite/element
            float edgeVisiblePosRight = (myTransform.position.x + spriteWidth / 2) - camHorExtend;
            float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth / 2) + camHorExtend;

            //if camera edge pos is >= a then - offset and the right buddy has not been instantiated
            //Then call make new buddy if we can
            if(cam.transform.position.x >= edgeVisiblePosRight - offsetX && !hasARightBuddy)
            {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePosLeft + offsetX && !hasALeftBuddy)
            {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    /**
     * Function that creates a buddy on the side required
     */
    void MakeNewBuddy(int rightOrLeft) //-1 or 1 (flip on edge)
    {
        //calculating position for new buffy
        Vector3 newPos = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        //Instantiating new buddy and storing it in a variable
        Transform newBuddy = (Transform)Instantiate(myTransform, newPos, myTransform.rotation);

        //If not tilable, reverse size of object to get rid of ugly seems
        if(reverseScale)
        {
            newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
            
        newBuddy.parent = myTransform.parent; //   ALT CODE
        if(rightOrLeft > 0)
        {
            newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
        }
    }

}
