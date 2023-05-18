namespace Game.Save {

	[System.Serializable]
	public class SaveInventory {
		public int coins;
		public SaveItem[] inventoryItems;

		public SaveInventory() {
			coins = 0;
			inventoryItems = new SaveItem[0];
		}
	}

	[System.Serializable]
	public class SaveItem {
		public string itemCode;
		public int quantity;

		public SaveItem(string itemCode, int quantity) {
			this.itemCode = itemCode;
			this.quantity = quantity;
		}
	}
}