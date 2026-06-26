using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.玩家;

namespace 物品包.Items;



// 特征成员 - 基础
public partial interface 接口_缓存包 : 接口_物品包 {
    bool 脏标记 { get; set; }

    void 更新缓存() { }
    void 清空缓存() { }
    void 建立缓存() { }
}

// 特征成员 - 切换
public partial interface 接口_缓存包 {
    bool 启用状态 { get; set; }

    void 切换启用状态() => 切换启用状态_缓存包_可切换();
    protected void 切换启用状态_缓存包_可切换() {
        启用状态 = !启用状态;
        脏标记 = true;
        if ( 玩家 is 接口_玩家_缓存包 缓存玩家 ) 缓存玩家.脏标记_缓存包 = true;
        Terraria.Audio.SoundEngine.PlaySound( Terraria.ID.SoundID.MenuTick );
    }
}

// 特征成员 - 模板
public partial interface 接口_缓存包<类型_缓存数据> : 接口_缓存包 {
    类型_缓存数据 缓存数据 { get; set; }
    类型_缓存数据 初始缓存数据() => default;
}

// 特征成员
public abstract partial class 类型_缓存包<类型_缓存数据> : 类型_物品包, 接口_缓存包<类型_缓存数据> {
    public bool 脏标记 { get; set; } = true;
    public bool 启用状态 { get; set; } = true;
    public 类型_缓存数据 缓存数据 { get; set; }

    public void 更新缓存() {
        if ( !脏标记 ) return;
        脏标记 = false;

        var 接口 = this as 接口_缓存包<类型_缓存数据>;
        if ( 缓存数据 == null ) 缓存数据 = 接口.初始缓存数据();
        else 接口.清空缓存();
        if ( 启用状态 ) 接口.建立缓存();
    }
}

// 特征重写成员
public abstract partial class 类型_缓存包<类型_缓存数据> {
    public override bool 更新容量() {
        if ( !base.更新容量() ) return false;
        脏标记 = true;
        return true;
    }
    public override ModItem Clone( Item 初始副本 ) {
        var 副本 = base.Clone( 初始副本 ) as 类型_缓存包<类型_缓存数据>;
        副本.脏标记 = true; 副本.缓存数据 = default;
        return 副本;
    }
    public override void SaveData( TagCompound 存档标签 ) { base.SaveData( 存档标签 ); 存档标签[ "启用状态" ] = 启用状态; }
    public override void LoadData( TagCompound 存档标签 ) { base.LoadData( 存档标签 ); 脏标记 = true; if ( 存档标签.ContainsKey( "启用状态" ) ) 启用状态 = 存档标签.GetBool( "启用状态" ); }
    public override void NetSend( BinaryWriter 网络流 ) { base.NetSend( 网络流 ); 网络流.Write( 启用状态 ); }
    public override void NetReceive( BinaryReader 网络流 ) { base.NetReceive( 网络流 ); 脏标记 = true; 启用状态 = 网络流.ReadBoolean(); }
}

// TML 成员
public abstract partial class 类型_缓存包<类型_缓存数据> {
    public override void ModifyTooltips( List<TooltipLine> 物品提示列表 ) {
        base.ModifyTooltips( 物品提示列表 );
        string 状态文本 = Terraria.Localization.Language.GetTextValue( 启用状态 ? "Mods.物品包.UI.缓存包启用" : "Mods.物品包.UI.缓存包禁用" );
        物品提示列表.Add( new TooltipLine( Mod, "物品包启用状态", 状态文本 ) );
    }
}