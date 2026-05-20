using System.ComponentModel;
using System.IO;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace 物品包.配置;



public record struct 结构_功能配置_饰品包 {
    public bool 应用前缀效果;
    public bool 应用视觉效果;
}

// 配置字段
public partial class 类型_配置_饰品包 : 类型_配置_物品包, 接口_配置_功能包<结构_功能配置_饰品包> {

    [Header( "功能设置" )]

    [DefaultValue( false )]
    public bool 允许饰品重复 { get; set; } = false;

    [DefaultValue( true )]
    public bool 应用前缀效果 { get; set; } = true;

    [DefaultValue( true )]
    public bool 应用视觉效果 { get; set; } = true;
}

// 特征重写函数
public partial class 类型_配置_饰品包 {
    public 结构_功能配置_饰品包 功能配置() => new() { 应用前缀效果 = 应用前缀效果, 应用视觉效果 = 应用视觉效果 };
    public override TagCompound 存档写入() {
        var 存档标签 = base.存档写入();
        存档标签[ "允许饰品重复" ] = 允许饰品重复;
        存档标签[ "应用前缀效果" ] = 应用前缀效果;
        存档标签[ "应用视觉效果" ] = 应用视觉效果;
        return 存档标签;
    }
    public override void 存档读取( TagCompound 存档标签 ) {
        base.存档读取( 存档标签 );
        if ( 存档标签.TryGet<bool>( "允许饰品重复", out var 允许饰品重复 ) ) this.允许饰品重复 = 允许饰品重复;
        if ( 存档标签.TryGet<bool>( "应用前缀效果", out var 应用前缀效果 ) ) this.应用前缀效果 = 应用前缀效果;
        if ( 存档标签.TryGet<bool>( "应用视觉效果", out var 应用视觉效果 ) ) this.应用视觉效果 = 应用视觉效果;
    }
    public override void 网络发送( BinaryWriter 网络流 ) { base.网络发送( 网络流 ); 网络流.Write( 允许饰品重复 ); 网络流.Write( 应用前缀效果 ); 网络流.Write( 应用视觉效果 ); }
    public override void 网络接收( BinaryReader 网络流 ) { base.网络接收( 网络流 ); 允许饰品重复 = 网络流.ReadBoolean(); 应用前缀效果 = 网络流.ReadBoolean(); 应用视觉效果 = 网络流.ReadBoolean(); }
}