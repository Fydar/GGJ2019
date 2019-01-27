﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineView : AbstractView
{
    public Item currentItem;

    [SerializeField]
    DialogManager dialogManager;

    SearchView searchView;

    public void Awake(){
        searchView = GetComponent<SearchView>();
        dialogManager.OnFinished += OnDialogComplete;
    }

    private void OnDialogComplete()
    {
        searchView.Begin();
        End();
    }

    public override void Begin()
    {
        if(!currentItem){
            searchView.Begin();
            End();
        }
        currentItem.hasBeenFound = true;
        dialogManager.Play(currentItem.dialog);
    }

    public override void End()
    {
        
    }

    
}
