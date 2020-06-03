using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalScore;
    public bool lose;
    public int level;
    public int oportunities = 3;
    public float maxTimeForStart = 3;
    public bool start;
    public bool finish;

    public Text countDownText;

    float countDown;
    float timeForStart;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        countDown = maxTimeForStart;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownForStart();
    }

    void CountDownForStart()
    {
        if (!start) {
            timeForStart += Time.deltaTime;
            countDown -= Time.deltaTime;
            if (timeForStart >= maxTimeForStart)
            {
                start = true;
                timeForStart = 0f;
                countDown = 0f;
                countDownText.enabled = false;
            }
            var cd = Mathf.Round(countDown);
            countDownText.text = cd == 0 ? "Ahora!" : cd.ToString();
        }
    }
}
