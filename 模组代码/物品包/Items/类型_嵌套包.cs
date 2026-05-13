using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



// 特征成员
public partial class 类型_嵌套包 : 类型_缓存包<类型_物品包> {
    public Dictionary<枚举_物品包类型, List<类型_物品包>> 缓存字典 = 缓存字典初始化();
}

// 简单重写成员
public partial class 类型_嵌套包 : 类型_缓存包<类型_物品包> {
    public override 枚举_物品包类型 类型标识 => 枚举_物品包类型.嵌套包;
    public override 类型_配置_嵌套包 配置 => ModContent.GetInstance<类型_配置_嵌套包>();
    public override 类型_包槽位_嵌套 界面槽位( int 索引 ) => new( this, 索引 );
    public override bool 放入许可( Item 物品 ) => 物品.ModItem is 类型_物品包 物品包 && !存在自我嵌套( 物品包 );
    public override bool 置换许可( Item 物品 ) => 放入许可( 物品 );
}

// 特征重写函数
public partial class 类型_嵌套包 : 类型_缓存包<类型_物品包> {
    public override void 切换启用状态() {
        base.切换启用状态();
        var 饰品玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>(); if ( 饰品玩家.缓存变更检测( this ) ) 饰品玩家.脏标记_缓存包 = true;
        var 护甲玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>(); if ( 护甲玩家.缓存变更检测( this ) ) 护甲玩家.脏标记_缓存包 = true;
    }
    public override ModItem Clone( Item 克隆目标 ) { var 克隆实例 = ( 类型_嵌套包 ) base.Clone( 克隆目标 ); 克隆实例.缓存字典 = 缓存字典初始化(); return 克隆实例; }
    protected override void 清空缓存() { base.清空缓存(); foreach ( var 列表 in 缓存字典.Values ) 列表.Clear(); }
    protected override void 建立缓存() { foreach ( var 物品 in 物品矩阵 ) if ( 物品.ModItem is 类型_物品包 物品包 ) 缓存字典[ 物品包.类型标识 ].Add( 物品包 ); }
}

// 辅助函数
public partial class 类型_嵌套包 : 类型_缓存包<类型_物品包> {
    private static Dictionary<枚举_物品包类型, List<类型_物品包>> 缓存字典初始化() {
        var 枚举数组 = Enum.GetValues<枚举_物品包类型>();
        var 返回字典 = new Dictionary<枚举_物品包类型, List<类型_物品包>>( 枚举数组.Length );
        foreach ( var 枚举 in 枚举数组 ) 返回字典[ 枚举 ] = [];
        return 返回字典;
    }
    private bool 存在自我嵌套( 类型_物品包 目标包 ) {
        if ( 目标包 is not 类型_嵌套包 目标嵌套包 ) return false;
        if ( 目标嵌套包.ID == ID ) return true;
        目标嵌套包.更新缓存(); foreach ( var 子嵌套包 in 目标嵌套包.缓存字典[ 枚举_物品包类型.嵌套包 ] ) if ( 存在自我嵌套( 子嵌套包 ) ) return true;
        return false;
    }
}