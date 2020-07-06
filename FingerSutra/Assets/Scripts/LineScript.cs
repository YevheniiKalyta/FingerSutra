using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public GameObject knob;
    LineRenderer line;
    public bool over;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        knob = transform.parent.gameObject;
        line.SetPosition(0, knob.GetComponent<Draggable>().canva.points[knob.GetComponent<Draggable>().nextPoint-1]);
    }

    // Update is called once per frame
    void Update()
    {
       
            if(!over) line.SetPosition(line.positionCount - 1,knob.GetComponent<Draggable>().canva.ClosestPoint(knob.transform.position));
        

    }
}
