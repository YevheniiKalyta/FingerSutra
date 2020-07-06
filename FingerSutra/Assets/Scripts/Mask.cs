using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public Transform enter,exit;
    public Vector3 differ,origin;
    public GameObject knob,mask,litSprite;
    public float xdiff, ydiff, dist, differ2;
    public string type;
    public Color levelColor;
    public Quaternion quaternion;

    // Start is called before the first frame update
    void Start()
    {
        origin = mask.transform.position;
        quaternion = mask.transform.rotation;
        levelColor = GetComponentInParent<SpriteRenderer>().color;
        litSprite.GetComponent<SpriteRenderer>().color = levelColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject == knob.GetComponent<Draggable>().currentLevel)
        { 
            switch (type)
            {
                case "horizontal": 
              differ = new Vector3(Mathf.Clamp(knob.transform.position.x - enter.transform.position.x, -2, 2), Mathf.Clamp(knob.transform.position.y - enter.transform.position.y, -2, 2), 0);
            xdiff = Mathf.Clamp(1 - (Mathf.Abs(differ.x) / 2), 0, 1);
            ydiff = Mathf.Clamp(1 - (Mathf.Abs(differ.y) / 2), 0, 1);
            // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);

            mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff)+0.5f, mask.transform.localScale.y, mask.transform.localScale.z);
            mask.transform.position = origin + differ ;
            break;
                case "circle":
                   /* differ = knob.transform.position - enter.transform.position;
                    xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                    ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 4, 0, 1);
                    // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);


                    mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                    mask.transform.position = origin + differ;
                    break;*/



                    differ = knob.transform.position - enter.transform.position;
                    // differ2 = exit.transform.position - knob.transform.position ;
                    differ2 = Mathf.Clamp01(Vector3.Distance(exit.transform.position, knob.transform.position) - 0.5f);


                    xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                    ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 2, 0, 1);
                    //ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 4, 0, 1);
                    //mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);


                    // mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                    //mask.transform.localScale = new Vector3(1, Mathf.Min(xdiff, ydiff), mask.transform.localScale.z);
                    //mask.transform.localScale = new Vector3(1, Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                    //mask.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg* Mathf.Atan2(differ2.y, differ2.x)-90);

                    mask.transform.localRotation = Quaternion.Euler(0, 0, differ2 * 90);

                    Debug.Log(differ2 * 90);

                    //mask.transform.position = origin + differ;
                    break;



                case "end":
                    differ = knob.transform.position - enter.transform.position;
                    xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                    ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 2, 0, 1);
                    // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);


                    mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), mask.transform.localScale.y, mask.transform.localScale.z);
                    mask.transform.position = origin + differ / 2;
                    break;
                case "corner":
                    
                    differ = knob.transform.position - enter.transform.position;
                    // differ2 = exit.transform.position - knob.transform.position ;
                    differ2 =  Mathf.Clamp01(Vector3.Distance(exit.transform.position, knob.transform.position)-0.5f);


                     xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                    ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 2, 0, 1);
                    //ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 4, 0, 1);
                    //mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);


                    // mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                    //mask.transform.localScale = new Vector3(1, Mathf.Min(xdiff, ydiff), mask.transform.localScale.z);
                    //mask.transform.localScale = new Vector3(1, Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                    //mask.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg* Mathf.Atan2(differ2.y, differ2.x)-90);

                    mask.transform.localRotation = Quaternion.Euler(0, 0, differ2 * 90);



                    //mask.transform.position = origin + differ;
                    break;
            } 
        
       /* if (type == "horizontal")
            {
                differ = new Vector3(Mathf.Clamp(knob.transform.position.x - enter.transform.position.x,-2,2), Mathf.Clamp(knob.transform.position.y - enter.transform.position.y,-2,2),0);
                xdiff = Mathf.Clamp(1 - (Mathf.Abs(differ.x) / 2), 0, 1);
                ydiff = Mathf.Clamp(1 - (Mathf.Abs(differ.y) / 2), 0, 1);
                // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);
                
                mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), mask.transform.localScale.y, mask.transform.localScale.z);
                mask.transform.position = origin + differ / 2;

            }

            if (type == "circle")
            {
                differ = knob.transform.position - enter.transform.position;
                xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 4, 0, 1);
                // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);

                
                mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                mask.transform.position = origin + differ;

            }

            if (type == "end")
            {
                differ = knob.transform.position - enter.transform.position;
                xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 2, 0, 1);
                // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);

               
                mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), mask.transform.localScale.y, mask.transform.localScale.z);
                mask.transform.position = origin + differ / 2;

            }

            if (type == "corner")
            {
                differ = knob.transform.position - enter.transform.position;
                xdiff = Mathf.Clamp(1 - Mathf.Abs(differ.x) / 2, 0, 1);
                ydiff = Mathf.Clamp(1 - Mathf.Abs(differ.y) / 4, 0, 1);
                // mask.transform.localScale = new Vector3(Mathf.MoveTowards(mask.transform.localScale.x, xdiff, 5000), Mathf.MoveTowards(mask.transform.localScale.y, ydiff, 5000), mask.transform.localScale.z);

               
                mask.transform.localScale = new Vector3(Mathf.Min(xdiff, ydiff), Mathf.Max(xdiff, ydiff), mask.transform.localScale.z);
                mask.transform.position = origin + differ;

            }
            */
        }
    }
}
