using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MimicStatueAI : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material statueMAT, chaseMAT, mimicMAT;
    private float health = 100f;
    private float maxHealth = 100f;

    private float mimicModeHealthTrigger = 30f;
    private bool isMimicMode = false;

    public Transform player;
    public FirstPersonPlayerMovement playerScript;
    private bool seenByPlayer = false;

    private Dictionary<string, float> actionScores;

    private NavMeshAgent agent;

    public TextMeshProUGUI statueText, chaseText, mimicText, healthText;

    public float viewDistance = 10f;
    public float viewAngle = 45f;

    private void Start()
    {
        actionScores = new Dictionary<string, float>()
        {
            {"Statue", 0f },
            {"Chase", 0f },
            {"Mimic", 0f }
        };
        health = maxHealth;
        gameObject.TryGetComponent(out agent);
        healthText.text = "Health = " + health;
    }

    private void Update()
    {
        if (playerScript.IsPlayerLookingAtMe(gameObject))
            seenByPlayer = true;
        else
            seenByPlayer = false;
        actionScores["Chase"] = 10f * (seenByPlayer == true ? 0 : 1);
        actionScores["Statue"] = 5f;

        statueText.text = "STATUE = " + actionScores["Statue"];
        chaseText.text = "CHASE = " + actionScores["Chase"];
        mimicText.text = "MIMIC = " + actionScores["Mimic"];

        string chosenAction = actionScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        switch (chosenAction)
        {
            case "Statue":
                Statue();
                break;
            case "Chase":
                Chase();
                break;
            case "Mimic":
                Mimic();
                break;
            default:
                break;
        }
    }

    public void GetHit(float hitDamage)
    {
        if (isMimicMode) return;
        health -= hitDamage;
        healthText.text = "Health = " + health;
        if (health <= mimicModeHealthTrigger)
            OnMimicModeStart();
    }

    public void OnMimicModeStart()
    {
        isMimicMode = true;
        actionScores["Mimic"] = 100f;
        agent.isStopped = true;
    }

    private void Statue()
    {
        if (meshRenderer.material != statueMAT)
            meshRenderer.material = statueMAT;
        agent.isStopped = true;
    }

    private void Chase()
    {
        if (meshRenderer.material != chaseMAT)
            meshRenderer.material = chaseMAT;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void Mimic()
    {
        if (meshRenderer.material != mimicMAT)
        {
            meshRenderer.material = mimicMAT;
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(playerPos);
        }
        float verticalInput = playerScript.GetPlayerMoveDirection().x;
        float horizontalInput = playerScript.GetPlayerMoveDirection().z;
        Vector3 moveDirection = transform.forward * horizontalInput + transform.right * -verticalInput;
        transform.rotation = Quaternion.Inverse(playerScript.GetPlayerBodyTransform().rotation);
        //transform.rotation = playerScript.GetPlayerBodyTransform().rotation;
        agent.Move(moveDirection.normalized * Time.deltaTime * 7f);
        //agent.SetDestination(newDestiny);
    }
}
