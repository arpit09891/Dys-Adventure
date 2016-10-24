using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Decision : MonoBehaviour
{
    // Use this for initialization


    GameObject[] choice;
    GameObject questionScreen;
    GameObject previousQuest;
    public static int counter = 0;
    int updateCounter = 0;
    
    void Awake()
    {
        PlayerPrefs.SetInt("counter", counter);
        LoadQuestion();
                
    }

    void LoadQuestion()
    {        
        questionScreen = GameObject.Find("Questions");
        choice = new GameObject[4];
        previousQuest = GameObject.Find("PreviousQuest");
        for (int i = 0; i < Questionnaire.Instance.numberOfChoices[counter]; i++)
        {
            string gameObjectName = "Choice" + (i + 1);
            choice[i] = GameObject.Find(gameObjectName);
            if (Questionnaire.Instance.questions[counter].choices[i].choice != null)
            {
                if (Questionnaire.Instance.questions[counter].choices[i].figuredChoice == null || Questionnaire.Instance.questions[counter].choices[i].figuredChoice == "" || Questionnaire.Instance.questions[counter].choices[i].figuredChoice == " ")
                {                    
                    choice[i].GetComponentInChildren<Text>().text = Questionnaire.Instance.questions[counter].choices[i].choice;
                }
                else
                {
                    choice[i].GetComponentInChildren<Text>().text = Questionnaire.Instance.questions[counter].choices[i].figuredChoice;

                    choice[i].GetComponentInChildren<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
                    //Resources.GetBuiltinResource<Font>("Arial.ttf") 
                }
            }
        }
        questionScreen.GetComponentInChildren<Text>().text = Questionnaire.Instance.questions[counter].question;
        if (Questionnaire.Instance.questions[counter].previousQuest != null)
            previousQuest.GetComponent<Text>().text = Questionnaire.Instance.questions[counter].previousQuest;
           
    }

    void LoadScene()
    {        
        if (Questionnaire.Instance.questions[counter].numberOfChoices == 2)
        {           
            SceneManager.LoadScene("Gameplay2Choices");
        }
        else if (Questionnaire.Instance.questions[counter].numberOfChoices == 3)
        {
            SceneManager.LoadScene("Gameplay3Choices");
        }
        else if (Questionnaire.Instance.questions[counter].numberOfChoices == 4)
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if (Questionnaire.Instance.questions[counter].numberOfChoices == 0)
        {
            SceneManager.LoadScene("Gameplay0Choices");
        }
        else if (Questionnaire.Instance.questions[counter].numberOfChoices == 1)
        {
            SceneManager.LoadScene("Gameplay1Choice");
        }
        else {
            Debug.LogError("WE FUCKED UP (NO MATCH FOR SCENE)");
        }
    }

    public void ClickedInput(GameObject choice)
    {
       
        string name = choice.name;
        char last = name[name.Length - 1];
        
        int lastNo = (int)char.GetNumericValue(last);
       
        lastNo = lastNo - 1;
        int temp = counter;
        counter = Questionnaire.Instance.questions[counter].choices[lastNo].linker;
        Questionnaire.Instance.questions[counter].previousQuest = Questionnaire.Instance.questions[temp].question;
        
        LoadScene();
        //for (int j=0; j< Questionnaire.Instance.questions[counter].numberOfChoices; j++ )
        //{
        //    if (Questionnaire.Instance.questions[counter].choices[j].figuredChoice == null) { }
        //    if (choice.GetComponentInChildren<Text>().text == Questionnaire.Instance.questions[counter].choices[j].choice)
        //    {
        //        counter = Questionnaire.Instance.questions[counter].choices[j].linker;
        //        PlayerPrefs.SetInt("counter", counter);
        //        Debug.Log("Counter: " + counter);
        //        LoadScene();
        //        return;
        //    }
            
        //}
    }
    public void JumpingQuestion()
    {
        questionScreen = GameObject.Find("Questions");
        string question = Questionnaire.Instance.questions[counter].question;
        string jumpedquest = jumpingLetters(question);
        questionScreen.GetComponentInChildren<Text>().text = jumpedquest;
    }

    public void JumpingPreviousQuestion()
    {
        if (Questionnaire.Instance.questions[counter].previousQuest != null)
        {
            previousQuest = GameObject.Find("PreviousQuest");
            string question = Questionnaire.Instance.questions[counter].previousQuest;
            string jumpedquest = jumpingLetters(question);
            previousQuest.GetComponentInChildren<Text>().text = jumpedquest;
        }
    }

    public string jumpingLetters(string question)
    {
        //questionScreen = GameObject.Find("Questions");
        //string question = Questionnaire.Instance.questions[counter].question;
        string jumpedQuestion = "";
        
        List<string> cutUp = question.Split(' ').ToList();
        cutUp.RemoveAll(str => str == " " || str == "");
        List<int> randomList;
        int number = 4;
        System.Random a = new System.Random();
        for(int i=0; i< cutUp.Count; i++)
        {
            string word = cutUp[i];
            randomList = new List<int>();
          
            int length = word.Length;
            if (word[word.Length - 1] == ',' || word[word.Length - 1] == '.')
                length--;


            if (length > 3)
            {
                while (randomList.Count != length - 2)
                {
                    number = a.Next(1, length -1);

                    if (!randomList.Contains(number))
                        randomList.Add(number);
                    
                }
               
                string temp = "";
                temp = temp + word[0];

                for (int j =0; j < randomList.Count; j++)
                {
                    temp += word[randomList[j]];
                }
               
                temp = temp + word[length -1];
                if (length != word.Length)
                    temp = temp + word[length];
                cutUp[i] = temp;
            }
            
        }
        cutUp.ForEach(word => jumpedQuestion = jumpedQuestion + word + " ");
        
        return jumpedQuestion;
    }
    
    public void ClickedVocab(GameObject Vocab)
    {
        SceneManager.LoadScene("Vocabulary");
    }

    // Update is called once per frame
    void Update()
    {
        if (updateCounter == 12)
        {
            JumpingQuestion();
            JumpingPreviousQuestion();
            updateCounter = 0;
        }
        else
            updateCounter++;

    }

    public void ClickedRestart()
    {
        counter = 0;
        LoadScene();
    }

}
