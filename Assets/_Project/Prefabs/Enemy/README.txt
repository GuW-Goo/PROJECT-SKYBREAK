# Enemy TEST GUIDE

---

## [1. Scene 설정]

###  Spawner 배치
- Enemy_Spawner을 Scene의 Hierarchy에 배치합니다.

##[2. Prefab 설정]

	1. Enemy Prefab 연결
1. Asset/_Project/Prefabs/Enemy/Hellicopter에 존재하는 Prefab선택
2. Mesh Renderer(마테리얼은 비어있어도 상관없음)
3. Rigidbody
4. Enemy_Controller에 Enemy_Shoter를 T_Roc_Spawn_Oa.008라는 하위 오브젝트를 Inspector에 배치
	T_Roc_Spawn_Oa.008에 Enemy_Shot.cs를 삽입 shoter는 본인을, Bullet에서는 
	Asset/_Project/Prefabs/Enemy/Bullet에 원하는 공격방식의 Prefab을 배치
5. 모든 Move_Pattern.cs를 전부 삽입하고 비활성화 시킨 다음 speed에 
	Asset/_Project/ScriptableObjects/Enemy_Move_Speed를 배치
6. 추가 Move_Pattern은 모든 적 Prefab의 Ispector에 배치해야 한다

	2. Bullet Prefab 연결
1. Asset/_Project/Prefabs/Enemy/Bullet 중 1개 선택
2. Bullet에는 Prefab/Enemy/Bullet 에 존재하는 원하는 공격방식을 가지는 Prefab에 
	Prefab/Scripts/Enemy/Shot에 존재하는 cs파일을 선택 Inspector에 배치
3. Explosion_effect는 Prefabs/Enemy에 존재하는 0_Hit_Effect를 Inspector에 배치 

Prefab에만 연결하면 Enemy_Spawner만 Scene에 배치하면 자동으로 실행 됨

---

## [3. 적 패턴]

1. 기본형 - 위에서 아래로 일직선으로 날아가는 투사체
2. 추적형 - 플레이어의 방향으로 날아가는 투사체
3. 방사형 - 3개의 투사체가 방사형으로 날아가는 투사체 

---

## [4. 테스트 전 확인 사항]
- Enemy_Spawner만 Scene에 배치되어있는지 확인합니다.
- Enemy_Spawner의 inspector에 모든 적 Prefab이 배치되어 있는지 확인합니다.
- Scene 탭이 Game 탭보다 넓은 크기를 비추고 있는지 확인합니다
---
