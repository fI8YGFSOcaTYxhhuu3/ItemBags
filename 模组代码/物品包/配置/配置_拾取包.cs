using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace 物品包.配置;



public class 类型_配置_拾取包 : 类型_配置_物品包 {

    [Range( 1, 10000 )]
    [DefaultValue( 1000 )]
    public override int 容量 { get; set; }

}