using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Text text_gameover;
    private Text text_level;
    private Button button_start;
    private Button button_restart;
    /// <summary>
    /// 血量预制体
    /// </summary>
    public GameObject heart;
    /// <summary>
    /// 血量父物体
    /// </summary>
    private Transform heartGrid;
    private Image[] heartArray;
    /// <summary>
    /// 满血图片
    /// </summary>
    private Sprite heartFull;
    /// <summary>
    /// 空血图片
    /// </summary>
    private Sprite heartEmpty;
    private void Start()
    {
        text_gameover = transform.Find("Text GameOver").GetComponent<Text>();
        text_level = transform.Find("Text Level").GetComponent<Text>();
        button_start = transform.Find("Button Start").GetComponent<Button>();
        button_restart = transform.Find("Button Restart").GetComponent<Button>();
        heartGrid = transform.Find("HeartGrid").GetComponent<Transform>();
        button_start.onClick.AddListener(ButtonClickStart);
        button_restart.onClick.AddListener(ButtonClickStart);
        //加载资源
        heartFull = Resources.Load<Sprite>("Image/Heart/ui_heart_full");
        heartEmpty = Resources.Load<Sprite>("Image/Heart/ui_heart_Empty");

        //初始UI显示隐藏
        button_start.gameObject.SetActive(true);
        text_level.gameObject.SetActive(false);
        text_gameover.gameObject.SetActive(false);
        button_restart.gameObject.SetActive(false);

        //获取血量
        heartArray = new Image[heartGrid.childCount];
        for (int i = 0; i < heartGrid.childCount; i++)
        {
            heartArray[i] = heartGrid.GetChild(i).GetComponent<Image>();
        }
    }

    private void ButtonClickStart()
    {
        PlayerInit();
        GameManager.Instance.player.Init();
        text_level.text = "关卡1";
        text_level.gameObject.SetActive(true);
        text_gameover.gameObject.SetActive(false);
        button_start.gameObject.SetActive(false);
        button_restart.gameObject.SetActive(false);

        GameManager.Instance.GameStart();

    }
    public void GameOver()
    {
        text_gameover.gameObject.SetActive(true);
        button_restart.gameObject.SetActive(true);
    }
    /// <summary>
    /// 下一关
    /// </summary>
    /// <param name="level">关卡shu</param>
    public void NextLevel(int level)
    {
        text_level.text = "关卡" + level;
    }
    /// <summary>
    /// 玩家扣血的UI效果
    /// </summary>
    public void PlayerHeart()
    {
        if (GameManager.Instance.player.Hp > 0)
        {
            Debug.Log(GameManager.Instance.player.Hp);
            heartArray[(int)GameManager.Instance.player.Hp-1].sprite = heartEmpty;
        }
    }
    public void PlayerInit()
    {
        for (int i = 0; i < heartArray.Length; i++)
        {
            heartArray[i].sprite = heartFull;
        }

    }
}
