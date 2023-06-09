using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class TileMaterialSetter : MonoBehaviour
{
    Grid grid;
    [SerializeField] TileMaterialColorSettingsSO settings;
    public TileMaterialColorSettings runTimeSettings;
    public void Start()
    {
        grid = GetComponent<Grid>();
        settings.settings.Initialize();
        if(settings)
        {
            runTimeSettings = new TileMaterialColorSettings(settings.settings);
        }
        runTimeSettings.Initialize();
        SetMaterials();
    }

    public void SetMaterials()
    {
            Renderer[] rend = new Renderer[grid.count];
            for(int i = 0; i < grid.count; i++)
            {
                rend[i] = grid[i].GetComponentInChildren<Renderer>();
                Debug.Log(rend[i]);
            }
        for(int i = 0; i < grid.count; i++)
        {
            Tile tile = grid[i].GetComponent<Tile>();
            
                rend[i].sharedMaterial = runTimeSettings.GetMatchingMaterial(tile);
        }
    }

    

    public void GrayIncompatible(CardSO card)
    {
        foreach(RuleMaterial current in runTimeSettings.ruleMaterials)
        {   
            bool compatible = true;
            foreach(TileProperty requiredProperty in card.requiredTileProperties)
            {
                if(!current.associatedProperties.Contains(requiredProperty))compatible = false;
            }
            if(!compatible)
            {
                current.material.color = runTimeSettings.InCompatibleColor;
                continue;
            }
            foreach(TileProperty forbiddenProperty in card.blockedTileProperties)
            {
                if(current.associatedProperties.Contains(forbiddenProperty))compatible = false;
            }
            if(!compatible)current.material.color = runTimeSettings.InCompatibleColor;
        }
    }

    public void ReturnColor()
    {
        foreach(RuleMaterial current in runTimeSettings.ruleMaterials)
        {
            current.material.color = current.InitialColor;
        }
    }
}

