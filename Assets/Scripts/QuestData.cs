using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string npcName;
    public string questName;
    public int[] npcId;

    public QuestData(string nName ,string qName, int[] nId)
    {
        npcName = nName;
        questName = qName;
        npcId = nId;
    }
}
