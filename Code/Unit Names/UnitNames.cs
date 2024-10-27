namespace Unit
{
    class UnitNames
    {
        public static void Init()
        {
            Names();
        }

        public static void Names()
        {

            NameGeneratorAsset dragonslayerName = new NameGeneratorAsset();
            dragonslayerName.id = "dragonslayer_name";
            dragonslayerName.part_groups.Add("Ky");
            dragonslayerName.templates.Add("part_group");
            AssetManager.nameGenerator.add(dragonslayerName);

            NameGeneratorAsset GrandMageName = new NameGeneratorAsset();
            GrandMageName.id = "GrandMage_name";
            GrandMageName.part_groups.Add("Grand Mage");
            GrandMageName.templates.Add("part_group");
            AssetManager.nameGenerator.add(GrandMageName);
        }
    }
}