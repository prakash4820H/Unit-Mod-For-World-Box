using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class ThiefActorTrait : MonoBehaviour
    {
        private Actor actor;

        void Start()
        {
            actor = GetComponent<Actor>();
            if (actor != null)
            {
                Debug.Log($"ThiefActorTrait applied to actor: {actor.data.id}");
                StealStrongestEquipment(actor);
            }
        }

        public void StealStrongestEquipment(Actor actor)
        {
            // Find the strongest equipment on the map for each type
            Dictionary<EquipmentType, ItemData> strongestEquipments = FindStrongestEquipmentOnMap();
            foreach (var equipment in strongestEquipments)
            {
                Debug.Log($"Found strongest equipment: {equipment.Key} with value: {GetItemValue(equipment.Value)}");
                // Give the strongest equipment to the actor
                EquipStrongestItem(actor, equipment.Value);
            }
        }

        private Dictionary<EquipmentType, ItemData> FindStrongestEquipmentOnMap()
        {
            Dictionary<EquipmentType, ItemData> strongestEquipments = new Dictionary<EquipmentType, ItemData>();
            Dictionary<EquipmentType, int> highestValues = new Dictionary<EquipmentType, int>();

            foreach (Actor otherActor in World.world.units)
            {
                if (otherActor.equipment == null) continue;

                List<ActorEquipmentSlot> equipmentSlots = ActorEquipment.getList(otherActor.equipment);
                if (equipmentSlots == null) continue;

                foreach (ActorEquipmentSlot slot in equipmentSlots)
                {
                    if (slot.data != null)
                    {
                        ItemAsset itemAsset = AssetManager.items.get(slot.data.id);
                        int itemValue = GetItemValue(slot.data);
                        if (itemAsset != null && (!highestValues.ContainsKey(itemAsset.equipmentType) || itemValue > highestValues[itemAsset.equipmentType]))
                        {
                            highestValues[itemAsset.equipmentType] = itemValue;
                            strongestEquipments[itemAsset.equipmentType] = slot.data;
                        }
                    }
                }
            }

            return strongestEquipments;
        }

        private int GetItemValue(ItemData itemData)
        {
            ItemTools.calcItemValues(itemData);
            return ItemTools.s_value;
        }

        private void EquipStrongestItem(Actor actor, ItemData strongestEquipment)
        {
            ItemAsset itemAsset = AssetManager.items.get(strongestEquipment.id);
            if (itemAsset == null)
            {
                Debug.LogWarning("ItemAsset not found for ItemData.");
                return;
            }

            EquipmentType equipmentType = itemAsset.equipmentType;
            if (actor.equipment == null)
            {
                actor.equipment = new ActorEquipment();
            }

            ActorEquipmentSlot appropriateSlot = actor.equipment.getSlot(equipmentType);
            if (appropriateSlot.data == null || GetItemValue(strongestEquipment) > GetItemValue(appropriateSlot.data))
            {
                appropriateSlot.setItem(strongestEquipment);
                actor.setStatsDirty();
                actor.dirty_sprite_item = true;
                Debug.Log($"Equipped actor: {actor.data.id} with {equipmentType}: {strongestEquipment.id}");
            }
            else
            {
                Debug.Log($"Actor: {actor.data.id} already has a stronger or equal {equipmentType}.");
            }
        }
    }
}
