using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

namespace Game.Localization {

	public enum TableType {
			Plants
	}

	public class LocalizationManager : Singleton<LocalizationManager> {


		[Header("Info")]
		private Dictionary<TableType, StringTable> _stringTables = null;

		private protected override void Awake() {
			base.Awake();
			_stringTables = new Dictionary<TableType, StringTable> {
				{ TableType.Plants, LocalizationSettings.StringDatabase.GetTable(TableType.Plants.ToString()) }
			};
		}

		public string GetEntry(TableType tableType, string entryCode) {
			StringTableEntry entry = _stringTables[tableType].GetEntry(entryCode);
			return entry != null ? entry.Value : string.Format("[{0}]", entryCode);
		}
	}
}