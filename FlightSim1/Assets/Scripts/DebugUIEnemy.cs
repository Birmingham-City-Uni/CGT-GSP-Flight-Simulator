using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIEnemy : MonoBehaviour
{

    public GameObject Enemy;
    public GameObject Player;
    public GameObject StateTextObject;
    public Text text;
    public Text StateText;
    public float distance;

    void Start() // Start is called once before the first execution of Update after the MonoBehaviour is created
    {
        if (Enemy == null)
        {
            Enemy = GameObject.FindWithTag("Enemy");
        }

        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }

        if (StateTextObject == null)
        {
            StateTextObject = GameObject.FindWithTag("DebugText2");
        }

        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Enemy.transform.position, Player.transform.position);
        text.text = "Distance between enemy and player: " + distance;
        StateText = StateTextObject.GetComponent<Text>();
        StateText.text = "Enemy State: " + Enemy.GetComponent<EnemyAI>().currentState;
    }
}
