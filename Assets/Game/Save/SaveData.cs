namespace Game.Save {
	[System.Serializable]
	public class GameData {

		public SaveInventory inventory;
		public SavePlot[] plots;

		public GameData() {
			inventory = new SaveInventory();
			plots = new SavePlot[0];

			inventory.inventoryItems = new SaveItem[] {
				new SaveItem("item_aloeBag", 10),
				new SaveItem("item_gloves_0", 1),
				new SaveItem("item_wateringCan_0", 1)
			};
		}
	}
}