using UnityEngine;
using System.Collections;

public class ShotTurquoise : Shot {
	
	private static AudioClip sound = Resources.Load ("Sounds/clink_3", typeof(AudioClip)) as AudioClip;
	
	public override AudioClip GetSound() {
		return sound;
	}
	
}
