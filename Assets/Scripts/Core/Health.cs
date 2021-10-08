using UnityEngine;

namespace RPG.Core
{

    public class Health : MonoBehaviour

    {
        [SerializeField] float health = 100f;
        bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
            {
                return;
            }

            else
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }

        }


    }


}
