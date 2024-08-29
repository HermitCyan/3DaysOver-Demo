using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public GameObject gameUI; // 巴什博弈游戏的UI
    public GameObject explorationUI; // 探索模式的UI
    public Text chipCountText; // 显示棋子数量的文本
    public Text npcMoveText;
    public Text resultText; // 显示游戏结果的文本
    public GameObject restartButton; // 重新开始按钮
    public Button[] takeChipButtons; // 玩家选择拿棋子的按钮
    public GameObject quitButton;

    private int chipCount = 21; // 初始棋子数量
    private bool isPlayerTurn = true; // 是否为玩家回合

    void Start()
    {
        UpdateChipCountText(); // 初始化显示棋子数量
        explorationUI.SetActive(true);
        gameUI.SetActive(false);
    }

    public void StartGame()
    {
        // 切换到游戏模式
        explorationUI.SetActive(false); // 关闭探索模式UI
        gameUI.SetActive(true); // 打开游戏UI
        resultText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
        FindObjectOfType<PlayerMovement>().enabled = false; // 禁止主角移动
    }

    public void RestartGame()
    {
        resultText.gameObject.SetActive(false); // 隐藏游戏结果
        restartButton.SetActive(false); // 隐藏重新开始按钮
        ResetGame(); // 重置游戏状态
        StartGame(); // 重新开始游戏
    }

    public void QuitGame()
    {
        EndGame();
        quitButton.SetActive(false);
    }

    public void EndGame()
    {
        explorationUI.SetActive(true);
        gameUI.SetActive(false);
        FindObjectOfType<PlayerMovement>().enabled = true; // 允许主角移动
        npcMoveText.text = ""; // 重置NPC拿棋子数量显示
    }


    public void OnTakeChips(int chips)
    {
        if (chips >= 1 && chips <= 3 && chipCount > 0)
        {
            chipCount -= chips;
            UpdateChipCountText();
            CheckGameOver();
            if (chipCount > 0)
            {
                isPlayerTurn = false;
                NPCMove();
            }
        }
    }


    void NPCMove()
    {
        int npcChips = DetermineNPCMove();
        chipCount -= npcChips;
        UpdateChipCountText();
        UpdateNPCMoveText(npcChips); // 更新NPC拿棋子数量显示
        CheckGameOver();
        if (chipCount > 0)
        {
            isPlayerTurn = true;
        }
    }


    void UpdateNPCMoveText(int npcChips)
    {
        npcMoveText.text = "NPC 拿了: " + npcChips + " 棋子";
    }


    int DetermineNPCMove()
    {
        int maxChips = Mathf.Min(3, chipCount); // NPC最多能拿3颗或剩余的所有棋子
        int npcChips = Random.Range(1, maxChips + 1); // 在1到maxChips之间随机选择
        return npcChips;
    }

    void UpdateChipCountText()
    {
        chipCountText.text = "剩余棋子: " + chipCount;
    }

    void CheckGameOver()
    {
        if (chipCount <= 0)
        {
            if (isPlayerTurn)
            {
                resultText.text = "你赢了！";
                resultText.gameObject.SetActive(true); // 显示胜利结果
                quitButton.SetActive(true);
            }
            else
            {
                resultText.text = "你输了！";
                resultText.gameObject.SetActive(true); // 显示失败结果
                restartButton.SetActive(true); // 显示重新开始按钮
            }
            EndGame(); // 结束游戏
        }
    }


    void ResetGame()
    {
        chipCount = 21;
        isPlayerTurn = true;
        UpdateChipCountText();
    }
}
