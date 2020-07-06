using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject knob, nextStage;
    public EdgeCollider2D stageCanva;
    public WinChecker winChecker;
    public bool final;
    Draggable draggable;
    FinishedFX finishedFX;
    

    void Start()
    {
        draggable = knob.GetComponent<Draggable>();
        draggable.canva = stageCanva;
        knob.GetComponent<Return>().canva = stageCanva;
        finishedFX = this.transform.GetComponentInChildren<FinishedFX>();
        knob.GetComponent<Draggable>().finishedFX = finishedFX;
    }

    // Update is called once per frame
    void Update()
    {
        if (winChecker.winTimer < 0.5f && winChecker.winTimer > 0.3f && !final)
        {
            winChecker.whitePanel.SetActive(true);
        }

        
    }

    public void Changing()
    {

        if (!final)
        {
            
            nextStage.SetActive(true);
            knob.SetActive(false);
            knob.GetComponent<Return>().points = nextStage.GetComponent<ChangeStage>().stageCanva.points;
            knob.transform.position = knob.GetComponent<Return>().points[0];
            knob.GetComponent<Draggable>().currentPoint = 0;
            knob.GetComponent<Draggable>().nextPoint = 1;
            

            knob.SetActive(true);
            gameObject.SetActive(false);


        }
        else winChecker.final = true;



        

    }


  
  
}





/*
 * void LinesReload()
{
    for (int i = 0; i < draggable.lines.Count; i++)
    {
        Destroy(draggable.lines[i].gameObject);

    }
    draggable.lines.RemoveRange(0, draggable.lines.Count);

    draggable.nextPoint = 1;
    draggable.currentPoint = 0;
    //draggable.LineSpawn();


}
*/