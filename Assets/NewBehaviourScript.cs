using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.SceneManagement; using UnityEngine.UI;   public class NewBehaviourScript : MonoBehaviour {      public GameObject shpere;// Start is called before the first frame update     void OnCollisionEnter(Collision Other)     {
        // GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(shpere); // destroy the grenade         Timer.startTime += 5; 
    }  }
