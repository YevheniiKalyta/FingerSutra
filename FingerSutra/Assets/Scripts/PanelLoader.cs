using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLoader : MonoBehaviour
{
    public int num;
    public Text levelTimeText;
    public Text levelNameText;
    public Image stars;
    public Sprite[] starsVariants;
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadScene("Level" + num);
    }

    public void UpdateTimeStars(float lvlTime, int starCount)
    {

        string minutes = ((int)lvlTime / 60).ToString();
        int seconds = ((int)(lvlTime % 60));
        if (((int)lvlTime / 60) < 50 && ((int)lvlTime / 60) >= 0)
        {
            if (seconds >= 10)
            {
                levelTimeText.text = minutes + ":" + seconds;
            }
            else levelTimeText.text = minutes + ":0" + seconds;
        }




        else levelTimeText.text = "--:--";


        stars.sprite = starsVariants[starCount];
    }
}
