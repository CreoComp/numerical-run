using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This allows us to store a database of all characters currently in the bundles, indexed by name.
/// </summary>
public class CharacterDatabase
{
    static protected Dictionary<string, Character> m_CharactersDict;

    static public Dictionary<string, Character> dictionary {  get { return m_CharactersDict; } }

    static protected bool m_Loaded = false;
    static public bool loaded { get { return m_Loaded; } }

    static public Character GetCharacter(string type)
    {
        Character c;
        if (m_CharactersDict == null || !m_CharactersDict.TryGetValue(type, out c))
            return null;

        return c;
    }

    static public IEnumerator LoadDatabase()
    {
        Debug.Log(m_CharactersDict + "    m_CharactersDict");
        if (m_CharactersDict == null)
        {
            Debug.Log("m_CharactersDict == null");

            m_CharactersDict = new Dictionary<string, Character>();

            yield return Addressables.LoadAssetsAsync<GameObject>("characters", op =>
            {
                Debug.Log("Loaded Database1");

                Character c = op.GetComponent<Character>();
                if (c != null)
                {
                    Debug.Log("Loaded Database2");

                    m_CharactersDict.Add(c.characterName, c);
                }
            });
        Debug.Log("Loaded Database");

            m_Loaded = true;
        }

    }
}