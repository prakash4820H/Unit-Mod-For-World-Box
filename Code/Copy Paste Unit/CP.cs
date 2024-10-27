using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class CP : MonoBehaviour
    {
        private Actor controlledUnit = null;
        private bool isCameraLocked = false;

        public void Awake()
        {
            HarmonyPatchSetup();
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Ctrl + C detected");
                CopyUnit(ClosestActorToTile(MapBox.instance.getMouseTilePos(), 3f, null));
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
            {
                Debug.Log("Ctrl + V detected");
                PasteUnit(MapBox.instance.getMouseTilePos(), selectedUnitToPaste);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
            {
                showHideCopyPaste = !showHideCopyPaste;
            }
            if (Input.GetKeyDown(KeyCode.L) && selectedUnitToPaste != null)
            {
                Debug.Log("Locking camera to unit: " + selectedUnitToPaste.data.name);
                controlledUnit = ClosestActorToTile(MapBox.instance.getMouseTilePos(), 3f, null);
                isCameraLocked = true;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Unlocking camera");
                controlledUnit = null;
                isCameraLocked = false;
            }

            if (isCameraLocked && controlledUnit != null)
            {
                ControlUnit();
                Camera.main.transform.position = new Vector3(controlledUnit.currentPosition.x, controlledUnit.currentPosition.y, Camera.main.transform.position.z);
            }

            mainWindowRect.height = 0f;
        }

        private void ControlUnit()
        {
            Vector2 direction = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }

            if (direction != Vector2.zero)
            {
                float movementSpeed = 2f; // Define a reasonable movement speed
                Vector2 newPosition = controlledUnit.currentPosition + (direction.normalized * movementSpeed * Time.deltaTime);
                controlledUnit.currentPosition = newPosition;
                controlledUnit.updatePos();
            }
        }

        public static void PasteUnit(WorldTile targetTile, UnitData unitData)
        {
            Debug.Log("Attempting to paste unit");
            if (targetTile != null && unitData != null)
            {
                Debug.Log($"Creating new unit with asset_id: {unitData.asset_id} at tile ({targetTile.x}, {targetTile.y})");
                Actor actor = World.world.units.createNewUnit(unitData.asset_id, targetTile);
                if (actor != null)
                {
                    Debug.Log("New unit created successfully");

                    if (actor.data.traits != null && actor.data.traits.Count >= 1)
                    {
                        actor.data.traits.Clear();
                    }
                    if (unitData.equipment != null)
                    {
                        actor.equipment = unitData.equipment;
                    }
                    if (unitData.data != null)
                    {
                        CopyActorData(unitData.data, actor.data);
                    }
                    foreach (string pTrait in unitData.traits)
                    {
                        actor.addTrait(pTrait, false);
                    }
                    actor.setStatsDirty();
                    actor.restoreHealth(3);

                    if (unitData.data != null)
                    {
                        Debug.Log("Pasted " + unitData.data.name);
                    }
                }
                else
                {
                    Debug.LogError("Failed to create new unit");
                }
            }
            else
            {
                Debug.Log("TargetTile or UnitData is null");
            }
        }

        private static void CopyActorData(ActorData source, ActorData destination)
        {
            destination.created_time = source.created_time;
            destination.culture = source.culture;
            destination.children = source.children;
            destination.diplomacy = source.diplomacy;
            destination.experience = source.experience;
            destination.favoriteFood = source.favoriteFood;
            destination.name = source.name + " Pasted";
            destination.gender = source.gender;
            destination.head = source.head;
            destination.homeBuildingID = source.homeBuildingID;
            destination.hunger = source.hunger;
            destination.intelligence = source.intelligence;
            destination.kills = source.kills;
            destination.level = source.level;
            destination.mood = source.mood;
            destination.profession = source.profession;
            destination.skin_set = source.skin_set;
            destination.skin = source.skin;
            destination.asset_id = source.asset_id;
            destination.stewardship = source.stewardship;
            destination.warfare = source.warfare;
            destination.inventory = source.inventory;
            destination.items = source.items;
            destination.clan = source.clan;
        }

        public void OnGUI()
        {
            if (showHideCopyPaste)
            {
                mainWindowRect = GUILayout.Window(500401, mainWindowRect, UnitClipboardWindow, "Unit Clipboard", GUILayout.MaxWidth(300f), GUILayout.MinWidth(200f));
            }
            if (GUILayout.Button("Unit Clipboard"))
            {
                showHideCopyPaste = !showHideCopyPaste;
            }
        }

        public static Actor ClosestActorToTile(WorldTile pTarget, float range, Actor excludingActor = null)
        {
            Actor result = null;
            foreach (Actor actor in MapBox.instance.units)
            {
                float distance = Toolbox.Dist(actor.currentPosition.x, actor.currentPosition.y, (float)pTarget.pos.x, (float)pTarget.pos.y);
                if (distance < range && actor != excludingActor)
                {
                    range = distance;
                    result = actor;
                }
            }
            return result;
        }

        public void UnitClipboardWindow(int windowID)
        {
            if (unitClipboardDict.Count >= 1)
            {
                for (int i = 0; i < unitClipboardDict.Count; i++)
                {
                    if (unitClipboardDict.ContainsKey(i.ToString()))
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(unitClipboardDict[i.ToString()].data.name))
                        {
                            selectedUnitToPaste = unitClipboardDict[i.ToString()];
                        }
                        GUILayout.EndHorizontal();
                    }
                }
                if (GUILayout.Button("Clear clipboard"))
                {
                    unitClipboardDict.Clear();
                    unitClipboardDictNum = 0;
                }
            }
            GUI.DragWindow();
        }

        private void CopyUnit(Actor targetActor)
        {
            if (targetActor != null)
            {
                Debug.Log("Attempting to copy unit");
                ActorData data = targetActor.data;
                targetActor.prepareForSave();
                UnitData unitData = new UnitData
                {
                    traits = new List<string>(data.traits),
                    asset_id = targetActor.asset.id,
                    equipment = targetActor.equipment,
                    data = new ActorData
                    {
                        traits = data.traits,
                        created_time = data.created_time,
                        culture = data.culture,
                        children = data.children,
                        diplomacy = data.diplomacy,
                        experience = data.experience,
                        favoriteFood = data.favoriteFood,
                        name = data.name,
                        gender = data.gender,
                        head = data.head,
                        homeBuildingID = data.homeBuildingID,
                        hunger = data.hunger,
                        intelligence = data.intelligence,
                        kills = data.kills,
                        level = data.level,
                        mood = data.mood,
                        profession = data.profession,
                        skin_set = data.skin_set,
                        skin = data.skin,
                        asset_id = data.asset_id,
                        stewardship = data.stewardship,
                        warfare = data.warfare,
                        inventory = data.inventory,
                        items = data.items,
                        clan = data.clan
                    },
                    customData = targetActor.data
                };
                unitClipboardDict.Add(unitClipboardDictNum.ToString(), unitData);
                unitClipboardDictNum++;
                selectedUnitToPaste = unitData;
                Debug.Log("Copied " + targetActor.data.name);
            }
        }

        public void HarmonyPatchSetup()
        {
            var harmony = new Harmony("com.yourmod.harmonypatch");
            try
            {
                harmony.PatchAll();
                Debug.Log("Harmony patches applied successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in HarmonyPatchSetup: {e.Message}");
                Debug.LogError($"Stack Trace: {e.StackTrace}");
            }
        }

        public static List<string> addedTraits = new List<string>();
        public static bool showHideCopyPaste;
        public static Rect mainWindowRect = new Rect(0f, 1f, 1f, 1f);
        public static Dictionary<string, UnitData> unitClipboardDict = new Dictionary<string, UnitData>();
        public static int unitClipboardDictNum;
        public static UnitData selectedUnitToPaste;

        public class UnitData
        {
            public string asset_id = "";
            public List<string> traits = new List<string>();
            public ActorEquipment equipment;
            public ActorData data;
            public BaseObjectData customData;
        }
    }
}
