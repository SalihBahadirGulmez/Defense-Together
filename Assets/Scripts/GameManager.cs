using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject selectHeroPanel;

    private float countdown = 3;
    private bool startGame;

    public Text countdownText;
    public GameObject startGameButton;

    private int x;
    private int z;
    private int numberOfEnemy;
    private int numberOfEnemyForEachLoop;
    private int round;
    private int baseEnemyNumber = 16;

    private void Awake()
    {

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            Countdown();
        }
    }

    public void SelectHero()
    {
        if (PhotonNetwork.IsConnected)
        {
            string selectedHeroName = EventSystem.current.currentSelectedGameObject.name;
            GameObject hero = PhotonNetwork.Instantiate(selectedHeroName, new Vector3(0, 0.01f, -3), Quaternion.identity, 0, null);
            hero.name = selectedHeroName;
            selectHeroPanel.SetActive(false);
            if (PhotonNetwork.NickName == PhotonNetwork.CurrentRoom.Name)//todo start butonu sadece oyunu kuranda mý çýkýyor kontrol et
            {
                startGameButton.SetActive(true);
            }
        }
    }
    private void Countdown()
    {
        countdown -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(countdown).ToString();
        if (countdown <= -0.2)
        {
            startGame = false;
            countdown = 3;
            countdownText.gameObject.SetActive(false);
            NumberOfEnemyCalculator();
            EnemySpawn();
        }
    }
    public void StartGameButton()
    {
        startGame = true;
        countdownText.gameObject.SetActive(true);
        startGameButton.SetActive(false);
    }
    private void EnemySpawn()
    {
        for(z = 0; z < numberOfEnemyForEachLoop; z++)
        {
            for (x = 0; x < numberOfEnemyForEachLoop; x++)
            {
                PhotonNetwork.Instantiate("BasicCreep", new Vector3(-20 + x, 0.01f, 20 - z), Quaternion.identity, 0, null).name = "BasicCreep";
            }
        }
        for (z = 0; z < numberOfEnemyForEachLoop; z++)
        {
            for (x = 0; x < numberOfEnemyForEachLoop; x++)
            {
                PhotonNetwork.Instantiate("BasicCreep", new Vector3(20 - x, 0.01f, 20 - z), Quaternion.identity, 0, null).name = "BasicCreep"; ;
            }
        }
        for (z = 0; z < numberOfEnemyForEachLoop; z++)
        {
            for (x = 0; x < numberOfEnemyForEachLoop; x++)
            {
                PhotonNetwork.Instantiate("BasicCreep", new Vector3(-20 + x, 0.01f, -20 + z), Quaternion.identity, 0, null).name = "BasicCreep"; ;
            }
        }
        for (z = 0; z < numberOfEnemyForEachLoop; z++)
        {
            for (x = 0; x < numberOfEnemyForEachLoop; x++)
            {
                PhotonNetwork.Instantiate("BasicCreep", new Vector3(20 - x, 0.01f, -20 + z), Quaternion.identity, 0, null).name = "BasicCreep"; ;
            }
        }
    }
    private void NumberOfEnemyCalculator()
    {
        numberOfEnemyForEachLoop = (int)Mathf.Sqrt((++round * baseEnemyNumber)/4);
        numberOfEnemy = numberOfEnemyForEachLoop * numberOfEnemyForEachLoop * 4;
    }
    public void ReduceNumberOfEnemy() //stats ta kullanýlýyor
    {
        numberOfEnemy--;
        if(numberOfEnemy == 0)
        {
            StartCoroutine(StartNextRound());
        }
    }
    IEnumerator StartNextRound()
    {
        yield return new WaitForSecondsRealtime(5f);
        startGame = true;
        countdownText.gameObject.SetActive(true);
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
}
