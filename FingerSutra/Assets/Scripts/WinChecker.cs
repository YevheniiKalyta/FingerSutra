using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

using System;


public class WinChecker : MonoBehaviour
{
    string[] stringSeparators = new string[] { "Level" };
    GameObject[] pathes;
    Draggable[] knobs;
    
    public float winTimer,levelStartTime;
    public float timerAmount = 3f,oneStarTime,twoStarTime,threeStarTime;
    public bool final,levelOver;

    public GameObject counterPanel, winPanel,postProcessGO,startPanel;
    public Text counter,levelTimeText,finalQuote;
    public PlayableAsset oneStar, twoStar, threeStar, zeroStar;
    public PlayableDirector director;
    //PostProcessVolume volume;
    public Text startLevelName, startTwoStarAmount, startThreeStarAmount,bestTime;
    public Image startStarsCurrent;
    public string _levelName;
    public Sprite zeroStarIMG, oneStarIMG, twoStarIMG, threeStarIMG;
    public string[] quotes;
    public GameObject whitePanel;



    public float timerMenu =0;
    public int tripleTaps;
    public bool done = false;

    public static int screen;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        Starting();
       //Application.targetFrameRate = 60;
        pathes = GameObject.FindGameObjectsWithTag("Path");
        knobs = FindObjectsOfType<Draggable>();
        winTimer = timerAmount;
        levelStartTime = Time.time;

        //volume = postProcessGO.GetComponent<PostProcessVolume>();

        //volume.profile.TryGetSettings(out depth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("Levelok"+ screen + ".png");
            screen++;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        FinishedCounter();
        
        WinTimer();

        
        if (winPanel.activeInHierarchy == true)
        {
           // MenuLoader();
        }

