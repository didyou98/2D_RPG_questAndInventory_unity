using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateTalk();
    }

    void GenerateTalk()
    {
        // 일반 대화
        talkData.Add(1000, new string[] { "안녕~ 난 NPC01이야" , "만나서 반가워"});
        talkData.Add(2000, new string[] { "HI I'M NPC02", "Nice to meet you!" });
        talkData.Add(3000, new string[] { "안녕?", "재미있게 놀다가" });

        // 퀘스트 대화
        talkData.Add(10 + 1000, new string[] { "안녕? ", "내 이름은 지수야", "오른쪽 친구한테도 인사해줘" });
        talkData.Add(20 + 2000, new string[] { "안녕~? 만나서 반가워", "내 이름은 수호야", "저기 밑에있는 친구한테도 인사해줘" });
        talkData.Add(30 + 3000, new string[] { "안녕~? 만나서 반가워", "내 이름은 길동이야", "마을은 어때?" });
        talkData.Add(40 + 1000, new string[] { "인사하고왔어?", "앞으로 잘부탁해!" });
        talkData.Add(50 + 1000, new string[] { " ", " " });
    }

    public string GetTalk(int id, int talkIndex)
    {
        // 다음으로 수행할 대화가 없다면
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                return GetTalk(id - id % 10, talkIndex);
            }
        }
        if (talkIndex == talkData[id].Length)
        {
            return null;
        } else
        {
            return talkData[id][talkIndex];
        }
    }
}
