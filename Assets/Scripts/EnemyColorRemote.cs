using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EnemyColorRemote : MonoBehaviour
{

	//We probably only need to keep the ones that will actually be called.
	EnemyState curState;


	public Renderer cone;
    public SpriteRenderer eye;
    public GameObject idleParticles;
    public GameObject alertParticles;
    public Color idleColor = new Color (0.255f, 0.722f, 0.506f);
    public Color alertColor = new Color (0.831f, 0.118f, 0.337f);
    public EnemyState currentState;
    public PhotonView photonView;

     void Awake() {
     	photonView = PhotonView.Get(this);
     	Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/FakeVolumetricLightShader");
        currentState = EnemyState.Inactive;
        eye.color = idleColor;
		idleParticles.SetActive(true);
        alertParticles.SetActive(false);
     }  


     [PunRPC]
     void EnemySetColor(EnemyState eState)
     {
     	currentState = eState;
        switch (eState)
        {
        	//Note, we aren't going to send this for every state change, only the ones that matter
        	//If we did it for all of them, this would be a shitton of calls
        	case EnemyState.Search:
        		cone.material.SetColor("_MyColor", idleColor);
                eye.color = idleColor;
				idleParticles.SetActive(true);
                alertParticles.SetActive(false);
        		break;
        	case EnemyState.Wander:
				cone.material.SetColor("_MyColor", alertColor);
                eye.color = alertColor;
				idleParticles.SetActive(false);
                alertParticles.SetActive(true);
        		break;
        	default:
        		break;
        }
    }

    public void RemoteSetColor(EnemyState eState){
        photonView.RPC("EnemySetColor", PhotonTargets.All, eState);
    }
}