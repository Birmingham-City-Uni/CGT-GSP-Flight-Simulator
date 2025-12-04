using UnityEngine;
using UnityEngine.UI;

public class ComponentHealthManager : MonoBehaviour
{
    float test = 100;
    [SerializeField]
    private GameObject LWingUI;
    [SerializeField]
    private GameObject RWingUI;
    [SerializeField]
    private GameObject BodyUI;
    [SerializeField]
    private GameObject ThrustersUI;
    [SerializeField]
    private GameObject NoseUI;

    /// <summary>
    /// Update a component of the plane using a string to identify it and int for the health change
    /// </summary>
    /// <param name="componentName">String values for each component are as follows(string:coresponding component)<para /> LWing:left wing<para />RWing:right wing<para /> Body:body<para />Thrusters:thrusters/engines<para />Nose:nose/cockpit</param>
    void updateComponentDamage(string componentName, float health)
    {
        switch (componentName)
        {
            case "LWing":
                LWingUI.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, health / 100);
                break;
            case "RWing":
                RWingUI.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, health / 100);
                break;
            case "Body":
                BodyUI.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, health / 100);
                break;
            case "Thrusters":
                ThrustersUI.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, health / 100);
                break;
            case "Nose":
                NoseUI.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, health / 100);
                break;
        }
    }
}
