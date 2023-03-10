using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trying : MonoBehaviour
{
    public GameObject rotateObject;


    private Quaternion startingRotation;
    public float speed = 5;

    public Spin spin;
    public Spin spinleft;
    public GameObject play;
    public int rotateFlag = 0;
    //public Player player;

    void Start(){
        //save the starting rotation
        startingRotation = this.transform.rotation;
    }

    void Update () {
        

    //go to 90 degrees with right arrow
        if(spin.flag == 1 ){
            rotateFlag = 1;
            StopAllCoroutines();
            StartCoroutine(Rotate(90));
            //spin.flag = 0;
        }   
        if(spinleft.flag == 2 ){
            rotateFlag = 1;
            StopAllCoroutines();
            StartCoroutine(Rotate(-90));
            spinleft.flag = 0;
            //spin.flag = 0;
        }  
    

    }

    IEnumerator Rotate(float rotationAmount){
        Quaternion finalRotation = Quaternion.Euler( 0, 0, rotationAmount ) * startingRotation;
        rotateFlag = 0;
        startingRotation = finalRotation;

        while(rotateObject.transform.rotation != finalRotation){
            rotateObject.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime*speed);
            spin.flag = 0;
            yield return 0;
            rotateFlag = 1;
            //play.GetComponent<Player>().end = 0;
        }
        rotateFlag = 0;
        rotateObject.transform.rotation = finalRotation;
        yield return null;
        //spin.flag = 0;
    }
}
