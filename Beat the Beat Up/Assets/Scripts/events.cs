using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class events : MonoBehaviour
{
    AudioSource audioSource;
    StreamReader reader;

    [SerializeField] private GameObject[] enemy_1_parts;
    [SerializeField] private GameObject[] enemy_2_parts;
    [SerializeField] private GameObject[] enemy_3_parts;
    [SerializeField] private GameObject[] enemy_4_parts;

    [SerializeField] private GameObject floor;
    [SerializeField] private Material[] mat;



    Dictionary<string, GameObject> enemy_1_dots;
    Dictionary<string, GameObject> enemy_2_dots;
    Dictionary<string, GameObject> enemy_3_dots;
    Dictionary<string, GameObject> enemy_4_dots;
    Dictionary<string, GameObject> dict;

    private float time = 0;
    private String[] dotPositionList = {"Right Stomach","Mid Stomach","Left Stomach", "Chest","Left Arm", 
        "Left Hand", "Right Head", "Mid Head", "Left Head", "Right Arm", "Right Hand" };

    private String[] content;

    private void Start()
    {
        enemy_1_dots = new Dictionary<string, GameObject>();
        enemy_2_dots = new Dictionary<string, GameObject>();
        enemy_3_dots = new Dictionary<string, GameObject>();
        enemy_4_dots = new Dictionary<string, GameObject>();
        dict = new Dictionary<string, GameObject>();


        audioSource = GetComponent<AudioSource>();
        for(int i = 0; i < dotPositionList.Length; i++)
        {
            enemy_1_dots.Add(dotPositionList[i] ,enemy_1_parts[i]);
            enemy_2_dots.Add(dotPositionList[i], enemy_2_parts[i]);
            enemy_3_dots.Add(dotPositionList[i], enemy_3_parts[i]);
            enemy_4_dots.Add(dotPositionList[i], enemy_4_parts[i]);

        }

        string path = "Assets/Resources/test.txt";
        reader = new StreamReader(path);
        content = System.IO.File.ReadAllLines(path);
        //reader.Close();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //for(int i=0;i<content.Length;i++)
            //{
            //    Debug.Log(content[i].Substring(20, content[i].Length - 24));
            //}
            if (!audioSource.isPlaying)
                audioSource.Play();
            StartCoroutine(wait(0f,0));
            Debug.Log("Done");
        } 
       
    }

    IEnumerator wait(float previousTime,int eventNumber)
    {
        
        //Debug.Log("Boom");
        //var previousTime = waitTime;
        var input = content[eventNumber];
        //string input = reader.ReadLine();
        if (input.Contains("[Hit]"))
        {
            if (input.Contains("(1)"))
                dict = enemy_1_dots;
            else if (input.Contains("(2)"))
                dict = enemy_2_dots;
            else if (input.Contains("(3)"))
                dict = enemy_3_dots;
            else if (input.Contains("(4)"))
                dict = enemy_4_dots;
            //Debug.Log(input.Substring(20, input.Length - 24)+ input.Substring(20, input.Length - 24).Length);
            

            var minutes = int.Parse(input[6].ToString());
            var seconds = int.Parse(input[8].ToString()) * 10 + int.Parse(input[9].ToString());
            var miliSeconds = int.Parse(input[11].ToString()) * 100 + int.Parse(input[12].ToString()) * 10;

            float timeInSeconds = minutes * 60 + seconds + miliSeconds / 1000;

            yield return new WaitForSeconds(timeInSeconds - previousTime);
            dict[input.Substring(20, input.Length - 24)].SetActive(true);
            floor.GetComponent<MeshRenderer>().material = mat[eventNumber%11];


            //Debug.Log(timeInSeconds + " " + previousTime + " " + (timeInSeconds - previousTime));
            StartCoroutine(wait(timeInSeconds,eventNumber+1));

        }
    }
}
