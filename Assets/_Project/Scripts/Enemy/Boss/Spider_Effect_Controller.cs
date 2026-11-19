using UnityEngine;

public class Spider_Effect_Controller : MonoBehaviour
{
    [SerializeField] private GameObject landing_Effect;
    public void Create_Effect()
    {
        Instantiate(landing_Effect,new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }
    
}
