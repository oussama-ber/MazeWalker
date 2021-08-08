using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    #region Variables
    public int DNALength = 2;
    public DNA dna;
    public GameObject eyes; 
    bool seeWall = true;
    Vector3 startPosition;
    public float distanceTravelled = 0;
    bool alive = true;
    #endregion

    public void Init()
    {
        //initialise DNA
            //0 forward
            //1 angle turn
        dna = new DNA(DNALength, 360);
        startPosition = this.transform.position;
    }
    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            distanceTravelled = 0;
            alive = false;
        }
    }

    void Update()
    {
        if (!alive) return;

        seeWall = false;
        RaycastHit hit;
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 0.5f, Color.red);
        if( Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out hit, 0.5f))
        {
            if(hit.collider.gameObject.tag == "wall")
            {
                seeWall = true;
            }
        }

    }
    private void FixedUpdate()
    {
        if (!alive) return;
        //read DNA
        float h = 0;
        float v = dna.GetGene(0);

        if (seeWall)
            h = dna.GetGene(1);
        this.transform.Translate(0, 0, v * 0.0008f);
        this.transform.Rotate(0, h, 0);
        distanceTravelled = Vector3.Distance(startPosition, this.transform.position);
    }
}
