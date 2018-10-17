using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is going to be an abstract ones, since its whole purpose is to be inhereted by other classes
public abstract class FakePhysics : MonoBehaviour
{

    protected Rigidbody2D mSB2; //It's protected because we want derived classes to be able to access to this, but not other ones

    protected SpriteRenderer mSR2;

    protected Collider2D mBC2; 

    [SerializeField]
    protected float mSpeed = 1.0f;


    [SerializeField]
    protected float mMaxSpeed;

    [SerializeField]
    protected float mRotationSpeed = 360.0f;

    protected Vector3 mVelocity = Vector3.zero;


    // Use this for initialization
    protected virtual void Start()
    {

        mSR2 = gameObject.GetComponent<SpriteRenderer>();
        Debug.Assert(mSR2 != null, "Error: Missing SpriteRenderer");

        mBC2 = gameObject.GetComponent<Collider2D>();
        mBC2.isTrigger = true;

        mSB2 = gameObject.AddComponent<Rigidbody2D>();
        mSB2.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();

        Vector3 tNewPosition;
        if (DoWrap(out tNewPosition))
        {
            transform.position = tNewPosition;
        }
    }

    protected virtual void DoMove()
    {
        transform.position += mVelocity * Time.deltaTime;  //Work out new position
    }

    protected Vector3 ClampedVelocity()
    {
        if(mVelocity.magnitude > mMaxSpeed)
        {
            mVelocity = mVelocity.normalized * mMaxSpeed;
        }

        return mVelocity;
    }

    bool DoWrap(out Vector3 vNewPosition)
    {
        float tHeight = Camera.main.orthographicSize; // figures out what the camera can see
        float tWidth = Camera.main.aspect * tHeight; //Uses aspect ratio to work out width
        bool tMoved = false;
        vNewPosition = transform.position;

        if (vNewPosition.x > tWidth)
        {
            vNewPosition.x = -vNewPosition.x;
            tMoved = true;
        }

        else if (vNewPosition.x < -tWidth)
        {

            vNewPosition.x = -transform.position.x;
            tMoved = true;
        }

        if (vNewPosition.y > tHeight)
        { vNewPosition.y = -vNewPosition.y; tMoved = true; }

        else if (vNewPosition.y < -tHeight)
        { vNewPosition.y = -transform.position.y; tMoved = true; }
        return tMoved;
    }

    protected virtual void ObjectHit(FakePhysics vOtherObject)
    {
        Debug.LogFormat("{0} hit by {1}", name, vOtherObject.name);
    }
    private void OnTriggerEnter2D(Collider2D vCollision)
    {
        FakePhysics tOtherObject = vCollision.gameObject.GetComponent<FakePhysics>();
        Debug.Assert(tOtherObject != null, "Other Object is not FakePhysics compatible!");
        ObjectHit(tOtherObject);
    }
}
