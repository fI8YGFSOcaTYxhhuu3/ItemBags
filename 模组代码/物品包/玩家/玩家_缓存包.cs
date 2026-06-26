using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public partial interface 接口_玩家_缓存包 : 接口_玩家_物品包 {
    bool 脏标记_缓存包 { get; set; }
    枚举_物品包类型 缓存包标识 { get; }

    void 脏标记更新() => 接口_玩家_缓存包_脏标记更新();
    void 接口_玩家_缓存包_脏标记更新() { if ( 脏标记_缓存包 ) 脏标记更新_缓存包(); }
    void 脏标记更新_缓存包() => 接口_玩家_缓存包_脏标记更新_缓存包();
    void 接口_玩家_缓存包_脏标记更新_缓存包() { 脏标记_缓存包 = false; 重构缓存_缓存包(); }
    void 重构缓存_缓存包();
    bool 缓存变更检测( ModItem 物品 );
    bool 缓存变更检测_嵌套包( 接口_嵌套包 嵌套包 ) {
        嵌套包.更新缓存();
        if ( 嵌套包.缓存数据[ ( byte ) 缓存包标识 ].Count > 0 ) return true;
        foreach ( var 子嵌套包 in 嵌套包.缓存数据[ ( byte ) 枚举_物品包类型.嵌套包 ] ) if ( 缓存变更检测_嵌套包( 子嵌套包 as 接口_嵌套包 ) ) return true;
        return false;
    }
}

// 特征成员
public partial interface 接口_玩家_缓存包<接口_缓存包类型> : 接口_玩家_缓存包 where 接口_缓存包类型 : 接口_物品包 {
    public List<接口_缓存包类型> 缓存列表_缓存包 { get; set; }

    void 接口_玩家_缓存包.重构缓存_缓存包() { 缓存列表_缓存包.Clear(); 扫描容器( 玩家.inventory ); 扫描容器( 玩家.bank.item ); 扫描容器( 玩家.bank2.item ); 扫描容器( 玩家.bank3.item ); 扫描容器( 玩家.bank4.item ); }
    bool 接口_玩家_缓存包.缓存变更检测( ModItem 物品 ) {
        if ( 物品 is 接口_缓存包类型 ) return true;
        if ( 物品 is 接口_嵌套包 嵌套包 ) return 缓存变更检测_嵌套包( 嵌套包 );
        return false;
    }
}

// 辅助函数
public partial interface 接口_玩家_缓存包<接口_缓存包类型> {
    private void 扫描容器( Item[] 容器 ) {
        foreach ( var 物品 in 容器 ) {
            if ( 物品.IsAir ) continue;
            if ( 物品.ModItem is 接口_缓存包类型 目标包 ) 缓存列表_缓存包.Add( 目标包 );
            if ( 物品.ModItem is 接口_嵌套包 嵌套包 ) 扫描嵌套包( 嵌套包 );
        }
    }
    private void 扫描嵌套包( 接口_嵌套包 嵌套包 ) {
        嵌套包.更新缓存();
        foreach ( var 子嵌套包 in 嵌套包.缓存数据[ ( byte ) 枚举_物品包类型.嵌套包 ] ) 扫描嵌套包( 子嵌套包 as 接口_嵌套包 );
        foreach ( var 目标包 in 嵌套包.缓存数据[ ( byte ) 缓存包标识 ] ) 缓存列表_缓存包.Add( ( 接口_缓存包类型 ) 目标包 );
    }
}

// 接口实现
public abstract partial class 类型_玩家_缓存包<接口_缓存包类型> : 类型_玩家_物品包, 接口_玩家_缓存包<接口_缓存包类型> where 接口_缓存包类型 : 接口_物品包 {
    public new 接口_玩家_缓存包<接口_缓存包类型> 接口 => this;
    public bool 脏标记_缓存包 { get; set; } = true;
    public abstract 枚举_物品包类型 缓存包标识 { get; }
    public List<接口_缓存包类型> 缓存列表_缓存包 { get; set; } = [];
}

// 常规 TML 成员
public abstract partial class 类型_玩家_缓存包<接口_缓存包类型> {
    public override void UpdateEquips() { if ( Player.whoAmI == Main.myPlayer ) 接口.脏标记更新(); }
}

// 脏标记更新工具
public abstract partial class 类型_玩家_缓存包<接口_缓存包类型> {
    private Item 上一鼠标物品 = new();
    public override void OnEnterWorld() => 脏标记_缓存包 = true;
    public override bool OnPickup( Item 拾取物品 ) {
        if ( 接口.缓存变更检测( 拾取物品.ModItem ) ) 脏标记_缓存包 = true;
        return base.OnPickup( 拾取物品 );
    }
    public override bool ShiftClickSlot( Item[] 容器, int 容器索引, int 物品索引 ) {
        if ( 接口.缓存变更检测( 容器[ 物品索引 ].ModItem ) ) 脏标记_缓存包 = true;
        return false;
    }
    public override void PostUpdate() {
        if ( Player.whoAmI != Main.myPlayer ) return;
        if ( Main.mouseItem != 上一鼠标物品 ) if ( 接口.缓存变更检测( Main.mouseItem.ModItem ) || 接口.缓存变更检测( 上一鼠标物品.ModItem ) ) 脏标记_缓存包 = true;
        上一鼠标物品 = Main.mouseItem;
    }
}