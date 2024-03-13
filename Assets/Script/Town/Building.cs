using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string name; // 건물 이름
    public Sprite image; // 건물 사진
    public int level; // 건물 레벨
    public int needManaStone; // 건설에 필요한 마석
    public int needGold; // 건설에 필요한 골드
    public string explan; // 설명
    public bool isBuild; // 설치 여부
    public int placeNum; // 건설된 위치
}
