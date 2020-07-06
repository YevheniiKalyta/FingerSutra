using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedFX : MonoBehaviour
{
    public EdgeCollider2D canva;
    public int curNo = 0;
    public Vector3 curPoint, focus;
    public float speed, timer, timerAmount, angle, killTimer;
    public GameObject arrowPrefab;
    GameObject temp;
    bool back;
    // Start is called before the first frame update
    void OnEnable()
    {
        temp = Instantiate(arrowPrefab, canva.points[0], Quaternion.identity);
        //temp.transform.position = canva.points[0];
        curPoint = canva.points[0];
        curNo = 0;

        //transform.Rotate(Vector3.forward, Mathf.Acos(Mathf.Cos()));
    }

    private void Start()
    {
        Destroy(temp);
    }

    // Update is called once per frame
    void Update()
    {
        if (temp != null)
        {
            temp.transform.position = Vector3.MoveTowards(temp.transform.position, curPoint, speed * Time.deltaTime);

            if (!back)
            {
                if (Vector2.Distance(temp.transform.position, curPoint) < 0.1f && curNo < canva.pointCount - 1)
                {
                    curNo++;
                    curPoint = canva.points[curNo];
                    focus = curPoint - temp.transform.position;
                    angle = Mathf.Atan2(focus.y, focus.x) * Mathf.Rad2Deg;
                    temp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
            else
            {
                if (Vector2.Distance(temp.transform.position, curPoint) < 0.1f && curNo > 0)
                {
                    curNo--;
                    curPoint = canva.points[curNo];
                    focus = curPoint - temp.transform.position;
                    angle = Mathf.Atan2(focus.y, focus.x) * Mathf.Rad2Deg;
                    temp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }

            if (curNo == canva.pointCount - 1)
            {
                back = true;
            }
            if (curNo == 0)
            {
                back = false;
            }




            if (Vector2.Distance(temp.transform.position, canva.points[canva.pointCount - 1]) < 0.1f)
            {
                //Destroy(temp);
                //Destroy(this.gameObject);
            }



        }
        
    }
    private void OnDisable()
    {
        Destroy(temp);
    }
}
