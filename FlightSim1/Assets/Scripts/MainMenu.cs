using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject MainContainer;
    [SerializeField]
    GameObject OptionContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainContainer = transform.GetChild(0).gameObject;
        OptionContainer = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton()
    {
        SceneManager.LoadScene(1);
    }

    public void optionButton()
    {
        OptionContainer.SetActive(true);
        MainContainer.SetActive(false);
    }

    public void backOptionButton()
    {
        OptionContainer.SetActive(false);
        MainContainer.SetActive(true);
    }

    public void quitButton()
    {
        Application.Quit();
    }
}
