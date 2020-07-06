using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
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
            Time.timeScale = 0;
            //animator.SetInteger("stage", 2);
        }

        if (stage == 2 && Input.GetMouseButtonDown(0))
        {
            first.SetActive(false);
            Time.timeScale = 1;
        }

      

    }
}
