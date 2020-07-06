using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhitePanel : MonoBehaviour
{
    public Text text;
    string[] quotes = { "NICE", "COOL", "GO ON", "SUPER", "NEXT", "FINE" };
    // Start is called before the first frame update


    private void Start()
    {
        
    }
    void OnEnable()
    {
        StartCoroutine(Offing());
        text.text = quotes[Random.Range(0, quotes.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Offing()
    {
        yield return new WaitForSeconds(1.1f);
        this.gameObject.SetActive(false);
    }
}
