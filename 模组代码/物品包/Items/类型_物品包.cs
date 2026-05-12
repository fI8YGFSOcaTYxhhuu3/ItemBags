using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.玩家;
using 物品包.界面;
using 物品包.系统;
using 物品包.配置;

namespace 物品包.Items;



public enum 枚举_物品包类型 : int {
    物品包,
    嵌套包,
    饰品包,
    护甲包,
}

public class 类型_物品包 : ModItem {

    public Item[] 物品矩阵;
    public Guid ID = Guid.NewGuid();

    public virtual 枚举_物品包类型 类型标识 => 枚举_物品包类型.物品包;
    public virtual 类型_配置_物品包 配置 => ModContent.GetInstance<类型_配置_物品包>();
    public virtual 类型_玩家_物品包 玩家 => Main.LocalPlayer.GetModPlayer<类型_玩家_物品包>();
    public virtual 类型_包槽位_物品 界面槽位( int 索引 ) => new( this, 索引 );

    public virtual bool 放入许可( Item 物品 ) => 物品.ModItem is not 类型_物品包;

    public virtual void 更新容量() {
        int 先前容量 = 物品矩阵.Length;
        int 配置容量 = 配置.容量;

        if ( 配置容量 < 先前容量 ) {
            Player 玩家 = Main.LocalPlayer;
            for ( int i = 配置容量; i < 先前容量; i++ ) if ( !物品矩阵[ i ].IsAir ) 玩家.QuickSpawnItem( 玩家.GetSource_Misc( "物品包更新容量" ), 物品矩阵[ i ], 物品矩阵[ i ].stack );
        }

        Array.Resize( ref 物品矩阵, 配置容量 );
        for ( int i = 先前容量; i < 配置容量; i++ ) 物品矩阵[ i ] = new Item();
    }

    public override bool ConsumeItem( Player 玩家 ) => false;
    public override bool CanRightClick() => true;
    public override void RightClick( Player 玩家 ) { 类型_系统_界面管理.界面管理器.切换窗口( this ); }

    public override void SetDefaults() {
        Item.width = 32;
        Item.height = 32;
        Item.scale = 1.0f / 4.0f;
        Item.noGrabDelay = 100;

        物品矩阵 = new Item[ 配置.容量 ]; 
        for ( int i = 0; i < 物品矩阵.Length; i++ ) 物品矩阵[ i ] = new Item();
    }

    public override ModItem Clone( Item 克隆目标 ) {
        var 克隆实例 = ( 类型_物品包 ) base.Clone( 克隆目标 );
        克隆实例.物品矩阵 = Array.ConvertAll( 物品矩阵, 物品 => 物品.Clone() );
        return 克隆实例;
    }

    public override void AddRecipes() { CreateRecipe( 1 ).AddIngredient( ItemID.Silk, 10 ).AddIngredient( ItemID.IronBar, 10 ).AddTile( TileID.WorkBenches ).Register(); }

    public override void SaveData( TagCompound 存档标签 ) { 存档标签[ "物品矩阵" ] = 物品矩阵; }
    public override void LoadData( TagCompound 存档标签 ) {
        Item[] 存档矩阵 = 存档标签.Get<Item[]>( "物品矩阵" );
        Array.Resize( ref 物品矩阵, 存档矩阵.Length );
        for ( int i = 0; i < 存档矩阵.Length; i++ ) 物品矩阵[ i ] = 存档矩阵[ i ];
    }

    public override void NetSend( BinaryWriter 网络流 ) {
        网络流.Write( 物品矩阵.Length );
        for ( int i = 0; i < 物品矩阵.Length; i++ ) ItemIO.Send( 物品矩阵[ i ], 网络流, true, true );
    }
    public override void NetReceive( BinaryReader 网络流 ) {
        int 物品容量 = 网络流.ReadInt32();
        if ( 物品矩阵.Length != 物品容量 ) Array.Resize( ref 物品矩阵, 物品容量 );
        for ( int i = 0; i < 物品容量; i++ ) 物品矩阵[ i ] = ItemIO.Receive( 网络流, true, true );
    }
}