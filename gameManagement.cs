using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManagement : MonoBehaviour
{
    [Header("Object Ground")]
    [SerializeField] Rigidbody objectsGroundRigidbody;
    [SerializeField] float rotationSpeed = 20f;
    bool spinStart;

    [Header("Ball")]
    [SerializeField] Transform ballPos;
    [SerializeField] GameObject[] balls;
    [SerializeField] int ballNumber;
    [SerializeField] TMP_Text ballCountText;

    [Header("UI")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject inGamePanel;

    void Start()
    {
        StartCoroutine(groundRotate());
    }

    void FixedUpdate()
    {
        if (spinStart)
        {
            objectsGroundRigidbody.angularVelocity = Vector3.up * rotationSpeed;
        }
        
    }
    IEnumerator groundRotate()
    {
        yield return new WaitForSeconds(1);
        spinStart = true;
    }

    public void ballCounter()
    {
        if (ballNumber>=0)
        {
            balls[ballNumber].transform.position = ballPos.transform.position;
            balls[ballNumber].GetComponent<SlingShotString>().enabled = true;
            ballCountText.text = "1 Shot Left";
            ballNumber--;
        }
        else if (ballNumber<=0)
        {
            ballCountText.text = "No More Shots";
            gameOverCheck();
        }
    }

    public void gameOverCheck()
    {
        StartCoroutine(gameOverIEnu());
    }
    IEnumerator gameOverIEnu()
    {
        yield return new WaitForSeconds(5);
        Time.timeScale = 0;
        gameOverPanel.gameObject.SetActive(true);
        inGamePanel.gameObject.SetActive(false);
    }

    public void restartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void quitButton()
    {
        Application.Quit();
    }
}
