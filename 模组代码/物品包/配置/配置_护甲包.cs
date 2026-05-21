using MultipleArmorSetsFramework;
using System.ComponentModel;
using System.IO;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;
using 物品包.界面.配置控件;

namespace 物品包.配置;



public record struct 结构_功能配置_护甲包 {
    public 枚举_加成模式 单件加成模式;
    public 枚举_加成模式 套装加成模式;
}

// 配置字段
public partial class 类型_配置_护甲包 : 类型_配置_物品包, 接口_配置_功能包<结构_功能配置_护甲包> {

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
}

// 特征重写函数
public partial class 类型_配置_护甲包 {
    public 结构_功能配置_护甲包 功能配置() => new() { 单件加成模式 = 单件加成模式, 套装加成模式 = 套装加成模式 };
    public override TagCompound 存档写入() {
        var 存档标签 = base.存档写入();
        存档标签[ "单件加成模式" ] = ( byte ) 单件加成模式;
        存档标签[ "套装加成模式" ] = ( byte ) 套装加成模式;
        return 存档标签;
    }
    public override void 存档读取( TagCompound 存档标签 ) {
        base.存档读取( 存档标签 );
        if ( 存档标签.TryGet<byte>( "单件加成模式", out var 单件加成模式 ) ) this.单件加成模式 = ( 枚举_加成模式 ) 单件加成模式;
        if ( 存档标签.TryGet<byte>( "套装加成模式", out var 套装加成模式 ) ) this.套装加成模式 = ( 枚举_加成模式 ) 套装加成模式;
    }
    public override void 网络发送( BinaryWriter 网络流 ) { base.网络发送( 网络流 ); 网络流.Write( ( byte ) 单件加成模式 ); 网络流.Write( ( byte ) 套装加成模式 ); }
    public override void 网络接收( BinaryReader 网络流 ) { base.网络接收( 网络流 ); 单件加成模式 = ( 枚举_加成模式 ) 网络流.ReadByte(); 套装加成模式 = ( 枚举_加成模式 ) 网络流.ReadByte(); }
}

// 配置控件
public partial class 类型_配置_护甲包 {
    private static readonly string[] 模式选项 = [
        Language.GetTextValue( "Mods.物品包.Enums.枚举_加成模式.禁用" ),
        Language.GetTextValue( "Mods.物品包.Enums.枚举_加成模式.启用" ),
        Language.GetTextValue( "Mods.物品包.Enums.枚举_加成模式.唯一" )
    ];

    public override void 填充控件( UIList 条目列表 ) {
        base.填充控件( 条目列表 );

        条目列表.Add( new 控件_配置条目(
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_护甲包.单件加成模式.Label" ),
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_护甲包.单件加成模式.Tooltip" ),
            new 类型_控件_选项( 值 => 单件加成模式 = ( 枚举_加成模式 ) 值, 模式选项, ( int ) 单件加成模式 )
        ) );
        条目列表.Add( new 控件_配置条目(
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_护甲包.套装加成模式.Label" ),
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_护甲包.套装加成模式.Tooltip" ),
            new 类型_控件_选项( 值 => 套装加成模式 = ( 枚举_加成模式 ) 值, 模式选项, ( int ) 套装加成模式 )
        ) );
    }
}