using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

[Serializable]
public class MenuManager
{
    public UIDocument Hud;
    public ToolTip toolTip;
    public bool OnElement;

    public void Initialize()
    {
        Hud.rootVisualElement.Query(className: "GUIBackground").ForEach( (element) => 
        {
            element.RegisterCallback<MouseEnterEvent>(e => OnElement = true);
            element.RegisterCallback<MouseLeaveEvent>(e => OnElement = false);  
        });
        toolTip = new ToolTip("Test",Input.mousePosition);
        Hud.rootVisualElement.Add(toolTip);
    }

    //TODO: Someone please fix this code.  It has way too many lines
    public void ShowInfoScreen(GlobalVariables variables)
    {
        VisualElement InfoScreen = Hud.rootVisualElement.Query(name:"InfoScreen");
        InfoScreen.SetEnabled(true);
        InfoScreen.visible = true;
        VisualElement StatsContainer = InfoScreen.Query(name:"Stats");

        TextElement co2 = StatsContainer.Q<TextElement>(name:"CO2");
        co2.text = $"CO<sub>2</sub>: {variables.CO2}";
        TextElement happiness = StatsContainer.Q<TextElement>(name:"Happiness");
        happiness.text = $"Happiness: {variables.CitizenUnrest}";
        TextElement resources = StatsContainer.Q<TextElement>(name:"Resources");
        resources.text = $"Resources: {variables.RawResources}";
        TextElement food = StatsContainer.Q<TextElement>(name:"Food");
        food.text = $"Food: {variables.Food}";
        TextElement energy = StatsContainer.Q<TextElement>(name:"Energy");
        energy.text = $"Energy: {variables.Energy}";
        TextElement consumerGoods = StatsContainer.Q<TextElement>(name:"ConsumerGoods");
        consumerGoods.text = $"Consumer Goods {variables.ConsumerGoods}";
        TextElement industryGoods = StatsContainer.Q<TextElement>(name:"IndustryGoods");
        industryGoods.text = $"Industry Goods: {variables.IndustryGoods}";
        //Yet another thing that I could've used loops and arrays for if my foresight were as good as my hindsight

        Button closeButton = InfoScreen.Q<Button>();
        closeButton.clicked -= HideInfoScreen;
        closeButton.clicked += HideInfoScreen;
    }

    public void HideInfoScreen()
    {
        VisualElement InfoScreen = Hud.rootVisualElement.Query(name:"InfoScreen");
        InfoScreen.SetEnabled(false);
        InfoScreen.visible = false;
    }

    public void UpdateHud()
    {
        GlobalVariables variables = GameManager.gm.variables;
        VisualElement CurrentContainer = Hud.rootVisualElement.Query(name:"CurrentResources");
        VisualElement UpkeepContainer = Hud.rootVisualElement.Query(name:"UpkeepResources");
        TextElement emission = CurrentContainer.Q<TextElement>(name:"EmissionText");
        TextElement energy = CurrentContainer.Q<TextElement>(name:"EnergyText");
        TextElement raw = CurrentContainer.Q<TextElement>(name:"RawText");
        TextElement food = CurrentContainer.Q<TextElement>(name:"FoodText");
        TextElement consumer = CurrentContainer.Q<TextElement>(name:"ConsumerText");
        TextElement industrial = CurrentContainer.Q<TextElement>(name:"IndustrialText");
        TextElement happiness = UpkeepContainer.Q<TextElement>(name:"HappinessText");
        TextElement energyUpkeep = UpkeepContainer.Q<TextElement>(name:"EnergyUpkeepText");
        TextElement rawUpkeep = UpkeepContainer.Q<TextElement>(name:"RawUpkeepText");
        TextElement foodUpkeep = UpkeepContainer.Q<TextElement>(name:"FoodUpkeepText");
        TextElement consumerUpkeep = UpkeepContainer.Q<TextElement>(name:"ConsumerUpkeepText");
        TextElement industrialUpkeep = UpkeepContainer.Q<TextElement>(name:"IndustrialUpkeepText");

        emission.text = $"Emission: {variables.CO2}";
        energy.text = $"Energy: {variables.Energy}";
        raw.text = $"Raw Resources: {variables.RawResources}";
        food.text = $"Food: {variables.Food}";
        consumer.text = $"Consumer Goods: {variables.ConsumerGoods}";
        industrial.text = $"Industrial Goods: {variables.IndustryGoods}";
        happiness.text = $"Happiness: {-variables.CitizenUnrest}";
        energyUpkeep.text = $"Energy Upkeep: {variables.EnergyUpkeep}";
        rawUpkeep.text = $"Raw Resources Upkeep: {variables.RawResourcesUpkeep}";
        foodUpkeep.text = $"Food Upkeep: {variables.FoodUpkeep}";
        consumerUpkeep.text = $"Consumer Goods Upkeep: {variables.ConsumerGoodsUpkeep}";
        industrialUpkeep.text = $"Industry Goods Upkeep: {variables.IndustryGoodsUpkeep}";
    }

    public void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 mousePositionCorrected = new Vector2(Screen.width - mousePosition.x, Screen.height - mousePosition.y);
        mousePositionCorrected = RuntimePanelUtils.ScreenToPanel(Hud.rootVisualElement.panel,mousePositionCorrected);
        toolTip.screenPosition = mousePositionCorrected;
    }
}

public class ToolTip : Label
{
    Vector2 m_ScreenPosition;

    public Vector2 screenPosition
    {
        get
        {
            return m_ScreenPosition;
        }
        set
        {
            m_ScreenPosition = value;
           // Debug.Log(screenPosition);
            this.style.top = m_ScreenPosition.y;
            this.style.right = m_ScreenPosition.x;
        }
    }

    public ToolTip(string text, Vector2 screenPosition) : base(text)
    {
        this.visible = false;
        style.position = new StyleEnum<Position>(Position.Absolute);
        m_ScreenPosition = screenPosition;
        m_ScreenPosition.y = Screen.height - screenPosition.y;
        style.top = m_ScreenPosition.y;
        style.right = m_ScreenPosition.x;
        usageHints = UsageHints.DynamicTransform;
        AddToClassList("tooltip");
    }
}
