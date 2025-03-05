using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {// _instance가 없으면 "CharacterManger"라는 오브젝트를 생성하여 CharacterManager를 컴포넌트에 추가
                _instance = new GameObject("CharacterManger").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    private Player _player;
    public Player Player { get { return _player; } set { _player = value; } }


    private void Awake()
    {
        // _instance가 없으면 현재 오브젝트를 _instance로 설정하고 제거방지
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // _instance가 있으면 현제 오브젝트 제거
        else
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
        }
    }

}
