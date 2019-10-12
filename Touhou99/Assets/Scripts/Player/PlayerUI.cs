using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class PlayerUI : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject scoreBoard;

    [SerializeField]
    private Image[] healthbar = new Image[5];

    [SerializeField]
    private Sprite[] pictures = new Sprite[3];

    public Slider bombPowerBar;

    [HideInInspector] public Player player;
    [HideInInspector] public PlayerWeapon weapon;

    public void SetPlayer(Player _player)
    {
        player = _player;
        weapon = player.GetComponent<PlayerWeapon>();
        print("BERSAGLIO (UI): " + _player);
    }
    private void Start()
    {
        PauseMenu.IsOn = false;
    }
    private void Update()
    {
        SetHealthAmount(player.GetHealth());
        SetBombPowerAmount(player.GetBombPowerAmount());

        bombPowerBar.maxValue = weapon.bombPowerMax;
        bombPowerBar.value = weapon.bombPower;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreBoard.SetActive(true);

        }

        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreBoard.SetActive(false);
        }

        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            foreach (KeyValuePair<string, Player> entry in GameManager.playersAlive)
            {
                Debug.Log(GameManager.playersAlive.Count);
            }
        }
    }

    public void RemovePlayer()
    {
        GameManager.UnRegisterPlayer(player.transform.name);
        GameManager.RemoveDeadPlayer(player.transform.name);
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.IsOn = pauseMenu.activeSelf;

    }
    public void SetBombPowerAmount(float _amount)
    {
        bombPowerBar.value = _amount;
    }
    void SetHealthAmount(int _amount)
    {

        switch (_amount)
        {
            case 8:
                for (int i = 0; i < 4; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                healthbar[4].sprite = pictures[2];
                break;

            case 6:
                for (int i = 0; i < 3; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                for (int i = 4; i > 2; i--)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;

            case 4:
                for (int i = 0; i < 2; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                for (int i = 4; i > 1; i--)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;

            case 2:
                for (int i = 0; i < 1; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                for (int i = 4; i > 0; i--)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;

            case 0:
                for (int i = 0; i < 5; i++)
                {
                    healthbar[i].sprite = pictures[3];
                }
                break;
        }
        if (_amount == 10)
        {
            for (int i = 0; i < 5; i++)
            {
                healthbar[i].sprite = pictures[0];
            }
        }

    }
}
