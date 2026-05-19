using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace 物品包.配置;



public record struct 结构_功能配置_饰品包 {
    public bool 应用前缀效果;
    public bool 应用视觉效果;
}

public class 类型_配置_饰品包 : 类型_配置_物品包, 接口_配置_功能包<结构_功能配置_饰品包> {

    [Header( "功能设置" )]

    [DefaultValue( false )]
    public bool 允许饰品重复 { get; set; } = false;

    [DefaultValue( true )]
    public bool 应用前缀效果 { get; set; } = true;

    [DefaultValue( true )]
    public bool 应用视觉效果 { get; set; } = true;

    public 结构_功能配置_饰品包 功能配置 => new() { 应用前缀效果 = 应用前缀效果, 应用视觉效果 = 应用视觉效果 };
}