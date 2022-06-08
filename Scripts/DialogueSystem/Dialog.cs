using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using UnityEngine.EventSystems;
using Player.Movement;
using Turnbased.Combat;
using Audio.Sound;

namespace Dialogue
{
    public enum DialogueType {
        TalkOnly,
        TalkAndChoices
    }

    public class Dialog : MonoBehaviour
    {
        public Text nameText;
        public Text dialogText;
        public AudioManager audioManager;
        public float typeSpeed;
        public GameObject dialogBox;
        public GameObject continueBtn;

        [HideInInspector] public DialogueType dialogueType;
        [HideInInspector] [ReadOnly] public int MaxSentences = 0;
        public TalkSetting[] dialog;
        public ChoicesSetting[] choiceSetting;
        private int nameIndex;
        private int choiceIndex;
        private int index;
        private int sentenceCount;
        private Text continueBtnText;
        private Transform dialogContainer;
        private Transform battleContainer;
        private Transform container;
        private GameObject continueBtnClone;
        private GameObject leftBtnClone;
        private GameObject rightBtnClone;

        // button invoke
        private string objectName;
        private bool callNextDialog;

        private void Awake()
        {
            continueBtnText = continueBtn.GetComponent<Text>();
            container = FindObjectOfTransform(dialogBox.transform, "DialogImage");
        }

        private void Start()
        {
            CreateButton("continueBtn", 0, NextSentence);
            continueBtnClone = FindObjectOfTransform(container, "continueBtn").gameObject;

            if (dialogueType == DialogueType.TalkAndChoices) { // create branching button
                CreateButton("rightOption", 0, ChooseChoice);
                CreateButton("leftOption", -100, ChooseChoice);
                leftBtnClone = FindObjectOfTransform(container, "leftOption").gameObject;
                rightBtnClone = FindObjectOfTransform(container, "rightOption").gameObject;
            }
        }

        private void OnEnable()
        {
            continueBtnText.text = "Ä~Äò";
            nameIndex = 0;
            index = 0;
            StartCoroutine(Type());
        }

        IEnumerator Type()
        {
            sentenceCount++;
            nameText.text = dialog[nameIndex].name;

            foreach(char letter in dialog[nameIndex].sentence[index].ToCharArray()) {
                yield return new WaitForSeconds(typeSpeed);
                dialogText.text += letter;
                audioManager.PlayLoopFasting("type", 0f);
            }

            if (nameIndex == dialog.Length - 1 && index == dialog[nameIndex].sentence.Length - 1)
                continueBtnClone.GetComponent<Text>().text = "Ãö³¬";

            continueBtnClone.SetActive(true);

            if (dialogueType == DialogueType.TalkAndChoices && choiceSetting[choiceIndex].sentence == sentenceCount) { // branching
                continueBtnClone.SetActive(false);
                rightBtnClone.SetActive(true);
                rightBtnClone.GetComponent<Text>().text = choiceSetting[choiceIndex].label_right;

                if (choiceSetting[choiceIndex].Multiple) {
                    leftBtnClone.SetActive(true);
                    leftBtnClone.GetComponent<Text>().text = choiceSetting[choiceIndex].label_left;
                }
            }
        }

        public void ChooseChoice()
        {
            var curClickObj = EventSystem.current.currentSelectedGameObject;

            if (curClickObj.name == leftBtnClone.name)
                choiceSetting[choiceIndex].lEvents.Invoke();

            if (curClickObj.name == rightBtnClone.name)
                choiceSetting[choiceIndex].rEvents.Invoke();

            if (choiceIndex < choiceSetting.Length - 1)
                choiceIndex++;
        }

        private void AddListener(Transform parent, string name, UnityEngine.Events.UnityAction call)
        {
            parent = FindObjectOfTransform(parent, name);
            EventTrigger trigger = parent.GetComponent<EventTrigger>();
            List<EventTrigger.Entry> entry = trigger.triggers;
            var pointerDown = new EventTrigger.Entry();
            bool isExist = false;

            for (int i = 0; i < entry.Count; i++) {
                if (entry[i].eventID == EventTriggerType.PointerDown) {
                    pointerDown = entry[i];
                    isExist = true;
                }
            }

            if (!isExist) {
                pointerDown.eventID = EventTriggerType.PointerDown;
                pointerDown.callback.AddListener((e) => call());
                trigger.triggers.Add(pointerDown);
            }
        }

        private void RemoveListeners(Transform parent, string name)
        {
            if (parent != null) {
                parent = FindObjectOfTransform(parent, name);
                EventTrigger trigger = parent.GetComponent<EventTrigger>();
                List<EventTrigger.Entry> entry = trigger.triggers;
                for (int i = 0; i < entry.Count; i++) {
                    entry[i].callback.RemoveAllListeners();
                    trigger.triggers.Remove(entry[i]);
                }
            }
        }

