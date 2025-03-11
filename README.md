<div align="center">

# Sparta 3D Project
스파르타 내일배움캠프에서 Unity 3D project 기능을 익히기 위해 진행한 프로젝트입니다.

</div>
  
----
  
## 프로젝트 개요
  
- **프로젝트**: Sparta 3D Project  
- **개발환경**: Unity, C#  
- **타임라인**:  
  🔹25.03.04 (화) Unity 게임 개발 강의 숙련.  
  🔹25.03.05 (수) 플레이어 이동 기능, 무기 아이템, 자원 아이템, 아이템 상호작용, 인벤토리 제작.   
  🔹25.02.06 (목) 플레이어 스테미나 사용 기능, 점프대 제작, 버프 아이템.   
  🔹25.02.07 (금) 버프 아이템 종류 추가(점프 강화, 더블 점프), 버프 아이템 사용 시 버프 타이머 UI 표시.   
  🔹25.02.10 (월) AI 몬스터 제작, 사다리 제작, 플레이어 Render Texture 제작.  
- **주요기능**:  
  버프 아이템, 점프대, 플레이어 Render Texture, 사다리 
  
----

## 프로젝트 설명

이제 Untiy의 3D 환경에 익숙해지고 숙련 단계에 진입하기 위해 개발하게 되었습니다.   
장르는 3D, 1인칭, 서바이벌 이고 화면 비율은 16:9 입니다.

----

## 플레이 방법

1. 플레이어 이동
  - "W, A, S, D"키를 통해 플레이어를 움직이고 마우스를 통해 플레이어의 시선을 움직일 수 있습니다.
  - 왼쪽 "shift"키를 눌러 달리기를 하고 "space"키를 눌러 점프가 가능합니다.

2. 상호작용
   - "E"키를 눌러 아이템을 획득하고 상호작용 오브젝트를 작동 할 수 있습니다.

3. 플레이어 공격
   - "검"을 장착하고 있을 경우 "마우스 왼쪽"를 눌러 공격이 가능합니다.
   - "도끼"를 장착하고 있을 경우 "마우스 왼쪽"를 눌러 자원 채취가 가능합니다.

---

## 주요 기능

### 버프 아이템
버프 아이템 종류 : [당근 : 점프 높이 증가, 체력 배고픔 10 회복], [사과 : 이속 증가], [바나나 : 더블 점프 가능]  
버프 아이템 사용 시 각각 지속 시간동안 버프를 얻을 수 있습니다.  
버프 아이템은 인벤토리에서 사용 가능합니다.  
버프 지속 시간은 왼쪽 상단에 이미지의 fillAmout를 사용하여 남은 시간을 표시 했습니다.  
![GR1](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/GR1.gif)


### 점프대
점프대 종류 : [위로 높게 점프하는 점프대], [플레이어가 바라보는 방향으로 포물선으로 날아가는 발사대]  
점프대 오브젝트에 올라가 [E]키를 눌러 상호작용을 하면 사용 가능 합니다.  
점프대는 Rigidbody의 AddForce 기능을 사용하여 제작 하였습니다.  
![GR2](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/GR2.gif)

### 사다리
플레이어가 사다리의 Collider에 닿았을 때 이용 가능합니다.  
[W]키를 누르면 위로 올라가고 [S]키를 누르면 아래로 내려갑니다.  
사다리는 Rigidbody의 velocity를 이용하여 플레이어 위치의 변화를 주었습니다.  
사다리 사용 중에는 플레이어의 중력을 꺼서 사다리에 매달릴 수 있게 하였습니다.  
![GR3](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/GR3.gif)

### 플레이어 Render Texture
플레이어만 찍는 전용 카메라를 추가하여 Render Texture를 사용하여 제작하였습니다.  
![GR4](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/GR4.gif)

----

## 트러블 슈팅

1️⃣ 점프대
- **문제**: 포물선으로 날아가는 발사대를 사용했을 때 플레이어가 앞으로 날가지 않고 위로만 뛰는 문제가 있었습니다.
- **원인**: 플레이어 이동 로직에서 Move()메서드(플레이어가 움직이지 않을 때 Vector.zero로 설정)가 Update에서 계속 호출되어 발생한 문제였습니다.
- **해결**: bool 값을 이용하여 점프대를 사용 여부를 확인하고 사용하고 있을 때는 return을 하여 Move()메서드의 호출을 방지하였습니다.   
![TroubleShooting1](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/TroubleShooting1.png)

2️⃣ 버프 아이템 남은 시간 표시시
- **문제**: 버프가 지속 되고 있을 때 서로 다른 버프를 연속으로 사용했을 때 버프가 제대로 적용 되지 않고 사라지지 않는 문제가 있었습니다.  
- **원인**: 버프 아이템을 사용하면 기존 작동하는 코루틴을 제거 하고 새로운 버프가 적용 되는 문제였습니다.   
- **해결**: 코루틴 배열을 만들어 버프마다 작동 되는 코루틴을 구분하여 오류를 방지하였습니다.    
![TroubleShooting2](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/TroubleShooting2.png)
![TroubleShooting3](https://github.com/DDanPat/3Dproject/blob/main/ReadMeFiles/TroubleShooting3.png)
