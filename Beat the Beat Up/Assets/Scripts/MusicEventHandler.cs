using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MusicEventHandler : MonoBehaviour
{
    private AudioSource audioSource;
    private string[] dotPositionList = {"Right Stomach","Mid Stomach","Left Stomach", "Chest","Left Arm",
        "Left Hand", "Right Head", "Mid Head", "Left Head", "Right Arm", "Right Hand" };

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] private GameObject floor;
    [SerializeField] private Material[] mat;

    Queue<MusicEvent> events;
    float timeMusicStart;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        events = new Queue<MusicEvent>();

        string path = "Assets/Resources/test.txt";
        StreamReader reader = new StreamReader(path);
        string[] content = File.ReadAllLines(path);


        int idx = 0;
        foreach(string input in content)
        {
            MusicEvent newEvent = new MusicEvent();
            newEvent.eventIndex = idx++;
            newEvent.span = 2.0f;
            // set timing
            var minutes = int.Parse(input[6].ToString());
            var seconds = int.Parse(input[8].ToString()) * 10 + int.Parse(input[9].ToString());
            var miliSeconds = int.Parse(input[11].ToString()) * 100 + int.Parse(input[12].ToString()) * 10;
            newEvent.timing = minutes * 60 + seconds + miliSeconds / 1000;

            // set hit location
            newEvent.hitLocation = input.Substring(20, input.Length - 24);

            // set target enemy
            if (input.Contains("(1)"))
                newEvent.targetEnemyIndex = 1;
            else if (input.Contains("(2)"))
                newEvent.targetEnemyIndex = 2;
            else if (input.Contains("(3)"))
                newEvent.targetEnemyIndex = 3;
            else if (input.Contains("(4)"))
                newEvent.targetEnemyIndex = 4;

            events.Enqueue(newEvent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("number of events is " + events.Count);
            timeMusicStart = Time.time;
            if(events.Count > 0)
                StartCoroutine(DelayTriggerEvent());
            Debug.Log("Done");
        }
    }

    IEnumerator DelayTriggerEvent()
    {
        float deltaTimeMusicStart = Time.time - timeMusicStart;
        yield return new WaitForSeconds(events.Peek().timing - deltaTimeMusicStart);

        // trigger events
        bool playAllUpToCurrent = false;
        while(!playAllUpToCurrent && events.Count > 0)
        {
            MusicEvent e = events.Peek();
            if(e.timing > deltaTimeMusicStart)
            {
                playAllUpToCurrent = true;
            }
            else
            {
                Debug.Log("Triggering event " + e.eventIndex + " enemy" + e.targetEnemyIndex + " " + e.hitLocation + " for " + e.span + " seconds");
                GameObject target = null;
                // enable dots
                switch (e.targetEnemyIndex)
                {
                    case 1:
                        target = enemy1;
                        break;
                    case 2:
                        target = enemy2;
                        break;
                    case 3:
                        target = enemy3;
                        break;
                    case 4:
                        target = enemy4;
                        break;
                    default:
                        break;
                }
                if(target != null)
                {
                    target.GetComponent<Parts>().ActivateDotAtPart(e.hitLocation, e.span);
                }
                // switch ground material
                floor.GetComponent<MeshRenderer>().material = mat[e.eventIndex % 3];

                events.Dequeue();
            }
        }
        if(events.Count > 0)
            StartCoroutine(DelayTriggerEvent());
    }
}

public class MusicEvent
{
    public int eventIndex;
    public float timing;
    public string hitLocation;
    public int targetEnemyIndex;
    public float span;
}
