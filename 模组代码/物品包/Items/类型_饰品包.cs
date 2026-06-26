using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



using 类型_缓存_饰品包 = List<Item>;

public struct 结构_同步数据_饰品包 {
    public 结构_功能配置_饰品包 功能配置;
    public 类型_缓存_饰品包 数据列表;
}

// 简单重写成员
public partial interface 接口_饰品包 : 接口_缓存包<类型_缓存_饰品包>, 接口_网络同步包<结构_同步数据_饰品包> {
    枚举_物品包类型 接口_物品包.类型标识 => 枚举_物品包类型.饰品包;
    类型_配置_物品包 接口_物品包.默认配置 => ModContent.GetInstance<类型_配置_饰品包>();
    类型_配置_饰品包 类型配置 => 配置 as 类型_配置_饰品包;
    类型_玩家_物品包 接口_物品包.玩家 => Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>();
    类型_玩家_饰品包 类型玩家 => 玩家 as 类型_玩家_饰品包;
    类型_包槽位_物品 接口_物品包.界面槽位( int 索引 ) => new 类型_包槽位_饰品( this, 索引 );
    bool 接口_物品包.放入许可( Item 物品 ) => 物品.accessory && 物品.headSlot < 0 && 物品.bodySlot < 0 && 物品.legSlot < 0 && ( 类型配置.允许饰品重复 || !存在重复饰品( 物品 ) );
    结构_同步数据_饰品包 接口_网络同步包<结构_同步数据_饰品包>.同步数据() { 更新缓存(); return new() { 功能配置 = 类型配置.功能配置(), 数据列表 = 缓存数据 }; }
}

// 特征重写函数
public partial interface 接口_饰品包 {
    void 接口_缓存包.建立缓存() { foreach ( var 物品 in 物品矩阵 ) if ( !物品.IsAir ) 缓存数据.Add( 物品 ); }
    void 接口_缓存包.清空缓存() => 缓存数据.Clear();
    类型_缓存_饰品包 接口_缓存包<类型_缓存_饰品包>.初始缓存数据() => [];
    void 接口_物品包.更新配置( 类型_配置_物品包 新配置 ) {
        var 新功能 = ( ( 新配置 ?? 默认配置 ) as 类型_配置_饰品包 ).功能配置();
        var 旧功能 = ( 配置 as 类型_配置_饰品包 ).功能配置();
        if ( 新功能 != 旧功能 ) ( 玩家 as 类型_玩家_饰品包 ).脏标记_同步缓存 = true;
        更新配置_物品包( 新配置 );
    }
}

// 辅助函数
public partial interface 接口_饰品包 {
    private bool 存在重复饰品( Item 查询饰品 ) {
        var 玩家 = 类型玩家;
        for ( int i = 3; i < 10; i++ ) if ( 玩家.Player.armor[ i ].type == 查询饰品.type ) return true;
        玩家.接口.脏标记更新(); foreach( var 数据 in CollectionsMarshal.AsSpan( 玩家.缓存列表_同步缓存 ) ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) if ( 饰品.type == 查询饰品.type ) return true;
        return false;
    }
}

public class 类型_饰品包 : 类型_缓存包<类型_缓存_饰品包>, 接口_饰品包;