using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Speed = 3;
    GameObject Player;
    public float PosDiff;
    public GameObject Self;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PosDiff = transform.position.x - Player.transform.position.x;

        if(PosDiff > 0) //going right
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //flips the character to face the right
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        if(PosDiff < 0) //going left
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //flips the character to face the left
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("PlayerWeapon"))
        {
            Destroy(Self);
        }
    }
}
