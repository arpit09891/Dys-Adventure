using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GoobleDiGookController : MonoBehaviour {

    public void ClickedStart()
    {
        //Debug.Log(Questionnaire.Instance.questions[0].numberOfChoices);
        if (Questionnaire.Instance.questions[0].numberOfChoices == 2)
            SceneManager.LoadScene("Gameplay2Choices");
        else if (Questionnaire.Instance.questions[0].numberOfChoices == 3)
            SceneManager.LoadScene("Gameplay3Choices");
        else if (Questionnaire.Instance.questions[0].numberOfChoices == 4)
            SceneManager.LoadScene("Gameplay");
        else if (Questionnaire.Instance.questions[0].numberOfChoices == 0)
            SceneManager.LoadScene("Gameplay0Choices");
        else if (Questionnaire.Instance.questions[0].numberOfChoices == 1)
            SceneManager.LoadScene("Gameplay1Choice");    

    }

    public void ClickedCredits()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickedBack()
    {
        SceneManager.LoadScene("StartGame");
    }
}
