using MultipleArmorSetsFramework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



using 类型_缓存_护甲包 = List<类型_护甲组合>;

public struct 结构_同步数据_护甲包 {
    public Guid ID;
    public 结构_功能配置_护甲包 功能配置;
    public 类型_缓存_护甲包 数据列表;
}

// 简单重写成员
public partial interface 接口_护甲包 : 接口_缓存包<类型_缓存_护甲包>, 接口_网络同步包<结构_同步数据_护甲包> {
    枚举_物品包类型 接口_物品包.类型标识 => 枚举_物品包类型.护甲包;
    类型_配置_物品包 接口_物品包.默认配置 => ModContent.GetInstance<类型_配置_护甲包>();
    类型_配置_护甲包 类型配置 => 配置 as 类型_配置_护甲包;
    类型_玩家_物品包 接口_物品包.玩家 => Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>();
    类型_玩家_护甲包 类型玩家 => 玩家 as 类型_玩家_护甲包;
    类型_包槽位_物品 接口_物品包.界面槽位( int 索引 ) => new 类型_包槽位_护甲( this, 索引 );
    bool 接口_物品包.放入许可( Item 物品 ) => 物品.headSlot >= 0 || 物品.bodySlot >= 0 || 物品.legSlot >= 0;
    结构_同步数据_护甲包 接口_网络同步包<结构_同步数据_护甲包>.同步数据 => new() { ID = ID, 功能配置 = 类型配置.功能配置(), 数据列表 = 缓存数据 };
}

// 特征重写函数
public partial interface 接口_护甲包 {
    void 接口_缓存包.建立缓存() {
        for ( int i = 0; i + 2 < 物品矩阵.Length; i += 3 ) {
            Item 头盔 = 物品矩阵[ i ]; Item 胸甲 = 物品矩阵[ i + 1 ]; Item 护腿 = 物品矩阵[ i + 2 ];
            if ( !头盔.IsAir || !胸甲.IsAir || !护腿.IsAir ) 缓存数据.Add( new 类型_护甲组合( 头盔, 胸甲, 护腿 ) );
        }
    }
    void 接口_缓存包.清空缓存() => 缓存数据.Clear();
    类型_缓存_护甲包 接口_缓存包<类型_缓存_护甲包>.初始缓存数据() => [];
    void 接口_物品包.更新配置( 类型_配置_物品包 新配置 ) {
        var 新功能 = ( ( 新配置 ?? 默认配置 ) as 类型_配置_护甲包 ).功能配置();
        var 旧功能 = ( 配置 as 类型_配置_护甲包 ).功能配置();
        if ( 新功能 != 旧功能 ) ( 玩家 as 类型_玩家_护甲包 ).脏标记_同步缓存 = true;
        更新配置_物品包( 新配置 );
    }
}

public class 类型_护甲包 : 类型_缓存包<类型_缓存_护甲包>, 接口_护甲包;