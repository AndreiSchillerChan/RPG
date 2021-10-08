using System;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter: MonoBehaviour, IAction
    {   

        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] float weaponDamage = 5f;

        Health target;

        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target == null) return;

            if (target.IsDead())
            {
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<CreateMover>().MoveTo(target.transform.position, 1f);
            }

            else
            {
                GetComponent<CreateMover>().Cancel();
                AttackBehaviour();
            }

        }

        private void AttackBehaviour() 
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                transform.LookAt(target.transform);
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        void Hit()        
        {
            if(target == null) {return;}
            target.TakeDamage(weaponDamage);

        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            
            if (combatTarget == null) { return false; }
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            print("Target Acquired");
            return targetToTest != null && !targetToTest.IsDead();
            
        }

        public void Attack(GameObject combatTarget) 
        {
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.GetComponent<Health>();
            print("Attack Method Started");            
        }


        public void Cancel()
        {
            StopAttack();
            target = null;
            print("Cancelling Attack");
            GetComponent<CreateMover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopTrigger");
        }

    }
}

