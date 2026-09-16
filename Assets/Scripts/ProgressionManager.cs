using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProgressionManager : MonoBehaviour
{
    public GarbageCollector garbageCollector;
    public List<Milestone> milestones = new();
    public int currentMilestoneIndex;

    [System.Serializable]
    public class Milestone
    {
        public int threshold;
        public UnityEvent OnMilestoneReached = new();
    }

    public void UpdateCollected(int amount)
    {
        if (currentMilestoneIndex >= milestones.Count)
            return;
        
        Milestone currentMilestone = milestones[currentMilestoneIndex];
        if (amount >= currentMilestone.threshold)
        {
            currentMilestoneIndex++;
            currentMilestone.OnMilestoneReached.Invoke();
        }
    }
}
