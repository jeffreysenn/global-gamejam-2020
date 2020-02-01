using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputID : MonoBehaviour
{
    [SerializeField] int id = 1;

    public enum Action
    {
        H_AXIS,
        JUMP,
        FIRE,
    }

    public string GetActionName(Action action)
    {
        var result = "Player " + id.ToString() + " ";
        switch (action)
        {
            case Action.H_AXIS:
                result += "Horizontal";
                break;
            case Action.JUMP:
                result += "Jump";
                break;
            case Action.FIRE:
                result += "Fire";
                break;
        }
        return result;
    }
}
