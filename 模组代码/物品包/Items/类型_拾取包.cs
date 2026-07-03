using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using 物品包.配置;

namespace 物品包.Items;



public class 类型_缓存_拾取包 { }

// 简单重写成员
public partial interface 接口_拾取包 : 接口_缓存包<类型_缓存_拾取包> {
    枚举_物品包类型 接口_物品包.类型标识 => 枚举_物品包类型.拾取包;
    类型_配置_物品包 接口_物品包.默认配置 => ModContent.GetInstance<类型_配置_拾取包>();
}

// 特征成员
public partial interface 接口_拾取包 {
    bool 收纳许可( Item 物品 ) {
        if ( !启用状态 || !放入许可( 物品 ) ) return false;
        return true;
    }

    int 自动存入( Item 物品 ) {
        int 剩余数量 = 物品.stack;

        for ( int i = 0; i < 物品矩阵.Length; i++ ) {
            ref Item 槽位物品 = ref 物品矩阵[ i ];
            if ( !槽位物品.IsAir && 槽位物品.type == 物品.type && 槽位物品.stack < 槽位物品.maxStack ) {
                int 可填充量 = Math.Min( 槽位物品.maxStack - 槽位物品.stack, 剩余数量 );
                槽位物品.stack += 可填充量;
                剩余数量 -= 可填充量;
                if ( 剩余数量 <= 0 ) return 0;
            }
        }

        for ( int i = 0; i < 物品矩阵.Length; i++ ) {
            ref Item 槽位物品 = ref 物品矩阵[ i ];
            if ( 槽位物品.IsAir ) {
                槽位物品 = 物品.Clone();
                槽位物品.stack = 剩余数量;
                return 0;
            }
        }

        return 剩余数量;
    }
}

// 特征重写成员
public partial interface 接口_拾取包 {
    类型_缓存_拾取包 接口_缓存包<类型_缓存_拾取包>.初始缓存数据() => new();
    void 接口_缓存包.建立缓存() { }
    void 接口_缓存包.清空缓存() { }
}

// TML 重写函数
public class 类型_拾取包 : 类型_缓存包<类型_缓存_拾取包>, 接口_拾取包 {
    public override void ModifyTooltips( List<TooltipLine> 物品提示列表 ) {
        base.ModifyTooltips( 物品提示列表 );
        物品提示列表[ ^1 ].Text = Terraria.Localization.Language.GetTextValue( 启用状态 ? "Mods.物品包.UI.拾取包启用" : "Mods.物品包.UI.拾取包禁用" );
    }
}