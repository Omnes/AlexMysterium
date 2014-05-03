using UnityEngine;
using System.Collections;

public class ItemUseStates : MonoBehaviour {
	public bool radio = false;
	public bool button = false;
	public bool password = false;
	public bool passcode = false;
	public bool flashlight = false;
	public bool powerout = false;
	public bool card = false;
	public bool foundCode = false;
	public bool drawerUnlocked = false;
	public bool poweroutOcurred = false;
	public bool isLoggedIn = false;
	public ulong radioOffset = 0;
}
