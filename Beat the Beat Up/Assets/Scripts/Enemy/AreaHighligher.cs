using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHighligher : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer body, top;
    private float highlightTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (highlightTime > 0)
        {
            highlightTime -= Time.deltaTime;

            if (highlightTime <= 0)
            {
                highlightTime = 0;
                //
            }
        }
    }

    public void HighlightArea(Transform[] transforms, float time)
    {
        // high light transforms

        highlightTime = time;
    }
}
