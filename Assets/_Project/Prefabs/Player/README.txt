========================================
          PLAYER TEST GUIDE
========================================

[1. Scene 설정]

① Player 배치
- Player Prefab을 Scene의 Hierarchy에 배치합니다.

② WeaponChoiceManager 배치
- WeaponChoiceManager Prefab을 Scene의 Hierarchy에 배치합니다.

③ Inspector 참조 연결
- WeaponChoiceManager 오브젝트를 선택합니다.
- PlayerCollision 항목에 Scene에 배치한 Player를 연결합니다.
- WeaponManager 항목에 Player Prefab 내부의 WeaponSystem을 연결합니다.

※ WeaponChoiceManager는 Player의 스크립트를 참조하므로
   Prefab 자체에서는 참조 항목이 None으로 표시됩니다.
   Scene에 배치한 후 반드시 연결해 주세요.

④ Player 카메라 연결
- Player 오브젝트의 PlayerMove 스크립트에서
  Main Camera 항목에 현재 Scene의 Main Camera를 연결합니다.


[2. 조작법]

- WASD / 방향키 : 기체 이동
- Space : 무기 공격
- Left Ctrl : 폭탄 사용
- 1 / 2 / 3 : 무기 선택


[3. 아이템 테스트]

① 폭탄 충전
- Bomb 폴더의 BombItem Prefab을 Scene에 배치합니다.
- Player가 아이템을 획득하면 폭탄 보유량이 충전됩니다.

② 무기 선택 및 강화
- WeaponItem 폴더의 WeaponItem Prefab을 Scene에 배치합니다.
- Player가 아이템을 획득하면 게임 시간이 정지되고
  무작위 무기 선택지가 생성됩니다.
- 1 / 2 / 3 키를 눌러 원하는 무기를 선택합니다.
- 선택이 완료되면 게임 시간이 다시 진행됩니다.


[4. 테스트 전 확인 사항]

- Player와 WeaponChoiceManager가 Scene에 배치되어 있는지 확인합니다.
- WeaponChoiceManager의 Player 및 WeaponManager 참조를 확인합니다.
- PlayerMove의 Main Camera 참조를 확인합니다.
- WeaponChoiceManager의 무기 SO 목록이 정상적으로 등록되어 있는지 확인합니다.

========================================