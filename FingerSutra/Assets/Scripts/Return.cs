using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public EdgeCollider2D canva;
    public Vector2[] points;
    public float speed;
    Vector2 nearestPoint;
    public int curPoint;
    Transform parent;
    Vector3 curVector;
    void Start()
    {
        points = canva.points;
        transform.position = new Vector3(points[0].x, points[0].y);
    }
    

    // Update is called once per frame
    void Update()
    {
       

        if (this.GetComponent<Draggable>().test == 99 && Vector2.Distance(transform.position, points[0]) > 0.3f)
        {
            Returning();
        }


        
    }


    //public void ReturningPoint(Vector2[] points)
    //{
    //    float minDistance = Mathf.Infinity;
    //    int pointNo=0;
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        Vector2 point = new Vector2(points[i].x, points[i].y);
    //        float curDisance = Vector2.Distance(point, transform.position);
    //        if (curDisance < minDistance)
    //        {
    //            minDistance = curDisance;
    //            pointNo = i;
    //        }
    //    }
    //    if (pointNo > 0)
    //    {

    //        curPoint = pointNo - 1;

    //    }
    //    else curPoint = 0;
    //    nearestPoint = new Vector2(points[curPoint].x, points[curPoint].y);
    //    curVector = transform.position;
    //}

    public void ReturningPoint(Vector2[] points)
    {
        curPoint = GetComponent<Draggable>().currentPoint;
        nearestPoint = new Vector2(points[curPoint].x, points[curPoint].y);
        curVector = transform.position;
    }

    public void Returning()
    {

            curVector = Vector3.MoveTowards(curVector, nearestPoint, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, curVector) > 0.5f)
        {
            transform.position = canva.ClosestPoint(Vector3.Lerp(transform.position, curVector, 0.3f));

        }
        else transform.position = canva.GetComponent<EdgeCollider2D>().ClosestPoint(curVector);

       // transform.position = canva.GetComponent<EdgeCollider2D>().ClosestPoint(curVector);

            if (Vector2.Distance(transform.position, nearestPoint) < 0.01f)
            {
                if (curPoint > 0)
                {
                    curPoint--;
                    nearestPoint = new Vector2(points[curPoint].x, points[curPoint].y);
                
                }
            


            }
        


    }

   
}
