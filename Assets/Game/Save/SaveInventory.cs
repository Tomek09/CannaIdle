namespace Game.Save {

	[System.Serializable]
	public class SaveInventory {
		public int coins;
		public SaveItem[] inventoryItems;
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