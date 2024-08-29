using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public GameObject gameUI; // ��ʲ������Ϸ��UI
    public GameObject explorationUI; // ̽��ģʽ��UI
    public Text chipCountText; // ��ʾ�����������ı�
    public Text npcMoveText;
    public Text resultText; // ��ʾ��Ϸ������ı�
    public GameObject restartButton; // ���¿�ʼ��ť
    public Button[] takeChipButtons; // ���ѡ�������ӵİ�ť
    public GameObject quitButton;

    private int chipCount = 21; // ��ʼ��������
    private bool isPlayerTurn = true; // �Ƿ�Ϊ��һغ�

    void Start()
    {
        UpdateChipCountText(); // ��ʼ����ʾ��������
        explorationUI.SetActive(true);
        gameUI.SetActive(false);
    }

    public void StartGame()
    {
        // �л�����Ϸģʽ
        explorationUI.SetActive(false); // �ر�̽��ģʽUI
        gameUI.SetActive(true); // ����ϷUI
        resultText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
        FindObjectOfType<PlayerMovement>().enabled = false; // ��ֹ�����ƶ�
    }

    public void RestartGame()
    {
        resultText.gameObject.SetActive(false); // ������Ϸ���
        restartButton.SetActive(false); // �������¿�ʼ��ť
        ResetGame(); // ������Ϸ״̬
        StartGame(); // ���¿�ʼ��Ϸ
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
        FindObjectOfType<PlayerMovement>().enabled = true; // ���������ƶ�
        npcMoveText.text = ""; // ����NPC������������ʾ
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
        UpdateNPCMoveText(npcChips); // ����NPC������������ʾ
        CheckGameOver();
        if (chipCount > 0)
        {
            isPlayerTurn = true;
        }
    }


    void UpdateNPCMoveText(int npcChips)
    {
        npcMoveText.text = "NPC ����: " + npcChips + " ����";
    }


    int DetermineNPCMove()
    {
        int maxChips = Mathf.Min(3, chipCount); // NPC�������3�Ż�ʣ�����������
        int npcChips = Random.Range(1, maxChips + 1); // ��1��maxChips֮�����ѡ��
        return npcChips;
    }

    void UpdateChipCountText()
    {
        chipCountText.text = "ʣ������: " + chipCount;
    }

    void CheckGameOver()
    {
        if (chipCount <= 0)
        {
            if (isPlayerTurn)
            {
                resultText.text = "��Ӯ�ˣ�";
                resultText.gameObject.SetActive(true); // ��ʾʤ�����
                quitButton.SetActive(true);
            }
            else
            {
                resultText.text = "�����ˣ�";
                resultText.gameObject.SetActive(true); // ��ʾʧ�ܽ��
                restartButton.SetActive(true); // ��ʾ���¿�ʼ��ť
            }
            EndGame(); // ������Ϸ
        }
    }


    void ResetGame()
    {
        chipCount = 21;
        isPlayerTurn = true;
        UpdateChipCountText();
    }
}
