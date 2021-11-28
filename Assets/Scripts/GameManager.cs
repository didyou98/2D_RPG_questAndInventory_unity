using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isAction; // 스페이스바 눌렸나?
    public TalkManager talkManager;
    public GameObject scanObject; // 어떤 Object인가?
    public int talkIndex; // 가져올 딕셔너리 인덱스
    public GameObject DialogPanel;
    public Text dialogText;
    public QuestManager questManager;
    public int npcIndex;
    public GameObject menu;
    public Text questText;
    public GameObject player;


    Inventory inven;
    public GameObject inventoryPannel;
    bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;
    private void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPannel.SetActive(activeInventory);
    }
    private void Update()
    {

        questText.text = questManager.CheckQuest();
        if (Input.GetButtonDown("Cancel"))
        {
            if(menu.activeSelf)
            {
                TimeScale();
                menu.SetActive(false);
            } else
            {
                isAction = true;
                Time.timeScale = 0f;
                menu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("인벤 창이 열린다");
            activeInventory = !activeInventory;
            inventoryPannel.SetActive(activeInventory);
        }
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();
        menu.SetActive(false);
        Debug.Log("Saved!!");
        // SAVE VARIABLE
        // PLAYER.X
        // PLAYER.Y
        // QUEST ID
        // QUEST ACTION INDEX

    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void TimeScale()
    {
        Time.timeScale = 1f;
        isAction = false;
    }
    public void Action(GameObject scanObj)
    {
        isAction = true;
        scanObject = scanObj;
        ObjectData objData = scanObject.GetComponent<ObjectData>();
        Talk(objData.id, objData.isNPC);
        if (isAction) 
            DialogPanel.SetActive(true);
        else
            DialogPanel.SetActive(false);
    }

    void Talk(int id, bool isNPC)
    {
        // 퀘스트 인덱스 
        int questTalkIndex = 0;
        string talkData = "";
        questTalkIndex = questManager.GetQuestTalkIndex(id);
        talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        dialogText.text = talkData;
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        isAction = true;
        talkIndex++;
    }



    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            if (i < inven.SlotCnt)
                slots[i].GetComponentInChildren<Image>().enabled = true;
            else
                slots[i].GetComponentInChildren<Image>().enabled = false ;
        }
    }


    public void AddSlot()
    {
        Debug.Log("칸 늘리기가 눌렸다");
        Debug.Log(inven.SlotCnt);
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inven.items.Count; i++)
        {

            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
