
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : FakePhysics {

    private float mRotation = 0.0f; //Rotation Speed

    //Use this for initialization, to override tha base class Start

    protected override void Start()
    {
        base.Start(); //Call Fakephysics Start()

        //Taking the variables from the template that is our base class, we are creating a "new" value for those variables (it creates a new allocation in the computer memory) 
        //New we can do our own Rock specific init
        mVelocity = new Vector3(Random.Range(-mSpeed, mSpeed)
                               , Random.Range(-mSpeed, mSpeed)
                               , 0);
        mRotation = Random.Range(-mRotationSpeed, mRotationSpeed);
    }

    protected override void DoMove()
    {
        transform.Rotate(0, 0, mRotation * Time.deltaTime); //rotate the rock
        base.DoMove();
    }

}
	
