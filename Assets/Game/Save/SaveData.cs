namespace Game.Save {
	[System.Serializable]
	public class GameData {

		public SavePlot[] plots;

		public GameData() {
			plots = new SavePlot[0];
		}
	}
}