using UnityEngine;
using System.Collections;

public class WineGlass : Wine {
	
	private static AudioClip sound = Resources.Load ("Sounds/clink_4", typeof(AudioClip)) as AudioClip;
	
	public override AudioClip GetSound() {
		return sound;
	}

}