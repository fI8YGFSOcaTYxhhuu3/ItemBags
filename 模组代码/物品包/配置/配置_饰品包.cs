using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace 物品包.配置;



public class 类型_配置_饰品包 : 类型_配置_物品包 {

    [Header( "功能设置" )]

    [DefaultValue( false )]
    public bool 允许饰品重复 { get; set; } = false;

    [DefaultValue( true )]
    public bool 应用前缀效果 { get; set; } = true;

    [DefaultValue( true )]
    public bool 应用视觉效果 { get; set; } = true;

}