# PLAYER TEST GUIDE

---

## [1. Scene 설정]

### ① Player 배치
- Player Prefab을 Scene의 Hierarchy에 배치합니다.

### ② WeaponChoiceManager 배치
- WeaponChoiceManager Prefab을 Scene의 Hierarchy에 배치합니다.

### ③ ProjectilePoolManager 배치
- ProjectilePoolManager Prefab을 Scene의 Hierarchy에 배치합니다.
- ProjectilePoolManager는 Player의 자식이 아닌 **Scene에 독립적으로 배치**합니다.

### ④ Inspector 참조 연결

**WeaponChoiceManager 설정**
- WeaponChoiceManager 오브젝트를 선택합니다.
- PlayerCollision 항목에 Scene에 배치한 Player의 PlayerCollision 컴포넌트를 연결합니다.
- PlayerStats 항목에 Scene에 배치한 Player의 PlayerStats 컴포넌트를 연결합니다.
- WeaponManager 항목에 Player의 자식인 WeaponSystem에 부착된 WeaponManager 컴포넌트를 연결합니다.

※ WeaponChoiceManager는 Player의 스크립트 및 컴포넌트를 참조하므로 Prefab 자체에서는 참조 항목이 None으로 표시됩니다. Scene에 배치한 후 반드시 연결해 주세요.

**Player의 WeaponManager 설정**
- Player의 자식인 WeaponSystem을 선택합니다.
- WeaponManager 스크립트 내부의 ProjectilePool 항목에 Scene에 배치한 ProjectilePoolManager를 연결합니다.

**ProjectilePoolManager 설정**
- ProjectileContainer는 선택 사항입니다.
- 별도의 컨테이너를 지정하지 않으면 탄환은 Scene 최상위에 생성됩니다.
- 컨테이너를 지정하면 해당 Transform의 자식으로 탄환이 생성됩니다.

### ⑤ Player 카메라 연결
- Player 오브젝트의 PlayerMove 스크립트에서 Main Camera 항목에 현재 Scene의 Main Camera를 연결합니다.

---

## [2. 조작법]

- WASD / 방향키 : 기체 이동
- Space : 무기 공격
- Left Ctrl : 폭탄 사용
- 1 / 2 / 3 : 무기 선택

---

## [3. 아이템 테스트]

### ① 폭탄 충전
- Bomb 폴더의 BombItem Prefab을 Scene에 배치합니다.
- Player가 아이템을 획득하면 폭탄 보유량이 충전됩니다.

### ② 무기 선택 및 강화
- WeaponItem 폴더의 WeaponItem Prefab을 Scene에 배치합니다.
- Player가 아이템을 획득하면 게임 시간이 정지되고 무작위 무기 및 능력치 선택지가 생성됩니다.
- 1 / 2 / 3 키를 눌러 원하는 선택지를 선택합니다.
- 선택이 완료되면 게임 시간이 다시 진행됩니다.

---

## [4. 테스트 전 확인 사항]

- Player, WeaponChoiceManager, ProjectilePoolManager가 Scene에 배치되어 있는지 확인합니다.
- WeaponChoiceManager의 PlayerCollision 참조를 확인합니다.
- WeaponChoiceManager의 PlayerStats 참조를 확인합니다.
- WeaponChoiceManager의 WeaponManager 참조가 Player의 WeaponSystem에 연결되어 있는지 확인합니다.
- Player의 WeaponSystem에 있는 WeaponManager의 ProjectilePool 참조를 확인합니다.
- ProjectilePoolManager의 ProjectileContainer는 선택 사항입니다.
- PlayerMove의 Main Camera 참조를 확인합니다.
- WeaponChoiceManager의 무기 SO 목록과 능력치 SO 목록이 정상적으로 등록되어 있는지 확인합니다.

---