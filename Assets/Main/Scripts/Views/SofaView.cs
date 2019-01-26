﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SofaView : AbstractView
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask whatIsGapSpace;

    [SerializeField]
    private GameObject creaseIndicator;

    private Gap currentGap;


    public override void Begin()
    {
        throw new System.NotImplementedException();
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        creaseIndicator.SetActive(false);

        Ray ray = cam.ScreenPointToRay(PlayerInput.GetMousePos());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsGapSpace))
        {
            if (currentGap == null || currentGap.GetCollider() != hit.collider) {
                if (currentGap != null)
                    currentGap.ShowIndicator(false);

                currentGap = hit.collider.GetComponent<Gap>();

                if (currentGap != null)
                    currentGap.ShowIndicator(true);
            }

            creaseIndicator.SetActive(true);
            SetIndicatorPos(hit.point, hit.collider);
        } else
        {
            if (currentGap != null)
                currentGap.ShowIndicator(false);

            currentGap = null;
        }

    }

    void SetIndicatorPos (Vector3 hitPos, Collider hitCollider)
    {
        creaseIndicator.transform.SetParent(hitCollider.transform);

        Vector3 indPos = Vector3.zero;
        indPos.x = hitCollider.transform.InverseTransformPoint(hitPos).x;

        creaseIndicator.transform.localPosition = indPos;

        creaseIndicator.transform.SetParent(transform);
    }
}