using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public SpriteRenderer sprite,spriteBack;
    
    public EdgeCollider2D canva;
    public int test = 99;
    public Sprite activeSprite,idleSprite,idleBack,activeBack;
    public Color spriteColor;
    
    public bool finished, nextStep;
    public int nextPoint=1,currentPoint=0;
    public float scale=1.5f;
    public FinishedFX finishedFX;
    float distance1,distance2,x,y;
    Return returner;
    bool onceTouch;

    public  GameObject lastLevel,currentLevel;

    LayerMask touchCanvaMask,levelMask;
    //public float timr = 0, timeAmount = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

       

        sprite = GetComponent<SpriteRenderer>();
        
        
        returner = this.GetComponent<Return>();
        test = 99;
        

        //lines = new List<GameObject>();

        //LineSpawn();
        //currentLine.GetComponent<LineRenderer>().colorGradient = color;

        touchCanvaMask = LayerMask.GetMask("TouchCanva");
        levelMask= LayerMask.GetMask("Level");

    }


    // Update is called once per frame

    private void Update()
    {
        TestForLevel();
        BasicMovement();
        Corner();
        

    }




    void BasicMovement()
    {
        //Gets the world position of the mouse on the screen        

        int e = 0;
        if (Time.timeScale == 1)
        {
            if (!Camera.main.GetComponent<WinChecker>().levelOver)
            {

                while (e < Input.touchCount)
                {
                    Touch touch = Input.GetTouch(e);
                    //Checks whether the mouse is over the sprite
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    bool overSprite = sprite.bounds.Contains(touchPosition);

                    //If it's over the sprite
                    if (overSprite)
                    {

                        if (touch.phase == TouchPhase.Began)
                        {
                            if (test == 99)
                            {
                                test = touch.fingerId;

                            }
                            transform.localScale = new Vector3(scale, scale, 0);
                            BeActive();

                        }

                        //If we've pressed down on the mouse (or touched on the iphone)
                        if (test == touch.fingerId)
                        {



                            Ray ray = Camera.main.ScreenPointToRay(touch.position);
                            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, touchCanvaMask);

                            if (hit == true)
                            {
                                if (hit.collider.gameObject.tag == "Line")
                                {

                                    //Set the position to the mouse position
                                    Vector2 newpos = canva.ClosestPoint(touchPosition);
                                    Vector3 newPosV3 = new Vector3(newpos.x, newpos.y);



                                    if (currentPoint < canva.points.Length - 1)
                                    {
                                        x = Mathf.Clamp(newPosV3.x, Mathf.Min(canva.points[currentPoint].x, canva.points[currentPoint + 1].x), Mathf.Max(canva.points[currentPoint].x, canva.points[currentPoint + 1].x));
                                        y = Mathf.Clamp(newPosV3.y, Mathf.Min(canva.points[currentPoint].y, canva.points[currentPoint + 1].y), Mathf.Max(canva.points[currentPoint].y, canva.points[currentPoint + 1].y));
                                    }
                                    else
                                    {
                                        x = Mathf.Clamp(newPosV3.x, Mathf.Min(canva.points[currentPoint].x, canva.points[currentPoint - 1].x), Mathf.Max(canva.points[currentPoint].x, canva.points[currentPoint - 1].x));
                                        y = Mathf.Clamp(newPosV3.y, Mathf.Min(canva.points[currentPoint].y, canva.points[currentPoint - 1].y), Mathf.Max(canva.points[currentPoint].y, canva.points[currentPoint - 1].y));
                                    }

                                    Vector3 newPosV4 = new Vector3(x, y, 0);

                                    transform.position = newPosV4;


                                }
                            }
                            else
                            {
                                StartReturn();
                            }


                        }
                    }
                    else
                    {
                        if (test == touch.fingerId)
                        {
                            StartReturn();

                        }
                    }

                    if (touch.phase == TouchPhase.Ended && test == touch.fingerId)
                    {
                        StartReturn();

                    }
                    ++e;
                }

                FinishCheck();
            }
        }
    }


    void BeActive()
    {
        Color color;
        
            sprite.sprite = activeSprite;
 
           
            color = spriteColor;
            color.a = 0.8f;
            spriteBack.sprite = activeBack;

     
      

        sprite.color = color;
        spriteBack.color = color;

    }

    void BeIdle()
    {
        Color color;

            sprite.sprite = idleSprite;


            color = Color.white;
            color.a = 1f;
            spriteBack.sprite = idleBack;


        sprite.color = color;
        spriteBack.color = color;

    }



    void FinishCheck()
    {

        if (Vector2.Distance(transform.position, canva.points[canva.pointCount - 1]) < 0.3f)
        {
            if (!finished)
            {
                finished = true;
                finishedFX.gameObject.SetActive(true);
                if (PlayerPrefs.GetInt("vibro", 1)==1){
                    // Handheld.Vibrate();
                    Vibration.Vibrate(80);
                }
                //StartCoroutine(Vibro(0.01f));
            }
            


        }
        else
        {
            finished = false;
            finishedFX.gameObject.SetActive(false);
        }


    }

    

    void StartReturn()
    {
        returner.ReturningPoint(canva.points);
        test = 99;

        transform.localScale = new Vector3(1, 1, 0);
        BeIdle();
    }
            
    void Corner()
    {
        float dist;
        if (test != 99) dist = 0.2f;
        else dist = 0.1f;

        if (nextPoint <= canva.points.Length - 1)
        {
            if (Vector2.Distance(transform.position, canva.points[nextPoint]) < dist && nextPoint > currentPoint)
            {

                //КурентПоинт=ТочкеСтремленния
                currentPoint = nextPoint;
                //DistanceCheck
                distance1 = Vector2.Distance(canva.points[currentPoint - 1], transform.position);

            }
            if (Vector2.Distance(transform.position, canva.points[currentPoint]) > dist && nextPoint == currentPoint)
            {

                distance2 = Vector2.Distance(canva.points[currentPoint - 1], transform.position);
                if (distance2 - distance1 > 0.0001f && nextPoint != canva.points.Length - 1)
                {
                    if (test != 99)
                    {
                        nextPoint++;
                    }
                }
                else
                {
                    currentPoint--;
                }
            }
            if (Vector2.Distance(transform.position, canva.points[currentPoint]) < dist && nextPoint > currentPoint)
            {
                if (currentPoint > 0)
                {
                    distance1 = Vector2.Distance(canva.points[currentPoint - 1], transform.position);
                    nextPoint = currentPoint;
                    
                }


            }
        }
        else nextPoint = canva.points.Length - 1;

    }


    void TestForLevel()
    {
        Collider2D[] colliders;

        Mask curLev;

        float distancer;

        if ((colliders = Physics2D.OverlapCircleAll(transform.position, 0f, levelMask)).Length > 0) 
        {
            //foreach (var collider in colliders)
            
                GameObject go = colliders[0].gameObject; //This is the game object you collided with
                if (go.tag == "Level")
                { 

                    if (currentLevel == null)
                    {
                        currentLevel = go;
                        go.GetComponent<Mask>().knob = this.gameObject;
                        //curLev = currentLevel.GetComponent<Mask>();
                    }

                    if (go != currentLevel && currentLevel != null)
                    {
                        curLev = currentLevel.GetComponent<Mask>();

                        if (curLev.type == "horizontal")
                        {
                            distancer = 1.9f;
                        }
                        else distancer = 1f;

                            if (Vector3.Distance(transform.position, curLev.enter.position) >= distancer)
                            {
                                curLev.mask.transform.localScale = new Vector3(Mathf.Floor(curLev.mask.transform.localScale.x + 0.1f), Mathf.Floor(curLev.mask.transform.localScale.y + 0.1f), 0);


                                if (curLev.type == "corner" || curLev.type == "circle")
                        {
                                 curLev.mask.transform.localScale = new Vector3(0, 0, 0);
                                }

                                    }
                            else
                            {
                                curLev.mask.transform.localScale = new Vector3(1, 1, 1);
                                curLev.mask.transform.position = curLev.origin;
                                curLev.mask.transform.rotation = curLev.quaternion;


                        if (go.GetComponent<Mask>().type == "corner" || go.GetComponent<Mask>().type == "circle")
                        {
                            go.GetComponent<Mask>().mask.transform.localScale = new Vector3(1.1f, 1, 1);
                        }
                        if (curLev.type == "corner" || curLev.type == "circle")
                        {
                            curLev.mask.transform.localScale = new Vector3(1.1f, 1, 1);
                        }


                        //curLev.mask.transform.localPosition = Vector3.zero;

                    }

                            //currentLevel.GetComponent<Mask>().mask.transform.position = new Vector3(Mathf.Round(currentLevel.GetComponent<Mask>().mask.transform.position.x), Mathf.Round(currentLevel.GetComponent<Mask>().mask.transform.position.y), 0);
                            //currentLevel.GetComponent<Mask>().origin = currentLevel.transform.position;
                           
                            currentLevel = go;


                        
                    }
                  

                }
                
            
        }
    }



    public IEnumerator Vibro(float time)
    {
        float t;
        t = time;
        while (t>0)
        {
            Handheld.Vibrate();
            t -= Time.deltaTime; 

        }
        yield return new WaitForSecondsRealtime(0);

        //yield return new WaitForEndOfFrame();
    }





}

/*
 * 
 * Vector2 GetClosestPointOnLineSegment(Vector2 A, Vector2 B, Vector2 P)
{
    Vector2 AP = P - A;       //Vector from A to P   
    Vector2 AB = B - A;       //Vector from A to B  

    float magnitudeAB = Vector2.SqrMagnitude(AB);     //Magnitude of AB vector (it's length squared)     
    float ABAPproduct = Vector2.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
    float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  

    if (distance < 0)     //Check if P projection is over vectorAB     
    {
        return A;

    }
    else if (distance > 1)
    {
        return B;
    }
    else
    {
        return A + AB * distance;
    }
}

void ChangeBack()
{
    LayerMask layerMask = LayerMask.GetMask("back");
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.back, 100, layerMask);
    if (hit)
    {
        hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = spritik ;
    }

}

     public void LineSpawn()
    {
        
        
            currentLine = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
            currentLine.GetComponent<LineRenderer>().colorGradient=color;

            currentLine.GetComponent<LineRenderer>().useWorldSpace = true;
            currentLine.GetComponent<LineRenderer>().SetPosition(0, canva.points[nextPoint]);
            currentLine.GetComponent<LineRenderer>().SetPosition(1, transform.position);
            lines.Add(currentLine);
        
    }
*/










