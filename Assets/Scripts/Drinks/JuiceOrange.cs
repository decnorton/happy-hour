using UnityEngine;
using System.Collections;

public class JuiceOrange : Juice {
	
	private static AudioClip sound = Resources.Load ("Sounds/clink_2", typeof(AudioClip)) as AudioClip;
	
	public override AudioClip GetSound() {
		return sound;
	}

}
