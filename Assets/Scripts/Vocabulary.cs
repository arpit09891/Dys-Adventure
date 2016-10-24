using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;

public class Vocabulary : MonoBehaviour {

    // Use this for initialization
    GameObject[] words;
    static int ChoiceCounter = 0;
    InputField Container;
    
    public Text output;
    public int vocabCounter;
    void Awake()
    {
        //if(ChoiceCounter == 0)
        vocabCounter = PlayerPrefs.GetInt("counter");
        InitVocab();
    }
        

   public void InitVocab()
    {
        words = new GameObject[Questionnaire.Instance.numberOfQuestions*4];
       
        List<string> distinctChoices = new List<string>();
        distinctChoices = Questionnaire.Instance.text_choices.Distinct().ToList();
        
        distinctChoices.ForEach(dc => Debug.Log(dc));
        for (int i = 0; i < distinctChoices.Count; i++)
        {
            string gameObjectName = "Word (" + (i + 1) + ")";
            words[i] = GameObject.Find(gameObjectName);
            words[i].GetComponentInChildren<Text>().text = distinctChoices[i];
            for(int qcounter=0; qcounter< Questionnaire.Instance.numberOfQuestions; qcounter++)
            {
                for (int cCounter = 0; cCounter< Questionnaire.Instance.numberOfChoices[qcounter];cCounter++)
                {
                    if (Questionnaire.Instance.questions[qcounter].choices[cCounter].figuredChoice != null && distinctChoices[i].Equals(Questionnaire.Instance.questions[qcounter].choices[cCounter].choice))
                    {
                        words[i].GetComponentInChildren<InputField>().text = Questionnaire.Instance.questions[qcounter].choices[cCounter].figuredChoice;
                        break;
                    }
                }
            }
        }
        //for (int i = 0; i < Questionnaire.Instance.numberOfQuestions; i++)
        //{
        //    //Debug.Log("Init Vocab :" + Questionnaire.Instance.numberOfChoices[i]);
        //    for (int j = 0; j < Questionnaire.Instance.numberOfChoices[i]; j++)
        //    {

       

        //        //WCounter = j + (i * 4);
        //        string gameObjectName = "Word (" + (WCounter + 1) + ")";
        //        //Debug.Log("Init Vocab :" + gameObjectName);
        //        words[WCounter] = GameObject.Find(gameObjectName);
        //        words[WCounter].GetComponentInChildren<Text>().text = Questionnaire.Instance.questions[i].choices[j].choice;
        //        Questionnaire.Instance.questions[i].choices[j].fc = gameObjectName;
        //        if (Questionnaire.Instance.questions[i].choices[j].figuredChoice != null)
        //        {
        //            words[WCounter].GetComponentInChildren<InputField>().text = Questionnaire.Instance.questions[i].choices[j].figuredChoice;
        //        }
        //        WCounter++;
        //        //Debug.Log("Inside Init Vocab" + Questionnaire.Instance.questions[i].choices[j].fc);

        //    }

        //}
        //ChoiceCounter++;
    }

    public void FiguredChoices(GameObject GO)
    {
        Questionnaire.Instance = GameObject.Find("QuestionManger").GetComponent<Questionnaire>();
        string temp = GO.GetComponentInChildren<Text>().text;
        Container = GO.GetComponentInChildren<InputField>();
              
        for (int i = 0; i < Questionnaire.Instance.numberOfQuestions; i++)
        {
            for (int j = 0; j < Questionnaire.Instance.numberOfChoices[i]; j++)
            {
                string SNo = temp[0].ToString() + temp[1].ToString() + " " ;

                if (temp.Equals(Questionnaire.Instance.questions[i].choices[j].choice))
                {
                    SNo = SNo + Container.text.ToString();
                    Questionnaire.Instance.questions[i].choices[j].figuredChoice = SNo;                  
                }
            }
        }

       
    }
    public void ClickedBack(){
        vocabCounter = PlayerPrefs.GetInt("counter");
       
        int counter2 = Questionnaire.Instance.questions[vocabCounter].numberOfChoices;
        if (counter2 == 2)
        {       
            SceneManager.LoadScene("Gameplay2Choices");
        }
        else if (counter2 == 3)
        {
            SceneManager.LoadScene("Gameplay3Choices");
        }
        else if (counter2 == 4)
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if (counter2 == 0)
        {
            SceneManager.LoadScene("Gameplay0Choices");
        }
        else if (counter2 == 1)
        {
            SceneManager.LoadScene("Gameplay1Choice");
        }
        else
        {
            Debug.LogError("WE FUCKED UP (NO MATCH FOR SCENE)");
        }
    }
    
}

