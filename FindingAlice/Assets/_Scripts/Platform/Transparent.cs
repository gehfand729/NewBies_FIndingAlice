using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    static public bool require = false;
    private float holdingTime = 5.0f;
    private bool isAction = false;

    public GameObject[] platform;

    private void Awake() {
        // platform = transform.GetChild(0).gameObject;
        platform = GameObject.FindGameObjectsWithTag("Transparent");
    } 
    private void Start(){
        for(int i = 0; i < platform.Length; i++){
            platform[i].SetActive(false);
        }
    }
    
    private void Update() {
        VisibleFunc();
    }

    void VisibleFunc(){
        if(require){
            for(int i = 0; i < platform.Length; i++){
                platform[i].SetActive(true);
            }
            CancelInvoke();
            Invoke("DestroyPlatform", holdingTime);
            // StartCoroutine(TimeLimitFunc());
            require = false;
        }
    }
    // private IEnumerator TimeLimitFunc(){
    //     platform.SetActive(true);
    //     yield return new WaitForSeconds(holdingTime);
    //     platform.SetActive(false);
    // }
    private void DestroyPlatform(){
        for(int i = 0; i < platform.Length; i++){
            platform[i].SetActive(false);
        }
        Debug.Log($"Destroy{this.gameObject.name}");
    }
}