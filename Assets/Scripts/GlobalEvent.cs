using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class GlobalEvent 
{
    public static Action OnDamage;
    public static void InvokeOnDamage()
    {
        InvokeOnResetPlayerPosition();
        OnDamage?.Invoke();
    }


    public static Action OnResetPlayerPosition;

    public static void InvokeOnResetPlayerPosition()
    {
        OnResetPlayerPosition?.Invoke();
    }

    public static Action<CheckpointTrigger> OnReachCheckpoint;
    public static Action OnChangeCheckpoint;

    public static void InvokeReachCheckpoint(CheckpointTrigger c)
    {
        OnChangeCheckpoint?.Invoke();
        OnReachCheckpoint?.Invoke(c);
    }


    public static Action OnCoinCollection;
    public static void InvokeOnCoinCollect()
    {
        OnCoinCollection?.Invoke();
    }

    public static Action OnCoinDecrease;
    public static void InvokeOnCoinDecrease()
    {
        OnCoinDecrease?.Invoke();
    }

}
