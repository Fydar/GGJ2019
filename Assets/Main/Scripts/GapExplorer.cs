﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapExplorer : MonoBehaviour
{
    [SerializeField]
    Gap[] gaps;

    protected Gap currentGap;
    protected Vector3 currentPos;

    void MoveLeft(float amount)
    {
        if(!currentGap)
            return;

        currentGap.Move(-amount);       
    }

    void MoveRight(float amount)
    {
        if(!currentGap)
            return;
        
        currentGap.Move(amount);        
    }
    
    public Vector3 GetHandPosWorldSpace()
    {
        return currentPos;
    }

    public void SelectNearestGap(Vector3 pos)
    {
        Gap nearest = null;
        float nearestDistSqr = float.MaxValue;
        for(int i=0;i<gaps.Length;i++){
            var gapPos = gaps[i].GetNearest(pos);
            var nearPos = -gaps[i].GetNearest(pos);
            var distSqr = (pos-nearPos).sqrMagnitude;
            if( distSqr < nearestDistSqr ){
                nearestDistSqr = distSqr;
                nearest = gaps[i];
                currentPos = nearPos;
            }
        }

    }

}