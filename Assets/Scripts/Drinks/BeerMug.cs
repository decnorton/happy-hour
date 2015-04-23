using UnityEngine;
using System.Collections;

public class BeerMug : Beer {
	
	private static AudioClip sound = Resources.Load ("Sounds/bottle_open", typeof(AudioClip)) as AudioClip;
	
	public override AudioClip GetSound() {
		return sound;
	}

}
