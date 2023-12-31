using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fawn : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && ScoreManager.Instance.IsFoodFull()) {
            GameStateManager.Instance.Victory();
        }
    }
}
