using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WSMGameStudio.Settings;

public class DestructibleBox : MonoBehaviour
{
    //public GameObject destroyedversion;
    public GameObject[] brokenPieces;
    public WSMGameStudio.Behaviours.BreakingForce breakingForce;
    public UnityEvent OnBreak;

    private AudioSource _breakSFX;
    private Rigidbody _rigidBody;

    private Collider _collider;
    private Renderer _renderer;



    public bool RemoveBrokenPiecesFromScene = false;
   

    private void OnMouseDown()
    {


        //if (_breakSFX != null)
        //    _breakSFX.Play();

        //_rigidBody.isKinematic = true;

        //////Disable collider to avoid collision with spawned broken pieces
        //if (_collider != null)
        //    _collider.enabled = false;

        ////Make object dissapear
        //_renderer.enabled = false;

        foreach (var piece in brokenPieces)
        {
            GameObject pieceClone = Instantiate(piece, transform.position, transform.rotation);

            Rigidbody rb = pieceClone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(breakingForce.power, transform.position, breakingForce.radius, breakingForce.upForce, ForceMode.Impulse);
            }
        }

        if (OnBreak != null)
            OnBreak.Invoke();

        if (_breakSFX != null)
            Invoke("SelfDestroy", _breakSFX.clip.length);
        else
            Invoke("SelfDestroy", 0f);







        Destroy(gameObject);
    }
}

