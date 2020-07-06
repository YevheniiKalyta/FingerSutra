using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public WinChecker winChecker;
    public GameObject startPanel,first,second;
    public int stage = 1;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        stage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPanel.activeInHierarchy && !first.activeInHierarchy && stage==1)
        {
            first.SetActive(true);
            stage = 2;
            //animator.SetInteger("stage", 2);
        }

        if (first.activeInHierarchy && winChecker.winTimer<winChecker.timerAmount)
        {
            first.SetActive(false);
            second.SetActive(true);
            //Time.timeScale = 0;
        }
        else if (stage == 2 && winChecker.winTimer == winChecker.timerAmount)
        {
           
            stage = 1;
            first.SetActive(true);
            second.SetActive(false);
            //animator.SetInteger("stage", 1);
        }

        if (winChecker.winTimer < 0 && second.activeInHierarchy)
        {
            second.SetActive(false);
        }

    }
}
