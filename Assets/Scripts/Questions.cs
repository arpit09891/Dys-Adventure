using UnityEngine;
using System.Collections;

//this will hold 1 question, 4 choices and answer (included)
public class Questions
{
    public string heading;
    public Choices[] choices ;
    public string question;
    public string answer;
    public int numberOfChoices;
    public string previousQuest;
}

public class Choices
{
    public string choice;
    public string figuredChoice;
    public string fc;
    public int linker;
}
