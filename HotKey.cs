using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace AwkEverywhere
{
	/// <summary>
	/// Description résumée de Hotkey.
	/// </summary>
	public class HotKey : NativeWindow
	{
		private const int WM_HOTKEY = 0x312;

		/// <summary>
		/// HotKeyModifiers énumération
		/// </summary>
		[Flags]
		public enum HotKeyModifiers : int {
			Alt = 0x1,
			Control = 0x2,
			Shift = 0x4,
			Win = 0x8
		}

		private struct HotKeyData {
			public Keys Key;
			public HotKeyModifiers Modifiers;
			public IntPtr AtomID;

			public static HotKeyData Empty = new HotKeyData();

			public HotKeyData(Keys key, HotKeyModifiers modifiers, IntPtr atomId) {
				this.Key = key;
				this.Modifiers = modifiers;
				this.AtomID = atomId;
			}

			public override string ToString() {
				return Modifiers.ToString() + "+" + Key.ToString();
			}

			public override int GetHashCode() {
				return base.GetHashCode ();
			}

			public override bool Equals(object obj) {
				return this.AtomID.Equals (obj);
			}

		}


		private ArrayList hotkeys;
		private Form owner;

		public delegate void HotKeyHandler(object sender, HotKeyArgs args);
		public event HotKeyHandler HotKeyPress;

		public HotKey(Form owner) {
			this.AssignHandle(owner.Handle);
			this.owner = owner;
			owner.HandleCreated += new EventHandler(owner_HandleCreated);

			hotkeys = new ArrayList();
		}

		public void RegisterHotKey(Keys key, HotKeyModifiers modifiers) {
			if (!FindHotKey(key, modifiers).Equals(HotKeyData.Empty)) {
				this.registerHotKey(key, modifiers);
			}
		}

		public void UnregisterHotKey(Keys key, HotKeyModifiers modifiers) {
			HotKeyData hkData = FindHotKey(key, modifiers);

			if (!hkData.Equals(HotKeyData.Empty)) {
				unregisterHotKey(hkData);
				hotkeys.Remove(hkData);
			}
		}

		private HotKeyData FindHotKey(Keys key, HotKeyModifiers modifiers) {
			HotKeyData hkData;
			for (int i=0; i<this.hotkeys.Count; i++) {
				hkData = (HotKeyData)hotkeys[i];
				if (hkData.Key == key && hkData.Modifiers == modifiers) {
					return hkData;
				}
			}

			return HotKeyData.Empty;
		}

		protected override void WndProc(ref Message m) {
			base.WndProc (ref m);
			HotKeyData hkData;
			if (m.Msg == WM_HOTKEY) {
				for (int i=0; i<hotkeys.Count; i++) {
					hkData = (HotKeyData)hotkeys[i];
					if (hkData.Equals(m.WParam)) {
						if (this.HotKeyPress != null) {
							HotKeyPress(this.owner,
							            new HotKeyArgs(hkData.Key, hkData.Modifiers));
						}
						break;
					}
				}
			}
		}


		/// <summary>
		/// Destructeur de la classe.
		/// Il faut libérer les raccourcis créés
		/// </summary>
		~HotKey() {
			HotKeyData hkData;
			for (int i=hotkeys.Count-1; i>=0; i--) {
				hkData = (HotKeyData)hotkeys[i];
				this.unregisterHotKey(hkData);
			}
		}

		#region P/Invoke
		[DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		private static extern IntPtr GlobalAddAtom(string lpString);

		[DllImport("kernel32.dll", SetLastError=true, ExactSpelling=true)]
		private static extern IntPtr GlobalDeleteAtom(IntPtr nAtom);

		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
		#endregion

		private void registerHotKey(Keys key, HotKeyModifiers modifiers) {
			string atomName = string.Empty;
			IntPtr atomId;
			atomName = key.ToString() + modifiers.ToString() + Environment.TickCount.ToString();
			if (atomName.Length > 255) {
				atomName = atomName.Substring(0,255);
			}

			atomId = GlobalAddAtom(atomName);
			if (atomId == IntPtr.Zero) {
				throw new Exception("Impossible d'enregistrer l'atome du raccourci !");
			}

			if (!RegisterHotKey(this.Handle, atomId.ToInt32(), (int)modifiers, (int)key)) {
				GlobalDeleteAtom(atomId);
				throw new Exception("Impossible d'enregistrer le raccourci !");
			}

			this.hotkeys.Add(new HotKeyData(key, modifiers, atomId));
		}

		private void unregisterHotKey(HotKeyData hk) {
			UnregisterHotKey(this.Handle, hk.AtomID.ToInt32());
			GlobalDeleteAtom(hk.AtomID);
		}


		private void owner_HandleCreated(object sender, EventArgs e) {
			this.AssignHandle(owner.Handle);
		}
	}

	#region Classe HotKeyArgs
	public class HotKeyArgs : EventArgs {
		public HotKeyArgs(Keys key, HotKey.HotKeyModifiers modifiers) {
			this.modifiers = modifiers;
			this.key = key;
		}

		private Keys key;
		public Keys Key {
			get {return key;}
		}

		private HotKey.HotKeyModifiers modifiers;
		public HotKey.HotKeyModifiers Modifiers {
			get {return modifiers;}
		}
	}
	#endregion
}