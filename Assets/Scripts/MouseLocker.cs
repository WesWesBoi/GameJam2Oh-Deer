using System;
using UnityEngine;

public class MouseLocker : MonoBehaviour
{
    public void Lock(bool isLocked)
    {
        LockMouse(isLocked);
    }
    
    public static void LockMouse(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
}