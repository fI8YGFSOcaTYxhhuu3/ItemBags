using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace 物品包.配置; 



public class 类型_配置_物品包 : ModConfig {

    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header( "容量设置" )]

    [Range( 1, 1000 )]
    [DefaultValue( 200 )]
    public virtual int 容量 { get; set; }

    [Range( 1, 50 )]
    [DefaultValue( 20 )]
    public virtual int 列数 { get; set; }

    [Range( 1, 50 )]
    [DefaultValue( 10 )]
    public virtual int 行数 { get; set; }

}