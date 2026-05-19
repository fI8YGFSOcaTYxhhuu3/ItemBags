using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.玩家;

namespace 物品包.Items;



// 特征成员
public abstract partial class 类型_缓存包_非模板基类 : 类型_物品包 {
    public bool 脏标记 = true;
    public bool 启用状态 = true;

    public virtual void 切换启用状态() {
        启用状态 = !启用状态;
        脏标记 = true;
        if ( 玩家 is 类型_玩家_缓存包_非模板基类 缓存玩家 ) 缓存玩家.脏标记_缓存包 = true;
        Terraria.Audio.SoundEngine.PlaySound( Terraria.ID.SoundID.MenuTick );
    }

    public abstract void 更新缓存();
    public abstract void 清空缓存();
    public abstract void 建立缓存();
}

// 常规 TML 成员
public abstract partial class 类型_缓存包_非模板基类 : 类型_物品包 {
    public override void ModifyTooltips( List<TooltipLine> 物品提示列表 ) {
        base.ModifyTooltips( 物品提示列表 );
        string 状态文本 = Terraria.Localization.Language.GetTextValue( 启用状态 ? "Mods.物品包.UI.缓存包启用" : "Mods.物品包.UI.缓存包禁用" );
        物品提示列表.Add( new TooltipLine( Mod, "物品包启用状态", 状态文本 ) );
    }
}

// 特征重写函数
public abstract partial class 类型_缓存包_非模板基类 : 类型_物品包 {
    public override void 更新容量() { base.更新容量(); 脏标记 = true; }
    public override void SaveData( TagCompound 存档标签 ) { base.SaveData( 存档标签 ); 存档标签[ "启用状态" ] = 启用状态; }
    public override void LoadData( TagCompound 存档标签 ) { base.LoadData( 存档标签 ); if ( 存档标签.ContainsKey( "启用状态" ) ) 启用状态 = 存档标签.GetBool( "启用状态" ); 脏标记 = true; }
    public override void NetSend( BinaryWriter 网络流 ) { base.NetSend( 网络流 ); 网络流.Write( 启用状态 ); }
    public override void NetReceive( BinaryReader 网络流 ) { base.NetReceive( 网络流 ); 启用状态 = 网络流.ReadBoolean(); 脏标记 = true; }
    public override ModItem Clone( Item 克隆目标 ) { var 克隆实例 = ( 类型_缓存包_非模板基类 ) base.Clone( 克隆目标 ); 克隆实例.脏标记 = true; return 克隆实例; }
}

// 特征成员
public abstract partial class 类型_缓存包<类型_缓存数据> : 类型_缓存包_非模板基类 {
    public 类型_缓存数据 缓存数据;
    protected abstract 类型_缓存数据 初始缓存数据();
}

// 特征重写函数
public abstract partial class 类型_缓存包<类型_缓存数据> : 类型_缓存包_非模板基类 {
    public override void 更新缓存() {
        if ( !脏标记 ) return;
        脏标记 = false;
        if ( 缓存数据 == null ) 缓存数据 = 初始缓存数据();
        else 清空缓存();
        if ( 启用状态 ) 建立缓存();
    }
    public override ModItem Clone( Item 克隆目标 ) { var 克隆实例 = ( 类型_缓存包<类型_缓存数据> ) base.Clone( 克隆目标 ); 克隆实例.缓存数据 = default; return 克隆实例; }
}

// 特征重写函数
public abstract partial class 类型_缓存包_列表<类型_缓存数据> : 类型_缓存包<List<类型_缓存数据>> {
    public override void 清空缓存() { 缓存数据.Clear(); }
    protected override List<类型_缓存数据> 初始缓存数据() { return []; }
}