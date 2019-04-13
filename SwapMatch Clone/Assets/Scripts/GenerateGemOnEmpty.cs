using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGemOnEmpty : MonoBehaviour, IRefreshable
{

    public GenerateGemGridOnStart generateGemGridOnStartObj;
    
    public GameObject gemPrefab;

    private Ray2D _ray;
    private RaycastHit2D _hit;
    private float _distance;
    private LayerMask _mask;
    
    // Start is called before the first frame update
    private void Start()
    {
        _distance = CalculateDistance();
        Debug.Log(_distance);
        _mask = LayerMask.GetMask("Gem");
    }

    void IRefreshable.Refresh()
    {
        var position = transform.position;
        _hit = Physics2D.Raycast(position, Vector2.down, _distance, _mask);
        if (_hit.collider == null)
        {
            
        }
    }

    private float CalculateDistance()
    {
        var positionY = transform.position.y;
        var targetY = generateGemGridOnStartObj.startingPos.y;
        return Mathf.Abs(positionY - targetY);
    }

    private void GenerateGem()
    {
        
    }
}
