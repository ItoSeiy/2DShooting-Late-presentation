using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCommand : MonoBehaviour
{
    [SerializeField, Tooltip("�R�}���h")] 
    int[] answer = default;
    public void OnInputCommand(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            
        }
    }
}
