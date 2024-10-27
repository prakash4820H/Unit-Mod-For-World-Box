using ReflectionUtility;

namespace Unit
{

    class UnitStatusLibrary
    {

        public static void Init()
        {
            StatusEffect Stun = new StatusEffect();
            Stun.id = "stun";
            Stun.name = "stun";
            Stun.description = "Got stunned";
            Stun.duration = 12f;
            //	Stun.animated = true;
            //	Stun.animation_speed = 0.1f;
            //	Stun.animation_speed_random = 0.08f;
            //	Stun.random_frame = true;
            Stun.cancel_actor_job = true;
            Stun.random_flip = true;
            Stun.path_icon = "ui/Icons/iconStun";
            Stun.base_stats[S.fertility] = -3.3f;
            Stun.base_stats[S.knockback_reduction] = 1f;
            Stun.base_stats[S.armor] = -20f;
            Stun.base_stats[S.speed] = -10000f;
            Stun.base_stats[S.attack_speed] = -10000f;
            Stun.action_interval = 0.3f;
            Stun.opposite_status.Add("Chillin");
            Stun.action = new WorldAction(NotAttackEffect);
            Stun.remove_status.Add("Chillin");
            Stun.tier = StatusTier.Advanced;
            //	Stun.texture = "BF_t";
            AssetManager.status.add(Stun);
            LocalizationUtility.addTraitToLocalizedLibrary(Stun.id, "Got stunned");


            StatusEffect FE = new StatusEffect();
            FE.id = "FE";
            FE.name = "FE";
            FE.description = "Got FEned";
            FE.duration = 100f;
            FE.path_icon = "ui/Icons/iconBurningFeet";
            FE.animated = true;
            FE.action = new WorldAction(NotAttackEffect);
            FE.tier = StatusTier.Advanced;
            FE.texture = "FE";
            AssetManager.status.add(FE);
            LocalizationUtility.addTraitToLocalizedLibrary(FE.id, "Got FEned");


            StatusEffect TRage = new StatusEffect();
            TRage.id = "tempRage";
            TRage.name = "tempRage";
            TRage.animated = false;
            TRage.description = "Short Temper";
            TRage.duration = 12f;
            TRage.path_icon = "ui/Icons/iconSoulRage";
            TRage.base_stats[S.damage] = 25f;
            TRage.base_stats[S.knockback_reduction] = 1f;
            TRage.base_stats[S.armor] = 40f;
            TRage.base_stats[S.speed] = 125f;
            TRage.base_stats[S.attack_speed] = 220f;
            TRage.tier = StatusTier.Advanced;
            AssetManager.status.add(TRage);
            LocalizationUtility.addTraitToLocalizedLibrary(TRage.id, "Short Temper");


            StatusEffect Chillin = new StatusEffect();
            Chillin.id = "Chillin";
            Chillin.name = "Chillin";
            Chillin.description = "Just Chillin";
            Chillin.duration = 400f;
            Chillin.animated = false;
            Chillin.path_icon = "ui/Icons/iconChillin";
            Chillin.opposite_status.Add("slowness");
            Chillin.opposite_status.Add("cough");
            Chillin.opposite_status.Add("ash_fever");
            Chillin.opposite_status.Add("frozen");
            Chillin.opposite_status.Add("burning");
            Chillin.opposite_status.Add("poisoned");
            Chillin.opposite_status.Add("stun");
            Chillin.remove_status.Add("slowness");
            Chillin.remove_status.Add("cough");
            Chillin.remove_status.Add("ash_fever");
            Chillin.remove_status.Add("frozen");
            Chillin.remove_status.Add("burning");
            Chillin.remove_status.Add("poisoned");
            Chillin.remove_status.Add("stun");
            Chillin.tier = StatusTier.Advanced;
            AssetManager.status.add(Chillin);
            LocalizationUtility.addTraitToLocalizedLibrary(Chillin.id, "Just Chillin");


            StatusEffect Aura_Pink = new StatusEffect();
            Aura_Pink.id = "Aura_Pink";
            Aura_Pink.texture = "Aura_Pink";
            Aura_Pink.path_icon = "ui/Icons/iconSoulRage";
            Aura_Pink.animated = true;
            Aura_Pink.animation_speed = 0.15f;
            Aura_Pink.duration = 10f;
            Aura_Pink.base_stats[S.scale] = 0.0f;
            Aura_Pink.name = "Aura_Pink";
            Aura_Pink.description = "Aura_pink";
            AssetManager.status.add(Aura_Pink);
            LocalizationUtility.addTraitToLocalizedLibrary(Aura_Pink.id, "Aura_pink");


            StatusEffect Torchwood2 = new StatusEffect();
            Torchwood2.id = "Torchwood2";
            Torchwood2.name = "Torchwood2";
            Torchwood2.path_icon = "ui/Icons/iconPlantFood";
            Torchwood2.duration = 0.03f;
            Torchwood2.description = "Torch Wood";
            Torchwood2.action = new WorldAction(Torchwood_2);
            Torchwood2.action_interval = 0.2f;
            AssetManager.status.add(Torchwood2);
            LocalizationUtility.addTraitToLocalizedLibrary(Torchwood2.id, "Torchwood2");

            //  TRYING TO EDIT EXISTING STATUSES THINGS

            StatusEffect frozen = AssetManager.status.get("frozen");
            frozen.opposite_status.Add("Chillin");

            StatusEffect slowness = AssetManager.status.get("slowness");
            slowness.opposite_status.Add("Chillin");

            StatusEffect ash_fever = AssetManager.status.get("ash_fever");
            ash_fever.id = "ash_fever";
            ash_fever.opposite_status.Add("Chillin");

            StatusEffect cough = AssetManager.status.get("cough");
            cough.opposite_status.Add("Chillin");

            StatusEffect burning = AssetManager.status.get("burning");
            burning.opposite_status.Add("Chillin");

            StatusEffect poisoned = AssetManager.status.get("poisoned");
            poisoned.opposite_status.Add("Chillin");

        }

        public static bool Torchwood_2(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (Toolbox.randomChance(0.5f))
            {
                a.animationContainer = ActorAnimationLoader.loadAnimationUnit("actors/t_wolf", a.asset);
            }
            else if (Toolbox.randomChance(0.5f))
                a.checkSpriteHead();
            a.setHeadSprite(null);
            a.has_rendered_sprite_item = false;
            ActorRenderer.drawActor(a);
            return true;
        }

        public static bool NotAttackEffect(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = ReflectionUtility.GetFieldValue<Actor>(pTarget, "a");
            a.startColorEffect(ActorColorEffect.White);
            a.has_status_frozen = true;
            a.ai.setTask("end_job", true, true);
            a.ai.setTask("wait", true, true);
            a.makeWait(10f);
            return true;
        }
    }
}