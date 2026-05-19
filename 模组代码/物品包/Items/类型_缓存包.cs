using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



// 特征成员
public partial interface 接口_缓存包 : 接口_物品包 {
    bool 脏标记 { get; set; }

    void 更新缓存();
    void 清空缓存();
    void 建立缓存();
}

// 特征重写函数
public partial interface 接口_缓存包 {
    void 接口_物品包.更新容量() { 更新容量_物品包(); 脏标记 = true; }
    void 接口_物品包.LoadData( TagCompound 存档标签 ) => LoadData_缓存包( 存档标签 );
    protected void LoadData_缓存包( TagCompound 存档标签 ) { LoadData_物品包( 存档标签 ); 脏标记 = true; }
    void 接口_物品包.NetReceive( BinaryReader 网络流 ) => NetReceive_缓存包( 网络流 );
    protected void NetReceive_缓存包( BinaryReader 网络流 ) { NetReceive_物品包( 网络流 ); 脏标记 = true; }
}

// 特征成员
public partial interface 接口_缓存包<类型_缓存数据> : 接口_缓存包 {
    类型_缓存数据 缓存数据 { get; set; }
    类型_缓存数据 初始缓存数据();
}

// 特征重写函数
public partial interface 接口_缓存包<类型_缓存数据> {
    接口_物品包 接口_物品包.Clone( 接口_物品包 初始副本 ) { var 副本 = Clone_物品包( 初始副本 ) as 接口_缓存包<类型_缓存数据>; 副本.脏标记 = true; 副本.缓存数据 = default; return 副本; }
    void 接口_缓存包.更新缓存() {
        if ( !脏标记 ) return;
        脏标记 = false;
        if ( 缓存数据 == null ) 缓存数据 = 初始缓存数据();
        else 清空缓存();
        建立缓存();
    }
}

// 特征成员
public abstract partial class 类型_缓存包<类型_缓存数据, 泛型_配置, 泛型_玩家, 泛型_包槽位> : 类型_物品包<泛型_配置, 泛型_玩家, 泛型_包槽位> where 泛型_配置 : 类型_配置_物品包 where 泛型_玩家 : 类型_玩家_物品包 where 泛型_包槽位 : 类型_包槽位_物品 {
    public bool 脏标记 { get; set; } = true;
    public bool 启用状态 { get; set; } = true;
    public 类型_缓存数据 缓存数据 { get; set; }
}

// TML 成员
public abstract partial class 类型_缓存包<类型_缓存数据, 泛型_配置, 泛型_玩家, 泛型_包槽位> {
    public override void ModifyTooltips( List<TooltipLine> 物品提示列表 ) {
        base.ModifyTooltips( 物品提示列表 );
        string 状态文本 = Terraria.Localization.Language.GetTextValue( 启用状态 ? "Mods.物品包.UI.缓存包启用" : "Mods.物品包.UI.缓存包禁用" );
        物品提示列表.Add( new TooltipLine( Mod, "物品包启用状态", 状态文本 ) );
    }
}