        TripleChecker();



    }


    void TripleChecker()
    {
        
        if(Input.touchCount == 3 && !done)
        {
            
            tripleTaps++;
            done = true;
        }
        if (Input.touchCount == 0 && done)
        {
            done = false;
        }
            if (tripleTaps > 0)
        {

            timerMenu += Time.deltaTime;
        }
        if (timerMenu>2)
        {
            tripleTaps = 0;
            timerMenu = 0;
        }


        if (tripleTaps == 3)
        {
            SceneManager.LoadScene("Menu 1");
        }
    }




    int FinishedCounter()
    {
        int counter = 0;
        for (int i = 0; i < knobs.Length; i++)
        {
            if (knobs[i].finished == true) counter++;
        }
        return counter;
    }

    void WinTimer()
    {

        if (FinishedCounter() == knobs.Length)
        {
            counterPanel.SetActive(true);
            counter.text = Mathf.Ceil(winTimer).ToString();

            winTimer -= Time.deltaTime;

           

            if (winTimer < 0)
            {
                
                for (int i = 0; i < pathes.Length; i++)
                {
                    pathes[i].GetComponentInChildren<ChangeStage>().Changing();


                    
                }
                if (final == true)
                {
                    if (!winPanel.activeInHierarchy)
                    {
                        LevelTimer();
                        //StartCoroutine(Bluring());
                        levelOver = true;

                    }
                    winPanel.SetActive(true);
                    counterPanel.SetActive(false);
                    if (whitePanel!=null)
                    {
                        whitePanel.SetActive(false);
                    }

                }
                else
                {
                    
                    winTimer = timerAmount;
                    counterPanel.SetActive(false);
                }


            }
        }
        else
        {
            
            if (counterPanel.activeInHierarchy==true)
            {
                winTimer = timerAmount;
            }
            counterPanel.SetActive(false);


        }
    }


    public void MenuLoader()
    {
        
        
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Menu 1");
            }
        
    }

    public void NextLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ReloadLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void LevelTimer()
    {
        float lvlTime = Time.time - levelStartTime;
        string minutes = ((int)lvlTime / 60).ToString();
        int seconds = ((int)(lvlTime % 60));
        if (seconds >= 10)
        {
            levelTimeText.text = minutes + ":" + seconds;
        }
        else levelTimeText.text = minutes + ":0" + seconds;

        byte starCount;
        if (lvlTime <= threeStarTime)
        {
            starCount = 3;
            director.playableAsset = threeStar;
        }
        else if (lvlTime > threeStarTime && lvlTime <= twoStarTime)
        {
            starCount = 2;
            director.playableAsset = twoStar;
        }
        else if (lvlTime > twoStarTime)
        {
            starCount = 1;
            director.playableAsset = oneStar;
        }
        else
        {
            starCount = 0;
            director.playableAsset = zeroStar;
        }

        
        string[] lvlNum = SceneManager.GetActiveScene().name.Split(stringSeparators ,StringSplitOptions.RemoveEmptyEntries);
        int leveToBeat = int.Parse(lvlNum[0]) + 1;
        if (leveToBeat> PlayerPrefs.GetInt("LevelToBeat",0))
        {
            PlayerPrefs.SetInt("LevelToBeat", leveToBeat );
        }


        string lvlStarCountToPlayPref = "Level" + lvlNum[0]+"star";
        string lvlTimeToPlayPref = "Level" + lvlNum[0] + "time";


        if (starCount> PlayerPrefs.GetInt(lvlStarCountToPlayPref,0))
        {
            PlayerPrefs.SetInt(lvlStarCountToPlayPref, starCount);
        }

        if (lvlTime< PlayerPrefs.GetFloat(lvlTimeToPlayPref,Mathf.Infinity))
        {
            PlayerPrefs.SetFloat(lvlTimeToPlayPref, lvlTime);
        }

        
        finalQuote.text = quotes[UnityEngine.Random.Range(0, quotes.Length)];



    }

    /*public IEnumerator Bluring()
    {
        for (int i = 25; i < 73; i++)
        {
            depth.focalLength.value = new FloatParameter { value = i};
            //depth.enabled.value = true;
            
            yield return new WaitForSeconds(0.1f);
        }
        
    }*/


    public void Starting()
    {
        startLevelName.text = _levelName;

        string[] lvlNum = SceneManager.GetActiveScene().name.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        int leveToBeat = int.Parse(lvlNum[0]) + 1;
        string lvlStarCountToPlayPref = "Level" + lvlNum[0] + "star";
        string lvlTimeToPlayPref = "Level" + lvlNum[0] + "time";
        switch (PlayerPrefs.GetInt(lvlStarCountToPlayPref, 0))
        {
            case 0:
                startStarsCurrent.sprite = zeroStarIMG;
                break;
            case 1:
                startStarsCurrent.sprite = oneStarIMG;
                break;
            case 2:
                startStarsCurrent.sprite = twoStarIMG;
                break;
            case 3:
                startStarsCurrent.sprite = threeStarIMG;
                break;

        }


        float lvlTime = PlayerPrefs.GetFloat(lvlTimeToPlayPref, Mathf.Infinity);
        if (lvlTime == Mathf.Infinity)
        {
            bestTime.text = "Best time - --:--";
        }
        else
        {
            string minutes = ((int)lvlTime / 60).ToString();
            int seconds = ((int)(lvlTime % 60));
            if (seconds > 10)
            {
                bestTime.text = "Best time - " + minutes + ":" + seconds;
            }
            else bestTime.text = "Best time - " + minutes + ":0" + seconds;
        }

        if ((int)(twoStarTime % 60) > 9)
        {
            startTwoStarAmount.text = ((int)twoStarTime / 60).ToString() + ":" + ((int)(twoStarTime % 60));
        }
        else startTwoStarAmount.text = ((int)twoStarTime / 60).ToString() + ":0" + ((int)(twoStarTime % 60));
        if ((int)(threeStarTime % 60) > 9)
        {
            startThreeStarAmount.text = ((int)threeStarTime / 60).ToString() + ":" + ((int)(threeStarTime % 60));
        }
        else startThreeStarAmount.text = ((int)threeStarTime / 60).ToString() + ":0" + ((int)(threeStarTime % 60));

    }

    public void FirstPress()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
