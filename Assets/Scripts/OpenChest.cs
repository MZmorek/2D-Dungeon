using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{

    [SerializeField]private Animator chestAnimation;

    private void Start()
    {
        chestAnimation = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chestAnimation.Play("OpeningChest");
        }
    }
}
