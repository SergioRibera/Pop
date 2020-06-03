using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;

public class UsedPosition
{
    public int X;
    public int Y;

    public UsedPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class NumberManager : MonoBehaviour
{
    public static NumberManager instance;
    public float maxTimeForNextNumber;
    public float maxTimeForAllNumbers;
    public int penaltyScore;
    public int currentNumber = 1;
    public int nextNumber = 2;
    public GameObject[] screenPoints;
    public List<int> numbers;
    public List<int> lockedNumbers;
    public List<UsedPosition> usedPositions = new List<UsedPosition>();

    public GameObject numberPrefab;

    public float width;
    public int widthOffset;
    public float height;
    public int heightOffset;

    public float timeForNextNumber;
    public float timeForAllNumbers;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        height = (2f * Camera.main.orthographicSize) / 2f;
        width = (height * Camera.main.aspect) / 2f;
        CreateLevel();
        maxTimeForAllNumbers = GameManager.instance.level;
    }

    // Update is called once per frame
    void Update()
    {
        //Check();
        CountDownForNextNumber();
        CountDownForAllNumbers();
    }

    void Check()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.start)
        {
            RaycastHit hit;
            Ray ray = InputController.RayToObj(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                Number number = hit.collider.GetComponent<Number>();
                if (number.asignedNumber.Equals(currentNumber))
                {
                    GameManager.instance.totalScore += number.asignedNumber;
                    currentNumber++;
                    nextNumber++;
                    timeForNextNumber = 0f;
                    number.gameObject.SetActive(false);
                }
                else
                {
                    //Pierde una oportunidad
                    GameManager.instance.oportunities--;
                    if (screenPoints.Length > 0)
                        screenPoints[Math.Abs(screenPoints.Length - GameManager.instance.oportunities)].SetActive(false);
                    print("Pierdes una oportunidad");
                }
                //hit.collider.gameObject.SetActive(false);
            }
        }
    }

    void CountDownForNextNumber()
    {
        if(GameManager.instance.start)
        timeForNextNumber += Time.deltaTime;
        if (timeForNextNumber >= maxTimeForNextNumber)
        {
            if (GameManager.instance.oportunities > 0)
            {
                print("Pierdes una oportunidad");
                GameManager.instance.oportunities--;
                timeForNextNumber = 0f;
            }
            else
            {
                print("Lose");
            }
        }
    }

    void CountDownForAllNumbers()
    {
        if (GameManager.instance.start)
            timeForAllNumbers += Time.deltaTime;
        if (timeForAllNumbers >= maxTimeForAllNumbers)
        {
            if (GameManager.instance.oportunities > 0)
            {
                GameManager.instance.oportunities--;
                print("Pierdes una oportunidad");
                timeForAllNumbers = 0f;
            }
            else
            {
                print("Lose");
            }
        }
    }

    void CreateLevel()
    {
        for (int i = 0; i < GameManager.instance.level; i++)
        {
            var numberIns = Instantiate(numberPrefab, RandomPosition(), Quaternion.identity);
            Number n = numberIns.GetComponent<Number>();
            n.asignedNumber = i + 1;
            n.score = i + 1;
            n.onMouseDown += NumberMouseDown;
            numberIns.GetComponentInChildren<TextMeshPro>().text = n.asignedNumber + "";
        }
    }

    private void NumberMouseDown(Number number)
    {
        print("numer");
        if (number.asignedNumber.Equals(currentNumber))
        {
            GameManager.instance.totalScore += number.asignedNumber;
            currentNumber++;
            nextNumber++;
            timeForNextNumber = 0f;
            number.gameObject.SetActive(false);
        }
        else
        {
            //Pierde una oportunidad
            GameManager.instance.oportunities--;
            print("Pierdes una oportunidad");
        }
    }

    Vector3 RandomPosition()
    {
        int randX = Random.Range(-Mathf.RoundToInt(width) + widthOffset, Mathf.RoundToInt(width));
        int randY = Random.Range(-Mathf.RoundToInt(height) + heightOffset, Mathf.RoundToInt(height));

        var used = usedPositions.Find(x => x.X.Equals(randX) && x.Y.Equals(randY)) ?? null;

        while (used != null)
        {
            randX = Random.Range(-Mathf.RoundToInt(width) , Mathf.RoundToInt(width));
            randY = Random.Range(-Mathf.RoundToInt(height), Mathf.RoundToInt(height));

            used = usedPositions.Find(x => x.X.Equals(randX) && x.Y.Equals(randY));
        }

        usedPositions.Add(new UsedPosition(randX, randY));

        return new Vector3(randX, randY, 0f);
    }
}
