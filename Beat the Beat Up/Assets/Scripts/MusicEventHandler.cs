using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MusicEventHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] private GameObject floor;
    [SerializeField] private Material[] mat;

    Queue<MusicEvent> events;
    float timeMusicStart;
    bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        events = new Queue<MusicEvent>();
        isPlaying = false;

        string eventSource = "Assets/Resources/events.csv";
        CsvReader csv = new CsvReader(eventSource);
        while(csv.Read())
        {
            MusicEvent newEvent = new MusicEvent
            {
                eventIndex = int.Parse(csv.GetFieldOrEmpty("Event Number")),
                span = 2.0f,
                hitLocation = csv.GetFieldOrEmpty("Hit Location"),
                targetEnemyIndex = int.Parse(csv.GetFieldOrEmpty("Targetted Enemy")),
                attack = csv.GetFieldOrEmpty("Hit Type"),
            };
            int[] timeArray = csv.GetFieldOrEmpty("Timing").Split(':').Select(int.Parse).ToArray();
            newEvent.timing = timeArray[0] * 60 + timeArray[1] + timeArray[2] / 100.0f;
            events.Enqueue(newEvent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if ((Input.GetKeyDown(KeyCode.P) || OVRInput.GetDown(OVRInput.Button.Two)) && !isPlaying)
        {
            isPlaying = true;
            audioSource.Play();
            Debug.Log("number of events is " + events.Count);
            timeMusicStart = Time.time;
            if(events.Count > 0)
                StartCoroutine(DelayTriggerEvent());
            //Debug.Log("Done");
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
                //Debug.Log(e.eventIndex + " " + e.timing + " " + e.hitLocation + " " + e.targetEnemyIndex + " " + e.attack);
                //Debug.Log("Triggering event " + e.eventIndex + " enemy" + e.targetEnemyIndex + " " + e.hitLocation + " for " + e.span + " seconds");
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
                    HighlightEnemyPart(target, e);
                }
                // switch ground material
                floor.GetComponent<MeshRenderer>().material = mat[e.eventIndex % 3];

                events.Dequeue();
            }
        }
        if(events.Count > 0)
            StartCoroutine(DelayTriggerEvent());
    }
    public void HighlightEnemyPart(GameObject target, MusicEvent e)
    {
        // highlight part
        target.GetComponent<BoneHighlighter>().HighlightPart(e.hitLocation, e.span);
        // highlight dot
        target.GetComponent<PartDots>().ActivateDotAtPart(e.hitLocation, e.span, e.attack);
    }
}

public class MusicEvent
{
    public int eventIndex;
    public float timing;
    public string hitLocation;
    public int targetEnemyIndex;
    public float span;
    public string attack;
}
