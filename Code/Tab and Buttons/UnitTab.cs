using NCMS.Utils;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    class UnitTab
    {
        public static void Init()
        {
            Debug.Log("UnitTab.Init called.");
        }
        public static PowersTab GetTab(string tabID)
        {
            Transform tabTransform = GameObjects.FindEvenInactive(tabID)?.transform;
            return tabTransform?.GetComponent<PowersTab>();
        }

        public static void CreateTab(string buttonID, string tabID, string name, string desc, int xPos)
        {
            Debug.Log($"Creating tab with buttonID: {buttonID}, tabID: {tabID}, name: {name}, desc: {desc}, xPos: {xPos}");

            GameObject OtherTabButton = GameObjects.FindEvenInactive("Button_Other");
            if (OtherTabButton != null)
            {
                Debug.Log("OtherTabButton found.");

                LocalizationUtility.AddOrSet(buttonID, name);
                LocalizationUtility.AddOrSet($"{buttonID} Description", desc);
                LocalizationUtility.AddOrSet("PrakashUnit_mod_creator", "Made By: Prakash");
                LocalizationUtility.AddOrSet(tabID, name);

                GameObject newTabButton = GameObject.Instantiate(OtherTabButton);
                newTabButton.transform.SetParent(OtherTabButton.transform.parent);
                Button buttonComponent = newTabButton.GetComponent<Button>();
                TipButton tipButton = buttonComponent.gameObject.GetComponent<TipButton>();
                tipButton.textOnClick = buttonID;
                tipButton.textOnClickDescription = $"{buttonID} Description";
                tipButton.text_description_2 = "PrakashUnit_mod_creator";

                newTabButton.transform.localPosition = new Vector3(-290f, 49.57f);
                newTabButton.transform.localScale = new Vector3(1f, 1f);
                newTabButton.name = buttonID;

                var spriteForTab = Resources.Load<Sprite>("ui/Icons/tabIconUnit");
                newTabButton.transform.Find("Icon").GetComponent<Image>().sprite = spriteForTab;

                GameObject OtherTab = GameObjects.FindEvenInactive("Tab_Other");
                foreach (Transform child in OtherTab.transform)
                {
                    child.gameObject.SetActive(false);
                }

                GameObject additionalPowersTab = GameObject.Instantiate(OtherTab);

                foreach (Transform child in additionalPowersTab.transform)
                {
                    if (child.gameObject.name == "tabBackButton" || child.gameObject.name == "-space")
                    {
                        child.gameObject.SetActive(true);
                        continue;
                    }

                    GameObject.Destroy(child.gameObject);
                }

                foreach (Transform child in OtherTab.transform)
                {
                    child.gameObject.SetActive(true);
                }

                additionalPowersTab.transform.SetParent(OtherTab.transform.parent);
                PowersTab powersTabComponent = additionalPowersTab.GetComponent<PowersTab>();
                powersTabComponent.powerButton = buttonComponent;
                powersTabComponent.powerButtons.Clear();

                additionalPowersTab.name = tabID;
                powersTabComponent.powerButton.onClick = new Button.ButtonClickedEvent();
                powersTabComponent.powerButton.onClick.AddListener(() => tabOnClick(tabID));
                Reflection.SetField<GameObject>(powersTabComponent, "parentObj", OtherTab.transform.parent.parent.gameObject);

                additionalPowersTab.SetActive(true);
                powersTabComponent.powerButton.gameObject.SetActive(true);

                Debug.Log("Tab created successfully.");
            }
            else
            {
                Debug.LogError("OtherTabButton not found.");
            }
        }


        public static void tabOnClick(string tabID)
        {
            GameObject AdditionalTab = GameObjects.FindEvenInactive(tabID);
            PowersTab AdditionalPowersTab = AdditionalTab.GetComponent<PowersTab>();

            AdditionalPowersTab.showTab(AdditionalPowersTab.powerButton);
        }
    }
}
