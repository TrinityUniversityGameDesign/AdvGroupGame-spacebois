using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemySoundRemote : MonoBehaviour {

    //We probably only need to keep the ones that will actually be called.
    EnemyState curState;

    private AudioSource audio;
    public AudioClip lowNotice;
    public AudioClip notice;
    public EnemyState currentState;
    public PhotonView photonView;

    void Awake()
    {
        photonView = PhotonView.Get(this);
        audio = GetComponent<AudioSource>();
    }


    [PunRPC]
    void EnemyPlaySound(EnemyState eState)
    {
        currentState = eState;
        switch (eState)
        {
            case EnemyState.Sniff:
                //audio.clip = lowNotice;
                if(!audio.isPlaying)
                {
                    audio.PlayOneShot(lowNotice, 0.02f);
                }
                break;
            case EnemyState.Wander:
                if (!audio.isPlaying)
                {
                    audio.PlayOneShot(notice, 0.02f);
                }
                break;
            default:
                break;
        }
    }

    public void RemotePlaySound(EnemyState eState)
    {
        photonView.RPC("EnemyPlaySound", PhotonTargets.All, eState);
    }
}
