using System;
using System.Collections.Generic;
using UnityEngine;

namespace flanne
{
    public class AIController : MonoBehaviour
    {
        private void Awake()
        {
            AIController.SharedInstance = this;
            this.aiComponents = new List<AIComponent>();
        }

        private void Start()
        {
            this.OP = ObjectPooler.SharedInstance;
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < this.aiComponents.Count; i++)
            {
                if (this.aiComponents[i].gameObject.activeInHierarchy && !this.aiComponents[i].frozen)
                {
                    AIComponent aicomponent = this.aiComponents[i];
                    Vector2 vector = this.followTransform.position - aicomponent.transform.position;
                    if (this.playerRepel)
                    {
                        Vector2 vector2 = -1f * vector;
                        if (Vector3.Dot(aicomponent.moveComponent.vector, vector2.normalized) < aicomponent.maxMoveSpeed)
                        {
                            aicomponent.moveComponent.vector += vector2.normalized * aicomponent.acceleration * Time.fixedDeltaTime;
                        }
                    }
                    else if (vector.sqrMagnitude < aicomponent.specialRangeSqr || aicomponent.specialTimer > 0f)
                    {
                        if (!aicomponent.dontFaceDuringSpecial)
                        {
                            this.AILookTowards(aicomponent, vector);
                        }
                        if (aicomponent.specialTimer <= 0f)
                        {
                            aicomponent.specialTimer += aicomponent.specialCooldown;
                            aicomponent.specialSO.Use(aicomponent, this.followTransform);
                            if (!aicomponent.dontLookAtPlayer)
                            {
                                this.AILookTowards(aicomponent, vector);
                            }
                        }
                        aicomponent.specialTimer -= Time.fixedDeltaTime;
                    }
                    else
                    {
                        if (!aicomponent.dontLookAtPlayer)
                        {
                            this.AILookTowards(aicomponent, vector);
                        }
                        Vector2 vector3 = Vector2.zero;
                        Transform closestAI = this.GetClosestAI(aicomponent);
                        if (closestAI != null && !aicomponent.ignoreFlock)
                        {
                            vector3 = aicomponent.transform.position - closestAI.position;
                        }
                        else
                        {
                            vector3 = vector;
                        }
                        if (Vector3.Dot(aicomponent.moveComponent.vector, vector3.normalized) < aicomponent.maxMoveSpeed)
                        {
                            aicomponent.moveComponent.vector += vector3.normalized * aicomponent.acceleration * Time.fixedDeltaTime;
                        }
                    }
                }
            }
        }

        public void Register(AIComponent ai)
        {
            this.aiComponents.Add(ai);
        }

        private void AILookTowards(AIComponent ai, Vector2 direction)
        {
            if (ai.rotateTowardsPlayer)
            {
                float num = Mathf.Atan2(direction.y, direction.x) * 57.29578f;
                ai.transform.rotation = Quaternion.AngleAxis(num, Vector3.forward);
                return;
            }
            if (direction.x < 0f)
            {
                ai.transform.localScale = new Vector2(-1f, 1f);
                return;
            }
            if (direction.x > 0f)
            {
                ai.transform.localScale = new Vector2(1f, 1f);
            }
        }

        private Transform GetClosestAI(AIComponent ai)
        {
            Vector2 vector = ai.transform.position;
            Collider2D[] array = Physics2D.OverlapCircleAll(vector, this.flockDistance);
            Collider2D collider2D = null;
            float num = float.PositiveInfinity;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].gameObject != ai.gameObject && array[i].tag == "Enemy")
                {
                    Vector2 vector2 = array[i].transform.position;
                    float sqrMagnitude = (vector - vector2).sqrMagnitude;
                    if (sqrMagnitude < num)
                    {
                        num = sqrMagnitude;
                        collider2D = array[i];
                    }
                }
            }
            if (collider2D == null)
            {
                return null;
            }
            return collider2D.gameObject.transform;
        }

        public static AIController SharedInstance;

        public bool playerRepel;

        [SerializeField]
        private Transform followTransform;

        [SerializeField]
        private float flockDistance;

        private List<AIComponent> aiComponents;

        private ObjectPooler OP;
    }
}
