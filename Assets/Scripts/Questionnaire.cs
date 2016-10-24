using UnityEngine;
using System.Collections.Generic;

public class Questionnaire : MonoBehaviour{

    public static Questionnaire Instance;

   // [HideInInspector] public string serializedString;
    //[SerializeField] private string serializedString1;
    
    public List<string> text_questions;
    public List<string> question_headings;
    public List<int> numberOfChoices;
    public List<string> text_choices;
    //public string[] text_answer;
    public List<int> linker;
    public Questions[] questions;

    public int numberOfQuestions;

    void Awake() {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(gameObject);
        }

        PopulateQuestions();
    }

    void PopulateQuestions()
    {        
        int choiceCounter = 0;
        questions = new Questions[numberOfQuestions];
        
        for (int i = 0; i < numberOfQuestions; i++)
        {
            questions[i] = new Questions();
            questions[i].numberOfChoices = numberOfChoices[i];
            questions[i].question = text_questions[i];
            //questions[i].answer = text_answer[i];
            questions[i].choices = new Choices[numberOfChoices[i]];
            //Debug.LogWarning(i);
            for (int j = 0; j < numberOfChoices[i]; j++)
            {
                questions[i].choices[j] = new Choices();
                questions[i].choices[j].choice = text_choices[choiceCounter];
               // Debug.Log("Choice Counter :" + choiceCounter);
               // Debug.Log("linker " + linker[choiceCounter]);
                questions[i].choices[j].linker = linker[choiceCounter];
                
                choiceCounter++;               
            }
            
        }
        
    }
}
