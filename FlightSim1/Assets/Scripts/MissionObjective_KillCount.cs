using UnityEngine;
using UnityEngine.Events;

public class MissionObjective_KillCount : MonoBehaviour
{
    [Header("Mission Settings")]
    [Tooltip("Number of hostiles required to complete the mission")]
    public int requiredKills = 5;

    [Header("Mission State")]
    public int currentKills = 0;
    public bool missionCompleted = false;

    [Header("Events")]
    public UnityEvent onMissionCompleted;

    private void Start()
    {
        currentKills = 0;
        missionCompleted = false;

        Debug.Log($"Mission Started: Destroy {requiredKills} hostile aircraft.");
    }

    public void RegisterKill()
    {
        if (missionCompleted)
            return;

        currentKills++;
        Debug.Log($"Hostile destroyed ({currentKills}/{requiredKills})");

        if (currentKills >= requiredKills)
        {
            CompleteMission();
        }
    }

    private void CompleteMission()
    {
        missionCompleted = true;
        Debug.Log("Mission Complete: All hostiles neutralized.");

        onMissionCompleted?.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
