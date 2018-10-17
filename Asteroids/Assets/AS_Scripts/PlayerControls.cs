using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : FakePhysics
{

    [SerializeField]
    private Transform GunPosition;

    protected override void DoMove()
    {
        float tThrust = Input.GetAxis("Vertical") * mSpeed;
        float tRotate = Input.GetAxis("Horizontal") * mRotationSpeed;
        transform.Rotate(0, 0, tRotate * mRotationSpeed * Time.deltaTime);
        mVelocity += Quaternion.Euler(0, 0, transform.rotation.z) * transform.up * tThrust * mSpeed;
        mVelocity = ClampedVelocity();
        base.DoMove();
        DoFiring();
    }

   
    void DoFiring()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.CreateBullet(GunPosition.position, transform.up);
        }
    }


}
