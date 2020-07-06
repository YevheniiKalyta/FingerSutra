using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneLoader : MonoBehaviour
{
    public byte num;
    public Text levelTimeText;
    public Image stars;
    public Sprite[] starsVariants;
    public bool music, vibro;
    public Image musicIMG, vibroIMG, pagerSprite, magniSprite;
    public GameObject musicParticles;
    public Animation vibroAnim;
    public Sprite musicOn, musicOff, vibroOn, vibroOff;
    public GameObject levelMenu,mainMenuStuff,vibro1,vibro2,surePanel,scrollView;
    AudioSource audioSource,sfx;
    public AudioClip butPressedSFX;

    public AudioMixer master;
 
    


    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("music", 1) == 1)
        {
            music = true;
        }
        else music = false;
        if (PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            vibro = true;
        }
        else vibro = false;
        UpdateMusic();
        UpdateVibro();

        Time.timeScale = 1;


    }

    private void Update()
    {
        
    }



    public void LoadScene()
    {
        SceneManager.LoadScene("Level" + num);
    }

    public void UpdateTimeStars(float lvlTime,int starCount)
    {

        string minutes = ((int)lvlTime / 60).ToString();
        int seconds = ((int)(lvlTime % 60));
        if (((int)lvlTime / 60) < 50 && ((int)lvlTime / 60) >=0)
        {
            if (seconds > 10)
            {
                levelTimeText.text = minutes + ":" + seconds;
            }
            else  levelTimeText.text = minutes + ":0" + seconds; 
        }

       


        else levelTimeText.text = "--:--";


        stars.sprite = starsVariants[starCount];
    }


    public void MusicButPressed()
    {
        PlaySoundPressed();
        music = !music;
        UpdateMusic();

    }

    public void UpdateMusic()
    {
        if (music)
        {
            PlayerPrefs.SetInt("music", 1);
            musicParticles.SetActive(true);
            musicIMG.sprite = musicOn;
            magniSprite.color = Color.white;
            master.SetFloat("Voluma", 0);
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            musicParticles.SetActive(false);
            musicIMG.sprite = musicOff;
            magniSprite.color = Color.gray;
            master.SetFloat("Voluma", -80);
        }
    }

    public void VibroButPressed()
    {
        PlaySoundPressed();
        vibro = !vibro;
        UpdateVibro();

    }

    public void UpdateVibro()
    {
        if (vibro)
        {
            PlayerPrefs.SetInt("vibro", 1);
            vibroIMG.sprite = vibroOn;
            vibroAnim.enabled = true;
            pagerSprite.color = Color.white;
            vibro1.SetActive(true);
            vibro2.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("vibro", 0);
            vibroIMG.sprite = vibroOff;
            vibroAnim.enabled = false;
            pagerSprite.color = Color.grey;
            vibro1.SetActive(false);
            vibro2.SetActive(false);
        }
    }

    public void PlayButPressed()
    {
        PlaySoundPressed();
        levelMenu.SetActive(true);
        mainMenuStuff.SetActive(false);
    }

    public void BackButPressed()
    {
        PlaySoundPressed();
        levelMenu.SetActive(false);
        mainMenuStuff.SetActive(true);
    }


    public void PlaySoundPressed()
    {
        sfx.PlayOneShot(butPressedSFX,1);
    }


    public void ResetButtPressed()
    {
        surePanel.SetActive(true);
        scrollView.SetActive(false);
    }

    public void NoResetButtPressed()
    {
        surePanel.SetActive(false);
        scrollView.SetActive(true);
    }

    public void YesResetButtPressed()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


  




}
