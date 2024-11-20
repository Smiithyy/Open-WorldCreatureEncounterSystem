using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] private PartyMemberInfo[] allMembers;
    [SerializeField] private List<PartyMember> currentParty;

    [SerializeField] private PartyMemberInfo defaultPartyMember;

    private Vector3 playerPosition;
    private static GameObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this.gameObject;
            AddMemberToPartyByName(defaultPartyMember.MemberName);
            AddMemberToPartyByName(defaultPartyMember.MemberName);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddMemberToPartyByName(string memberName)
    {
        for (int i = 0; i < allMembers.Length; i++)
        {
            if (allMembers[i].MemberName == memberName)
            {
                PartyMember newPartyMember = new PartyMember();
                newPartyMember.MemberName = allMembers[i].MemberName;
                newPartyMember.Level = allMembers[i].StartingLevel;
                newPartyMember.CurrentHealth = allMembers[i].BaseHealth;
                newPartyMember.MaxHealth = newPartyMember.CurrentHealth;
                newPartyMember.Strength = allMembers[i].BaseStr;
                newPartyMember.Initiative = allMembers[i].BaseInitiative;
                newPartyMember.MemberBattleVisualPrefab = allMembers[i].MemberBattleVisualPrefab;
                newPartyMember.MemberOverworldVisualPrefab = allMembers[i].MemberOverworldVisualPrefab;

                currentParty.Add(newPartyMember);
            }
        }
    }

    public List<PartyMember> GetCurrentParty()
    {
        List<PartyMember> aliveParty = new List<PartyMember>();
        aliveParty = currentParty;
        for (int i = 0; i < aliveParty.Count; i++)
        {
            if(aliveParty[i].CurrentHealth <= 0)
            {
                aliveParty.RemoveAt(i);
            }
        }
        return currentParty;
    }

    public void SaveHealth(int PartyMember, int Health)
    {
        currentParty[PartyMember].CurrentHealth = Health;
    }

    public void SetPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public Vector3 GetPosition()
    {
        return playerPosition;
    }
}

[System.Serializable]
public class PartyMember
{
    public string MemberName;
    public int Level;
    public int CurrentHealth;
    public int MaxHealth;
    public int Strength;
    public int Initiative;
    public int CurrentExp;
    public int MaxExp;
    public GameObject MemberBattleVisualPrefab;
    public GameObject MemberOverworldVisualPrefab;
}
