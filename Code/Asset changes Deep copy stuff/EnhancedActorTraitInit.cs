using HarmonyLib;
using System.Collections.Generic;

namespace Unit
{
    [HarmonyPatch(typeof(Actor))]
    public class EnhancedActorTraitInit
    {
        public static void Init()
        {
            ActorTrait enhancedTrait = new ActorTrait
            {
                id = "enhanced_actor_trait",
                path_icon = "ui/Icons/icon_enhanced_actor_trait",
                group_id = UnitTraitGroup.Unit,
                action_special_effect = new WorldAction(ApplySpecialEffect),

                //    action_attack_target = new AttackAction(ApplyAttackEffect)
            };
            enhancedTrait.base_stats[S.health] = 120f;
            AssetManager.traits.add(enhancedTrait);
            addTraitToLocalizedLibrary(enhancedTrait.id, "Applies enhanced effects to an actor.");
            PlayerConfig.unlockTrait("enhanced_actor_trait");
        }

        public static bool ApplySpecialEffect(BaseSimObject pTarget = null, WorldTile pTile = null)
        {
            Actor actor = pTarget as Actor;
            if (actor == null || !actor.hasTrait("enhanced_actor_trait"))
                return false;

            EnhancedActorTrait component = actor.gameObject.GetComponent<EnhancedActorTrait>();
            if (component == null)
            {
                component = actor.gameObject.AddComponent<EnhancedActorTrait>();
            }
            return component.ApplySpecialEffect();
        }

        public static bool ApplyAttackEffect(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null || !pSelf.isActor() || !pTarget.isActor())
                return false;

            Actor selfActor = pSelf as Actor;
            Actor targetActor = pTarget as Actor;

            // Apply attack effects using ActionLibrary
            //     ActionLibrary.restoreHealthOnHit(selfActor, targetActor, pTile);
            ActionLibrary.thornsDefense(selfActor, targetActor, pTile);
            ActionLibrary.addBurningEffectOnTarget(selfActor, targetActor, pTile);
            ActionLibrary.addFrozenEffectOnTarget(selfActor, targetActor, pTile);
            ActionLibrary.addPoisonedEffectOnTarget(selfActor, targetActor, pTile);
            ActionLibrary.addSlowEffectOnTarget(selfActor, targetActor, pTile);
            ActionLibrary.castCurses(pSelf, pTarget, pTile);
            ActionLibrary.castFire(pSelf, pTarget, pTile);
            ActionLibrary.castLightning(pSelf, pTarget, pTile);
            ActionLibrary.castTornado(pSelf, pTarget, pTile);

            return true;
        }

        public static void addTraitToLocalizedLibrary(string id, string description)
        {
            string language = LocalizedTextManager.instance.language;
            Dictionary<string, string> localizedText = (Dictionary<string, string>)typeof(LocalizedTextManager).GetField("localizedText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(LocalizedTextManager.instance);
            localizedText.Add("trait_" + id, id);
            localizedText.Add("trait_" + id + "_info", description);
        }
    }
}

