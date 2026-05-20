using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



// 特征成员
public partial interface 接口_物品包 {
    Guid ID { get; set; }
    Item[] 物品矩阵 { get; set; }
}

// 简单虚成员
public partial interface 接口_物品包 {
    枚举_物品包类型 类型标识 => 枚举_物品包类型.物品包;
    类型_配置_物品包 独立配置 { get; set; }
    类型_配置_物品包 默认配置 => ModContent.GetInstance<类型_配置_物品包>();
    类型_配置_物品包 配置 => 独立配置 ?? 默认配置;
    类型_玩家_物品包 玩家 => Main.LocalPlayer.GetModPlayer<类型_玩家_物品包>();
    类型_包槽位_物品 界面槽位( int 索引 ) => new( this, 索引 );
    bool 放入许可( Item 物品 ) => 物品.ModItem is not 接口_物品包;
    bool 置换许可( Item 物品 ) => true;
}

// 特征成员
public partial class 类型_物品包 : ModItem, 接口_物品包 {
    public virtual 接口_物品包 接口 => this;
    public Guid ID { get; set; } = Guid.NewGuid();
    public Item[] 物品矩阵 { get; set; }
    public 类型_配置_物品包 独立配置 { get; set; }

    public virtual void 更新容量() {
        int 先前容量 = 物品矩阵.Length;
        int 配置容量 = 接口.配置.容量;
        for ( int i = 配置容量; i < 先前容量; i++ ) 弹出物品( 物品矩阵[ i ] );
        容量更改( 配置容量 );
        for ( int i = 先前容量; i < 配置容量; i++ ) 物品矩阵[ i ] = new();
    }
}

// TML 重写成员
public partial class 类型_物品包 {
    public override ModItem Clone( Item 初始副本 ) {
        var 副本 = base.Clone( 初始副本 ) as 类型_物品包;
        副本.物品矩阵 = new Item[ 物品矩阵.Length ]; for ( int i = 0; i < 物品矩阵.Length; i++ ) 副本.物品矩阵[ i ] = 物品矩阵[ i ].Clone();
        if ( 独立配置 != null ) 副本.独立配置 = 独立配置.Clone() as 类型_配置_物品包;
        return 副本;
    }
    public override void SaveData( TagCompound 存档标签 ) { 存档标签[ "物品矩阵" ] = 物品矩阵; if ( 独立配置 != null ) 存档标签[ "独立配置" ] = 独立配置.存档写入(); }
    public override void LoadData( TagCompound 存档标签 ) {
        物品矩阵 = 存档标签.Get<Item[]>( "物品矩阵" );
        if ( 存档标签.TryGet( "独立配置", out TagCompound 配置标签 ) ) { 独立配置 = 接口.配置.Clone() as 类型_配置_物品包; 独立配置.存档读取( 配置标签 ); }
    }
    public override void NetSend( BinaryWriter 网络流 ) {
        网络流.Write( 物品矩阵.Length ); for ( int i = 0; i < 物品矩阵.Length; i++ ) ItemIO.Send( 物品矩阵[ i ], 网络流, true, true );
        网络流.Write( 独立配置 != null ); 独立配置?.网络发送( 网络流 );
    }
    public override void NetReceive( BinaryReader 网络流 ) {
        容量更改( 网络流.ReadInt32() ); for ( int i = 0; i < 物品矩阵.Length; i++ ) 物品矩阵[ i ] = ItemIO.Receive( 网络流, true, true );
        if ( 网络流.ReadBoolean() ) { 独立配置 = 接口.配置.Clone() as 类型_配置_物品包; 独立配置.网络接收( 网络流 ); }
    }
}

// TML 成员
public partial class 类型_物品包 {
    public override bool ConsumeItem( Player 玩家 ) => false;
    public override void AddRecipes() { CreateRecipe( 1 ).AddIngredient( ItemID.Silk, 10 ).AddIngredient( ItemID.IronBar, 10 ).AddTile( TileID.WorkBenches ).Register(); }
    public override void SetDefaults() {
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1.0f / 4.0f;
        Item.noGrabDelay = 100;
        物品矩阵 = new Item[ 接口.配置.容量 ]; for ( int i = 0; i < 物品矩阵.Length; i++ ) 物品矩阵[ i ] = new();
    }
}

// 辅助函数
public partial class 类型_物品包 {
    private void 容量更改( int 更新容量 ) { var 物品矩阵 = this.物品矩阵; Array.Resize( ref 物品矩阵, 更新容量 ); this.物品矩阵 = 物品矩阵; }
    private static void 弹出物品( Item 物品 ) { if ( !物品.IsAir ) Main.LocalPlayer.QuickSpawnItem( new EntitySource_Misc( "物品包弹出物品" ), 物品, 物品.stack ); }
}