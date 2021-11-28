using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    public Dictionary<int, QuestData> questList;

    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    private void FixedUpdate()
    {
       CheckQuestMark();
    }
    void CheckQuestMark()
    {
        foreach (int questid in questList.Keys)
        {
            if (questId == questid)
            {
                if (questList[questId].npcId[1] == 0)
                {
                    GameObject paraent = GameObject.Find(questList[questId].npcName);
                    Transform children = paraent.transform.FindChild("1");
                    children.gameObject.SetActive(true);
                } else if (questList[questId].npcId[1] == 1)
                {
                    GameObject paraent = GameObject.Find(questList[questId].npcName);
                    Transform children = paraent.transform.FindChild("2");
                    children.gameObject.SetActive(true);
                }
                else 
                {
                    Debug.Log("퀘스트 종료");
                    GameObject paraent = GameObject.Find(questList[questId].npcName);
                    Transform children = paraent.transform.FindChild("2");
                    Transform children1 = paraent.transform.FindChild("1");
                    children.gameObject.SetActive(false);
                    children1.gameObject.SetActive(false);
                }
            }
        }
    }
    void GenerateData()
    {
        // 퀘스트 제목과 연관된 NPC
        questList.Add(10, new QuestData("NPC01", "인사하기1", new int[] { 1000 , 0}));
        questList.Add(20, new QuestData("NPC02", "인사하기2", new int[] { 2000 , 0}));
        questList.Add(30, new QuestData("NPC03", "인사하기3", new int[] { 3000 , 0}));
        questList.Add(40, new QuestData("NPC01", "인사하기4", new int[] { 1000 , 1}));
        questList.Add(50, new QuestData("NPC01", "인사하기 클리어", new int[] { 0, -1}));
    } 
    
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    // 현재 퀘스트 정보
    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    // 퀘스트 정보 다음 퀘스트로 넘기기
    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
        {
            questActionIndex++;
        }
        if (questActionIndex == questList[questId].npcId.Length - 1)
        {
            GameObject paraent = GameObject.Find(questList[questId].npcName);
            Transform children = paraent.transform.FindChild("2");
            Transform children1 = paraent.transform.FindChild("1");
            children.gameObject.SetActive(false);
            children1.gameObject.SetActive(false);
            NextQuest();
        }
        return questList[questId].questName;
    }
    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;

    }
}
