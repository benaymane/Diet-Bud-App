using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransfer : MonoBehaviour {

	public void changeScene( string sceneName ) {
        SceneManager.LoadScene( sceneName );
    }
}
