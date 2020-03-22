using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    [SerializeField] Avatar avatar;
    const int maxValue = 3;
    const int minValue = -4;


    [SerializeField] GameObject incCirclePrefab;
    [SerializeField] incomingCircle currentIncoming;

    [SerializeField] float spawnTimer = 3f;
    float spawnCounter = 0;

    int currentLevel = 1;
    

    [SerializeField] float levelTime = 20f;
    float levelTimeCounter = 0;

    [SerializeField] int pointsRequiredPerLevel = 30;
    int pointsRequired = 30;
    [SerializeField] Image pointBar;

    // Start is called before the first frame update
    void Start()
    {
        SwipeController.OnSwipe += GetSwipe;
        currentIncoming = FindObjectOfType<incomingCircle>();

        SpawnIncCircle();

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnTimer)
        {
            spawnCounter = 0;
            SpawnIncCircle();
        }

        levelTimeCounter += Time.deltaTime;
        if(levelTimeCounter >= levelTime)
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        if (avatar.GetPoints() >= pointsRequired)
        {
            // WIN
            currentLevel++;
            levelTimeCounter = 0;
            pointsRequired += pointsRequiredPerLevel;
            UpdateUI();
        }
        else
        {
            // LOSE
            Time.timeScale = 0;
        }
    }

    void GetSwipe(SwipeInfo info)
    {
        if (info.SwipeDirection == ESwipeDirection.Swipe_Left)
        {
            //currentIncoming.RotateLeft();
        }else if (info.SwipeDirection == ESwipeDirection.Swipe_Right)
        {
            //currentIncoming.RotateRight();
        }
    }

    public void SpawnIncCircle()
    {
        currentIncoming = Instantiate(incCirclePrefab, Vector3.zero, Quaternion.identity).GetComponent<incomingCircle>();
        bubble[] bubbles = currentIncoming.getBubbles();

        foreach (bubble b in bubbles)
        {
            b.SetValue(Random.Range(minValue, maxValue + 1));
        }

        Debug.Log("spawnd");
    }

    public void UpdateUI()
    {
        float perc = (float)avatar.GetPoints() / (float)pointsRequired;

        if(perc> 1)
        {
            perc = 1;
        }else if (perc < 0)
        {
            perc = 0;
        }

        pointBar.transform.localScale = new Vector3(perc, 1,1);
    }
}
