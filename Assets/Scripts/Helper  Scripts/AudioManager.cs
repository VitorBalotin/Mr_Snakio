using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{
    public static AudioManager instance;
    public AudioClip pickup_sound, death_sound;

    void Awake(){
        MakeInstance();
    }

    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    public void PlayPickupSound(){
        AudioSource.PlayClipAtPoint(pickup_sound, transform.position);
    }

    public void PlayDeathSound(){
        AudioSource.PlayClipAtPoint(death_sound, transform.position);
    }
}
