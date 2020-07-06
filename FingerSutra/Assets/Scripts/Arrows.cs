using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public EdgeCollider2D canva;
    public int curNo=0;
    public Vector3 curPoint,focus;
    public float speed,timer,timerAmount,angle,killTimer;
    public GameObject arrowPrefab;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = Instantiate(arrowPrefab, canva.points[0], Quaternion.identity);
     
        curPoint = canva.points[0];
      
        
    }

    // Update is called once per frame
    void Update()
    {

        temp.transform.position = Vector3.MoveTowards(temp.transform.position, curPoint, speed * Time.deltaTime);
        
        if (Vector2.Distance(temp.transform.position, curPoint) < 0.1f && curNo<canva.pointCount-1)
        {
            curNo++;
            curPoint = canva.points[curNo];
            focus = curPoint - temp.transform.position;
            angle = Mathf.Atan2(focus.y, focus.x) * Mathf.Rad2Deg;
            temp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }


        timer -= Time.deltaTime;
        if (timer < 0 && Vector2.Distance(temp.transform.position, canva.points[canva.pointCount - 1]) >0.1f)
        {
            //GameObject temp = Instantiate(arrowPrefab,transform.position,Quaternion.identity);
            temp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           // Destroy(temp, killTimer);
            timer = timerAmount;
        }

        if(Vector2.Distance(temp.transform.position, canva.points[canva.pointCount - 1]) < 0.1f)
        {
            Destroy(temp);
            Destroy(this.gameObject);
        }
     

        
    }
}
