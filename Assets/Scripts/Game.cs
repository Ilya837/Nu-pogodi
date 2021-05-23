using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{    

    private int Position;

    public GameObject Eggs;

    private float time;

    private int count;

    public GameObject brek;

    private int last;

    public GameObject Hp;

    public GameObject Score;

    public GameObject GameOver;


    private float k = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
        foreach (Transform t in this.gameObject.transform)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform t in brek.gameObject.transform)
        {
            t.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, k);
        }

        

        foreach (Transform t in Hp.gameObject.transform)
        {
            t.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, k);
        }

        foreach (Transform t in Eggs.gameObject.transform)
        {
            foreach (Transform l in t.gameObject.transform)
            {
                l.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, k);
            }
        }

        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        GameOver.gameObject.SetActive(false);

        time = 0.8f;

        count = 0;

        last = -1;

        Score.GetComponent<Text>().text = count.ToString();

       

        StartCoroutine(Timer());

    }

    void Clear()
    {
        foreach (Transform t in this.gameObject.transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Clear();
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Position = 0;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Clear();
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Position = 1;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Clear();
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            Position = 2;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Clear();
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            Position = 3;
        }

    }


    IEnumerator Timer()
    {
        Next();
        
        yield return new WaitForSeconds(time);
        StartCoroutine(Timer());
        time = 0.8f - 0.0001f * count;
        
    }

    void Next()
    {
        int j = 0;
        foreach (Transform t in brek.gameObject.transform)
        {
            t.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, k);
        }
        foreach (Transform t in Eggs.gameObject.transform)
        {
            if(t.GetChild(4).GetComponent<SpriteRenderer>().color.a == 1)
            {
                if(Position == j)
                {
                    count++;
                }
                else
                {         
                    if(j < 2)
                        brek.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                    else
                        brek.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

                    Hpn();
                }

                Score.GetComponent<Text>().text = count.ToString();
            }

            bool[] a = { false, false, false, false, false };
            for(int i = 0; i< t.childCount-1;i++)
            {
                if (t.GetChild(i).GetComponent<SpriteRenderer>().color.a == 1)
                {
                    a[i + 1] = true;
                }
            }
            for (int i = 1; i < t.childCount ; i++)
            {
                if (a[i])
                {
                    t.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
                }
                else
                {
                    t.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, k);
                }
            }
            t.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, k);
            j++;
        }

        if(Random.Range(0,100) <= 60)
        {
            int a;
            a = Random.Range(0, 4);
            while (a == last)
            {
                a = Random.Range(0, 4);
            }
            Eggs.gameObject.transform.GetChild(a).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

            last = a;
        }
        
    }

    private void Hpn()
    {
        if(Hp.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a == k)
        {
            Hp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
        else
        {
            if(Hp.transform.GetChild(1).GetComponent<SpriteRenderer>().color.a == k)
            {
                Hp.transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
            }
            else
            {
                if (Hp.transform.GetChild(2).GetComponent<SpriteRenderer>().color.a == k)
                {
                    Hp.transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);

                    //Game over

                    GameOver.gameObject.SetActive(true);
                    enabled = false;
                    Time.timeScale = 0;
                }
            }
        }
    }
}
