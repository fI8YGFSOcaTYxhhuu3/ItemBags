using MultipleArmorSetsFramework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace 物品包.配置;



public record struct 结构_功能配置_护甲包 {
    public 枚举_加成模式 单件加成模式;
    public 枚举_加成模式 套装加成模式;
}

public class 类型_配置_护甲包 : 类型_配置_物品包, 接口_配置_功能包<结构_功能配置_护甲包> {

    [DefaultValue( 150 )]
    public override int 容量 { get; set; }

    [DefaultValue( 15 )]
    public override int 列数 { get; set; }

    [DefaultValue( 10 )]
    public override int 行数 { get; set; }

    [Header( "功能设置" )]

    [DefaultValue( 枚举_加成模式.唯一 )]
    public 枚举_加成模式 单件加成模式 { get; set; }

    [DefaultValue( 枚举_加成模式.唯一 )]
    public 枚举_加成模式 套装加成模式 { get; set; }

    public 结构_功能配置_护甲包 功能配置 => new() { 单件加成模式 = 单件加成模式, 套装加成模式 = 套装加成模式 };
}