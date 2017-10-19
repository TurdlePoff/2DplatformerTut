using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds; //Array of all of the back and fore-grounds to be parallaxed
    private float[] parallaxScales; //The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;     //Parallaxing amount - How smooth the parallax is going to be - Make sure to set this above 0 or the parallax effect will not work

    private Transform cam;          //Reference to the main camera's transform //Faster to store this as a variable + easy to change
    private Vector3 prevCamPos;     //Stores position of the camera in the previous frame

    void Awake() //Called before Start() //Assigns all pre-logic. Great for references.
    {
        //Set up camera reference
        cam = Camera.main.transform;
    }

	// Use this for initialization
	void Start ()
    {
        //The previous frame will now have current frames position
        prevCamPos = cam.position;

        //Assigning corresponding parallax scales
        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //this parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the current position + the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the backgrounds current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and target position using lerp
            parallaxScales[i] = backgrounds[i].position.z * -1;

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        //Set prevcampos to camera's position at the end of the frame
        prevCamPos = cam.position;
    }
}
