using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



using 类型_缓存_嵌套包 = List<接口_物品包>[];

// 简单重写成员
public partial interface 接口_嵌套包 : 接口_缓存包<类型_缓存_嵌套包> {
    枚举_物品包类型 接口_物品包.类型标识 => 枚举_物品包类型.嵌套包;
    类型_配置_物品包 接口_物品包.默认配置 => ModContent.GetInstance<类型_配置_嵌套包>();
    类型_包槽位_物品 接口_物品包.界面槽位( int 索引 ) => new 类型_包槽位_嵌套( this, 索引 );
    bool 接口_物品包.放入许可( Item 物品 ) => 物品.ModItem is 接口_物品包 物品包 && !存在自我嵌套( 物品包 );
    bool 接口_物品包.置换许可( Item 物品 ) => 放入许可( 物品 );
}

// 特征重写函数
public partial interface 接口_嵌套包 {
    类型_缓存_嵌套包 接口_缓存包<类型_缓存_嵌套包>.初始缓存数据() {
        var 枚举数组 = Enum.GetValues<枚举_物品包类型>();
        var 返回数组 = new List<接口_物品包>[ 枚举数组.Length ]; for ( int i = 0; i < 返回数组.Length; ++i ) 返回数组[ i ] = [];
        return 返回数组;
    }
    void 接口_缓存包.清空缓存() { for ( int i = 0; i < 缓存数据.Length; ++i ) 缓存数据[ i ].Clear(); }
    void 接口_缓存包.建立缓存() { foreach ( var 物品 in 物品矩阵 ) if ( 物品.ModItem is 接口_物品包 物品包 ) 缓存数据[ ( byte ) 物品包.类型标识 ].Add( 物品包 ); }
    void 接口_缓存包.切换启用状态() { if ( 启用状态 ) 玩家缓存标记(); 切换启用状态_缓存包_可切换(); if ( 启用状态 ) 玩家缓存标记(); }
}

// 辅助函数
public partial interface 接口_嵌套包 {
    private bool 存在自我嵌套( 接口_物品包 目标包 ) {
        if ( 目标包 is not 接口_嵌套包 目标嵌套包 ) return false;
        if ( 目标嵌套包.ID == ID ) return true;
        foreach ( var 子嵌套包 in 目标嵌套包.缓存数据[ ( byte ) 枚举_物品包类型.嵌套包 ] ) if ( 存在自我嵌套( 子嵌套包 ) ) return true;
        return false;
    }
    private void 玩家缓存标记() {
        var 饰品玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>().接口; if ( 饰品玩家.缓存变更检测_嵌套包( this ) ) 饰品玩家.脏标记_缓存包 = true;
        var 护甲玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>().接口; if ( 护甲玩家.缓存变更检测_嵌套包( this ) ) 护甲玩家.脏标记_缓存包 = true;
    }
}

public class 类型_嵌套包 : 类型_缓存包<类型_缓存_嵌套包>, 接口_嵌套包;