        private void CreateButton(string name,int width, UnityEngine.Events.UnityAction call)
        {
            GameObject button = Instantiate(continueBtn, container);
            button.name = name;
            button.transform.position = new Vector2(button.transform.position.x + width,  button.transform.position.y);
            button.AddComponent<EventTrigger>();
            AddListener(container, name, call);
        }

        public static Transform FindObjectOfTransform(Transform parent, string name)
        {
            if (parent != null) {
                Transform[] childTrans = parent.GetComponentsInChildren<Transform>(true);
                foreach (Transform t in childTrans)
                    if (t.name == name)
                        return t;
            }
            return null;
        }

        private GameObject FindInActiveObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
                if (objs[i].hideFlags == HideFlags.None)
                    if (objs[i].name == name)
                        return objs[i].gameObject;

            return null;
        }

        private void NotLastSentence()
        {
            if (nameIndex < dialog.Length) {
                dialogBox.SetActive(true);
                FindObjectOfType<PlayerMovement>().canMove = false;
                NextSentence();

                if (callNextDialog)
                    NextDialog("Defeat_Dialog");
            } 
        }

        //////////////////////////////////choices////////////////////////////////////
        public void AddIndex(int Index)
        {
            index += Index;
        }

        public void CallAfterBattle(bool call)
        {
            callNextDialog = call;
        }
        
        public void NextSentence()
        {
            if (dialogueType == DialogueType.TalkAndChoices) {
                leftBtnClone.SetActive(false);
                rightBtnClone.SetActive(false);
            }

            continueBtnClone.SetActive(false);

            if (nameIndex == dialog.Length - 1 && index == dialog[nameIndex].sentence.Length - 1) {
                if (dialogueType == DialogueType.TalkAndChoices) {
                    Destroy(leftBtnClone);
                    Destroy(rightBtnClone);
                    RemoveListeners(FindObjectOfTransform(dialogContainer, "CloseBtn"), "CloseBtn");
                }

                dialogBox.SetActive(false);
                gameObject.SetActive(false);
                Destroy(continueBtnClone);
                FindObjectOfType<PlayerMovement>().canMove = true;
            }

            if (index < dialog[nameIndex].sentence.Length - 1) {
                index++;
                dialogText.text = "";
                StopCoroutine(Type());
                StartCoroutine(Type());
            }
            else {
                nameIndex++;
                index = 0;
                dialogText.text = "";

                if (nameIndex < dialog.Length) {
                    StopCoroutine(Type());
                    StartCoroutine(Type());
                }
            }
        }

        public void TeleportPlayer(Transform trans)
        {
            FindObjectOfType<PlayerInfo>().transform.position = trans.position;
            NotLastSentence();
        }

        public void CallBattleSystem(string name)
        {
            battleContainer = GameObject.Find("Boss").transform;
            Battle battleSetting = FindObjectOfTransform(battleContainer, name).GetComponent<Battle>();
            battleSetting.StartBattle();
            battleSetting.start = !battleSetting.start;
            dialogBox.SetActive(false);
            dialogContainer = FindObjectOfType<BattleSystem>().transform;
            AddListener(dialogContainer, "CloseBtn", BattleEnd);
        }

        public void BattleEnd()
        {
            FindObjectOfType<BattleSystem>().CloseDialog();
            NotLastSentence();
        }

        private void DelaySetActive()
        {
            GameObject d = FindInActiveObjectByName(objectName);
            d.SetActive(true);
            d.GetComponent<Dialog>().dialogBox.SetActive(true);
        }

        public void NextDialog(string name)
        {
            objectName = name;
            int count = FindObjectOfType<GlobalCounter>().defeatCount;

            if (count == 12) { // all boss
                FindObjectOfType<PlayerMovement>().canMove = false;
                Invoke(nameof(DelaySetActive), 3f);
            }
        }

        private IEnumerator WorldExplosion(GameObject obj)
        {
            Animator explosion = obj.GetComponent<Animator>();
            audioManager.Play("explosion", 0.1f);

            while (true) {
                AnimatorStateInfo info = explosion.GetCurrentAnimatorStateInfo(0);

                if (info.normalizedTime > 0.5f) {
                    GameObject bossObjParent = FindInActiveObjectByName("Boss");
                    GameObject LandObjParent = FindInActiveObjectByName("Land");
                    bossObjParent.SetActive(false);
                    LandObjParent.SetActive(false);
                }

                if (info.normalizedTime >= 0.9f) {
                    obj.SetActive(false);
                    NextSentence();
                    StopCoroutine(WorldExplosion(obj));
                }
                yield return null;
            }
        }

        public void Explosion(GameObject explosionObj)
        {
            explosionObj.SetActive(true);
            StartCoroutine(WorldExplosion(explosionObj));
        }
    }
}
