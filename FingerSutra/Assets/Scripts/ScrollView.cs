using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{

    public int panCount, panOffset, selectedPanID;
    public GameObject panPrefab;
    GameObject[] instPans;
    Vector2[] pansPos;
    Vector2[] pansScale;
    RectTransform contentRect;
    Vector2 contentVector;
    [Range(0f, 20f)]
    public float snapSpeed, scaleOffset, scaleSpeed;
    bool isScrolling;
    public ScrollRect scrollRect;
    public Sprite[] images;
    public string[] strings = {"1-1. Easy one",
"1-2. Warm-up",
"1-3. Hills",
"1-4. Wavey",
"1-5. Cover",
"1-6. Hugs",
"1-7. Stairs",
"1-8. Keep close",
"1-9. Snake and arrow",
"1-10. |o|",
"1-11. Up & down",
"1-12. Rectangle",
"1-13. Pasodoble",
"1-14. Group up",
"1-15. Bell",
"1-16. Wavey Reloaded",
"1-17. PIP",
"1-18. Seaside",
"1-19. Strangers",
"1-20. DJ",
"1-21. Is it a hair?",
"1-22. Goosebumps",
"1-23. Seeking",
"1-24. Frame",
"1-25. Hot-Dog",
"1-26. Abstract",
"1-27. CMS",
"1-28. Hookah",
"1-29. 225",
"1-30. SMS",
"1-31. Robot",
"1-32. Running around",
"1-33. Tornado",
"1-34. Flag",
"1-35. Fishy",
"1-36. Lock",
"1-37. Smoothing things over",
"1-38. Octopus",
"1-39. In And Out",
"1-40. Guitar",
"1-41. Crosses",
"1-42. Convergence ",
"1-43. Jellyfish",
"1-44. Sides",
"1-45. Smoking kills",
"1-46. Differences",
"1-47. Billboard on the road",
"1-48. BJ",
"1-49. Raindrops",
"1-50. Counting"
 };
    public int first = 0;

    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        selectedPanID = PlayerPrefs.GetInt("LevelToBeat", 0);


        for (int i = 0; i < panCount; i++)
        {
            
            instPans[i] = Instantiate(panPrefab, transform, false);
            PanelLoader sceneLoader = instPans[i].GetComponent<PanelLoader>();
            sceneLoader.num = (byte)i;
            if (i<images.Length) instPans[i].GetComponent<Image>().sprite = images[i];
            if (i < strings.Length) instPans[i].GetComponent<PanelLoader>().levelNameText.text = strings[i];

            string lvlStarPP = "Level" + i.ToString() + "star";
            string lvlTimePP= "Level" + i.ToString() + "time";

            sceneLoader.UpdateTimeStars(PlayerPrefs.GetFloat(lvlTimePP, Mathf.Infinity), PlayerPrefs.GetInt(lvlStarPP, 0));

            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;

            if (PlayerPrefs.GetInt("LevelToBeat", 0) < i)
            {
                instPans[i].GetComponent<Button>().interactable = false;
                
            }

            if (PlayerPrefs.GetInt("LevelToBeat", 0) >= i)
            {
                //if (contentRect.anchoredPosition.x > -1265 * i)
                {
                   
                    //contentRect.transform.position = new Vector2(-1265 * i, 0);
                    //contentVector = new Vector2(-1265 * i, 0); ;
                    //Debug.Log(contentRect.anchoredPosition  +" "+i + contentVector);
                   // selectedPanID = i;
                    //isScrolling = true;
                    Vector2 gg = new Vector2(-1265 * i, 0);
                    contentRect.anchoredPosition = gg;
                }

            }

            

        }
        Snapping();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (first<3)
        {
            Vector2 gg = new Vector2(-1265 * PlayerPrefs.GetInt("LevelToBeat", 0), 0);
            contentRect.anchoredPosition = gg;
            first++;
        }
      
       
        

        if (contentRect.anchoredPosition.x >= pansPos[0].x || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x)
       {
            isScrolling = false;
            scrollRect.inertia = false;
        }

        Snapping();


       float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        
       if(scrollVelocity<600 && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        


        if (isScrolling || scrollVelocity>600) return;

        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed*Time.fixedDeltaTime);
       contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }


    void Snapping()
    {

        float nearestPos = float.MaxValue;
        for (int i = panCount-1; i >=0; i--)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
               
            }
            float scale = Mathf.Clamp((1 / (distance / panOffset)) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = new Vector3(pansScale[i].x, pansScale[i].y, 1);

        }
    }
}
