using UnityEngine;

public class DebugPrint : MonoBehaviour
{
    public string debugMessage = "";

    public void PrintDebugMessage()
    {
        PrintDebugMessage(debugMessage);
    }
    
    public void PrintDebugMessage(string msg)
    {
        Debug.Log(msg);
    }
}