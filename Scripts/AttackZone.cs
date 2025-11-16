using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pig")) // Check if the collided object is tagged as "Enemy"
        {
            NormalPig pig = collision.GetComponent<NormalPig>();
            if (pig != null)
            {
                pig.TakeDamage(20); // Deal 10 damage to the pig

                Debug.Log("Hit");
            }
            PigThrowTheBox throwPig = collision.GetComponent<PigThrowTheBox>();
            if (throwPig != null) 
            {
                throwPig.TakeDamage(20); // Deal 10 damage to the pig
                Debug.Log("Hit Throw Pig");
            }
            PigThrowTheBomb throwBombPig = collision.GetComponent<PigThrowTheBomb>();
            if (throwBombPig != null) 
            {
                throwBombPig.TakeDamage(20); // Deal 10 damage to the pig
                Debug.Log("Hit Throw Bomb Pig");
            }
            KingPig kingPig = collision.GetComponent<KingPig>();
            if(kingPig != null) 
            {
                 kingPig.TakeDamage(20);
                Debug.Log("Hit king pig");
            }
        }
        
    }
}
