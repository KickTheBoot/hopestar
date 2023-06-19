using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveStrip : MonoBehaviour
{
    [SerializeField] GameObject icon;
    [SerializeField] GameObject iconGrid;

    //string name; //should name it something othere than 'name' as it may conflict with gameobject.name
    int target;
    [HideInInspector] public Sprite defaultSprite;
    [HideInInspector] public Sprite fulfilledSprite;

    TextMeshProUGUI nameOfStrip;

    public void SetNameAs(string stripName){
        nameOfStrip = GetComponentInChildren<TextMeshProUGUI>();
        nameOfStrip.text = stripName;
    }
    
    public void SetPropertiesAs(Objectives.ResourceObjective obj){
        SetNameAs(obj.nameEnum.ToString());
        target = obj.target;
        defaultSprite = obj.unfulfilledSprite;
        fulfilledSprite = obj.fulfilledSprite;
    }

    private void DisplayObjectiveIcons(){
        for (int i = 0; i < target; i++){
            GameObject newIcon = Instantiate(icon);
            newIcon.transform.SetParent(iconGrid.transform);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
       DisplayObjectiveIcons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
