namespace Game.Save {
	[System.Serializable]
	public class GameData {

		public SaveInventory inventory;
		public SavePlot[] plots;

		public GameData() {
			inventory = new SaveInventory();
			plots = new SavePlot[0];
		}
	}
}