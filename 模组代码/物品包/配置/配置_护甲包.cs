using MultipleArmorSetsFramework;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;
using 物品包.玩家;

namespace 物品包.配置;



public class 类型_配置_护甲包 : 类型_配置_物品包 {

    [DefaultValue( 150 )]
    public override int 容量 { get; set; }

    [DefaultValue( 15 )]
    public override int 列数 { get; set; }

    [DefaultValue( 10 )]
    public override int 行数 { get; set; }

    [Header( "功能设置" )]

    [DefaultValue( true )]
    public bool 启用单件加成 { get; set; }

    [DefaultValue( true )]
    public bool 启用套装加成 { get; set; }

    public override void OnChanged() {
        if ( Main.LocalPlayer == null || !Main.LocalPlayer.active ) return;
        Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>().注册护甲();
    }

}