using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InteractableObject))]
public class Stackable : MonoBehaviour
{
    public int stackCount = 1;
    public UnityEvent OnStackDepleted = new();

    public void AddStacks(int stacks)
    {
        stackCount += stacks;
    }

    public void RemoveStacks(int stacks)
    {
        if (stacks > stackCount)
            stackCount = 0;
        else
            stackCount -= stacks;
        
        if(stackCount == 0)
            OnStackDepleted.Invoke();
    }
}