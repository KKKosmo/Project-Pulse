using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    public EnemyMovement enemyMovement;
    public GameObject counterpartPrefab;
    public override void Awake()
    {
        base.Awake();
        hostileMask = LayerMask.GetMask("Friendly");

    }
    public void Start()
    {
        //Debug.Log("COMING FROM STATUS, " + gameObject.name + " HOSTILE MASK IS " + hostileMask.value);
    }
    protected override void PulseMutate()
    {
        base.PulseMutate();
        StartCoroutine(PulseCalled());
    }
    IEnumerator PulseCalled()
    {
        //enemyMovement.hostileDetectCollider.radius = 0f;
        hostileMask = LayerMask.GetMask("Friendly", "Enemy");
        //enemyMovement.hostileDetectCollider.radius = enemyMovement.aggroRange;
        Debug.Log("WAITING FOR 10");
        yield return new WaitForSeconds(10f);
        Debug.Log("DONE");
        //enemyMovement.hostileDetectCollider.radius = 0f;
        //hostileMask = LayerMask.GetMask("Friendly");
        //enemyMovement.hostileDetectCollider.radius = enemyMovement.aggroRange;

        GameObject counterpart = Instantiate(counterpartPrefab, gameObject.transform.position, gameObject.transform.rotation);
        counterpart.GetComponent<CharacterStatus>().currentHealth = currentHealth;
        counterpart.GetComponent<EnemyMovement>().moveSpeed = GetComponent<EnemyMovement>().moveSpeed;
        counterpart.GetComponent<CharacterMovement>().m_FacingRight = GetComponent<CharacterMovement>().m_FacingRight;
        Destroy(gameObject);
    }
}
