using UnityEngine;
using TMPro;

public class MissionObjectiveUI : MonoBehaviour
{
    [Header("References")]
    public MissionObjective_KillCount missionObjective;
    public TMP_Text objectiveText;
    public TMP_Text missionCompleteText;

    private void Start()
    {
        if (missionCompleteText != null)
            missionCompleteText.gameObject.SetActive(false);

        UpdateObjectiveText();
    }

    private void Update()
    {
        if (missionObjective == null)
            return;

        UpdateObjectiveText();

        if (missionObjective.missionCompleted)
        {
            ShowMissionComplete();
        }
    }

    private void UpdateObjectiveText()
    {
        objectiveText.text =
            $"Destroy Hostile Aircraft: {missionObjective.currentKills} / {missionObjective.requiredKills}";
    }

    private void ShowMissionComplete()
    {
        objectiveText.gameObject.SetActive(false);

        if (missionCompleteText != null)
        {
            missionCompleteText.gameObject.SetActive(true);
            missionCompleteText.text = "MISSION COMPLETE";
        }
    }
}