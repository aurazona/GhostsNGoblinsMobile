using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    public bool IsRight;
    public GameObject Self;
    public int DespawnTimer;
    public int Speed = 7;
    // Start is called before the first frame update
    void Start()
    {
        IsRight = PlayerScript.IsRight;
    }

    // Update is called once per frame
    void Update()
    {

        if(IsRight == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //flips the spear to face the right
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        if(IsRight == false)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //flips the spear to face the left
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        DespawnTimer++;
        if(DespawnTimer == 120)
        {
            Destroy(Self);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hazard"))
        {
            Destroy(Self);
        }
    }
}
