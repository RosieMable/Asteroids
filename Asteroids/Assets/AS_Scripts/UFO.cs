using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : FakePhysics {

    private float mRotation = 0.0f;

    private float mDelay; //States the dealy of every couple of seconds 

    [SerializeField]
    private float mCooldown;
    protected override void Start()
    {
        base.Start();

    }


    protected override void DoMove()
    {

        base.DoMove();

        if (Time.time > mDelay)
        {
            mVelocity = new Vector3(Random.Range(-mSpeed, mSpeed), Random.Range(-mSpeed, mSpeed), 0);
            mDelay += mCooldown;
        }


    }

}
