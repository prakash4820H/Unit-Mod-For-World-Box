namespace Unit
{
    class UnitTraitGroup
    {

        public static string Unit = "Unit";

        public static void init()
        {

            ActorTraitGroupAsset Unit = new ActorTraitGroupAsset();
            Unit.id = "Unit";
            Unit.name = "Unit Mod Section";
            Unit.color = Toolbox.makeColor("#008000", -1f);
            AssetManager.trait_groups.add(Unit);
            LocalizationUtility.addTraitToLocalizedLibrary(Unit.id, "Unit Mod");

        }
    }
}
