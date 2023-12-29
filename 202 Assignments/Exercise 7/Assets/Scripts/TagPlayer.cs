using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagStates
{
    NotIt,
    Counting,
    It
}

public class TagPlayer : Agent
{
    [SerializeField]
    float wanderTime = 1f;
    [SerializeField]
    float wanderRadius = 1f;
    [SerializeField]
    [Range(1.0f, 100.0f)]
    float boundsWeight = 10.0f;

    [SerializeField]
    TagStates currentStates;

    float countTimer = 0f;

    TagPlayer target;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    float fleeWeight = 1f;
    

    protected override void CalcStearingForces()
    {
        switch(currentStates)
        {
            case TagStates.NotIt:
                totalForce += Wander(wanderTime, wanderRadius);
                totalForce += SeparateTag();
                totalForce += Flee(tagManager.itPlayer.transform.position) * fleeWeight;
                break;

            case TagStates.Counting:
                countTimer += Time.deltaTime;
                if (countTimer >= tagManager.CountTimer)
                {
                    countTimer = 0f;
                    SetState(TagStates.It);
                }
                break;

            case TagStates.It:
                target = FindClosest() as TagPlayer;
                if (target != null)
                {
                    totalForce += Seek(target.transform.position);

                    if (Vector3.Distance(transform.position, target.transform.position) <= myPhysicsObject.Radius + target.myPhysicsObject.Radius) 
                    { 
                        SetState(TagStates.NotIt);
                        target.SetState(TagStates.Counting);
                    }
                }
                
                break;
        }
        totalForce += BoundsForce() * boundsWeight;
    }

    public void SetState(TagStates state)
    {
        if (state == TagStates.Counting)
        {
            this.myPhysicsObject.StopMoving();
            tagManager.itPlayer = this;
        }
        currentStates = state;
        spriteRenderer.sprite = tagManager.tagSprites[(int)currentStates];
    }
}
