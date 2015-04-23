using UnityEngine;
using System.Collections;

public class CocktailMartini : Cocktail {
	
	private static AudioClip sound = Resources.Load ("Sounds/clink_1", typeof(AudioClip)) as AudioClip;
	
	public override AudioClip GetSound() {
		return sound;
	}

}
