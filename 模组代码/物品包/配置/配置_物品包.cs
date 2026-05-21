using System.ComponentModel;
using System.IO;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;
using 物品包.界面.配置控件;

namespace 物品包.配置; 



// 配置字段
public partial class 类型_配置_物品包 : ModConfig {
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header( "容量设置" )]

    [Range( 1, 1000 )]
    [DefaultValue( 200 )]
    public virtual int 容量 { get; set; }

    [Range( 1, 50 )]
    [DefaultValue( 20 )]
    public virtual int 列数 { get; set; }

    [Range( 1, 25 )]
    [DefaultValue( 10 )]
    public virtual int 行数 { get; set; }
}

// IO 函数
public partial class 类型_配置_物品包 {
    public virtual TagCompound 存档写入() => new() { [ "容量" ] = 容量, [ "列数" ] = 列数, [ "行数" ] = 行数 };
    public virtual void 存档读取( TagCompound 存档标签 ) {
        if ( 存档标签.TryGet<int>( "容量", out var 容量 ) ) this.容量 = 容量;
        if ( 存档标签.TryGet<int>( "列数", out var 列数 ) ) this.列数 = 列数;
        if ( 存档标签.TryGet<int>( "行数", out var 行数 ) ) this.行数 = 行数;
    }
    public virtual void 网络发送( BinaryWriter 网络流 ) { 网络流.Write( 容量 ); 网络流.Write( 列数 ); 网络流.Write( 行数 ); }
    public virtual void 网络接收( BinaryReader 网络流 ) { 容量 = 网络流.ReadInt32(); 列数 = 网络流.ReadInt32(); 行数 = 网络流.ReadInt32(); }
}

// 配置控件
public partial class 类型_配置_物品包 {
    public virtual void 填充控件( UIList 条目列表 ) {
        条目列表.Add( new 控件_配置条目(
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_物品包.容量.Label" ),
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_物品包.容量.Tooltip" ),
            new 类型_控件_数值( 值 => 容量 = 值, 容量, 1, 1000 )
        ) );
        条目列表.Add( new 控件_配置条目(
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_物品包.列数.Label" ),
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_物品包.列数.Tooltip" ),
            new 类型_控件_数值( 值 => 列数 = 值, 列数, 1, 50 )
        ) );
        条目列表.Add( new 控件_配置条目(
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_物品包.行数.Label" ),
            Language.GetTextValue( "Mods.物品包.Configs.类型_配置_物品包.行数.Tooltip" ),
            new 类型_控件_数值( 值 => 行数 = 值, 行数, 1, 25 )
        ) );
    }
}